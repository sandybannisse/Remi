using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class MeshGeneratorV2 : MonoBehaviour
{
    Mesh mesh;
    private int MESH_SCALE = 2;
    public GameObject[] objects;
    [SerializeField] private AnimationCurve heightCurve;
    private Vector3[] vertices;
    private int[] triangles;

    private Color[] colors;
    [SerializeField] private Gradient gradient;

    private float minTerrainheight;
    private float maxTerrainheight;

    public int xSize;
    public int zSize;

    public float scale;
    public int octaves;
    public float lacunarity;

    public int seed;

    private float lastNoiseHeight;

    MeshCollider meshCollider;

    [SerializeField] private NavMeshSurface navMeshSurface; // Reference to NavMeshSurface

    void Start()
    {
        mesh = new Mesh();
        meshCollider = GetComponent<MeshCollider>();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateNewMap();
    }

    public void CreateNewMap()
    {
        CreateMeshShape();
        CreateTriangles();
        ColorMap();
        UpdateMesh();
    }

    private void CreateMeshShape()
    {
        Vector2[] octaveOffsets = GetOffsetSeed();

        if (scale <= 0) scale = 0.0001f;

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        float xOffset = xSize / 2f;
        float zOffset = zSize / 2f;

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float noiseHeight = GenerateNoiseHeight(z, x, octaveOffsets);
                SetMinMaxHeights(noiseHeight);
                vertices[i] = new Vector3(x - xOffset, noiseHeight, z - zOffset);
                i++;
            }
        }
    }

    private Vector2[] GetOffsetSeed()
    {
        seed = Random.Range(0, 1000);
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int o = 0; o < octaves; o++)
        {
            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);
            octaveOffsets[o] = new Vector2(offsetX, offsetY);
        }
        return octaveOffsets;
    }

    private float GenerateNoiseHeight(int z, int x, Vector2[] octaveOffsets)
    {
        float amplitude = 20;
        float frequency = 1;
        float persistence = 0.5f;
        float noiseHeight = 0;

        for (int y = 0; y < octaves; y++)
        {
            float mapZ = z / scale * frequency + octaveOffsets[y].y;
            float mapX = x / scale * frequency + octaveOffsets[y].x;

            float perlinValue = (Mathf.PerlinNoise(mapZ, mapX)) * 2 - 1;
            noiseHeight += heightCurve.Evaluate(perlinValue) * amplitude;
            frequency *= lacunarity;
            amplitude *= persistence;
        }
        return noiseHeight;
    }

    private void SetMinMaxHeights(float noiseHeight)
    {
        if (noiseHeight > maxTerrainheight)
            maxTerrainheight = noiseHeight;
        if (noiseHeight < minTerrainheight)
            minTerrainheight = noiseHeight;
    }

    private void CreateTriangles()
    {
        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    private void ColorMap()
    {
        colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            float height = Mathf.InverseLerp(minTerrainheight, maxTerrainheight, vertices[i].y);
            colors[i] = gradient.Evaluate(height);
        }
    }

    private void MapEmbellishments()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPt = transform.TransformPoint(mesh.vertices[i]);
            var noiseHeight = worldPt.y;
            if (System.Math.Abs(lastNoiseHeight - worldPt.y) < 1)
            {
                if (noiseHeight > 2)
                {
                    if (Random.Range(1, 5) == 1)
                    {
                        GameObject objectToSpawn = objects[Random.Range(0, objects.Length)];
                        var spawnAboveTerrainBy = noiseHeight * 2;
                        Instantiate(objectToSpawn, new Vector3(mesh.vertices[i].x * MESH_SCALE, spawnAboveTerrainBy, mesh.vertices[i].z * MESH_SCALE), Quaternion.identity);
                    }
                }
            }
            lastNoiseHeight = noiseHeight;
        }
    }

    public void UpdateGradient(Gradient newGradient)
    {
        gradient = newGradient;
        ColorMap();
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        meshCollider.sharedMesh = mesh;

        gameObject.transform.localScale = new Vector3(MESH_SCALE, MESH_SCALE, MESH_SCALE);

        // Check for non-readable meshes and log warnings
        if (meshCollider.sharedMesh != null && !meshCollider.sharedMesh.isReadable)
        {
            Debug.LogError($"Mesh {meshCollider.sharedMesh.name} does not allow read access. Enable Read/Write in import settings.");
            return;
        }

        if (navMeshSurface != null)
        {
            BuildNavMeshLinksForGround();
            navMeshSurface.BuildNavMesh();
        }

        MapEmbellishments();
    }

    public void BuildNavMeshLinksForGround()
    {
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground");

        foreach (GameObject groundObject in groundObjects)
        {
            NavMeshSurface surface = groundObject.GetComponent<NavMeshSurface>();
            if (surface != null)
            {
                NavMeshLink navMeshLink = groundObject.GetComponent<NavMeshLink>();
                if (navMeshLink == null)
                {
                    navMeshLink = groundObject.AddComponent<NavMeshLink>();
                }

                navMeshLink.startPoint = new Vector3(-1, 0, 0);
                navMeshLink.endPoint = new Vector3(1, 0, 0);
                navMeshLink.width = 1.0f;
                navMeshLink.bidirectional = true;

                Debug.Log($"NavMeshLink added to {groundObject.name}");
            }
        }
    }
}

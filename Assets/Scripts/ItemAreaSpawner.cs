using UnityEngine;

public class ItemAreaSpawner : MonoBehaviour
{

    public GameObject itemToSpread;

    
    public int numItemsToSpawn = 200;
    public float itemXSpread = 200;
    public float itemYSpread = 0;
    public float itemZSpread = 200;

    void Start()
    {
        for (int i = 0; i < numItemsToSpawn; i++)
        {
            SpreadItem();
        }
    }

    // Update is called once per frame
    void SpreadItem()
    {
        Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread),Random.Range(-itemZSpread, itemZSpread)) + transform.position;
        GameObject clone = Instantiate(itemToSpread,randPosition,Quaternion.identity);
    }
}
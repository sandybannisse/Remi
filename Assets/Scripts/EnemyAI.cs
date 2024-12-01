using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour 
{
    public NavMeshAgent agent; 
    public Transform player; 
    public LayerMask whatIsGround, whatIsPlayer; 
    public float health;
    public Vector3 walkPoint; 
    bool walkPointSet; 
    public float walkPointRange; 
    public float timeBetweenAttacks; 
    bool alreadyAttacked; 
    public GameObject projectile; 
    public float sightRange, attackRange; 
    public bool playerInSightRange, playerInAttackRange;

    public bool isVeggie;
    public bool isUnicorn;
    private bool isSoundPlaying = false;

    private void Awake() 
    { 
        player = GameObject.Find("Remi_2")?.transform; 
        if (player == null)
        {
            Debug.LogError("Player Transform (Remi_2) not found!");
        }

        agent = GetComponent<NavMeshAgent>(); 
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on this GameObject!");
        }
    } 

    private void Update() 
    { 
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); 
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); 

        if (!playerInSightRange && !playerInAttackRange)
            Patroling(); 
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer(); 
        if (playerInAttackRange && playerInSightRange)
        {
            PlayAttackSound();
            AttackPlayer();
        }
    } 

    private void Patroling() 
    {
        if (!walkPointSet) 
            SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false; 
    } 

    private void SearchWalkPoint() 
    { 
        float randomZ = Random.Range(-walkPointRange, walkPointRange); 
        float randomX = Random.Range(-walkPointRange, walkPointRange); 
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); 
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) 
            walkPointSet = true; 
    } 

    private void ChasePlayer() 
    {
        if (player != null)
        {
            agent.SetDestination(player.position); 
        }
    } 

    private void AttackPlayer() 
    { 
        agent.SetDestination(transform.position); 
        transform.LookAt(player); 
        if (!alreadyAttacked) 
        { 
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>(); 
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse); 
            rb.AddForce(transform.up * 8f, ForceMode.Impulse); 
            alreadyAttacked = true; 
            Invoke(nameof(ResetAttack), timeBetweenAttacks); 
        } 
    } 

    private void ResetAttack() 
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage) 
    {
        health -= damage; 
        if (health <= 0)
            Invoke(nameof(DestroyEnemy), 0.5f); 
    }

    private void DestroyEnemy() 
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, attackRange); 
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange); 
    }

    private void PlayAttackSound()
    {
        if (!isSoundPlaying)
        {
            isSoundPlaying = true;

            if (isVeggie)
            {
                AkSoundEngine.PostEvent("VeggieAttack", gameObject);
            }
            else if (isUnicorn)
            {
                AkSoundEngine.PostEvent("UnicornAttack", gameObject);
            }
            else
            {
                AkSoundEngine.PostEvent("VeggieProx", gameObject);
            }

            AkSoundEngine.PostEvent("VeggieAttack", gameObject, (uint)AkCallbackType.AK_EndOfEvent, SoundEndedCallback, null);
        }
    }

    private void SoundEndedCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
    {
        isSoundPlaying = false;
    }
}

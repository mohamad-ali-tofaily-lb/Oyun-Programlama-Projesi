using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public ParticleSystem muzzleFlash;
    public AudioSource gunAudio;
    
    private NavMeshAgent agent;
    private Animator animator;
    private EnemyAI ai;

    public float enemySpeed = 9f; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ai = GetComponent<EnemyAI>();
        agent.speed = enemySpeed; 
    }

    public void MoveToPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void StopMoving()
    {
        agent.isStopped = true;
        animator.SetFloat("Speed", 0);
    }

    public void Dodge()
    {
        agent.isStopped = false;
        Vector3 dodgeTarget = transform.position - transform.forward * 5f;
        agent.SetDestination(dodgeTarget);
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void Shoot()
    {
        StopMoving();
        animator.SetTrigger("Shoot");
        
        if (muzzleFlash != null) muzzleFlash.Play();
        if (gunAudio != null) gunAudio.PlayOneShot(gunAudio.clip);

        Vector3 rayOrigin = transform.position + (Vector3.up * 1f); 
        Vector3 targetPoint = player.position + (Vector3.up * 1f); 
        Vector3 direction = (targetPoint - rayOrigin).normalized;

        RaycastHit hit;
        if (Physics.SphereCast(rayOrigin, 0.2f, direction, out hit, 50f))
        {
            if (hit.transform.root == this.transform.root) return;

            PlayerHealthAndShooting playerHealth = hit.transform.GetComponentInParent<PlayerHealthAndShooting>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10f);
                if (ai != null) ai.ReceiveReward(10f); // Positive reward for hitting player
            }
        }
    }
}
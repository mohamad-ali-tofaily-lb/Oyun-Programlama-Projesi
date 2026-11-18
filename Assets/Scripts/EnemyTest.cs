using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    public Transform player;
    public ParticleSystem muzzleFlash;
    public AudioSource gunAudio;
    
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = 2f; 
    }

    void Update()
    {
        // 1. Move towards player
        if (player != null)
        {
            agent.SetDestination(player.position);
        }

        // 2. Set walking animation speed
        animator.SetFloat("Speed", agent.velocity.magnitude);

        // 3. Press Space to Shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Play the animation and effects
        animator.SetTrigger("Shoot");
        if (muzzleFlash != null) muzzleFlash.Play();
        if (gunAudio != null) gunAudio.PlayOneShot(gunAudio.clip);

        // 1. Start the ray 1 meter above the enemy's feet (Chest height)
        Vector3 rayOrigin = transform.position + (Vector3.up * 1f); 
        
        // 2. Aim at a point 1 meter above the player's feet (Chest height)
        Vector3 targetPoint = player.position + (Vector3.up * 1f); 
        Vector3 direction = (targetPoint - rayOrigin).normalized;

        RaycastHit hit;
        
        // 3. Use SphereCast (a raycast with thickness) so we don't miss!
        // 0.2f is the radius of the bullet
        if (Physics.SphereCast(rayOrigin, 0.2f, direction, out hit, 50f))
        {
            // Ignore if he hits himself
            if (hit.transform.root == this.transform.root) return;

            Debug.Log("Enemy bullet hit: " + hit.transform.name);
            Debug.DrawLine(rayOrigin, hit.point, Color.green, 3f);

            // Check if we hit the player
            PlayerHealthAndShooting playerHealth = hit.transform.GetComponentInParent<PlayerHealthAndShooting>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10f);
                Debug.Log("<color=cyan>DEALT DAMAGE TO PLAYER!</color>");
            }
            else
            {
                Debug.Log("<color=orange>Hit obstacle, not the player.</color>");
            }
        }
    }
}
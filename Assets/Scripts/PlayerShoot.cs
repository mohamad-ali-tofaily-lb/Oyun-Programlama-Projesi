using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 1f; // Cooldown in seconds (1 means 1 shot per second)
    private float nextFireTime = 0f;

    [Header("References")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public AudioSource gunAudio;

    void Update()
    {
        // Check if Left Mouse Button is pressed AND the cooldown is over
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Set the next allowed shooting time
        }
    }

    void Shoot()
    {
        // Play effects
        if (muzzleFlash != null) muzzleFlash.Play();
        if (gunAudio != null) gunAudio.PlayOneShot(gunAudio.clip);

        // The Raycast (The invisible bullet)
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * hit.distance, Color.red, 2f);

            // Find the enemy health script
            EnemyHealth enemy = hit.transform.GetComponentInParent<EnemyHealth>();
            
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
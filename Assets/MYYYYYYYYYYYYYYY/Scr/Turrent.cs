    using UnityEngine ;
using System.Collections;

public class Turrent : MonoBehaviour
{
    public GameObject muzzleFlashPrefab; 
    public Transform turret;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float turretRotationSpeed = 80f;
    public float fireRate = 2f; 
    private float nextFireTime = 0f;

    
    public AudioSource audioSource; 
    public AudioClip shootClip; 

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * turretRotationSpeed * Time.deltaTime;
        turret.Rotate(0, mouseX, 0);

        
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;
        if (muzzleFlashPrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            Destroy(flash, 0.1f); 
        }

        
        if (audioSource != null && shootClip != null)
        {
            audioSource.PlayOneShot(shootClip);
        }
    }
}
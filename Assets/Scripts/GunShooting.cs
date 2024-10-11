using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject bulletPrefab;         // Bullet prefab to spawn
    public Transform muzzlePoint;           // Muzzle point where the bullet spawns
    public float bulletSpeed = 50f;         // Speed of the bullet
    public float fireRate = 750;           // Time between shots
    private float nextFireTime = 0f;        // Timer to control fire rate
    
    private void Update()
    {
        // Detect the trigger press (replace with the appropriate trigger based on your input setup)
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && Time.time > nextFireTime)
        {
            Shoot();  // Call the shoot function
            nextFireTime = Time.time + fireRate;  // Control the fire rate
        }
    }

    private void Shoot()
    {
        // Instantiate a bullet at the muzzle point
        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        Debug.Log("BIEM 1");

        if (bullet.GetComponent<BoxCollider>() == null)
        {
            bullet.AddComponent<BoxCollider>();
        }
        // Get the Rigidbody of the bullet and apply force to it
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        if (bulletRb != null)
        {
            
            bulletRb.velocity = muzzlePoint.forward * bulletSpeed;  // Move the bullet forward at the set speed
            Debug.Log("BIEM");
        }

        // Optionally, destroy the bullet after a few seconds to clean up the scene
        Destroy(bullet, 5f);  // Destroys the bullet after 5 seconds
        Debug.Log("BAM");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour 
{
    public GunData weaponStats; // ScriptableObject containing gun stats
    public Transform muzzleTransform; // Transform where bullets are spawned
    
    private int currentAmmo; 
    private bool isReloading;
    private float lastShootTime;
    private bool wasTriggerPulled;
    
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = weaponStats.ammoCapacity;
    }

    public void UpdateFiring(bool triggerPressed) 
    {
        if (isReloading || currentAmmo <= 0)
        {
            return;
        }
        
        if (weaponStats.currentFireMode == GunData.FireMode.SemiAutomatic)
        {
            // Shoot once on trigger press (edge detection)
            if (triggerPressed && !wasTriggerPulled)
            {
                Shoot();
            }
        }
        else if (weaponStats.currentFireMode == GunData.FireMode.Automatic)
        {
            // Shoot continuously while trigger is held, respecting fire rate
            if (triggerPressed && Time.time >= lastShootTime + 1f / weaponStats.fireRate)
            {
                Shoot();
            }
        }

        wasTriggerPulled = triggerPressed;
    }
    
    private void Shoot()
    {
        if (currentAmmo <= 0)
            return;

        // Instantiate bullet at muzzle position and rotation
        GameObject bullet = Instantiate(weaponStats.bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = weaponStats.damage;
            // Optionally set bullet speed, direction, etc.
        }

        currentAmmo--;
        lastShootTime = Time.time;
        // Optionally play sound effects or animations here
    }

    public void Reload() 
    {
        if (isReloading || currentAmmo == weaponStats.ammoCapacity)
            return;

        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine() // Coroutine for reloading
    {
        isReloading = true; // Prevent shooting while reloading
        yield return new WaitForSeconds(weaponStats.reloadTime); // Wait for reload time
        currentAmmo = weaponStats.ammoCapacity; // Refill ammo
        isReloading = false; // Allow shooting again
    }

    // Methods for UI or other systems to access gun state
    public int GetCurrentAmmo() => currentAmmo;
    public int GetMaxAmmo() => weaponStats.ammoCapacity;
    public bool IsReloading() => isReloading;
}

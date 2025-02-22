using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Gun/Gun Data")]
public class GunData : ScriptableObject
{
    public enum GunType // Different types of guns
    {
        Pistol,
        AssaultRifle,
        Shotgun,
        Sniper
    }
    
    public enum FireMode // Set the fire mode, works for both semi-automatic and automatic
    {
        SemiAutomatic,
        Automatic
    }
    
    public GunType gunType; // Set the gun type, using the enum here to change it in the inspector
    public string gunName; // Set the gun name
    public int damage;
    public float fireRate;
    public int ammoCapacity;
    public float reloadTime;
    public FireMode currentFireMode;
    public GameObject bulletPrefab;
}

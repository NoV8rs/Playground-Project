using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public int enemyLevel = 1;
    public int enemyHealth;
    public int enemyDamage;
    
    public int enemyPhysicalResistance; // Physical resistance, but is any damage for now
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStats enemyStats;
    public string enemyName;
    public int enemyLevel;
    public int enemyHealth;
    public int enemyDamage;
    
    
    void Start()
    {
        enemyName = enemyStats.enemyName;
        enemyLevel = enemyStats.enemyLevel;
        enemyHealth = enemyStats.enemyHealth;
        enemyDamage = enemyStats.enemyDamage;
        EnemyLevelScale();
        Debug.Log("Enemy Name: " + enemyHealth);
    }
    
    public void EnemyTakeDamage(int damage)
    {
        enemyHealth -= damage;
        
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void EnemyLevelScale()
    {
        int currentEnemyLevel = enemyLevel;
        enemyHealth = (int)(enemyHealth * Mathf.Pow(1.13f, currentEnemyLevel));
        enemyDamage += currentEnemyLevel * 5;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            EnemyTakeDamage((int)bullet.damage);
            Destroy(collision.gameObject);
            Debug.Log("Enemy hit by bullet " + enemyHealth);
        }
    }
}

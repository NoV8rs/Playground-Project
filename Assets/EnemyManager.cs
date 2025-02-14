using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public int enemyHealth;
    public int enemyMoneyDrop;
    public int enemyDiamondDrop;
    
    public Image enemyImage;
    public Transform enemyTransform;
    
    public Slider enemyHealthText;
    
    private void Start()
    {
        enemyHealthText.maxValue = enemyHealth;
        enemyHealthText.value = enemyHealth;
    }
    
    public void EnemyTakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Die();
        }
    }
    
    public void OnClick()
    {
        EnemyTakeDamage(1);
        enemyHealthText.value = enemyHealth;
    }
    
    private void EnemyDeath()
    {
        // Add money and diamonds to player
        FindObjectOfType<RPG_Elements>().AddMoney(enemyMoneyDrop);
        FindObjectOfType<RPG_Elements>().AddDiamonds(enemyDiamondDrop);
    }
    
    private void Die()
    {
        // Find the child GameObject with the Image component and destroy it
        Transform imageTransform = transform.Find("Image");
        if (imageTransform != null)
        {
            Destroy(imageTransform.gameObject);
        }
        EnemyDeath();
        EnemySpawn();
    }

    private void EnemySpawn()
    {
        Instantiate(enemyTransform, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity);
    }
}

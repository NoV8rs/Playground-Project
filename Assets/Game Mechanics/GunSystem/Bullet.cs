using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // RequireComponent is used to automatically add the component to the GameObject if it doesn't have it
public class Bullet : MonoBehaviour
{
    public float damage; 
    public float speed = 20f; 
    public float lifeTime = 5f;
    
    private Rigidbody rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime); // Destroy has 2 parameters, the object and the time to wait before destroying it
    }

    private void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision) // OnCollisionEnter is called when the GameObject collides with another GameObject
    {
        if (collision == null)
        {
            return;
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyManager>().EnemyTakeDamage((int)damage);
            Destroy(gameObject);
        }

        if (collision.gameObject)
        {
            Destroy(gameObject);
        }
        
        Debug.Log("Bullet hit: " + collision.gameObject.name);
        Debug.Log("Gun deals: " + damage + " damage");
    }
}

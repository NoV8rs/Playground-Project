using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanging : MonoBehaviour
{
    private LevelManager levelManager;
    
    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.levelChangingTrigger = !levelManager.levelChangingTrigger;
            levelManager.LoadLevel();
        }
    }
}

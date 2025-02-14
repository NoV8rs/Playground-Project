using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject Level1;
    public GameObject Level2;
    
    public bool levelChangingTrigger;
    
    public void LoadLevel()
    {
        if (levelChangingTrigger)
        {
            Level1.SetActive(false);
            Level2.SetActive(true);
        }
        else
        {
            Level1.SetActive(true);
            Level2.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Platformer.Core.Simulation;

public class EnemiesAliveCheck : MonoBehaviour
{
    List<GameObject> listOfOpponents = new List<GameObject>();
    
    
 
    void Start()
    {
        listOfOpponents.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        var ev = Schedule<PlayerEnemyCollision>();
        ev.enemyCheck = this;
        
    }
 
    public void KilledEnemy()
    {
        Debug.Log("KilledEnemy - listOfOpponents.Count " + listOfOpponents.Count);
        if(listOfOpponents.Count > 0)
        {
            listOfOpponents.RemoveAt(0);
            Debug.Log("KilledEnemy - RemoveAt " + listOfOpponents.Count);

        }
    }
 
    void Update()
    {
        if(listOfOpponents.Count <= 0)
        {
            SceneManager.LoadScene("SuccessScreen");
        }
    }
}

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
        
        Debug.Log("somos " + listOfOpponents.Count);
        listOfOpponents.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        var ev = Schedule<PlayerEnemyCollision>();
        ev.enemyCheck = this;
    }
 
    public void KilledEnemy()
    {
        if(listOfOpponents.Count > 0)
        {
            listOfOpponents.RemoveAt(0);
        }
    }
 
    void Update()
    {

        if(listOfOpponents.Count <= 0)
        {
            StartCoroutine(EnemyDeath());
        }
    }
    
    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("SuccessScreen");
    }
}

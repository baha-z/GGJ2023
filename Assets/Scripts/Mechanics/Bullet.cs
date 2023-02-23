using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    
    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyController enemy = hitInfo.GetComponent<EnemyController>();
        BossController boss = hitInfo.GetComponent<BossController>();
        
        if (enemy !=null)
        {
            
            var ev = Schedule<PlayerEnemyCollision>();
            ev.bullet = this;
            ev.enemy = enemy;
        }
        if (boss !=null)
        {
            
            var ev = Schedule<PlayerEnemyCollision>();
            ev.bullet = this;
            ev.boss = boss;
        }

    }

    public void KillBullet()
    {
        Destroy(gameObject);
    }


}

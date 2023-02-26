using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;
        public BossController boss;
        public Bullet bullet;
        public EnemiesAliveCheck enemyCheck;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();


        public override void Execute()
        {
            if (enemy != null)
            {
                var enemyHealth = enemy.GetComponent<Health>();

                if (player != null)
                {
                    var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

                    if (willHurtEnemy)
                    {
                        if (enemyHealth != null)
                        {
                            enemyHealth.Decrement(true);
                            if (!enemyHealth.IsAlive)
                            {
                                Schedule<EnemyDeath>().enemy = enemy;
                                player.Bounce(2);
                            }
                            else
                            {
                                player.Bounce(7);
                            }
                        }
                        else
                        {
                            Schedule<EnemyDeath>().enemy = enemy;
                            player.Bounce(2);
                        }
                    }
                    else
                    {
                        var playerHealth = player.GetComponent<Health>();
                        playerHealth.Decrement(true);
                    }
                    bullet.KillBullet();
                    player = null;
                }

                if (bullet != null)
                {
                    if (enemyHealth != null)
                    {
                        Debug.Log("enemyHealth" + enemyHealth.GetHP());
                        enemyHealth.Decrement(false);
                        if (!enemyHealth.IsAlive)
                        {
                            enemyCheck.KilledEnemy();
                            Schedule<EnemyDeath>().enemy = enemy;
                        }
                    }
                    bullet.KillBullet();
                    bullet = null;
                }

                enemy = null;
            }
            
            if (boss != null)
            {

                var bossHealth = boss.GetComponent<Health>();
                Debug.Log("bossHealth" + bossHealth.GetHP());
                
                if (player != null)
                {
                    var playerHealth = player.GetComponent<Health>();
                    playerHealth.Decrement(true);
                    player = null;
                }
                
                if (bullet != null)
                {
                    if (bossHealth != null)
                    {
                        bossHealth.Decrement(false);
                        if (!bossHealth.IsAlive)
                        {
                            enemyCheck.KilledEnemy();
                            Schedule<EnemyDeath>().boss = boss;
                        }
                    }
                    bullet.KillBullet();
                    bullet = null;
                }
                boss = null;
            }
        }
    }
}
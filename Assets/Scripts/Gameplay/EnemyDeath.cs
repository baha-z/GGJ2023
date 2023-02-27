using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the health component on an enemy has a hitpoint value of  0.
    /// </summary>
    /// <typeparam name="EnemyDeath"></typeparam>
    public class EnemyDeath : Simulation.Event<EnemyDeath>
    {
        public EnemyController enemy;
        public BossController boss;
        
        public override void Execute()
        {

            if (enemy != null)
            {
                enemy._collider.enabled = false;
                enemy.animator.SetTrigger("death");

                var renderer = enemy.GetComponent<Renderer>();
                renderer.sortingOrder = 1;
                
                var position = new Vector3( enemy.xCoord, enemy.yCoord, 0 );
                var scale = new Vector3( 2f, 2f, 2f );

                enemy.transform.position = position;
                enemy.transform.localScale = scale;
                
                if (enemy._audio && enemy.ouch)
                    enemy._audio.PlayOneShot(enemy.ouch);
            }

            if (boss != null)
            {
                var position = new Vector3( 0.02f, 5.69f, 0 );
                var scale = new Vector3( 2f, 2f, 2f );
                boss.transform.position = position;
                boss.transform.localScale = scale;
                
                boss._collider.enabled = false;
                boss.animator.SetTrigger("death");
            }
            

        }
    }
}
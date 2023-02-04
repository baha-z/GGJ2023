using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Unity.VisualScripting;

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
                if (enemy._audio && enemy.ouch)
                    enemy._audio.PlayOneShot(enemy.ouch);
            }

            if (boss != null)
            {
                boss._collider.enabled = false;
                boss.animator.SetTrigger("death");
            }
            

        }
    }
}
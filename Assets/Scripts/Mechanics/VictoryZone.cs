using System.Collections;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        
        public GameObject bossPrefab;
        public GameObject vineWall1;
        public GameObject vineWall2;
        public GameObject vineWall3;
        public AudioSource audioSource;
        public AudioClip vineSound;
        public AudioClip monsterRoar;
        public Transform bossSpawnPoint;
        public Camera cam1;
        public Camera cam2;

        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null)
            {
                
                cam1.enabled = false;
                cam2.enabled = true;
                
                var ev = Schedule<PlayerEnteredVictoryZone>();
                ev.victoryZone = this;

                p.controlEnabled = false;
                StartCoroutine (Spawn(p));
                Destroy(GetComponent<BoxCollider2D>());
                
            }
        }
        
                
        IEnumerator Spawn(PlayerController player)
        {
            
            var vine1Pos = new Vector3( -8.46f , 0.86f , 0f );
            var vine2Pos = new Vector3( -8.46f , 4.18f , 0f );
            var vine3Pos = new Vector3( -8.46f , 7.51f , 0f );
            
            yield return new WaitForSeconds(1);
            audioSource.PlayOneShot(vineSound);
            Instantiate(vineWall1, vine1Pos, Quaternion.identity);

            yield return new WaitForSeconds(1);
            audioSource.PlayOneShot(vineSound);
            Instantiate(vineWall2, vine2Pos, Quaternion.identity);
            
            yield return new WaitForSeconds(1);
            audioSource.PlayOneShot(vineSound);
            Instantiate(vineWall3, vine3Pos, Quaternion.identity);
            
            yield return new WaitForSeconds(1);
            audioSource.PlayOneShot(monsterRoar);
            Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
            player.controlEnabled = true;
        }
        

    }
}
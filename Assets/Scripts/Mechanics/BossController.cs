using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent( typeof(Collider2D))]
    public class BossController : MonoBehaviour
    {
        public AudioClip ouch;

        //internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        public Health health;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        public float width = 4.5f;
        public float height = 4.5f;
        public Vector3 position = new Vector3( 0.02f, 2f, 0 );
        public Vector3 originalPosition = new Vector3( 0.02f, 5.69f, 0 );


        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            //control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }
        
        void Start () 
        {
            StartCoroutine (Chomp());
        }

        IEnumerator Chomp()
        {
            
            transform.position = originalPosition;
            Vector3 original = new Vector3( width , height , 4.5f );
            transform.localScale = original;
            // set the position
            
            
            var waitTime = Random.Range(5, 10);
            Debug.Log("Attack on  "+ waitTime + "sec");

            yield return new WaitForSeconds(waitTime);
            
            animator.SetTrigger("attack");
            // set the scaling
            Vector3 scale = new Vector3( width * 2 , height * 2, 4.5f );
            transform.localScale = scale;
            // set the position
            transform.position = position;
            
            yield return new WaitForSeconds(1);

            StartCoroutine (Chomp());
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("choque con player" + player.name);

                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.boss = this;
            }
        }



    }
}
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
    public class EnemyController : MonoBehaviour
    {
        public AudioClip ouch;
        public float xCoord = -3.66f;
        public float yCoord = 2.91f;

        //internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        public Health health;
        SpriteRenderer spriteRenderer;
        internal Animator animator;

        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            //control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            
            if (player != null)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = this;
            }
        }



    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyEntity : MonoBehaviour
    {
        protected Animator animator;

        public GameObject ledgeDetector;
        public Transform target;
        public float speed;
        public float distToChasePlayer = 8f;
        public int typeAttack = 0;

        [Tooltip("The sound effect that plays when this enemy is damaged (but does not die).")]
        public AudioClip soundOnDamage;

        [Tooltip("The sound effect that plays when this enemy dies.")]
        public AudioClip soundOnDeath;

        [Tooltip("The sound effect that plays whenever this enemy attacks.")]
        public AudioClip attackSoundEffect;

        // The Audio Source attached to this game object.
        protected AudioSource audioSource;

        public bool _canMove = true;
        public bool CanMove
        {
            get
            {
                return animator.GetBool(AnimationStrings.canMove);
            }
        }
        public bool _hasTarget = false;
        public bool HasTarget
        {
            get
            {
                return _hasTarget;
            }
            protected set
            {
                _hasTarget = value;
                animator.SetBool(AnimationStrings.hasTarget, value);
            }
        }
        public float AttackCooldown
        {
            get
            {
                return animator.GetFloat(AnimationStrings.attackCooldown);
            }
            protected set
            {
                animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
            }
        }

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        protected virtual void Start()
        {
            target = GameObject.Find("KeyMaster").transform;
        }

        protected virtual void Update()
        {
            // Common enemy behavior here
        }

        protected virtual void FixedUpdate()
        {
            // Common enemy physics here
        }

        // This function deals damage to the enemy.
        public virtual void TakeDamage()
        {
            // Common code for taking damage
        }

        protected virtual void OnDestroy()
        {
            if (audioSource != null && soundOnDeath != null)
            {
                // Play the death sound effect.
                AudioSource.PlayClipAtPoint(soundOnDeath, new Vector3(0, 0, -10), audioSource.volume);
            }
        }
    }

}


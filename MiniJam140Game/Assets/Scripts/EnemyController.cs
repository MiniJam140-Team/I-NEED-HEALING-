using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour
    {

        Animator animator;
        Rigidbody2D rb;
        CapsuleCollider2D capsuleCollider;
        public GameObject ledgeDetector;
        public RuntimeAnimatorController werewolf2Controller;
        public Transform target;
        private bool isChasing;
        public float speed;
        public float distToChasePlayer = 8f;
        public int typeAttack = 0;

        [Tooltip("The sound effect that plays when this enemy is damaged (but does not die).")]
        public AudioClip soundOnDamage;

        [Tooltip("The sound effect that plays when this enemy dies.")]
        public AudioClip soundOnDeath;

        [Tooltip("The sound effect that plays whenever this tower fires a projectile.")]
        public AudioClip shootSoundEffect;

        // The Audio Source attached to this game object.
        private AudioSource audioSource;


        public bool IsChasing
        {
            get
            {
                return isChasing;
            }
            set
            {
                isChasing = value;
                animator.SetBool(AnimationStrings.isChasing, value);
            }
        }
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
            private set
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
            private set
            {
                animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
            }
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();

        }
        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.Find("MainPlayer").transform;
        }
        // Update is called once per frame
        void Update()
        {
            //HasTarget = attackZone.detectedColliders.Count > 0;
            Vector3 scale = transform.localScale;
            //IsChasing = Vector2.Distance(transform.position, target.position) < distToChasePlayer ? true : false;

            if (transform.position.x > target.position.x)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            if (transform.position.x < target.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * 1;
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (transform.position.y > target.position.y)
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            if (transform.position.y < target.position.y)
            {
                scale.x = Mathf.Abs(scale.x) * 1;
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
            transform.localScale = scale;
        }
        private void FixedUpdate()
        {
        }
        // This function deals damage to the enemy. Projectiles call this function on collision.
        public void DestroyEnemy()
        {
            // Reduce our health by the amount of damage taken.
            Destroy(gameObject);
        }
        private void OnDestroy()
        {
            if (audioSource != null && soundOnDeath != null)
            {
                // ... then play the damage sound effect!
                // PlayClipAtPoint is an easy (but messy) way of playing a sound effect even after the object is destroyed.
                AudioSource.PlayClipAtPoint(soundOnDeath, new Vector3(0, 0, -10), audioSource.volume);
            }
        }
    }
}

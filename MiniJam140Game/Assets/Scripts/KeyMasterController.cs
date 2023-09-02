using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class KeyMasterController : MonoBehaviour
    {
        //Damagable damagable;
        public enum attacks { attack1, attack2 };
        //public DetectionZone attackZone;
        Animator animator;
        Rigidbody2D rb;
        CapsuleCollider2D capsuleCollider;
        public GameObject ledgeDetector;
        public Transform target;
        private bool isChasing;
        public float speed;
        public float distToChasePlayer = 8f;
        public int typeAttack = 0;

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
            //animator = GetComponent<Animator>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();

        }
        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.Find("Door").transform;
        }
        // Update is called once per frame
        void Update()
        {
            ////if (damagable.Health <= 50)
            //{
            //}

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
    }
}

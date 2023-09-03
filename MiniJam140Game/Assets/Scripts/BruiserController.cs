using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class BruiserController : MonoBehaviour
    {
        //Damagable damagable;
        public enum attacks { attack1, attack2 };
        //public DetectionZone attackZone;
        Animator animator;
        Rigidbody2D rb;
        SpriteRenderer rend;
        public Transform target;
        private bool isMoving;
        public float speed;

        public bool IsMoving
        {
            get
            {
                return isMoving;
            }
            set
            {
                isMoving = value;
                animator.SetBool(AnimationStrings.isMoving, value);
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

        }
        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<SpriteRenderer>();
        }
        // Update is called once per frame
        void Update()
        {
            //HasTarget = attackZone.detectedColliders.Count > 0;
            Vector3 scale = transform.localScale;
            //IsChasing = Vector2.Distance(transform.position, target.position) < distToChasePlayer ? true : false;
            if (target)
            {
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
        }
        private void FixedUpdate()
        {
            SetTarget();
        }
        void SetTarget()
        {
            if (GameObject.FindWithTag("Barrier").transform != null)
            {
                target = GameObject.FindWithTag("Barrier").transform;
            }
        }
    }
}

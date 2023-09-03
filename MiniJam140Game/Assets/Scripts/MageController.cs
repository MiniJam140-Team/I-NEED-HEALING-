using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    internal class MageController : MonoBehaviour
    {
        //Damagable damagable;
        Animator animator;
        [SerializeField] 
        GameObject ledgeDetector;
        [SerializeField]
        Transform target;
        [SerializeField]
        Transform player;
        private bool isMoving;
        float speed = 6f;
        float maxFollowDist = 3f;
        float minFollowDist = 1.5f;

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
            animator = GetComponent<Animator>();
        }
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        // Update is called once per frame
        void Update()
        {
            if(Vector2.Distance(transform.position, player.transform.position) >= minFollowDist && Vector2.Distance(transform.position, player.transform.position) <= maxFollowDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            target = GameObject.FindWithTag("Enemy").transform;
            Vector3 scale = transform.localScale;

            if (transform.position.x > target.position.x)
            {
                //transform.position += Vector3.left * speed * Time.deltaTime;
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            if (transform.position.x < target.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * 1;
                //transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (transform.position.y > target.position.y)
            {
                //transform.position += Vector3.down * speed * Time.deltaTime;
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            if (transform.position.y < target.position.y)
            {
                scale.x = Mathf.Abs(scale.x) * 1;
                //transform.position += Vector3.up * speed * Time.deltaTime;
            }
            transform.localScale = scale;
        }
    }
}

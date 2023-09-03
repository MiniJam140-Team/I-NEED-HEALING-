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
        Animator animator;
        Rigidbody2D rb;
        [SerializeField]
        Transform target;
        [SerializeField]
        Transform player;
        private bool isMoving;
        float speed = 6f;
        float maxFollowDist = 3f;
        float minFollowDist = 1.5f;
        float minAttackTargDist = 5f;

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
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        // Update is called once per frame
        void Update()
        {
            if (Vector2.Distance(transform.position, player.transform.position) >= minFollowDist && Vector2.Distance(transform.position, player.transform.position) <= maxFollowDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            SetTarget();
            Vector3 scale = transform.localScale;
            if (Vector2.Distance(transform.position, target.transform.position) <= minAttackTargDist)
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
        }
        void SetTarget()
        {
            if (GameObject.FindWithTag("Barrier").transform != null)
            {

                target = GetClosestBarrier().transform;
            }            
        }
        public GameObject GetClosestBarrier()
        {
            List<BarrierController> barriers = new List<BarrierController>(FindObjectsOfType<BarrierController>());
            if (barriers.Count == 0)
            {
                // ... don't return an enemy (there isn't any!) and exit the function.
                return null;
                
            }
            // Sort the enemies by distance to this tower.
            barriers.Sort((enemy1, enemy2) =>
            {
                float enemy1Distance = Vector2.Distance(enemy1.transform.position, transform.position);
                float enemy2Distance = Vector2.Distance(enemy2.transform.position, transform.position);
                return enemy1Distance.CompareTo(enemy2Distance);
            });
            // Return the first enemy in the list (which is the closest since we sorted it).
            return barriers[0].gameObject;
        }
    }
}

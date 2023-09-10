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
        [SerializeField]
        Transform player;
        [SerializeField]
        Transform target;
        [SerializeField]
        TimerScript timer;
        [SerializeField]
        GameObject loseScreen;
        private bool isMoving;
        float speed = 6f;
        float maxFollowDist = 3f;
        float minFollowDist = 1.5f;
        float minGotoDoorDist = 5f;
        public bool barrierFound;

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
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            //animator = GetComponent<Animator>();
            timer = FindAnyObjectByType<TimerScript>();
        }
        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.Find("Door").transform;
            player = GameObject.FindGameObjectWithTag("KeymasterFollowObject").GetComponent<Transform>();
        }
        // Update is called once per frame
        void Update()
        {
            barrierFound = GameObject.FindGameObjectWithTag("Barrier");
            if (Vector2.Distance(transform.position, player.transform.position) >= minFollowDist && Vector2.Distance(transform.position, player.transform.position) <= maxFollowDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            Vector3 scale = transform.localScale;

            if (Vector2.Distance(transform.position, target.transform.position) <= minGotoDoorDist)
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
        private void OnDestroy()
        {
            //stop timer
            timer.timerOn = false;
            //activate losescreen
            loseScreen.SetActive(true);
        }
    }
}

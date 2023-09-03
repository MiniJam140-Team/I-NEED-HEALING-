using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class barrierdestroy : MonoBehaviour
    {
        [SerializeField]
        public List<BarrierController> detectedBarrierControllers = new List<BarrierController>();
        Animator animator;
        BarrierController barrierController;
        [SerializeField]
        public float attackSpeed = 10f;
        [SerializeField]
        public float timeSinceAttack = 0;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            HasTarget = detectedBarrierControllers.Count > 0;
            while (HasTarget)
            {
                timeSinceAttack += Time.deltaTime;
                if (timeSinceAttack >= attackSpeed)
                {
                    //attack animation
                    BarrierController controller = detectedBarrierControllers[0];
                    controller.TakeDamage(.5f);
                    timeSinceAttack = 0;
                }
            }
        }
        public bool HasTarget = false;
        //public bool HasTarget
        //{
        //    get
        //    {
        //        return _hasTarget;
        //    }
        //    private set
        //    {
        //        _hasTarget = value;
        //        animator.SetBool(AnimationStrings.hasTarget, value);
        //    }
        //}
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Barrier"))
            {
                detectedBarrierControllers.Add(collision.gameObject.GetComponent<BarrierController>());
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Barrier"))
            {
                detectedBarrierControllers.Remove(collision.gameObject.GetComponent<BarrierController>());
            }
        }
        public GameObject GetClosestBarrier()
        {
            if (detectedBarrierControllers.Count == 0)
            {
                // ... don't return an enemy (there isn't any!) and exit the function.
                return null;
            }

            // Sort the enemies by distance to this tower.
            detectedBarrierControllers.Sort((enemy1, enemy2) =>
            {
                float enemy1Distance = Vector2.Distance(enemy1.transform.position, transform.position);
                float enemy2Distance = Vector2.Distance(enemy2.transform.position, transform.position);
                return enemy1Distance.CompareTo(enemy2Distance);
            });

            // Return the first enemy in the list (which is the closest since we sorted it).
            return detectedBarrierControllers[0].gameObject;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class MageProjectileController : MonoBehaviour
    {

        [Tooltip("How quickly this projectile moves (in units per second).")]
        public float speed = 10f;

        [Tooltip("How much distance this projectile can travel before it is destroyed.")]
        public float distanceToTravel = 3f;

        // This variable helps us keep track of whether or not we collided with multiple enemies in the same frame.
        private bool hasCollided = false;

        // This is how much distance the projectile has travelled. When this is greater than distanceToTravel, we destroy the projectile.
        private float distanceAlive;

        // This function is called by the tower as soon as this projectile is created.
        public void MoveTowards(Vector3 position)
        {
            // Get our attached Rigidbody2D...
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            // ... and set its velocity to move towards the target position.
            body.velocity = (position - transform.position).normalized * speed;

            // Rotate towards the target position.
            float angle = Vector3.SignedAngle(transform.up, transform.position - position,
                transform.forward) + 180;
            transform.Rotate(0, 0, angle);
        }

        // Update is called once per frame
        void Update()
        {
            // Add how much distance we travelled (distance = time x speed).
            distanceAlive += Time.deltaTime * speed;
            // If we have travelled greater than distanceToTravel...
            if (distanceAlive >= distanceToTravel)
            {
                // ... then destroy this projectile.
                Destroy(gameObject);
            }
        }


        // This is a special function that is called whenever a trigger collider attached to this game object touches another collider
        private void OnTriggerEnter2D(Collider2D collider)
        {
            // If we've already collided with an enemy...
            if (hasCollided)
            {
                // Exit the function (don't do anything).
                return;
                // We're doing this because sometimes the projectile can collide with two enemies in the same frame.
            }

            // Get the EnemyController component attached to the collider we just collided with.
            EnemyController enemyController = collider.GetComponent<EnemyController>();
            // ... then destroy that EnemyController.
            enemyController.DestroyEnemy();

            // And mark ourselves as having collided with something.
            hasCollided = true;

            // And then destroy this projectile.
            Destroy(gameObject);
        }
    }
}

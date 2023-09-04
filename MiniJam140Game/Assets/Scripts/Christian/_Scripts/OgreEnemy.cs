using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class OgreEnemy : EnemyEntity
    {
        public float chaseSpeed = 5f; // Adjust this speed as needed
        public float meleeAttackRange = 2f; // Adjust the melee attack range as needed
        public float stopDistance = 1f; // Adjust the stopping distance from the target

        public bool isAttacking = false;
        private bool isChasing = true;
        public int damageAmount = 25;
        private Coroutine attackCoroutine;

        protected override void Update()
        {


            base.Update();
            if (target != null)
            {
                // Calculate the direction to the target
                Vector3 directionToTarget = target.position - transform.position;
                directionToTarget.Normalize();

                // Check if the target is within the chase distance
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (isAttacking)
                {
                    // Check if the Keymaster is still within range (in case the target moved away)
                    if (distanceToTarget <= meleeAttackRange)
                    {
                    }
                    else
                    {
                        // Stop attacking and resume chasing if the player is out of melee attack range
                        StopAttackCoroutine();
                        isAttacking = false;
                        animator.SetBool("isAttacking", false); // Set the attack animation boolean to false
                        isChasing = true; // Resume chasing
                        animator.SetBool("isChasing", true);
                    }
                }
                else if (isChasing)
                {
                    // If within melee attack range, stop and start melee attack
                    if (distanceToTarget <= meleeAttackRange)
                    {
                        isChasing = false;
                        animator.SetBool("isChasing", false);
                        StartMeleeAttack();
                    }
                    else if (distanceToTarget < distToChasePlayer)
                    {
                        // Chase the target
                        Vector3 movement = directionToTarget * chaseSpeed * Time.deltaTime;
                        transform.position += movement;

                        // Flip both Ogre and sword to face the target
                        FlipToFaceTarget(directionToTarget);
                    }
                }
            }
        }

        // Implement other Ogre-specific methods and properties here

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            // Ogre-specific physics here
        }

        public override void TakeDamage()
        {
            base.TakeDamage();

            // Ogre-specific damage handling
        }

        private void StartMeleeAttack()
        {
            // Play the attack animation if not already attacking
            if (!isAttacking)
            {
                animator.SetBool("isAttacking", true); // Set the attack animation boolean to true
                attackCoroutine = StartCoroutine(AttackCoroutine());
            }
        }


        private IEnumerator AttackCoroutine()
        {
            yield return new WaitForSeconds(0.5f); // Adjust the duration as needed for your attack animation
                                                   // Check if the Keymaster is still within range (in case the target moved away)
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= meleeAttackRange)
            {
                // Kill the Keymaster (you may have a separate method for this)
                KeyMasterHealth keymasterHealth = target.GetComponent<KeyMasterHealth>();
                if (keymasterHealth != null)
                {
                    keymasterHealth.TakeDamage(damageAmount);
                    Debug.Log("Keymaster health: " + keymasterHealth.currentHealth); // Debug line
                }
            }
            // Stop the attack coroutine and transition to chasing
            StopAttackCoroutine();
            isAttacking = false;
            animator.SetBool("isAttacking", false); // Set the attack animation boolean to false
            isChasing = true; // Resume chasing
            animator.SetBool("isChasing", true);
        }

        private void StopAttackCoroutine()
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }

        private void FlipToFaceTarget(Vector3 direction)
        {
            // Flip the Ogre based on the direction
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Flip horizontally
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // No flip
            }
        }
    }
}


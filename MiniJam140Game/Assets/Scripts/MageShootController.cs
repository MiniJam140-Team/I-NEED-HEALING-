using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class MageShootController : MonoBehaviour
    {
        [Tooltip("The prefab that this tower will spawn. Make sure the prefab has an attached Projectile Controller!")]
        public GameObject projectilePrefab;

        [Tooltip("How nearby an enemy has to be for this tower to shoot at it.")]
        public float range = 6;

        [Tooltip("How much time (in seconds) between projectile spawns.")]
        public float shootSpeed = 0.5f;

        [Tooltip("The sound effect that plays when this enemy is damaged (but does not die).")]
        public AudioClip soundOnDamage;

        [Tooltip("The sound effect that plays whenever this tower fires a projectile.")]
        public AudioClip shootSoundEffect;

        // The amount of time (in seconds) since this tower last fired a projectile.
        private float timeSinceLastShot = 0;

        // The Audio Source attached to this game object.
        private AudioSource audioSource;

        // Start is called before the first frame update
        void Start()
        {
            // Cache the audio source so we can reference it later. (Calling GetComponent() is expensive, so we prefer to only do it once)
            audioSource = GetComponent<AudioSource>();

            // If we have a shoot sound assigned but we don't have an attached audio source...
            if (audioSource == null && shootSoundEffect != null)
            {
                // ... warn the dev making this game.
                Debug.LogWarning(name + " has an assigned Shoot Sound Effect but it doesn't have an attached Audio Source!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Get the closest enemy.
            GameObject closestEnemy = GetClosestEnemy();
            // If there is no closest enemy, or if the closest enemy is out of range...
            if (closestEnemy == null || Vector2.Distance(closestEnemy.transform.position, transform.position) > range)
            {
                // ... then exit this function (don't do anything else).
                return;
            }

            //// Rotate towards the closest enemy.
            //float angle = Vector3.SignedAngle(transform.up, transform.position - closestEnemy.transform.position,
            //    transform.forward) + 180;
            //transform.Rotate(0, 0, angle);

            // Add how much time has passed since last frame.
            timeSinceLastShot += Time.deltaTime;

            // If enough time has passed that we should shoot a projectile...
            if (timeSinceLastShot >= shootSpeed)
            {
                // ... spawn the projectile!
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                // Then get the ProjectileController component attached to it.
                MageProjectileController towerProjectileController = projectile.GetComponent<MageProjectileController>();
                // And then tell it what direction to move.
                towerProjectileController.MoveTowards(closestEnemy.transform.position);

                // If we have an attached audio source and we have a shoot sound effect...
                if (audioSource != null && shootSoundEffect != null)
                {
                    // ... play our shoot sound effect!
                    audioSource.PlayOneShot(shootSoundEffect);
                }

                // Lastly, reset how much time has passed since we last shot.
                timeSinceLastShot = 0;
            }
        }

        // This function lets us draw debug shapes in the scene, even while the game is not running.
        private void OnDrawGizmos()
        {
            // Draw a red circle with a radius equal to the range of the tower (this shows us how far it can shoot).
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        // This function returns the enemy that is closest to this tower.
        private GameObject GetClosestEnemy()
        {
            // This "FindObjectsOfType()" function is VERY slow, although it is a easy-ish to understand implementation for the desired result.
            // it would be great to find a more efficient way to do this

            //thoughts
            //maybe swap FindObjectsOfType() with a public property of the type of enemy that way you set it in editor
            //and it isn't being run over and over
            List<EnemyController> enemies = new List<EnemyController>(FindObjectsOfType<EnemyController>());

            // If there are no enemies...
            if (enemies.Count == 0)
            {
                // ... don't return an enemy (there isn't any!) and exit the function.
                return null;
            }

            // Sort the enemies by distance to this tower.
            enemies.Sort(((enemy1, enemy2) =>
            {
                float enemy1Distance = Vector2.Distance(enemy1.transform.position, transform.position);
                float enemy2Distance = Vector2.Distance(enemy2.transform.position, transform.position);
                return enemy1Distance.CompareTo(enemy2Distance);
            }));

            // Return the first enemy in the list (which is the closest since we sorted it).
            return enemies[0].gameObject;
        }
    }
}

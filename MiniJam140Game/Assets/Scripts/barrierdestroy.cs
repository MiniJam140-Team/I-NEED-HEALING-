using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class barrierdestroy : MonoBehaviour
    {

        public int barrierHealth;
        public List<Collision2D> detectedColliders = new List<Collision2D>();
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Barrier")
            {
                detectedColliders.Add(collision);
                Destroy(collision.gameObject);
            }

            if(collision.gameObject.tag == "Barrier")
            {
                detectedColliders.Add(collision);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Barrier")
            {
                detectedColliders.Remove(collision);
            }
        }
    }
}
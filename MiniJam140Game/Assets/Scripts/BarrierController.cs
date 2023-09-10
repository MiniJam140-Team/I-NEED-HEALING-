using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    //public float barrierHealth = 100;
    SpriteRenderer barrierRenderer;
    GameObject barrier;
    //each time brusier hits barrier decrement by damage amount

    // Start is called before the first frame update
    void Start()
    {
        barrierRenderer = GetComponent<SpriteRenderer>();
        barrier = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void TakeDamage()
    {
        //barrierHealth -= damage;
        //set sprites color to red and back to normal
        Destroy(gameObject);
    }
}

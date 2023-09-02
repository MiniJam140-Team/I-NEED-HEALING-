using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public int barrierHealth;
    //each time brusier hits barrier decrement by damage amount

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TakeDamage(int damage)
    {
        barrierHealth -= damage;
        if(barrierHealth <= 0 )
        {
            Destroy(gameObject);
        }
    }
}

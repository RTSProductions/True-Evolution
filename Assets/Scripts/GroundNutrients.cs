using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundNutrients : MonoBehaviour
{
    public float nutrients = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Consume(float amount)
    {
        if (amount > 1 || nutrients <= 0)
        {
            return;
        }
        nutrients -= amount;
    }

    public void Replenish(float amount)
    {
        if (amount <= 0 || amount + nutrients > 1)
        {
            return;
        }
        nutrients += amount;
    }
}

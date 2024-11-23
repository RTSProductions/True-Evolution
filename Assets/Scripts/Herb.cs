using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : MonoBehaviour
{
    public float lifeForce = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Eat(float consumptionAmount)
    {
        lifeForce -= consumptionAmount;
        transform.localScale = Vector3.one * lifeForce;
    }
}

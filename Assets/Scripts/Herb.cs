using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : MonoBehaviour
{
    public float lifeForce = 1;

    public float volume;

    float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Assign(float volume)
    {
        maxHealth = volume;
    }

    public void Eat(float consumptionAmount)
    {
        volume -= consumptionAmount;
        lifeForce = volume / maxHealth;
        transform.localScale = Vector3.one * lifeForce;
    }
}

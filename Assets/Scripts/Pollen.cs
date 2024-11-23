using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour
{
    public Plant father;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Seed>(out Seed seed))
        {
            seed.father = father;
            Debug.Log("Pollenated");
            Destroy(gameObject);
        }
    }
}

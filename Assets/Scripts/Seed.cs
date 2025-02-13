using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public Plant mother;
    public Plant father;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator WaitToPass()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(Random.Range(20, 180));
        transform.parent = null;
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        if (Random.value >= 0.99f)
        {
            PlantTree();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void PlantTree()
    {
        if (mother.genes.asexual)
        {
            GameObject plant = Instantiate(mother.gameObject, transform.position, Quaternion.identity);
            Plant plantPlant = plant.GetComponent<Plant>();
            plantPlant.genes.InheritGenes(mother.genes);
            plantPlant.Birth();
            Destroy(gameObject);
        }
        else if (father != null)
        {
            GameObject plant = Instantiate(mother.gameObject, transform.position, Quaternion.identity);
            Plant plantPlant = plant.GetComponent<Plant>();
            plantPlant.genes.InheritGenes(mother.genes, father.genes);
            plantPlant.Birth();
            Destroy(gameObject);
        }
    }
}

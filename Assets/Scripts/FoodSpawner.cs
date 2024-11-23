using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject food;

    public float spawnRadius = 50;

    public float foodDelay = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMoreFood());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    IEnumerator SpawnMoreFood()
    {

        yield return new WaitForSeconds(foodDelay);

        Vector3 spawnPoint = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)) + transform.position;

        Instantiate(food, spawnPoint, Quaternion.identity, transform);

        StartCoroutine(SpawnMoreFood());
    }
}

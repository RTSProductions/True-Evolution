using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    bool mature = false;

    [HideInInspector]
    public float maturity;

    public float spawnRadius = 5;

    public float weight = 1;


    // Start is called before the first frame update
    void Start()
    {
        spawnRadius /= weight;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mature)
        {
            Grow();
            return;
        }
    }

    IEnumerator Drop()
    {
        yield return new WaitForSeconds(Random.Range(60, 180));

        Fall();
    }

    void Fall()
    {
        if (transform.parent != null && transform.parent.TryGetComponent<Plant>(out Plant plant))
        {
            plant.currentSeedCount--;
        }
        Vector3 spawnPoint = new Vector3(Random.Range(-spawnRadius, spawnRadius), 1, Random.Range(-spawnRadius, spawnRadius)) + transform.position;
        spawnPoint = new Vector3(spawnPoint.x, 0, spawnPoint.z);
        transform.parent = null;
        transform.position = spawnPoint;
    }

    void Grow()
    {
        maturity = Mathf.Lerp(maturity, 2, 0.01f * Time.deltaTime);

        transform.localScale = Vector3.one * maturity;

        mature = maturity >= 1f;
        if (mature)
        {
            StartCoroutine(Drop());
        }
    }
}

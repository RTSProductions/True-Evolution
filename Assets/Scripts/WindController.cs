using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public Transform windDir;
    Vector3 windDirection;
    public float windStrength = 0.0001f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeWindSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        windDir.Rotate(new Vector3(0, Time.deltaTime, 0));
        windDirection = windDir.forward;
    }

    private void FixedUpdate()
    {
        Pollen[] pollens = FindObjectsOfType<Pollen>();

        foreach (Pollen pollen in pollens)
        {
            pollen.GetComponent<Rigidbody>().AddForce(windDirection * windStrength);
        }
    }

    IEnumerator ChangeWindSpeed()
    {
        yield return new WaitForSeconds(Random.Range(15, 180));

        windStrength += Random.Range(-0.02f, 0.02f);

        StartCoroutine(ChangeWindSpeed());
    }
}

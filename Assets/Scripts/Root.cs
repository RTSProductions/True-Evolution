using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RRoot : MonoBehaviour
{
    public Transform last;
    public List<Transform> next = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(last.position, transform.position);

        if (next != null)
        {
            for (int i = 0; i < next.Count; i++)
            {
                Gizmos.DrawLine(transform.position, next[i].position);
            }
        }
    }
}

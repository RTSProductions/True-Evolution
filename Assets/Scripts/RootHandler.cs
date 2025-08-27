using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RootHandler : MonoBehaviour
{
    public Plant plant;
    public List<Root> roots = new List<Root>();
    public List<Root> lastRoots = new List<Root>();

    public List<Vector3> takenSpots = new List<Vector3>();
    int recursed = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateRoots()
    {
        Root fatherRoot = new Root(transform.position, null);

        for (int i = 0; i < 5; i++)
        {
            float xDir = Random.Range(-1f, 1f);
            float zDir = Random.Range(-1f, 1f);
            Root root = new Root((new Vector3(Mathf.Round(xDir) * plant.genes.rootDistance, 0, Mathf.Round(zDir) * plant.genes.rootDistance)) + (new Vector3(transform.position.x, -1, transform.position.z)), fatherRoot);
            roots.Add(root);
            lastRoots.Add(root);
            takenSpots.Add(root.position);
        }
    }

    public void GrowRoots()
    {
        Debug.Log("growing");
        float resourcesAvailable = 0.5f;

        if (Random.value > 0.5f && recursed < plant.genes.rootRecursion)
        {
            List<Root> removeRoots = new List<Root>();
            int currentCount = lastRoots.Count;
            for (int i = 0; i < currentCount; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    float xDir = Random.Range(-1f, 1f);
                    float zDir = Random.Range(-1f, 1f);
                    Root root = new Root((new Vector3(Mathf.Round(xDir) * plant.genes.rootDistance, 0, Mathf.Round(zDir) * plant.genes.rootDistance)) + (new Vector3(lastRoots[i].position.x, -1, lastRoots[i].position.z)), null);
                    if (takenSpots.Contains(root.position))
                    {
                        Vector3 newPos = GetnNewPos(lastRoots[i].position, 0);
                        root.position = newPos;
                    }
                    root.last = lastRoots[i];
                    takenSpots.Add(root.position);
                    lastRoots[i].next.Add(root);
                    removeRoots.Add(lastRoots[i]);
                    roots.Add(root);
                    lastRoots.Add(root);
                    if (resourcesAvailable < .2f)
                    {
                        break;
                    }
                }
            }
            for (int i = 0; i < removeRoots.Count; i++)
            {
                lastRoots.Remove(removeRoots[i]);
            }
            plant.nutrientsAchieved -= ((2 * (float)currentCount) / 10);
        }
        else
        {
            List<Root> removeRoots = new List<Root>();
            int currentCount = lastRoots.Count;
            for (int i = 0; i < currentCount; i++)
            {
                float xDir = Random.Range(-1f, 1f);
                float zDir = Random.Range(-1f, 1f);
                Root root = new Root((new Vector3(Mathf.Round(xDir) * plant.genes.rootDistance, 0, Mathf.Round(zDir) * plant.genes.rootDistance)) + (new Vector3(lastRoots[i].position.x, -1, lastRoots[i].position.z)), null);
                root.last = lastRoots[i];
                if (takenSpots.Contains(root.position))
                {
                    Vector3 newPos = GetnNewPos(lastRoots[i].position, 0);
                    root.position = newPos;
                }
                lastRoots[i].next.Add(root);
                removeRoots.Add(lastRoots[i]);
                roots.Add(root);
                lastRoots.Add(root);
                takenSpots.Add(root.position);
                plant.nutrientsAchieved -= (2 / 10);
                resourcesAvailable -= (2 / 10);
                if (resourcesAvailable < .2f)
                {
                    break;
                }
            }
            for (int i = 0; i < removeRoots.Count; i++)
            {
                lastRoots.Remove(removeRoots[i]);
            }
        }
    }

    public Vector3 GetnNewPos(Vector3 relativeTo, int tries)
    {
        float xDir = Random.Range(-1f, 1f);
        float zDir = Random.Range(-1f, 1f);
        Vector3 newPos = new Vector3(Mathf.Round(xDir) * plant.genes.rootDistance, 0, Mathf.Round(zDir) * plant.genes.rootDistance) + (new Vector3(relativeTo.x, -1, relativeTo.z));
        if (tries >= 30)
        {
            return newPos;
        }
        if (takenSpots.Contains(newPos))
        {
            return GetnNewPos(relativeTo, tries + 1);
        }
        return newPos;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < roots.Count; i++)
        {
            Gizmos.DrawLine(roots[i].last.position, roots[i].position);

            // if (roots[i].next.Count > 0)
            // {
            //     for (int j = 0; j < roots[i].next.Count; j++)
            //     {
            //         Gizmos.DrawLine(transform.position, roots[i].next[j].position);
            //     }
            // }
        }
    }
}
[System.Serializable]
public class Root
{
    public Vector3 position;
    public Root last;
    public List<Root> next = new List<Root>();

    public Root(Vector3 position, Root last)
    {
        this.position = position;
        this.last = last;
    }

}

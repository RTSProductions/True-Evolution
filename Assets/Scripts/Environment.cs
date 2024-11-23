using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [Range(0, 50)]
    public float timeScale;

    public float geneSampleDelay = 30;

    float timeToNextSample = 0;

    public int Hunger = 0;

    public int Age = 0;

    public List<Animal> animals = new List<Animal>();

    public List<Generation> generations = new List<Generation>();

    public List<float> speed = new List<float>();

    public List<float> sensoryRadius = new List<float>();

    public List<float> reproductiveUrge = new List<float>();

    public List<float> weight = new List<float>();

    public List<int> population = new List<int>();

    public static float mutChance = 0.5f;
    public static float mutStrength = 0.5f;

    [Range(0, 1)]
    public float mutationChance = 0.5f;

    [Range(0, 1)]
    public float mutationStrength = 0.5f;

    public bool populationControl;
    public int maxPopulation = 300;

    public GameObject plantPrefab;
    public GameObject animalPrefab;

    public int plantCount;
    public int animalCount;
    public float spawnRadius;
    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        mutChance = mutationChance;
        SpawnInitialPopulations();
    }

    // Update is called once per frame
    void Update()
    {
        mutChance = mutationChance;
        mutStrength = mutationStrength;

        if (Time.timeScale != timeScale)
        {
            Time.timeScale = timeScale;
        }

        if (Time.time >= timeToNextSample)
        {
            float currentSpeed = 0;
            float currentSensoryRadius = 0;
            float currentReproductiveUrge = 0;
            float currentWeight = 0;

            foreach (Animal animal in animals)
            {
                currentSpeed += animal.genes.speed;
                currentSensoryRadius += animal.genes.sensoryRadius;
                currentReproductiveUrge += animal.genes.reproductiveUrge;
                currentWeight += animal.weight;
            }

            currentSpeed /= animals.Count;

            currentSensoryRadius /= animals.Count;

            currentReproductiveUrge /= animals.Count;

            currentWeight /= animals.Count;

            speed.Add(currentSpeed);

            sensoryRadius.Add(currentSensoryRadius);

            reproductiveUrge.Add(currentReproductiveUrge);

            weight.Add(currentWeight);

            population.Add(animals.Count);

            timeToNextSample = Time.time + geneSampleDelay;
        }

        if (populationControl && animals.Count >= maxPopulation)
        {
            int destroy = Random.Range(50, maxPopulation - 50);
            int start = animals.Count - destroy;

            for (int i = start; i < animals.Count; i++)
            {
                animals[i].Die(CauseOfDeath.populationControl);
            }
        }
    }

    void SpawnInitialPopulations()
    {
        for (int i = 0; i < animalCount; i++)
        {
            Vector3 point = newSpawnPoint();
            point = new Vector3(point.x, 4, point.z);
            GameObject animal = Instantiate(animalPrefab, point, Quaternion.identity);
            animals.Add(animal.GetComponent<Animal>());
        }
        for (int i = 0; i < plantCount; i++)
        {
            Vector3 point = newSpawnPoint();
            GameObject plant = Instantiate(plantPrefab, point, Quaternion.identity);
        }
    }

    public void RegisterDeath(Animal animal, CauseOfDeath cause)
    {
        if (animals.Contains(animal))
        {
            animals.Remove(animal);
        }

        switch (cause)
        {
            case CauseOfDeath.age:
                Age++;
                Debug.Log(animal.gameObject.name + " died from old age");
                break;
            case CauseOfDeath.starvation:
                Hunger++;
                Debug.Log(animal.gameObject.name + " died from starvation");
                break;
            case CauseOfDeath.populationControl:
                Hunger++;
                Debug.Log(animal.gameObject.name + " died from population control");
                break;
        }
        generations[animal.generation].currentPopulation -= 1;
        if (generations[animal.generation].currentPopulation <= 0)
        {
            generations[animal.generation].endTime = Time.time;
        }
        if (animals.Count <= 0)
        {
            DataSaver.SaveDataCollected(this);
        }
        Destroy(animal.gameObject);
    }

    public void RegisterBirth(Animal animal, int generationIndex)
    {
        if (generations.Count < generationIndex + 1)
        {
            Generation generation = new Generation();

            generation.startTime = Time.time;

            generation.genIndex = generationIndex;

            generation.name = "" + generation.genIndex;

            generation.totalPopulation = 1;

            generation.currentPopulation = 1;

            generations.Add(generation);
        }
        else
        {
            generations[generationIndex].totalPopulation++;
            generations[generationIndex].currentPopulation++;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    Vector3 newSpawnPoint()
    {
        float randomZ = Random.Range(-spawnRadius, spawnRadius);
        float randomX = Random.Range(-spawnRadius, spawnRadius);

        RaycastHit hit;

        Vector3 spawnPoint = new Vector3(randomX, 1, randomZ);
        if (Physics.Raycast(spawnPoint, -transform.up, out hit, ground))
        {
            return spawnPoint;
        }
        else
        {
            return newSpawnPoint();
        }
    }
}
[System.Serializable]
public class Generation
{
    public string name;
    public int genIndex;
    public float startTime;
    public float endTime;
    public int totalPopulation;
    public int currentPopulation;
}

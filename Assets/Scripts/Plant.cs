using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public PlantGenes genes;

    public GameObject Stem;

    public GameObject apendagePrefab;

    public GameObject SeedPrefab;

    public GameObject PollenPrefab;

    List<Transform> spawnPoints = new List<Transform>();

    public List<GameObject> apendages = new List<GameObject>();

    bool mature = false;

    public float maturity;

    int maxSeeds;

    float timeToDeathByAge;

    public int currentSeedCount;

    bool waitingToGrowSeeds = false;

    float stemVolume;

    float nutrientRequirement;

    float nutrientsAchieved = 0f;

    List<Root> roots = new List<Root>();
    List<Root> lastRoots = new List<Root>();

    int recursed = 0;

    Environment environment;


    // Start is called before the first frame update
    void Start()
    {
        float old = maturity;
        Birth();
        maturity = old;
        genes.male = Random.value > 0.5f;
        if (environment == null)
        {
            environment = FindObjectOfType<Environment>();
        }
    }

    public void Birth()
    {
        maturity = 0;
        mature = false;
        genes.seedWeight = genes.CalculateSeedWeight();

        Material stemMat = new Material(genes.stem);
        stemMat.color = Color.Lerp(genes.apendageColor, genes.stemColor, genes.stemRoughness);
        Stem.GetComponent<MeshRenderer>().sharedMaterial = stemMat;

        Stem.transform.localScale = new Vector3(genes.stemThickness, genes.height, genes.stemThickness);
        Stem.transform.position = new Vector3(Stem.transform.position.x, genes.height, Stem.transform.position.z);

        stemVolume = 3.14f * Mathf.Pow(genes.stemThickness, 2) * genes.height;

        nutrientRequirement = (stemVolume / 10) + ((float)genes.apendageCount / 10);

        MakeApendages();

        GenerateRoots();
    }

    void MakeApendages()
    {
        int j = 0;
        for (int i = 0; i < genes.apendageCount; i++)
        {
            float y = Random.Range(genes.apenageThreshhold, genes.height * 2);
            if (y <= genes.apenageThreshhold)
            {
                Debug.Log("Erm what the flip");
            }
            float randomRot = Random.Range(0.0f, 360.0f);
            float size = Mathf.MoveTowards(genes.lowerApendageSize, genes.upperApendageSize, (y - genes.apenageThreshhold) / (genes.height/3.5f));

            Material apendageMat = new Material(genes.apendage);
            apendageMat.color = genes.apendageColor;

            Quaternion newRot = Quaternion.Euler(new Vector3(0, randomRot, 0));

            GameObject apendage = Instantiate(apendagePrefab, new Vector3(transform.position.x, y, transform.position.z), Quaternion.identity, transform);

            apendage.GetComponentInChildren<MeshFilter>().transform.localScale = apendage.GetComponentInChildren<MeshFilter>().transform.localScale * size;

            apendage.GetComponentInChildren<MeshFilter>().transform.position = new Vector3(apendage.transform.position.x, apendage.transform.position.y, apendage.transform.position.z + ((genes.stemThickness / 2) + 0f) + (apendage.GetComponentInChildren<MeshFilter>().transform.localScale.z / 2)) ;

            apendage.transform.rotation = newRot;

            MeshRenderer[] apendageRenderers = apendage.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer apend in apendageRenderers)
            {
                apend.sharedMaterial = apendageMat;
            }

            apendages.Add(apendage);

            if (Random.value < .1f && j < genes.seedCount)
            {
                Transform newTransform = new GameObject("Seed point " + j).transform;
                newTransform.position = apendage.GetComponentInChildren<MeshFilter>().transform.position;
                newTransform.parent = transform;
                spawnPoints.Add(newTransform);
                j++;
            }
        }
        maxSeeds = j;
    }

    void GenerateRoots()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject root = new GameObject("Root");
            Root rootComp = root.AddComponent<Root>();
            float xDir = Random.Range(-1f, 1f);
            float zDir = Random.Range(-1f, 1f);
            root.transform.position = (new Vector3(Mathf.Round(xDir), 0, Mathf.Round(zDir)) * genes.rootDistance) + (new Vector3(transform.position.x, -1, transform.position.z));
            //root.transform.parent = transform;
            rootComp.last = transform;
            roots.Add(rootComp);
            lastRoots.Add(rootComp);
        }
    }

    void GrowRoots()
    {
        float resourcesAvailable = 0.5f;

        if (Random.value > 0.5f && recursed < genes.rootRecursion)
        {
            List<Root> removeRoots = new List<Root>();
            int currentCount = lastRoots.Count;
            for (int i = 0; i < currentCount; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    GameObject root = new GameObject("Root");
                    Root rootComp = root.AddComponent<Root>();
                    float xDir = Random.Range(-1f, 1f);
                    float zDir = Random.Range(-1f, 1f);
                    root.transform.position = (new Vector3(Mathf.Round(xDir), 0, Mathf.Round(zDir)) * genes.rootDistance) + lastRoots[i].transform.position;
                    //root.transform.parent = transform;
                    rootComp.last = lastRoots[i].transform;
                    lastRoots[i].next.Add(root.transform);
                    removeRoots.Add(lastRoots[i]);
                    roots.Add(rootComp);
                    lastRoots.Add(rootComp);
                    //nutrientsAchieved -= (2 / 10);
                    //resourcesAvailable -= (2 / 10);
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
            nutrientsAchieved -= ((2 * (float)currentCount) / 10);
        }
        else
        {
            List<Root> removeRoots = new List<Root>();
            int currentCount = lastRoots.Count;
            for (int i = 0; i < currentCount; i++)
            {
                GameObject root = new GameObject("Root");
                Root rootComp = root.AddComponent<Root>();
                float xDir = Random.Range(-1, 1);
                float zDir = Random.Range(-1, 1);
                root.transform.position = (new Vector3(xDir, 0, zDir) * genes.rootDistance) + lastRoots[i].transform.position;
                //root.transform.parent = transform;
                rootComp.last = lastRoots[i].transform;
                lastRoots[i].next.Add(root.transform);
                removeRoots.Add(lastRoots[i]);
                roots.Add(rootComp);
                lastRoots.Add(rootComp);
                nutrientsAchieved -= (2 / 10);
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

    float GetNutrients(bool toGrowMoreRoots)
    {
        float amountNeeded = nutrientRequirement - nutrientsAchieved;
        float amountAquired = 0;
        int outOfUse = 0;


        for (int i = 0; i < lastRoots.Count; i++)
        {
            if (environment.AccessDeposit(lastRoots[i].transform.position) > 0)
            {
                if (environment.AccessDeposit(lastRoots[i].transform.position) >= 0.15f * Time.deltaTime)
                {
                    environment.DepleteDeposit(lastRoots[i].transform.position, 0.15f * Time.deltaTime);
                    amountAquired += 0.15f * Time.deltaTime;
                }
                else
                {
                    float whatsLeft = environment.AccessDeposit(lastRoots[i].transform.position);
                    environment.DepleteDeposit(lastRoots[i].transform.position, whatsLeft);
                    amountAquired += whatsLeft;
                    outOfUse++;
                }
            }
            else
            {
                outOfUse++;
            }
        }
        amountNeeded -= amountAquired;
        if (amountNeeded >= .6)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (environment.AccessDeposit(roots[i].transform.position) > 0)
                {
                    if (environment.AccessDeposit(roots[i].transform.position) >= 0.15f * Time.deltaTime)
                    {
                        environment.DepleteDeposit(roots[i].transform.position, 0.15f * Time.deltaTime);
                        amountAquired += 0.15f * Time.deltaTime;
                    }
                    else
                    {
                        float whatsLeft = environment.AccessDeposit(roots[i].transform.position);
                        environment.DepleteDeposit(roots[i].transform.position, whatsLeft);
                        amountAquired += whatsLeft;
                    }
                }
            }
        }


        if (toGrowMoreRoots == false && (float)outOfUse / (float)lastRoots.Count >= 0.5)
        {
            GrowRoots();
        }

        return amountAquired;
    }

    // Update is called once per frame
    void Update()
    {
        if (nutrientsAchieved < nutrientRequirement)
        {
            nutrientsAchieved += GetNutrients(false);
        }

        if (!mature)
        {
            Grow();
            return;
        }
        else if (!waitingToGrowSeeds)
        {
            if (genes.asexual || !genes.male)
            {
                StartCoroutine(WaitGrowFruit());
            }
            else
            {
                StartCoroutine(WaitGrowPollen());
            }
        }
        if (mature)
            timeToDeathByAge += Time.deltaTime * 1 / 5000;

        if (timeToDeathByAge >= 1)
        {
            Destroy(gameObject);
        }

    }

    void Grow()
    {
        //if (maturity * (nutrientsAchieved / nutrientRequirement) < transform.localScale.x)
        //{
        //    return;
        //}

        maturity = Mathf.Lerp(maturity, 2, 0.001f * Time.deltaTime);

        transform.localScale = Vector3.one * maturity;

        mature = maturity >= 1f;
        if (mature)
        {
            nutrientRequirement = 0;
            nutrientsAchieved = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                roots[i].transform.parent = transform;
            }
        }
    }


    IEnumerator WaitGrowFruit()
    {
        waitingToGrowSeeds = true;
        yield return new WaitForSeconds(Random.Range(60, 180));
        if (currentSeedCount < maxSeeds)
        {
            StartCoroutine(GrowFruit());
        }
        waitingToGrowSeeds = false;
        
    }

    IEnumerator GrowFruit()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject fruit = Instantiate(SeedPrefab, spawnPoint);
            Material seedMat = genes.seed;
            seedMat.color = Color.Lerp(genes.nutrientPoor, genes.nutrientRich, genes.seedNutrientValue);
            fruit.GetComponent<MeshRenderer>().sharedMaterial = seedMat;
            fruit.transform.localScale = new Vector3(genes.seedThickness, genes.seedLength, genes.seedThickness);
            fruit.GetComponent<Seed>().mother = this;
            fruit.GetComponent<Fruit>().weight = genes.seedWeight;
            fruit.transform.parent = transform;
            currentSeedCount++;
            if (currentSeedCount >= maxSeeds)
            {
                break;
            }
            yield return new WaitForSeconds(Random.Range(30, 180));
        }
    }

    IEnumerator WaitGrowPollen()
    {
        waitingToGrowSeeds = true;
        yield return new WaitForSeconds(Random.Range(60, 180));
        if (currentSeedCount < maxSeeds)
        {
            GrowPollen();
        }
        waitingToGrowSeeds = false;

    }

    void GrowPollen()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            for (int i = 0; i < genes.pollenAmount; i++)
            {
                GameObject pollen = Instantiate(PollenPrefab, spawnPoint);
                pollen.GetComponent<Rigidbody>().mass = genes.pollenWeight;
                pollen.GetComponent<Pollen>().father = this;
                pollen.transform.parent = null;
                pollen.GetComponent<Rigidbody>().AddExplosionForce(30, spawnPoint.position, 10);
                Destroy(pollen.gameObject, Random.Range(30, 180));
            }
        }
    }
}

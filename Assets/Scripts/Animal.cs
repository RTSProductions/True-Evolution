using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Genes genes;

    public State state;

    public float weight;

    public float timeToDeathByAge = 0;

    public float timeToDeathByHunger = 0;

    public float timeToMaturity = 0;

    public float timeToReurge = 0;

    public int generation = 0;

    public int speciesIndex = 0;

    public int ancestorIndex = 0;

    public LayerMask possibleSensory;

    float energyCost;

    float veiwAngle;

    public Rigidbody rb;

    public Transform head;

    public Transform neck;

    public Transform Body;

    public Transform RFrontLeg;

    public Transform LFrontLeg;

    public Transform RBackLeg;

    public Transform LBackLeg;

    public Transform headAttachmentPoint;

    public Transform neckAttachmentPoint;

    public Transform RFrontLegAttachmentPoint;

    public Transform LFrontLegAttachmentPoint;

    public Transform RBackLegAttachmentPoint;

    public Transform LBackLegAttachmentPoint;

    bool foundSMT = false;

    Vector3 thingFound = Vector3.zero;

    List<Animal> UnimpressedFemales = new List<Animal>();

    Animal possibleMate;

    Food possibleFood;

    Environment environment;

    // Start is called before the first frame update
    void Start()
    {
        genes.isMale = Random.value > 0.5f;

        weight = genes.CalculateWeight();


        StartCoroutine(ChangeVeiwAngle());

        if (genes.isMale)
        {
            UnimpressedFemales = new List<Animal>();
        }
        if (environment == null)
        {
            environment = FindObjectOfType<Environment>();
        }
        if (!environment.animals.Contains(this))
        {
            environment.animals.Add(this);
        }
        ArrangeParts();

    }

    // Update is called once per frame
    void Update()
    {
        if (runInEditMode)
        {
            ArrangeParts();
        }
        timeToMaturity += Time.deltaTime * 1 / 200;

        if (timeToMaturity >= 1)
            timeToDeathByAge += Time.deltaTime * 1 / 1000;

        timeToDeathByHunger += Time.deltaTime / ((energyCost * 3.50877192982f) * 2);

        if (timeToDeathByAge >= 1)
        {
            Die(CauseOfDeath.age);
        }
        else if (timeToDeathByHunger >= 1)
        {
            Die(CauseOfDeath.starvation);
        }

        Action();

        if (environment == null)
        {
            environment = FindObjectOfType<Environment>();
        }
        //if (!environment.animals.Contains(this))
        //{
        //    environment.animals.Add(this);
        //}
    }

    private void FixedUpdate()
    {
        Sense();
        Move();
    }

    void Move()
    {
        energyCost = Mathf.Pow(weight, 3) + Mathf.Pow(genes.speed, 2) + genes.sensoryRadius;


        Vector3 moveAmount;
        Vector3 movementDir = transform.forward * 2;
        moveAmount = movementDir * (genes.speed / weight);
        rb.linearVelocity = (moveAmount * Time.fixedDeltaTime * 30) + new Vector3(0, rb.linearVelocity.y, 0);

        if (foundSMT == false)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, veiwAngle, 0)), Time.deltaTime);
        else
            FaceTarget(thingFound);
    }

    void Action()
    {
        float distance = Vector3.Distance(transform.position, new Vector3(thingFound.x, transform.position.y, thingFound.z));
        switch (state)
        {
            case State.exploring:
                break;
            case State.lookingForMate:
                if (distance <= 5)
                {
                    if (possibleMate.isAttracted(this))
                    {
                        //Debug.Log("" + (generation + 1));
                        UnimpressedFemales.Add(possibleMate);
                        StartCoroutine(ForgetHer(possibleMate));
                        foundSMT = false;
                        state = State.exploring;
                        possibleMate = null;
                        timeToReurge = genes.reproductiveStaminaRegenerationTime + Time.time;
                    }
                    else
                    {
                        UnimpressedFemales.Add(possibleMate);
                        StartCoroutine(ForgetHer(possibleMate));
                        foundSMT = false;
                        state = State.exploring;
                        possibleMate = null;
                    }
                }
                break;
            case State.lookingForFood:
                if (distance <= 3)
                {
                    if (possibleFood == null)
                    {
                        state = State.exploring;
                        break;
                    }
                    if (possibleFood.TryGetComponent<Seed>(out Seed seed))
                    {
                        StartCoroutine(seed.WaitToPass());
                        timeToDeathByHunger = (1 -seed.mother.genes.seedNutrientValue) * seed.GetComponent<Fruit>().maturity;
                        if (seed.transform.parent != null && seed.transform.parent.TryGetComponent<Plant>(out Plant plant))
                        {
                            plant.currentSeedCount--;
                        }
                        seed.transform.parent = transform;
                        Destroy(possibleFood.GetComponent<Food>());
                        possibleFood.GetComponent<MeshRenderer>().enabled = false;
                        possibleFood.GetComponent<Collider>().enabled = false;
                        possibleFood = null;
                    }
                    else
                    {
                        Destroy(possibleFood.gameObject);
                        timeToDeathByHunger = 0;
                        Debug.Log("What the fuck");
                    }
                    state = State.exploring;
                    Debug.Log("no crumbs");
                }
                break;
        }
    }

    void Sense()
    {
        Collider[] objectsFound = Physics.OverlapSphere(transform.position, genes.sensoryRadius, possibleSensory);
        foundSMT = false;
        float bestDist = 100000;
        foreach (Collider obj in objectsFound)
        {
            if (obj.gameObject != gameObject && obj.transform.parent != transform)
            {
                float dist = Vector3.Distance(transform.position, obj.transform.position);
                Animal animal = obj.GetComponentInParent<Animal>();
                if (animal != null && timeToDeathByHunger <= genes.reproductiveUrge && animal.genes.isMale == false && genes.isMale == true && timeToDeathByAge >= 0.1f && !UnimpressedFemales.Contains(animal) && timeToMaturity >= 1 && animal.timeToMaturity >= 1 && Time.time >= timeToReurge && Time.time >= animal.timeToReurge && speciesIndex == animal.speciesIndex)
                {
                    if (dist < bestDist)
                    {
                        bestDist = dist;
                        foundSMT = true;
                        thingFound = obj.transform.position;
                        state = State.lookingForMate;
                        possibleMate = animal;
                    }
                }
                else if (obj.TryGetComponent<Food>(out Food food) && timeToDeathByHunger > genes.reproductiveUrge && state != State.lookingForMate && state != State.runningFromPredator)
                {
                    if (dist < bestDist)
                    {
                        bestDist = dist;
                        foundSMT = true;
                        thingFound = obj.transform.position;
                        possibleFood = food;
                        state = State.lookingForFood;
                    }
                }
            }
        }
    }

    IEnumerator ForgetHer(Animal her)
    {
        yield return new WaitForSeconds(30);

        UnimpressedFemales.Remove(her);
    }

    public bool isAttracted(Animal other)
    {
        float randVal = Random.value;

        if (timeToDeathByHunger <= genes.reproductiveUrge && timeToDeathByAge >= 0.1f && randVal >= 0.2f && timeToMaturity >= 1 && other.timeToMaturity >= 1)
        {
            Reproduce(other.genes);
        }

        return timeToDeathByHunger <= genes.reproductiveUrge && timeToDeathByAge >= 0.1f && randVal >= 0.2f;
    }

    void Reproduce(Genes father)
    {
        var child = Instantiate(gameObject, transform.position, Quaternion.identity);

        Animal childAnimal = child.GetComponent<Animal>();

        childAnimal.genes.InheritGenes(genes, father);

        childAnimal.ArrangeParts();

        childAnimal.timeToDeathByAge = 0;

        childAnimal.timeToMaturity = 0;

        childAnimal.timeToDeathByHunger = 0f;

        childAnimal.generation = generation + 1;

        int newSpeciesIndex = 0;

        Speciary speciary = environment.GetComponent<Speciary>();

        int tryFind = speciary.FindAnimalSpecies(childAnimal.genes, speciesIndex);

        if (tryFind == -1)
        {
            newSpeciesIndex = speciary.CreateAnimalSpecies(childAnimal.genes, ancestorIndex);
            childAnimal.ancestorIndex = speciesIndex;
        }
        else
        {
            newSpeciesIndex = tryFind;
        }

        childAnimal.speciesIndex = newSpeciesIndex;

        environment.RegisterBirth(childAnimal, childAnimal.generation);

        Debug.Log("" + (generation + 1));

        timeToReurge = genes.reproductiveStaminaRegenerationTime + Time.time;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, genes.sensoryRadius);
    }

    IEnumerator ChangeVeiwAngle()
    {
        yield return new WaitForSeconds(1);

        if (foundSMT == false)
            veiwAngle += Random.Range(-90, 90);
        StartCoroutine(ChangeVeiwAngle());
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void FaceAwayFromPredator(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-direction.x, 0, -direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void ArrangeParts()
    {
        Material material = new Material(genes.material);

        material.color = genes.color;

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
        {
            renderer.sharedMaterial = material;
        }

        head.localScale = new Vector3(genes.headWidth, genes.headHeight, genes.headLength);

        neck.localScale = new Vector3(genes.neckThickness, genes.neckLength, genes.neckThickness);

        Body.localScale = new Vector3(genes.bodyWidth, genes.bodyHeight, genes.bodyLength);

        RFrontLeg.localScale = new Vector3(genes.frontLegWidth, genes.frontLegHeight, genes.frontLegLength);

        LFrontLeg.localScale = new Vector3(genes.frontLegWidth, genes.frontLegHeight, genes.frontLegLength);

        RBackLeg.localScale = new Vector3(genes.backLegThickness, genes.backLegHeight, genes.backLegThickness);

        LBackLeg.localScale = new Vector3(genes.backLegThickness, genes.backLegHeight, genes.backLegThickness);

        neckAttachmentPoint.rotation = Quaternion.Euler(genes.neckAngle, 0, 0);

        RFrontLegAttachmentPoint.rotation = Quaternion.Euler(0, 0, -genes.frontLegAngle);

        LFrontLegAttachmentPoint.rotation = Quaternion.Euler(0, 0, genes.frontLegAngle);

        Body.rotation = Quaternion.Euler(genes.bodyAngle, 0, 0);

        neck.position = neckAttachmentPoint.position + new Vector3(0, genes.neckLength / 2, 0);
        neck.rotation = neckAttachmentPoint.rotation;

        head.position = headAttachmentPoint.position + new Vector3(0, genes.headHeight / 2, 0);

        RFrontLeg.position = RFrontLegAttachmentPoint.position - new Vector3(0, genes.frontLegHeight / 2, 0);
        RFrontLeg.rotation = RFrontLegAttachmentPoint.rotation;

        LFrontLeg.position = LFrontLegAttachmentPoint.position - new Vector3(0, genes.frontLegHeight / 2, 0);
        LFrontLeg.rotation = LFrontLegAttachmentPoint.rotation;

        RBackLeg.position = RBackLegAttachmentPoint.position - new Vector3(0, genes.backLegHeight / 2, 0);

        LBackLeg.position = LBackLegAttachmentPoint.position - new Vector3(0, genes.backLegHeight / 2, 0);
    }

    public void Die(CauseOfDeath cause)
    {
        environment.RegisterDeath(this, cause);
    }
}

public enum State
{
    exploring, lookingForMate, lookingForFood, runningFromPredator
}

public enum CauseOfDeath
{
    age, starvation, beingEaten, populationControl
}

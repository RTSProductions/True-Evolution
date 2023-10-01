using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Genes genes;

    public float weight;

    public float reproductionTime = 0;

    public float timeToDeath = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        genes.isMale = Random.value > 0.5f;

        weight = genes.CalculateWeight();

        ArrangeParts();

        StartCoroutine(ChangeVeiwAngle());

        reproductionTime = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        reproductionTime += Time.deltaTime * 1 / 200;

        timeToDeath += Time.deltaTime * 1 / 400;

        if (timeToDeath >= 1)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Sense();
        Move();
    }

    void Move()
    {
        //Ray forward = new Ray(transform.position + (Vector3.up * 0.8f), transform.forward);
        //Ray right = new Ray(transform.position + (Vector3.up * 0.8f), transform.right);
        //Ray left = new Ray(transform.position + (Vector3.up * 0.8f), -transform.right);
        //if (Physics.Raycast(forward, 0.32f * 2, obstacleAvoidance))
        //{
        //    if (!Physics.Raycast(right, 0.32f * 2, obstacleAvoidance))
        //    {
        //        Vector3 stirAmount = transform.right.normalized;
        //        //Vector3 movementDir = (transform.position + stirAmount - transform.position).normalized;
        //        moveAmount = stirAmount * speed;
        //    }
        //    else if (!Physics.Raycast(left, 0.32f * 2, obstacleAvoidance))
        //    {
        //        Vector3 stirAmount = -transform.right.normalized;
        //        //Vector3 movementDir = (transform.position + stirAmount - transform.position).normalized;
        //        moveAmount = stirAmount * speed;
        //    }
        //}

        energyCost = Mathf.Pow(weight, 3) + Mathf.Pow(genes.speed, 2) + genes.sensoryRadius;


        Vector3 moveAmount;
        Vector3 movementDir = transform.forward * 2;
        moveAmount = movementDir * (genes.speed / weight);
        rb.velocity = (moveAmount * Time.fixedDeltaTime * 30) + new Vector3(0, rb.velocity.y, 0);

        //Quaternion veiwAngleRot = Quaternion.Euler(new Vector3(0, veiwAngle, 0));

        //Quaternion newRot = new Quaternion(transform.rotation.x + veiwAngleRot.x, transform.rotation.y + veiwAngleRot.y, transform.rotation.z + veiwAngleRot.z, transform.rotation.w + veiwAngleRot.w);

        if (foundSMT == false)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, veiwAngle, 0)), Time.deltaTime);
        else
            FaceTarget(thingFound);
    }

    void Sense()
    {
        Collider[] objectsFound = Physics.OverlapSphere(transform.position, genes.sensoryRadius, possibleSensory);
        foundSMT = false;
        foreach (Collider obj in objectsFound)
        {
            if (obj.gameObject != gameObject && obj.transform.parent != transform)
            {
                Animal animal = obj.GetComponentInParent<Animal>();
                if (reproductionTime >= 0.5f && animal.genes.isMale != genes.isMale)
                {
                    foundSMT = true;
                    thingFound = obj.transform.position;
                    float dist = Vector3.Distance(transform.position, obj.transform.position);

                    //animal.ArrangeParts();

                    if (dist <= 2 && animal.isAttracted(this) && isAttracted(animal))
                    {
                        reproductionTime = 0;
                    }
                }
            }
        }
    }

    public bool isAttracted(Animal other)
    {
        if (reproductionTime >= 0.5f && !genes.isMale)
        {
            reproductionTime = 0;
            Reproduce(other.genes);
        }

        return reproductionTime >= 1;
    }

    void Reproduce(Genes father)
    {
        var child = Instantiate(gameObject, transform.position, Quaternion.identity);

        Animal childAnimal = child.GetComponent<Animal>();

        childAnimal.genes.InheritGenes(genes, father);

        childAnimal.ArrangeParts();

        childAnimal.timeToDeath = 0;
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
}

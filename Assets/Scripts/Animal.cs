using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Genes genes;

    public float weight;

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

    // Start is called before the first frame update
    void Start()
    {
        weight = genes.CalculateWeight();

        ArrangeParts();
    }

    // Update is called once per frame
    void Update()
    {
        
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

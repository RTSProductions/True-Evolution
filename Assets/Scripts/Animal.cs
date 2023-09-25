using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float headLength = 1;

    public float headWidth = 1;

    public float headHeight = 1;

    public float neckLength = 1;

    public float neckThickness = 0.5f;

    public float bodyLength = 4;

    public float bodyHeight = 1;

    public float bodyWidth = 2;

    public float frontLegWidth = 0.5f;

    public float frontLegLength = 0.5f;

    public float frontLegHeight = 1.5f;

    public float backLegThickness = 0.5f;

    public float backLegHeight = 1.5f;

    public float neckAngle = 0;

    public float frontLegAngle = 0;

    public float bodyAngle = 0;

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
        head.localScale = new Vector3(headWidth, headHeight, headLength);

        neck.localScale = new Vector3(neckThickness, neckLength, neckThickness);

        Body.localScale = new Vector3(bodyWidth, bodyHeight, bodyLength);

        RFrontLeg.localScale = new Vector3(frontLegWidth, frontLegHeight, frontLegLength);

        LFrontLeg.localScale = new Vector3(frontLegWidth, frontLegHeight, frontLegLength);

        RBackLeg.localScale = new Vector3(backLegThickness, backLegHeight, backLegThickness);

        LBackLeg.localScale = new Vector3(backLegThickness, backLegHeight, backLegThickness);

        neckAttachmentPoint.rotation = Quaternion.Euler(neckAngle, 0, 0);

        RFrontLegAttachmentPoint.rotation = Quaternion.Euler(0, 0, -frontLegAngle);

        LFrontLegAttachmentPoint.rotation = Quaternion.Euler(0, 0, frontLegAngle);

        Body.rotation = Quaternion.Euler(bodyAngle, 0, 0);

        neck.position = neckAttachmentPoint.position + new Vector3(0, neckLength / 2, 0);
        neck.rotation = neckAttachmentPoint.rotation;

        head.position = headAttachmentPoint.position + new Vector3(0, headHeight / 2, 0);

        RFrontLeg.position = RFrontLegAttachmentPoint.position - new Vector3(0, frontLegHeight / 2, 0);
        RFrontLeg.rotation = RFrontLegAttachmentPoint.rotation;

        LFrontLeg.position = LFrontLegAttachmentPoint.position - new Vector3(0, frontLegHeight / 2, 0);
        LFrontLeg.rotation = LFrontLegAttachmentPoint.rotation;

        RBackLeg.position = RBackLegAttachmentPoint.position - new Vector3(0, backLegHeight / 2, 0);

        LBackLeg.position = LBackLegAttachmentPoint.position - new Vector3(0, backLegHeight / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

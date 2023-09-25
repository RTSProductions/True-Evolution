using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Genes
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

    public Color color;

    public Material material;

    public float CalculateWeight()
    {
        float weight = 0;

        weight += ((headHeight * headWidth * headHeight) + (neckLength * neckThickness * neckThickness) + (bodyLength * bodyWidth * bodyHeight) + 2 * (frontLegHeight * frontLegLength * frontLegWidth) + 2 * (backLegHeight * backLegThickness * backLegThickness)) / 10.75f;

        return weight;
    }
}

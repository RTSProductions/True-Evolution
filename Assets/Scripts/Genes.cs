using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Genes
{
    public bool isMale = false;

    public float speed = 6;

    public float sensoryRadius = 10;

    public float reproductiveUrge = 0.5f;

    public float reproductiveStaminaRegenerationTime = 20f;

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


    public float SelectGene(float mother, float father)
    {
        float gene = 0;

        if (Random.value >= 0.5f)
        {
            gene = mother;
        }
        else
        {
            gene = father;
        }

        return gene;
    }

    public float MutateGene(float gene, float minChange, float maxChange)
    {
        float mutantGene = gene;

        if (Random.value <= Environment.mutChance)
        {
            mutantGene += (Environment.mutStrength*  Random.Range(minChange, maxChange));
        }

        return mutantGene;
    }

    public float ChangeGene(float gene, float minChange, float maxChange)
    {
        return Random.Range(minChange, maxChange);
    }

    public void InheritGenes(Genes motherGenes, Genes fatherGenes)
    {
        if (Random.value >= 0.9f)
        {
            color.r = ChangeGene(color.r, 1 / 255, 1);

            color.g = ChangeGene(color.g, 1 / 255, 1);

            color.b = ChangeGene(color.b, 1 / 255, 1);

            color.a = 255;
        }
        else
        {
            color = Color.Lerp(fatherGenes.color, motherGenes.color, 0.5f);
            color.a = 255;
        }

        float newSpeed = SelectGene(motherGenes.speed, fatherGenes.speed);

        newSpeed = MutateGene(newSpeed, -2.5f, 2.5f);

        if (newSpeed < 0.01f)
        {
            newSpeed = 0.01f;
        }

        speed = newSpeed;

        float newReproductiveStaminaRegenerationTime = SelectGene(motherGenes.reproductiveStaminaRegenerationTime, fatherGenes.reproductiveStaminaRegenerationTime);

        newReproductiveStaminaRegenerationTime = MutateGene(newReproductiveStaminaRegenerationTime, -1.5f, 1.5f);

        if (newReproductiveStaminaRegenerationTime < 1f)
        {
            newReproductiveStaminaRegenerationTime = 1f;
        }

        reproductiveStaminaRegenerationTime = newReproductiveStaminaRegenerationTime;

        float newSensoryRadius = SelectGene(motherGenes.sensoryRadius, fatherGenes.sensoryRadius);

        newSensoryRadius = MutateGene(newSensoryRadius, -4.5f, 4.5f);

        if (newSensoryRadius < 1f)
        {
            newSensoryRadius = 1f;
        }

        sensoryRadius = newSensoryRadius;

        float newReproductiveUrge = SelectGene(motherGenes.reproductiveUrge, fatherGenes.reproductiveUrge);

        newReproductiveUrge = MutateGene(newReproductiveUrge, -0.1f, 0.1f);

        if (newReproductiveUrge <= 0f)
        {
            newReproductiveUrge = 0.0001f;
        }

        reproductiveUrge = newReproductiveUrge;

        float newHeadLength = SelectGene(motherGenes.headLength, fatherGenes.headLength);

        newHeadLength = MutateGene(newHeadLength, -0.5f, 0.5f);

        if (newHeadLength < 0.01f)
        {
            newHeadLength = 0.01f;
        }

        headLength = newHeadLength;

        float newHeadWidth = SelectGene(motherGenes.headWidth, fatherGenes.headWidth);

        newHeadWidth = MutateGene(newHeadWidth, -0.5f, 0.5f);

        if (newHeadWidth < 0.01f)
        {
            newHeadWidth = 0.01f;
        }

        headWidth = newHeadWidth;

        float newHeadHeight = SelectGene(motherGenes.headHeight, fatherGenes.headHeight);

        newHeadHeight = MutateGene(newHeadHeight, -0.5f, 0.5f);

        if (newHeadHeight < 0.01f)
        {
            newHeadHeight = 0.01f;
        }

        headHeight = newHeadHeight;

        float newNeckLength = SelectGene(motherGenes.neckLength, fatherGenes.neckLength);

        newNeckLength = MutateGene(newNeckLength, -0.6f, 0.6f);

        if (newNeckLength < 0.005f)
        {
            newNeckLength = 0.005f;
        }

        neckLength = newNeckLength;

        float newNeckThickness = SelectGene(motherGenes.neckThickness, fatherGenes.neckThickness);

        newNeckThickness = MutateGene(newNeckThickness, -0.2f, 0.2f);

        if (newNeckThickness < 0.005f)
        {
            newNeckThickness = 0.005f;
        }

        neckThickness = newNeckThickness;

        float newBodyLength = SelectGene(motherGenes.bodyLength, fatherGenes.bodyLength);

        newBodyLength = MutateGene(newBodyLength, -0.5f, 0.5f);

        if (newBodyLength < 0.01f)
        {
            newBodyLength = 0.01f;
        }

        bodyLength = newBodyLength;

        float newBodyHeight = SelectGene(motherGenes.bodyHeight, fatherGenes.bodyHeight);

        newBodyHeight = MutateGene(newBodyHeight, -0.5f, 0.5f);

        if (newBodyHeight < 0.01f)
        {
            newBodyHeight = 0.01f;
        }

        bodyHeight = newBodyHeight;

        float newBodyWidth = SelectGene(motherGenes.bodyWidth, fatherGenes.bodyWidth);

        newBodyWidth = MutateGene(newBodyWidth, -0.5f, 0.5f);

        if (newBodyWidth < 0.01f)
        {
            newBodyWidth = 0.01f;
        }

        bodyWidth = newBodyWidth;

        float newFrontLegWidth = SelectGene(motherGenes.frontLegWidth, fatherGenes.frontLegWidth);

        newFrontLegWidth = MutateGene(newFrontLegWidth, -0.6f, 0.6f);

        if (newFrontLegWidth < 0.005f)
        {
            newFrontLegWidth = 0.005f;
        }

        frontLegWidth = newFrontLegWidth;

        float newFrontLegLength = SelectGene(motherGenes.frontLegLength, fatherGenes.frontLegLength);

        newFrontLegLength = MutateGene(newFrontLegLength, -0.6f, 0.6f);

        if (newFrontLegLength < 0.005f)
        {
            newFrontLegLength = 0.005f;
        }

        frontLegLength = newFrontLegLength;

        float newFrontLegHeight = SelectGene(motherGenes.frontLegHeight, fatherGenes.frontLegHeight);

        newFrontLegHeight = MutateGene(newFrontLegHeight, -1.5f, 1.5f);

        if (newFrontLegHeight < 0.005f)
        {
            newFrontLegHeight = 0.005f;
        }

        frontLegHeight = newFrontLegHeight;

        float newBackLegThickness = SelectGene(motherGenes.backLegThickness, fatherGenes.backLegThickness);

        newBackLegThickness = MutateGene(newBackLegThickness, -0.6f, 0.6f);

        if (newBackLegThickness < 0.005f)
        {
            newBackLegThickness = 0.005f;
        }

        backLegThickness = newBackLegThickness;

        float newBackLegHeight = SelectGene(motherGenes.backLegHeight, fatherGenes.backLegHeight);

        newBackLegHeight = MutateGene(newBackLegHeight, -1.5f, 1.5f);

        if (newBackLegHeight < 0.005f)
        {
            newBackLegHeight = 0.005f;
        }

        backLegHeight = newBackLegHeight;

        float newNeckAngle = SelectGene(motherGenes.neckAngle, fatherGenes.neckAngle);

        newNeckAngle = MutateGene(newNeckAngle, -15, 15);

        neckAngle = newNeckAngle;

        float newFrontLegAngle = SelectGene(motherGenes.frontLegAngle, fatherGenes.frontLegAngle);

        newFrontLegAngle = MutateGene(newFrontLegAngle, -15, 15);

        frontLegAngle = newFrontLegAngle;

        float newBodyAngle = SelectGene(motherGenes.bodyAngle, fatherGenes.bodyAngle);

        newBodyAngle = MutateGene(newBodyAngle, -15, 15);

        bodyAngle = newBodyAngle;
    }
}

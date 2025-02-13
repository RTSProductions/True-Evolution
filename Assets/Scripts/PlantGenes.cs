using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantGenes
{
    public bool asexual = true;
    public bool male = false;
    public float seedNutrientValue = 0.75f;
    public Color seedColor;
    public Color nutrientRich;
    public Color nutrientPoor;
    public float seedThickness = 2;
    public float seedLength = 2;
    public int seedCount = 5;
    public float seedRoughness = 0;
    public float stemRoughness = 1;
    public float apendageRoughness = 0;
    public float pollenWeight = 0.001f;
    public int pollenAmount = 3;
    public Color stemColor;
    public Color apendageColor;
    public float seedWeight;
    public float height = 10f;
    public float apenageThreshhold = 1f;
    public float stemThickness = 1;
    public int apendageCount = 30;
    public float upperApendageSize = 0.5f;
    public float lowerApendageSize = 5;
    public int rootRecursion = 1;
    public float rootDistance = 4.2f;
    public Material stem;
    public Material apendage;
    public Material seed;

    public float CalculateSeedWeight()
    {
        float baseWeight = (4 / 3) * 3.14f * 2 * 2 * 2;

        return ((4 / 3) * 3.14f * seedLength * seedThickness * seedLength) / baseWeight;
    }

    public float MutateGene(float gene, float minChange, float maxChange)
    {
        float mutantGene = gene;

        if (Random.value <= Environment.mutChance)
        {
            mutantGene += (Environment.mutStrength * Random.Range(minChange, maxChange));
        }

        return mutantGene;
    }

    public int MutateGene(int gene, int minChange, int maxChange)
    {
        int mutantGene = gene;

        if (Random.value <= Environment.mutChance)
        {
            mutantGene += (int)(Environment.mutStrength * Random.Range(minChange, maxChange));
        }

        return mutantGene;
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

    public int SelectGene(int mother, int father)
    {
        int gene = 0;

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

    public float DealWithAllThisBullshit(float motherGene, float fatherGene, float minimumChange, float maximumChange, float minimumValue)
    {
        float Gene = SelectGene(motherGene, fatherGene);

        Gene = MutateGene(Gene, minimumChange, maximumChange);

        if (Gene < minimumValue)
        {
            Gene = minimumValue;
        }

        return Gene;
    }

    public float DealWithAllThisBullshit(float parentGene, float minimumChange, float maximumChange, float minimumValue)
    {
        float Gene = parentGene;

        Gene = MutateGene(Gene, minimumChange, maximumChange);

        if (Gene < minimumValue)
        {
            Gene = minimumValue;
        }

        return Gene;
    }

    public int DealWithAllThisBullshit(int motherGene, int fatherGene, int minimumChange, int maximumChange, int minimumValue)
    {
        int Gene = SelectGene(motherGene, fatherGene);

        Gene = MutateGene(Gene, minimumChange, maximumChange);

        if (Gene < minimumValue)
        {
            Gene = minimumValue;
        }

        return Gene;
    }

    public int DealWithAllThisBullshit(int parentGene, int minimumChange, int maximumChange, int minimumValue)
    {
        int Gene = parentGene;

        Gene = MutateGene(Gene, minimumChange, maximumChange);

        if (Gene < minimumValue)
        {
            Gene = minimumValue;
        }

        return Gene;
    }

    public void InheritGenes(PlantGenes motherGenes, PlantGenes fatherGenes)
    {
        asexual = Random.value <= Environment.mutChance;

        seedNutrientValue = DealWithAllThisBullshit(motherGenes.seedNutrientValue, fatherGenes.seedNutrientValue, -0.20f, 0.20f, 0.01f);

        seedThickness = DealWithAllThisBullshit(motherGenes.seedThickness, fatherGenes.seedThickness, -0.5f, 0.5f, 0.1f);

        seedLength = DealWithAllThisBullshit(motherGenes.seedLength, fatherGenes.seedLength, -0.5f, 0.5f, 0.1f);

        seedRoughness = DealWithAllThisBullshit(motherGenes.seedRoughness, fatherGenes.seedRoughness, -1f, 1f, 1f);

        stemRoughness = DealWithAllThisBullshit(motherGenes.seedRoughness, fatherGenes.seedRoughness, -1f, 1f, 1f);

        apendageRoughness = DealWithAllThisBullshit(motherGenes.apendageRoughness, fatherGenes.apendageRoughness, -1f, 1f, 1f);

        pollenWeight = DealWithAllThisBullshit(motherGenes.pollenWeight, fatherGenes.pollenWeight, -0.01f, 0.01f, 0.001f);

        height = DealWithAllThisBullshit(motherGenes.height, fatherGenes.height, -2f, 2f, 0.5f);

        apenageThreshhold = DealWithAllThisBullshit(motherGenes.height, fatherGenes.height, -1f, 1f, 0.1f);

        stemThickness = DealWithAllThisBullshit(motherGenes.stemThickness, fatherGenes.stemThickness, -0.5f, 0.5f, 0.5f);

        upperApendageSize = DealWithAllThisBullshit(motherGenes.upperApendageSize, fatherGenes.upperApendageSize, -0.5f, 0.5f, 0.1f);

        lowerApendageSize = DealWithAllThisBullshit(motherGenes.lowerApendageSize, fatherGenes.lowerApendageSize, -0.5f, 0.5f, 0.1f);

        apendageCount = DealWithAllThisBullshit(motherGenes.apendageCount, fatherGenes.apendageCount, -5, 5, 2);

        pollenAmount = DealWithAllThisBullshit(motherGenes.pollenAmount, fatherGenes.pollenAmount, -2, 2, 1);

        seedCount = DealWithAllThisBullshit(motherGenes.seedCount, fatherGenes.seedCount, -2, 2, 1);

        rootRecursion = DealWithAllThisBullshit(motherGenes.rootRecursion, fatherGenes.rootRecursion, -2, 2, 1);

        rootDistance = DealWithAllThisBullshit(motherGenes.rootDistance, fatherGenes.rootDistance, -1.5f, 1.5f, .2f);
    }

    public void InheritGenes(PlantGenes parentGenes)
    {
        asexual = Random.value <= Environment.mutChance;
        if (asexual == false)
        {
            male = Random.value > 0.5f;
        }

        seedNutrientValue = DealWithAllThisBullshit(parentGenes.seedNutrientValue, -0.20f, 0.20f, 0.01f);

        seedThickness = DealWithAllThisBullshit(parentGenes.seedThickness, -0.5f, 0.5f, 0.1f);

        seedLength = DealWithAllThisBullshit(parentGenes.seedLength, -0.5f, 0.5f, 0.1f);

        seedRoughness = DealWithAllThisBullshit(parentGenes.seedRoughness, -1f, 1f, 1f);

        stemRoughness = DealWithAllThisBullshit(parentGenes.seedRoughness, -1f, 1f, 1f);

        apendageRoughness = DealWithAllThisBullshit(parentGenes.apendageRoughness, -1f, 1f, 1f);

        pollenWeight = DealWithAllThisBullshit(parentGenes.pollenWeight, -0.01f, 0.01f, 0.001f);

        height = DealWithAllThisBullshit(parentGenes.height, -2f, 2f, 0.5f);

        apenageThreshhold = DealWithAllThisBullshit(parentGenes.height, -1f, 1f, 0.1f);

        stemThickness = DealWithAllThisBullshit(parentGenes.stemThickness, -0.5f, 0.5f, 0.5f);

        upperApendageSize = DealWithAllThisBullshit(parentGenes.upperApendageSize, -0.5f, 0.5f, 0.1f);

        lowerApendageSize = DealWithAllThisBullshit(parentGenes.lowerApendageSize, -0.5f, 0.5f, 0.1f);

        apendageCount = DealWithAllThisBullshit(parentGenes.apendageCount, -5, 5, 2);

        pollenAmount = DealWithAllThisBullshit(parentGenes.pollenAmount, -2, 2, 1);

        seedCount = DealWithAllThisBullshit(parentGenes.seedCount, -2, 2, 1);

        rootRecursion = DealWithAllThisBullshit(parentGenes.rootRecursion, -2, 2, 1);

        rootDistance = DealWithAllThisBullshit(parentGenes.rootDistance, -1.5f, 1.5f, .2f);
    }
}

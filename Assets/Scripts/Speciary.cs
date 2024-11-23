using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speciary : MonoBehaviour
{
    public List<AnimalSpecies> animals = new List<AnimalSpecies>();

    // Start is called before the first frame update    primum
    void Start()
    {
        animals[0].ancestor = animals[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NameAnimalSpecies(AnimalSpecies species, float bodyWeight)
    {
        List<string> FirstNames = new List<string>();

        FirstNames.Add("Capitatus");
        FirstNames.Add("Ventralis");


        switch (species.speed)
        {
            case < 2:
                FirstNames.Add("Tardus");
                break;
            case < 6:
                FirstNames.Add("Lentus");
                break;
            case > 12:
                FirstNames.Add("Velox");
                break;
            case > 7:
                FirstNames.Add("Celer");
                break;

        }

        switch (species.sensoryRadius)
        {
            case <= 10:
                FirstNames.Add("Caecus");
                break;
            case > 10:
                FirstNames.Add("Sensorius");
                FirstNames.Add("Longivisus");
                FirstNames.Add("Odoratus");
                FirstNames.Add("Auditorius");
                FirstNames.Add("Tangibilis");
                break;

        }

        if (bodyWeight <= 0.25f)
        {
            FirstNames.Add("Minimus");
        }
        else if (bodyWeight > 0.25f && bodyWeight < 1)
        {
            FirstNames.Add("Parvus");
        }
        else if (bodyWeight >= 1 && bodyWeight <= 1.5f)
        {
            FirstNames.Add("Medius");
        }
        else if (bodyWeight > 1.5f && bodyWeight <= 3f)
        {
            FirstNames.Add("Magnus");
        }
        else if (bodyWeight > 4f)
        {
            FirstNames.Add("Giganteus");
        }


        if (species.headLength >= 1.5f)
        {
            FirstNames.Add("Longiceps");
        }

        if (species.headWidth >= 1.5f)
        {
            FirstNames.Add("Laticeps");
        }

        if (species.headHeight >= 1.5f)
        {
            FirstNames.Add("Alticeps");
        }
        else if (species.headHeight <= 0.7f)
        {
            FirstNames.Add("Breviceps");
        }

        if (species.bodyLength >= 4.5f)
        {
            FirstNames.Add("Longiventris");
        }

        if (species.bodyWidth >= 2.5f)
        {
            FirstNames.Add("Lativentris");
        }

        if (species.bodyHeight >= 1.5f)
        {
            FirstNames.Add("Altiventris");
        }
        else if (species.bodyHeight <= 0.7f)
        {
            FirstNames.Add("Planiventris");
        }

        species.firstName = FirstNames[Random.Range(0, FirstNames.Count - 1)];
        if (Random.value > .7f)
        {
            species.lastName = animals[species.ancestor.index].firstName;
        }
        else
        {
            species.lastName = animals[species.ancestor.index].lastName;
        }

        species.name = species.firstName + " " + species.lastName;
    }    

    bool CompareAnimal(Genes genes, AnimalSpecies species)
    {
        int overThreshHold = 0;
        float threshold = 0.60f;

        List<float> animalGenes = new List<float>
        {
            genes.speed,
            genes.sensoryRadius,
            genes.reproductiveUrge,
            genes.reproductiveStaminaRegenerationTime,
            genes.headLength,
            genes.headWidth,
            genes.headHeight,
            genes.neckLength,
            genes.neckThickness,
            genes.bodyLength,
            genes.bodyHeight,
            genes.bodyWidth,
            genes.frontLegWidth,
            genes.frontLegLength,
            genes.frontLegHeight,
            genes.backLegThickness,
            genes.backLegHeight,
            genes.neckAngle,
            genes.frontLegAngle,
            genes.bodyAngle
        };

        List<float> speciesGenes = new List<float>
        {
            species.speed,
            species.sensoryRadius,
            species.reproductiveUrge,
            species.reproductiveStaminaRegenerationTime,
            species.headLength,
            species.headWidth,
            species.headHeight,
            species.neckLength,
            species.neckThickness,
            species.bodyLength,
            species.bodyHeight,
            species.bodyWidth,
            species.frontLegWidth,
            species.frontLegLength,
            species.frontLegHeight,
            species.backLegThickness,
            species.backLegHeight,
            species.neckAngle,
            species.frontLegAngle,
            species.bodyAngle
        };

        for (int i = 0; i < animalGenes.Count; i++)
        {
            float animalGene = animalGenes[i];
            float speciesGene = speciesGenes[i];

            if (speciesGene > animalGene)
            {
                animalGene /= speciesGene;
                speciesGene /= speciesGene;
            }
            else if (speciesGene < animalGene)
            {
                animalGene /= animalGene;
                speciesGene /= animalGene;
            }

            if (Mathf.Abs(animalGene - speciesGene) >= threshold)
            {
                overThreshHold++;
            }
        }

        float amount = (float)overThreshHold / (float)animalGenes.Count;

        //Debug.Log("Amount: " + amount);

        if (amount == 0)
        {
            return false;
        }

        return amount >= 0.35f;
    }

    float CompareAnimalFloat(Genes genes, AnimalSpecies species)
    {
        int overThreshHold = 0;
        float threshold = 0.60f;

        List<float> animalGenes = new List<float>
        {
            genes.speed,
            genes.sensoryRadius,
            genes.reproductiveUrge,
            genes.reproductiveStaminaRegenerationTime,
            genes.headLength,
            genes.headWidth,
            genes.headHeight,
            genes.neckLength,
            genes.neckThickness,
            genes.bodyLength,
            genes.bodyHeight,
            genes.bodyWidth,
            genes.frontLegWidth,
            genes.frontLegLength,
            genes.frontLegHeight,
            genes.backLegThickness,
            genes.backLegHeight,
            genes.neckAngle,
            genes.frontLegAngle,
            genes.bodyAngle
        };

        List<float> speciesGenes = new List<float>
        {
            species.speed,
            species.sensoryRadius,
            species.reproductiveUrge,
            species.reproductiveStaminaRegenerationTime,
            species.headLength,
            species.headWidth,
            species.headHeight,
            species.neckLength,
            species.neckThickness,
            species.bodyLength,
            species.bodyHeight,
            species.bodyWidth,
            species.frontLegWidth,
            species.frontLegLength,
            species.frontLegHeight,
            species.backLegThickness,
            species.backLegHeight,
            species.neckAngle,
            species.frontLegAngle,
            species.bodyAngle
        };

        for (int i = 0; i < animalGenes.Count; i++)
        {
            float animalGene = animalGenes[i];
            float speciesGene = speciesGenes[i];

            if (speciesGene > animalGene)
            {
                animalGene /= speciesGene;
                speciesGene /= speciesGene;
            }
            else if (speciesGene < animalGene)
            {
                animalGene /= animalGene;
                speciesGene /= animalGene;
            }

            if (Mathf.Abs(animalGene - speciesGene) >= threshold)
            {
                overThreshHold++;
            }
        }

        float amount = (float)overThreshHold / (float)animalGenes.Count;

        //Debug.Log("Amount: " + amount);

        if (amount == 0)
        {
            return 0;
        }

        return amount;
    }

    public int FindAnimalSpecies(Genes genes, int ancestor)
    {
        bool lastOver = false;

        List<AnimalSpecies> viableSpecies = new List<AnimalSpecies>();

        if (animals.Count > 1)
        {

            for (int i = 0; i < animals.Count; i++)
            {
                if (animals[i].ancestor.index == ancestor && !viableSpecies.Contains(animals[i]))
                {
                    viableSpecies.Add(animals[i]);
                }
            }

            List<int> overIndicies = new List<int>();

            if (viableSpecies.Count == 0)
            {
                return -1;
            }

            for (int i = 0; i < viableSpecies.Count; i++)
            {
                if (CompareAnimal(genes, viableSpecies[i]))
                {
                    overIndicies.Add(i);
                }
            }

            List<AnimalSpecies> removeThese = new List<AnimalSpecies>();
            for (int i = 0; i < overIndicies.Count; i++)
            {
                removeThese.Add(viableSpecies[overIndicies[i]]);
            }

            for (int i = 0; i < removeThese.Count; i++)
            {
                viableSpecies.Remove(removeThese[i]);
            }
        }
        else
        {
            viableSpecies.Add(animals[0]);
            if (CompareAnimal(genes, animals[0]))
            {
                viableSpecies.RemoveAt(0);
            }
        }

        if (viableSpecies.Count == 1)
        {
            return viableSpecies[0].index;
        }
        else if (viableSpecies.Count > 1)
        {
            List<float> vs = new List<float>();
            for (int i = 0; i < viableSpecies.Count; i++)
            {
                vs.Add(CompareAnimalFloat(genes, viableSpecies[i]));
            }

            int bestInd = -1;
            float bestVal = 0;

            for (int i = 0; i < vs.Count; i++)
            {
                if (vs[i] > bestVal)
                {
                    bestVal = vs[i];
                    bestInd = i;
                }
            }

            if (bestInd == -1)
            {
                return viableSpecies[Random.Range(0, viableSpecies.Count - 1)].index;
            }
            else
            {
                return viableSpecies[bestInd].index;
            }

            return -2;
        }
        else if (viableSpecies.Count == 0)
        {
            Debug.Log("new");
            return -1;
        }

        Debug.Log("What the fuck again (species)");
        return -3;

    }

    public int CreateAnimalSpecies(Genes animal, int ancestor)
    {
        AnimalSpecies newSpecies = new AnimalSpecies();

        newSpecies.ancestor = animals[ancestor];


        newSpecies.index = animals.Count;

        newSpecies.speed = animal.speed;

        newSpecies.sensoryRadius = animal.sensoryRadius;

        newSpecies.reproductiveUrge = animal.reproductiveUrge;
        newSpecies.reproductiveStaminaRegenerationTime = animal.reproductiveStaminaRegenerationTime;

        newSpecies.headLength = animal.headLength;
        newSpecies.headWidth = animal.headWidth;
        newSpecies.headHeight = animal.headHeight;

        newSpecies.neckLength = animal.neckLength;
        newSpecies.neckThickness = animal.neckThickness;

        newSpecies.bodyLength = animal.bodyLength;
        newSpecies.bodyHeight = animal.bodyHeight;
        newSpecies.bodyWidth = animal.bodyWidth;

        newSpecies.frontLegWidth = animal.frontLegWidth;
        newSpecies.frontLegLength = animal.frontLegLength;
        newSpecies.frontLegHeight = animal.frontLegHeight;

        newSpecies.backLegThickness = animal.backLegThickness;
        newSpecies.backLegHeight = animal.backLegHeight;

        newSpecies.neckAngle = animal.neckAngle;
        newSpecies.frontLegAngle = animal.frontLegAngle;
        newSpecies.bodyAngle = animal.bodyAngle;

        newSpecies.color = animal.color;

        NameAnimalSpecies(newSpecies, animal.CalculateWeight());

        animals.Add(newSpecies);

        return newSpecies.index;
    }
}
[System.Serializable]
public class AnimalSpecies
{
    public string name;

    public string firstName;

    public string lastName;

    public AnimalSpecies ancestor = null;

    public int index;

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
}

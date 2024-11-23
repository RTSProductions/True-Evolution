using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSaver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SaveDataCollected(Environment environment)
    {
        string path = Application.dataPath + "/Data.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Simulation Data \n\n");
        }

        string contents = "Addition Date " + System.DateTime.Now + "\n";

        string speeds = "speeds overtime: \n";

        foreach (float speed in environment.speed)
        {
            speeds += "" + speed + "\n";
            speeds += "(" + (environment.speed.IndexOf(speed) * environment.geneSampleDelay) + ")\n";
        }

        string sensoryRadii = "sensory radii overtime: \n";

        foreach (float sensoryRadius in environment.sensoryRadius)
        {
            sensoryRadii += "" + sensoryRadius + "\n";
            sensoryRadii += "(" + (environment.speed.IndexOf(sensoryRadius) * environment.geneSampleDelay) + ")\n";
        }

        string reproductiveUrges = "reproductive urges overtime: \n";

        foreach (float reproductiveUrge in environment.reproductiveUrge)
        {
            reproductiveUrges += "" + reproductiveUrge + "\n";
            reproductiveUrges += "(" + (environment.speed.IndexOf(reproductiveUrge) * environment.geneSampleDelay) + ")\n";
        }

        string weights = "weights overtime: \n";

        foreach (float weight in environment.weight)
        {
            weights += "" + weight + "\n";
            weights += "(" + (environment.speed.IndexOf(weight) * environment.geneSampleDelay) + ")\n";
        }

        string populations = "population overtime: \n";

        foreach (float population in environment.population)
        {
            populations += "" + population + "\n";
            populations += "(" + (environment.speed.IndexOf(population) * environment.geneSampleDelay) + ")\n";
        }

        string deaths = "Total deaths: " + (environment.Hunger + environment.Age) + "\n" + "Death by hunger: " + environment.Hunger + "\n" + "Death by age: " + environment.Age + "\n";

        contents = contents + speeds + sensoryRadii + reproductiveUrges + weights + populations + deaths;

        File.AppendAllText(path, contents);
    }
}

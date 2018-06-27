using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotsManager : MonoBehaviour
{
    public static float ElapsedTime;

    public GameObject botPrefab;
    public Transform StartPoint;
    public int PopulationSize = 50;
    public float TrialTime = 5;

    private GUIStyle guiStyle;
    private List<GameObject> population;
    private int generation = 1;

    void Awake()
    {
        population = new List<GameObject>();
        guiStyle = new GUIStyle();
    }

    void Start()
    {
        for (var i = 0; i < PopulationSize; i++)
        {
            var instanceGO = InstantiateNewBot();
            instanceGO.GetComponent<BrainBehaviour>().Init();
            population.Add(instanceGO);
        }
    }

    private void OnGUI()
    {
        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.black;

        GUI.BeginGroup(new Rect(10, 10, 250, 150));
        GUI.Box(new Rect(0, 0, 140, 140), "Stats", guiStyle);
        GUI.Label(new Rect(10, 25, 200, 30), "Gen: " + generation, guiStyle);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time: {0:0:00}", ElapsedTime), guiStyle);
        GUI.Label(new Rect(10, 75, 200, 30), "Population: " + population.Count, guiStyle);
        GUI.EndGroup();
    }

    private GameObject InstantiateNewBot()
    {
        var startingPos = new Vector3(
            StartPoint.position.x + Random.Range(-2, 2),
            StartPoint.position.y,
            StartPoint.position.z + Random.Range(-2, 2)
        );

        return Instantiate(botPrefab, startingPos, transform.rotation);
    }

    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        var brain = InstantiateNewBot()
                   .GetComponent<BrainBehaviour>()
                   .Init();

        brain.Dna.Combine(
            parent1.GetComponent<BrainBehaviour>().Dna,
            parent2.GetComponent<BrainBehaviour>().Dna
        );

        return brain.gameObject;
    }

    private void BreedNewPopulation()
    {
        TrialTime += 2;
        var sortedList = population
                        .OrderBy(x => x.GetComponent<BrainBehaviour>().DistanceTravelled)
                        .ToList();

        population.Clear();

        var startIndex = Mathf.RoundToInt(sortedList.Count / 2 - 1);

        for (var i = startIndex; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        sortedList.ForEach(Destroy);

        generation++;
    }


    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime > TrialTime)
        {
            BreedNewPopulation();
            ElapsedTime = 0;
        }
    }
}
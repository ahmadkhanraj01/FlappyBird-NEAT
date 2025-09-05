using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class EvolutionManager : MonoBehaviour
{
    public static EvolutionManager Instance;

    [Header("Prefabs & Spawn")]
    public GameObject birdAIPrefab;   // assign BirdAI prefab (copy of Player)
    public Transform spawnPoint;

    [Header("Population")]
    public int populationSize = 100;
    public int inputCount = 4;
    public int hiddenCount = 6;
    public int outputCount = 1;

    [Header("GA parameters")]
    [Range(0f, 1f)] public float mutationRate = 0.05f;
    public float mutationStrength = 0.5f;
    public int eliteCount = 5;

    [Header("Training")]
    public float timeScale = 1f; // change to speed up
    public bool hideGraphicsWhileTraining = false;

    // internal
    private List<NeuralNetwork> population;
    private float[] fitnesses;
    private GameObject[] birdObjects;
    private BirdAI[] birdScripts;
    private int deadCount = 0;
    private int generation = 0;
    private float bestFitnessEver = 0f;
    private NeuralNetwork bestNetworkEver = null;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
        InitializePopulation();
        StartGeneration();
    }

    private void Update()
    {
        Time.timeScale = timeScale;
    }

    void InitializePopulation()
    {
        population = new List<NeuralNetwork>(populationSize);
        fitnesses = new float[populationSize];

        for (int i = 0; i < populationSize; i++)
        {
            population.Add(new NeuralNetwork(inputCount, hiddenCount, outputCount));
            fitnesses[i] = 0f;
        }
    }

    void StartGeneration()
    {
        generation++;
        deadCount = 0;

        // clear all existing pipes
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        foreach (var p in pipes) Destroy(p.gameObject);

        // spawn birds
        birdObjects = new GameObject[populationSize];
        birdScripts = new BirdAI[populationSize];
        for (int i = 0; i < populationSize; i++)
        {
            var go = Instantiate(birdAIPrefab, spawnPoint.position, Quaternion.identity);
            BirdAI ai = go.GetComponent<BirdAI>();
            ai.Initialize(population[i].Clone(), i); // clone to avoid shared refs
            birdObjects[i] = go;
            birdScripts[i] = ai;
        }

        // optionally hide graphics
        if (hideGraphicsWhileTraining)
        {
            Camera.main.enabled = false;
        }
        else
        {
            Camera.main.enabled = true;
        }
    }

    // Called when a BirdAI dies
    public void ReportDeath(int genomeIndex, int pipesPassed, float timeAlive)
    {
        deadCount++;
        // fitness formula; tune as needed
        float fitness = pipesPassed * 1000f + timeAlive;
        fitnesses[genomeIndex] = fitness;

        // track best
        if (fitness > bestFitnessEver)
        {
            bestFitnessEver = fitness;
            bestNetworkEver = population[genomeIndex].Clone();
            SaveBestBrain(); // optionally save automatically
        }

        if (deadCount >= populationSize)
        {
            EvolvePopulation();
            // cleanup active bird objects
            for (int i = 0; i < birdObjects.Length; i++)
                if (birdObjects[i] != null) Destroy(birdObjects[i]);

            StartGeneration();
        }
    }

    void EvolvePopulation()
    {
        // Create list of indices sorted by fitness desc
        List<int> indices = Enumerable.Range(0, populationSize).ToList();
        indices.Sort((a, b) => fitnesses[b].CompareTo(fitnesses[a]));

        List<NeuralNetwork> newPop = new List<NeuralNetwork>(populationSize);

        // Elitism: copy top N
        for (int e = 0; e < eliteCount; e++)
        {
            int idx = indices[e];
            newPop.Add(population[idx].Clone());
        }

        // Summed fitness for roulette selection (add small epsilon)
        float totalFitness = fitnesses.Sum() + 1e-6f;

        // Fill rest using roulette + crossover + mutate
        while (newPop.Count < populationSize)
        {
            NeuralNetwork parentA = RouletteSelect(totalFitness);
            NeuralNetwork parentB = RouletteSelect(totalFitness);
            NeuralNetwork child = NeuralNetwork.Crossover(parentA, parentB);
            child.Mutate(mutationRate, mutationStrength);
            newPop.Add(child);
        }

        population = newPop;
        // reset fitness array
        fitnesses = new float[populationSize];
        Debug.Log($"Generation {generation} - bestFitness {bestFitnessEver}");
    }

    NeuralNetwork RouletteSelect(float totalFitness)
    {
        // if all zero fitness, pick random
        if (totalFitness <= 1e-5f)
        {
            return population[Random.Range(0, populationSize)];
        }

        float r = Random.Range(0f, totalFitness);
        float accum = 0f;
        for (int i = 0; i < populationSize; i++)
        {
            accum += fitnesses[i];
            if (accum >= r)
                return population[i];
        }

        // fallback
        return population[Random.Range(0, populationSize)];
    }

    // Save best brain as JSON to Application.persistentDataPath
    void SaveBestBrain()
    {
        if (bestNetworkEver == null) return;
        string path = Path.Combine(Application.persistentDataPath, "bestBrain.json");
        // Simple manual serializable container
        BrainData data = new BrainData()
        {
            inputCount = bestNetworkEver.inputCount,
            hiddenCount = bestNetworkEver.hiddenCount,
            outputCount = bestNetworkEver.outputCount,
            weightsIH = bestNetworkEver.weightsIH,
            biasH = bestNetworkEver.biasH,
            weightsHO = bestNetworkEver.weightsHO,
            biasO = bestNetworkEver.biasO
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
        Debug.Log("Saved best brain to " + path);
    }

    [System.Serializable]
    private class BrainData
    {
        public int inputCount;
        public int hiddenCount;
        public int outputCount;
        public float[] weightsIH;
        public float[] biasH;
        public float[] weightsHO;
        public float[] biasO;
    }
    public int Generation => generation;
    public float BestFitness => bestFitnessEver;

    public int AliveCount
    {
        get
        {
            if (birdScripts == null) return 0;
            int alive = 0;
            foreach (var b in birdScripts)
            {
                if (b != null && b.isAlive) alive++;
            }
            return alive;
        }
    }
}

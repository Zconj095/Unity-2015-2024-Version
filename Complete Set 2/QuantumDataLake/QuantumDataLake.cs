using UnityEngine;
using System.Collections.Generic;

public class QuantumDataLake : MonoBehaviour
{
    // A simple Quantum Data Class to represent quantum states
    [System.Serializable]
    public class QuantumData
    {
        public string name;
        public float[] stateProbabilities; // Represents the probability of different states
        
        // Constructor
        public QuantumData(string name, int numStates)
        {
            this.name = name;
            this.stateProbabilities = new float[numStates];
            InitializeQuantumState();
        }

        // Initialize the quantum state with random probabilities (mimicking superposition)
        private void InitializeQuantumState()
        {
            float total = 0f;
            for (int i = 0; i < stateProbabilities.Length; i++)
            {
                stateProbabilities[i] = Random.Range(0f, 1f);
                total += stateProbabilities[i];
            }

            // Normalize the probabilities to sum to 1
            for (int i = 0; i < stateProbabilities.Length; i++)
            {
                stateProbabilities[i] /= total;
            }
        }

        // Simulate a quantum measurement (collapse to one state based on probabilities)
        public int Measure()
        {
            float rand = Random.Range(0f, 1f);
            float cumulativeProbability = 0f;
            for (int i = 0; i < stateProbabilities.Length; i++)
            {
                cumulativeProbability += stateProbabilities[i];
                if (rand <= cumulativeProbability)
                {
                    return i; // Return the state index
                }
            }
            return stateProbabilities.Length - 1; // Should never reach here if probabilities sum to 1
        }
    }

    public List<QuantumData> quantumDataLake = new List<QuantumData>();

    // Start is called before the first frame update
    void Start()
    {
        // Example of adding some quantum data to the lake
        quantumDataLake.Add(new QuantumData("QuantumState1", 4));
        quantumDataLake.Add(new QuantumData("QuantumState2", 3));
        
        // Perform quantum measurements
        PerformQuantumMeasurements();
    }

    // Perform a series of quantum measurements
    private void PerformQuantumMeasurements()
    {
        foreach (var quantumData in quantumDataLake)
        {
            int measuredState = quantumData.Measure();
            Debug.Log(quantumData.name + " collapsed to state: " + measuredState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Optionally, you could have the quantum data interact here
    }
}

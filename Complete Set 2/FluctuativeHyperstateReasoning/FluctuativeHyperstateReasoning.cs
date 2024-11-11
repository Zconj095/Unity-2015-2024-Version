using UnityEngine;
using System.Collections.Generic;

public class FluctuativeHyperstateReasoning : MonoBehaviour
{
    // Class to represent a hyperstate and its fluctuative probability
    [System.Serializable]
    public class Hyperstate
    {
        public string name;
        public float probabilityWeight; // Current probability weight

        // Constructor
        public Hyperstate(string name, float initialWeight)
        {
            this.name = name;
            this.probabilityWeight = initialWeight;
        }
    }

    public List<Hyperstate> hyperstates = new List<Hyperstate>();

    // Define fluctuation factors to apply to probabilities over time
    public float fluctuationRate = 0.05f;
    public float fluctuationFrequency = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize a few hyperstates
        hyperstates.Add(new Hyperstate("Correlation", 0.3f));
        hyperstates.Add(new Hyperstate("Blessing", 0.2f));
        hyperstates.Add(new Hyperstate("Event Trigger", 0.25f));
        hyperstates.Add(new Hyperstate("Sound Reasoning", 0.25f));

        // Begin fluctuation updates
        InvokeRepeating("FluctuateHyperstateProbabilities", 0, fluctuationFrequency);
    }

    // Apply fluctuations to each hyperstate's probability weight
    void FluctuateHyperstateProbabilities()
    {
        float totalWeight = 0f;
        
        foreach (var hyperstate in hyperstates)
        {
            // Apply fluctuation as a small random adjustment
            hyperstate.probabilityWeight += Random.Range(-fluctuationRate, fluctuationRate);
            hyperstate.probabilityWeight = Mathf.Clamp(hyperstate.probabilityWeight, 0.05f, 1f); // Keep in a reasonable range
            totalWeight += hyperstate.probabilityWeight;
        }

        // Normalize probabilities to sum to 1
        foreach (var hyperstate in hyperstates)
        {
            hyperstate.probabilityWeight /= totalWeight;
        }
    }

    // Simulate reasoning by selecting a hyperstate based on fluctuating weights
    public Hyperstate Reason()
    {
        float randomValue = Random.Range(0f, 1f);
        float cumulativeWeight = 0f;

        foreach (var hyperstate in hyperstates)
        {
            cumulativeWeight += hyperstate.probabilityWeight;
            if (randomValue <= cumulativeWeight)
            {
                Debug.Log("Reasoning led to decision: " + hyperstate.name);
                return hyperstate;
            }
        }

        return hyperstates[hyperstates.Count - 1]; // Fallback in case of rounding errors
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Press space to make a reasoning decision
        {
            Reason();
        }
    }
}

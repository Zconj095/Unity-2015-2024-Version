using UnityEngine;
using System.Collections.Generic;

public class MarkovFieldNode : MonoBehaviour
{
    public int State;
    public List<MarkovFieldNode> Neighbors = new List<MarkovFieldNode>();

    // Probability distribution for transitions
    public Dictionary<int, float> TransitionProbabilities = new Dictionary<int, float>();

    public void AddNeighbor(MarkovFieldNode neighbor)
    {
        if (!Neighbors.Contains(neighbor))
        {
            Neighbors.Add(neighbor);
        }
    }

    // Initialize with random transition probabilities (for demonstration)
	public void InitializeProbabilities(int numStates)
	{
		float total = 0;
		// Calculate the total sum first
		Dictionary<int, float> tempProbabilities = new Dictionary<int, float>();

		for (int i = 0; i < numStates; i++)
		{
			float probability = Random.Range(0.1f, 1f);
			tempProbabilities[i] = probability;
			total += probability;
		}

		// Normalize the probabilities by dividing by the total
		foreach (var kvp in tempProbabilities)
		{
			TransitionProbabilities[kvp.Key] = kvp.Value / total;
		}
	}


    public int GetNextState()
    {
        float randomValue = Random.value;
        float cumulativeProbability = 0;

        foreach (var kvp in TransitionProbabilities)
        {
            cumulativeProbability += kvp.Value;
            if (randomValue <= cumulativeProbability)
            {
                return kvp.Key;
            }
        }
        return State;
    }

    // Optional method to update color for visualization
    public void UpdateColor(int numStates)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.Lerp(Color.red, Color.blue, (float)State / (float)numStates);
    }
}

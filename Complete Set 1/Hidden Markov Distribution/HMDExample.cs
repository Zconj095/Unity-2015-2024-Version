using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HMDExample : MonoBehaviour
{
    private HiddenMarkovDistribution hmm;

    void Start()
    {
        // Initialize an HMM with 3 states and 2 possible observations (e.g., 0 and 1)
        hmm = new HiddenMarkovDistribution(3, 2);

        // Example observation sequence (e.g., [1, 0, 1, 0])
        int[] observations = new int[] { 1, 0, 1, 0 };

        // Use Viterbi algorithm to decode the most likely sequence of hidden states
        int[] mostLikelyStates = hmm.Viterbi(observations);

        // Convert the observation and state arrays to strings for logging
        string observationsStr = string.Join(", ", Array.ConvertAll(observations, i => i.ToString()));
        string mostLikelyStatesStr = string.Join(", ", Array.ConvertAll(mostLikelyStates, i => i.ToString()));

        // Output the results to the Unity console
        Debug.Log("Observation Sequence: " + observationsStr);
        Debug.Log("Most Likely Hidden States: " + mostLikelyStatesStr);
    }
}

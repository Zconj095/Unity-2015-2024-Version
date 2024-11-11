using System;
using System.Collections.Generic;
using UnityEngine;

public class HiddenMarkovDistribution : MonoBehaviour
{
    private int numStates;  // Number of hidden states
    private int numObservations;  // Number of possible observations

    private float[,] transitionProbabilities;  // Transition probabilities (states x states)
    private float[,] emissionProbabilities;    // Emission probabilities (states x observations)
    private float[] initialStateProbabilities; // Initial state probabilities

    // Constructor to initialize the HMM
    public HiddenMarkovDistribution(int numStates, int numObservations)
    {
        this.numStates = numStates;
        this.numObservations = numObservations;

        // Initialize probabilities with random values (for simplicity)
        transitionProbabilities = new float[numStates, numStates];
        emissionProbabilities = new float[numStates, numObservations];
        initialStateProbabilities = new float[numStates];

        // Populate with random values (these values should typically be provided based on the data)
        for (int i = 0; i < numStates; i++)
        {
            initialStateProbabilities[i] = 1.0f / numStates;
            for (int j = 0; j < numStates; j++)
                transitionProbabilities[i, j] = 1.0f / numStates;

            for (int k = 0; k < numObservations; k++)
                emissionProbabilities[i, k] = 1.0f / numObservations;
        }
    }

    // Viterbi Algorithm to decode the most likely state sequence
    public int[] Viterbi(int[] observations)
    {
        int numObservations = observations.Length;

        // Initialize dynamic programming matrices
        float[,] dp = new float[numStates, numObservations];
        int[,] backpointer = new int[numStates, numObservations];

        // Initialization step
        for (int state = 0; state < numStates; state++)
        {
            dp[state, 0] = initialStateProbabilities[state] * emissionProbabilities[state, observations[0]];
            backpointer[state, 0] = 0;  // No previous state for the first observation
        }

        // Recursion step (for each subsequent observation)
        for (int t = 1; t < numObservations; t++)
        {
            for (int currentState = 0; currentState < numStates; currentState++)
            {
                float maxProb = -1;
                int bestPrevState = 0;

                for (int prevState = 0; prevState < numStates; prevState++)
                {
                    float prob = dp[prevState, t - 1] * transitionProbabilities[prevState, currentState] * emissionProbabilities[currentState, observations[t]];
                    if (prob > maxProb)
                    {
                        maxProb = prob;
                        bestPrevState = prevState;
                    }
                }

                dp[currentState, t] = maxProb;
                backpointer[currentState, t] = bestPrevState;
            }
        }

        // Backtrack to find the most likely state sequence
        int[] mostLikelyStates = new int[numObservations];
        int lastState = 0;
        float maxFinalProb = -1;

        // Find the last state with the highest probability
        for (int state = 0; state < numStates; state++)
        {
            if (dp[state, numObservations - 1] > maxFinalProb)
            {
                maxFinalProb = dp[state, numObservations - 1];
                lastState = state;
            }
        }

        mostLikelyStates[numObservations - 1] = lastState;

        // Backtrack through the backpointer matrix to get the full state sequence
        for (int t = numObservations - 2; t >= 0; t--)
        {
            mostLikelyStates[t] = backpointer[mostLikelyStates[t + 1], t + 1];
        }

        return mostLikelyStates;
    }

    // Method to simulate random observation sequence (for testing purposes)
    public int[] GenerateRandomObservations(int length)
    {
        int[] observations = new int[length];
        System.Random rand = new System.Random();

        for (int i = 0; i < length; i++)
        {
            observations[i] = rand.Next(numObservations);
        }

        return observations;
    }
}

using UnityEngine;
using System;

public class ForwardPropagation : MonoBehaviour
{
    // Number of neurons in each layer
    private int inputLayerSize = 3;  // Example: 3 input features
    private int hiddenLayerSize = 4; // Example: 4 neurons in hidden layer
    private int outputLayerSize = 1; // Example: 1 output (e.g., binary classification)

    // Weights for each layer
    private float[,] inputToHiddenWeights;
    private float[,] hiddenToOutputWeights;

    // Biases for each layer
    private float[] hiddenBiases;
    private float[] outputBiases;

    void Start()
    {
        // Initialize weights and biases
        inputToHiddenWeights = InitializeWeights(inputLayerSize, hiddenLayerSize);
        hiddenToOutputWeights = InitializeWeights(hiddenLayerSize, outputLayerSize);
        hiddenBiases = InitializeBiases(hiddenLayerSize);
        outputBiases = InitializeBiases(outputLayerSize);

        // Example input (e.g., for 3 input features)
        float[] input = new float[] { 1.0f, 0.5f, -1.5f };

        // Perform forward propagation
        float[] output = ForwardPropagate(input);

        // Output the result to the console
        Debug.Log("Network Output: " + output[0]);
    }

    // Sigmoid activation function
    private float Sigmoid(float x)
    {
        return 1.0f / (1.0f + Mathf.Exp(-x));
    }

    // Initialize weights for the layer (random values between -1 and 1)
    private float[,] InitializeWeights(int rows, int cols)
    {
        float[,] weights = new float[rows, cols];
        System.Random rand = new System.Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                weights[i, j] = (float)rand.NextDouble() * 2 - 1; // Values between -1 and 1
            }
        }

        return weights;
    }

    // Initialize biases (random values between -1 and 1)
    private float[] InitializeBiases(int size)
    {
        float[] biases = new float[size];
        System.Random rand = new System.Random();

        for (int i = 0; i < size; i++)
        {
            biases[i] = (float)rand.NextDouble() * 2 - 1; // Values between -1 and 1
        }

        return biases;
    }

    // Forward propagation function
    private float[] ForwardPropagate(float[] input)
    {
        // Input to Hidden Layer
        float[] hiddenLayerInput = new float[hiddenLayerSize];
        for (int i = 0; i < hiddenLayerSize; i++)
        {
            hiddenLayerInput[i] = 0.0f;

            // Sum inputs weighted by the input-to-hidden weights
            for (int j = 0; j < inputLayerSize; j++)
            {
                hiddenLayerInput[i] += input[j] * inputToHiddenWeights[j, i];
            }

            // Add bias to the hidden layer
            hiddenLayerInput[i] += hiddenBiases[i];

            // Apply activation function (sigmoid)
            hiddenLayerInput[i] = Sigmoid(hiddenLayerInput[i]);
        }

        // Hidden to Output Layer
        float[] outputLayerInput = new float[outputLayerSize];
        for (int i = 0; i < outputLayerSize; i++)
        {
            outputLayerInput[i] = 0.0f;

            // Sum hidden outputs weighted by the hidden-to-output weights
            for (int j = 0; j < hiddenLayerSize; j++)
            {
                outputLayerInput[i] += hiddenLayerInput[j] * hiddenToOutputWeights[j, i];
            }

            // Add bias to the output layer
            outputLayerInput[i] += outputBiases[i];

            // Apply activation function (sigmoid)
            outputLayerInput[i] = Sigmoid(outputLayerInput[i]);
        }

        return outputLayerInput; // Final output of the network
    }
}

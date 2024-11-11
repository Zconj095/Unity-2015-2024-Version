using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportVectorMachine : MonoBehaviour
{
    // Parameters for the SVM model
    private float[] weights;  // Weights for the hyperplane
    private float bias;       // Bias term for the hyperplane
    private float learningRate = 0.01f;  // Learning rate for gradient descent
    private int epochs = 1000;  // Number of iterations for training

    public SupportVectorMachine(int numberOfFeatures)
    {
        weights = new float[numberOfFeatures];
        bias = 0f;
    }

    // Train the SVM model using the provided data
    public void Train(List<TrainingSample> trainingData)
    {
        for (int epoch = 0; epoch < epochs; epoch++)
        {
            foreach (var sample in trainingData)
            {
                // Compute the dot product of weights and the sample features
                float dotProduct = 0;
                for (int i = 0; i < weights.Length; i++)
                {
                    dotProduct += weights[i] * sample.Features[i];
                }

                // Check if the sample is on the correct side of the margin
                if (sample.Label * (dotProduct + bias) < 1)
                {
                    // Update weights and bias based on the margin condition
                    for (int i = 0; i < weights.Length; i++)
                    {
                        weights[i] -= learningRate * (2 * weights[i] - sample.Label * sample.Features[i]);
                    }
                    bias -= learningRate * (-sample.Label);
                }
                else
                {
                    // Regularize weights if the sample is on the correct side of the margin
                    for (int i = 0; i < weights.Length; i++)
                    {
                        weights[i] -= learningRate * 2 * weights[i];
                    }
                }
            }
        }
    }

    // Classify a new data point
    public int Predict(float[] features)
    {
        // Implement prediction logic
        float sum = bias;
        for (int i = 0; i < features.Length; i++)
        {
            sum += weights[i] * features[i];
        }
        return sum >= 0 ? 1 : -1;  // Simple threshold for classification
    }

    // Get the learned weights and bias for debugging
    public float[] GetWeights()
    {
        return weights;
    }

    public float GetBias()
    {
        return bias;
    }
}

public class TrainingSample
{
    public float[] Features;  // Feature vector of the sample
    public int Label;         // Label (1 or -1)

    public TrainingSample(float[] features, int label)
    {
        Features = features;
        Label = label;
    }
}

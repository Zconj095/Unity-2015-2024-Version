using System.Collections.Generic;
using UnityEngine;
using System;

public class SVMExample : MonoBehaviour
{
    private SupportVectorMachine svm;

    void Start()
    {
        // Example: training data for a 2D dataset
        List<TrainingSample> trainingData = new List<TrainingSample>
        {
            new TrainingSample(new float[] { 2.0f, 3.0f }, 1),  // Label 1
            new TrainingSample(new float[] { 3.0f, 3.0f }, 1),  // Label 1
            new TrainingSample(new float[] { 1.0f, 1.0f }, -1), // Label -1
            new TrainingSample(new float[] { 2.0f, 1.0f }, -1), // Label -1
        };

        // Create and train the SVM with the dataset
        svm = new SupportVectorMachine(2);  // 2D feature vectors
        svm.Train(trainingData);

        // Output the learned weights and bias
        Debug.Log("Trained SVM:");
        Debug.Log("Weights: " + string.Join(", ", Array.ConvertAll(svm.GetWeights(), w => w.ToString())));
        Debug.Log("Bias: " + svm.GetBias());

        // Example: classify new data points
        ClassifyNewPoints();
    }

    // Example method to classify new points
    void ClassifyNewPoints()
    {
        // New point to classify (2D)
        float[] point1 = new float[] { 2.5f, 2.5f };
        int result1 = svm.Predict(point1);
        Debug.Log("Point " + string.Join(", ", Array.ConvertAll(point1, p => p.ToString())) + " classified as: " + result1);

        float[] point2 = new float[] { 1.5f, 2.0f };
        int result2 = svm.Predict(point2);
        Debug.Log("Point " + string.Join(", ", Array.ConvertAll(point2, p => p.ToString())) + " classified as: " + result2);
    }
}


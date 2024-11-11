using UnityEngine;
using System.Collections.Generic;

public class CrossValidationExample : MonoBehaviour
{
    void Start()
    {
        // Example data (you would replace this with your actual dataset)
        List<DataPoint> dataPoints = new List<DataPoint>
        {
            new DataPoint(new float[] { 1, 2 }, 0),
            new DataPoint(new float[] { 1, 3 }, 1),
            new DataPoint(new float[] { 2, 1 }, 0),
            new DataPoint(new float[] { 2, 3 }, 1),
            new DataPoint(new float[] { 3, 4 }, 1),
            new DataPoint(new float[] { 4, 5 }, 0)
            // Add more data points as needed
        };

        Dataset dataset = new Dataset(dataPoints);

        // Perform 3-fold cross-validation
        int k = 3;
        float averageAccuracy = CrossValidator.KFoldCrossValidation(dataset, k, SimpleModel.TrainAndEvaluate);

        // Output the result in Unity's console (updated string formatting)
        Debug.Log("Average Accuracy from " + k + "-fold cross-validation: " + (averageAccuracy * 100) + "%");
    }
}

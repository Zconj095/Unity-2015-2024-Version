using UnityEngine;
using System.Collections.Generic;

public class HyperplanePrediction : MonoBehaviour
{
    // Define a data structure to store data points
    public struct DataPoint
    {
        public Vector3 position;
        public int label; // 0 or 1 (for binary classification)

        public DataPoint(Vector3 pos, int lbl)
        {
            position = pos;
            label = lbl;
        }
    }

    private List<DataPoint> trainingData = new List<DataPoint>(); // All training data points
    private Vector3 hyperplaneNormal; // Direction of the separating hyperplane
    private float hyperplaneBias; // Bias term for the hyperplane

    void Start()
    {
        // Generate some initial training data
        trainingData.Add(new DataPoint(new Vector3(1, 1, 0), 0)); // Class 0
        trainingData.Add(new DataPoint(new Vector3(2, 2, 0), 0)); // Class 0
        trainingData.Add(new DataPoint(new Vector3(5, 5, 0), 1)); // Class 1
        trainingData.Add(new DataPoint(new Vector3(6, 6, 0), 1)); // Class 1

        // Train the model by calculating the hyperplane
        TrainModel();
    }

    void TrainModel()
    {
        // For simplicity, calculate the average position for each class
        Vector3 class0Avg = Vector3.zero;
        Vector3 class1Avg = Vector3.zero;

        int class0Count = 0;
        int class1Count = 0;

        // Calculate average position for each class
        foreach (var data in trainingData)
        {
            if (data.label == 0)
            {
                class0Avg += data.position;
                class0Count++;
            }
            else
            {
                class1Avg += data.position;
                class1Count++;
            }
        }

        class0Avg /= class0Count;
        class1Avg /= class1Count;

        // Calculate the hyperplane normal as the vector between class centers
        hyperplaneNormal = (class1Avg - class0Avg).normalized;

        // Calculate the bias term by projecting one of the class centers onto the hyperplane
        hyperplaneBias = Vector3.Dot(hyperplaneNormal, class0Avg);

        Debug.Log("Model Trained: Hyperplane Normal: " + hyperplaneNormal + ", Bias: " + hyperplaneBias);
    }

    void Update()
    {
        // Example usage: Check if a point is classified correctly using the current model
        Vector3 testPoint = new Vector3(4, 4, 0); // Example test point

        int predictedLabel = ClassifyPoint(testPoint);
        Debug.Log("Predicted Label for Test Point: " + predictedLabel);
    }

    // Classify a point based on the current hyperplane
    int ClassifyPoint(Vector3 point)
    {
        // Calculate the result of the decision function
        float decisionValue = Vector3.Dot(point, hyperplaneNormal) - hyperplaneBias;

        // If the value is positive, classify as class 1, else class 0
        return decisionValue >= 0 ? 1 : 0;
    }

    // Visualize the hyperplane and points in the scene (for debugging)
    void OnDrawGizmos()
    {
        // Visualize the training data
        foreach (var data in trainingData)
        {
            Gizmos.color = data.label == 0 ? Color.red : Color.green;
            Gizmos.DrawSphere(data.position, 0.1f);
        }

        // Visualize the hyperplane (using a line to represent the normal vector)
        Gizmos.color = Color.blue;
        Vector3 start = new Vector3(0, 0, 0);
        Vector3 end = start + hyperplaneNormal * 5;
        Gizmos.DrawLine(start, end);
    }
}

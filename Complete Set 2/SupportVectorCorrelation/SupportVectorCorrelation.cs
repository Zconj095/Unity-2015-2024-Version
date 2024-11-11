using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SupportVectorCorrelation : MonoBehaviour
{
    // Structure to represent data points
    public struct DataPoint
    {
        public Vector3 position;
        public float value; // The continuous value we want to predict

        public DataPoint(Vector3 pos, float val)
        {
            position = pos;
            value = val;
        }
    }

    // List of training data points
    private List<DataPoint> trainingData = new List<DataPoint>();
    private List<DataPoint> supportVectors = new List<DataPoint>();
    private float epsilon = 0.1f; // Margin of tolerance
    private float C = 1.0f; // Regularization parameter

    void Start()
    {
        // Example: Generate some training data
        trainingData.Add(new DataPoint(new Vector3(1, 1, 0), 2.0f));
        trainingData.Add(new DataPoint(new Vector3(2, 2, 0), 3.0f));
        trainingData.Add(new DataPoint(new Vector3(3, 3, 0), 4.0f));
        trainingData.Add(new DataPoint(new Vector3(4, 4, 0), 5.0f));

        // Train the model using Support Vector Regression (SVR)
        TrainModel();
    }

    void TrainModel()
    {
        // Simplified Support Vector Regression (SVR) using RBF kernel

        // Use the RBF kernel function to transform the data into a higher-dimensional space
        List<float> predictions = new List<float>();

        foreach (var data in trainingData)
        {
            float prediction = 0.0f;

            // Calculate the kernel for each data point
            foreach (var supportVector in supportVectors)
            {
                float kernelValue = RbfKernel(data.position, supportVector.position);
                prediction += kernelValue * supportVector.value;
            }

            predictions.Add(prediction);
        }

        // After training (we're assuming a simplified training method here), we should
        // have support vectors that are most impactful for predicting new points.

        // For this basic example, we're storing the support vectors as part of the model.
        supportVectors = trainingData; // In reality, we'd select the most influential data points here
        Debug.Log("Model Trained: Support Vectors Count: " + supportVectors.Count);
    }

    // The Radial Basis Function (RBF) kernel, which is commonly used in SVMs and SVR
    float RbfKernel(Vector3 x1, Vector3 x2)
    {
        float gamma = 0.5f; // RBF parameter that controls the kernel's influence
        float distanceSquared = (x1 - x2).sqrMagnitude;
        return Mathf.Exp(-gamma * distanceSquared);
    }

    void Update()
    {
        // Example usage: Predict the value for a new test point
        Vector3 testPoint = new Vector3(2.5f, 2.5f, 0); // Example test point

        float predictedValue = Predict(testPoint);
        Debug.Log("Predicted Value for Test Point: " + predictedValue);
    }

    // Predict the value for a given test point based on the support vectors
    float Predict(Vector3 point)
    {
        float prediction = 0.0f;

        foreach (var supportVector in supportVectors)
        {
            float kernelValue = RbfKernel(point, supportVector.position);
            prediction += kernelValue * supportVector.value;
        }

        return prediction;
    }

    // Visualize the training data and predictions in the Unity scene for debugging
    void OnDrawGizmos()
    {
        // Visualize the training data points
        foreach (var data in trainingData)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(data.position, 0.1f);
        }

        // Visualize the support vectors
        foreach (var vector in supportVectors)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(vector.position, 0.2f); // Support vectors are visualized in red
        }
    }
}

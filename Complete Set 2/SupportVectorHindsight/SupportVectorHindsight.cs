using UnityEngine;
using System.Collections.Generic;

public class SupportVectorHindsight : MonoBehaviour
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
    private List<DataPoint> improbableData = new List<DataPoint>(); // Rare or improbable data points
    private Vector3 hyperplaneNormal; // Direction of the separating hyperplane
    private float hyperplaneBias; // Bias term for the hyperplane

    private float margin = 5f; // Margin for the decision boundary

    void Start()
    {
        // Generate some initial training data
        trainingData.Add(new DataPoint(new Vector3(1, 1, 0), 0)); // Class 0
        trainingData.Add(new DataPoint(new Vector3(2, 2, 0), 0)); // Class 0
        trainingData.Add(new DataPoint(new Vector3(5, 5, 0), 1)); // Class 1
        trainingData.Add(new DataPoint(new Vector3(6, 6, 0), 1)); // Class 1

        // Generate improbable or rare data (outliers)
        improbableData.Add(new DataPoint(new Vector3(8, 0, 0), 1)); // Class 1 outlier
        improbableData.Add(new DataPoint(new Vector3(0, 8, 0), 0)); // Class 0 outlier

        // Train SVM model with initial data
        TrainSVM();

        // Use improbable data to refine the model
        RefineModelWithImprobability();
    }

    void TrainSVM()
    {
        // Here we would normally use an SVM algorithm. 
        // For simplicity, we approximate it by calculating a decision boundary 
        // based on the average position of data points from each class.
        
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

        // Set the hyperplane direction to be the difference between the class centers
        hyperplaneNormal = (class1Avg - class0Avg).normalized;

        // Set the bias term of the hyperplane
        hyperplaneBias = Vector3.Dot(hyperplaneNormal, class0Avg);
        
        Debug.Log("SVM Model Trained.");
    }

    void RefineModelWithImprobability()
    {
        // Use improbable data to adjust the hyperplane
        foreach (var improbablePoint in improbableData)
        {
            float distanceToHyperplane = Mathf.Abs(Vector3.Dot(improbablePoint.position, hyperplaneNormal) - hyperplaneBias);

            // If the improbable point is far from the hyperplane, we consider it important
            if (distanceToHyperplane > margin)
            {
                // Adjust the hyperplane using the improbable point (a simplified adjustment)
                hyperplaneNormal += (improbablePoint.position - (hyperplaneNormal * Vector3.Dot(improbablePoint.position, hyperplaneNormal))).normalized;
                hyperplaneNormal.Normalize();

                // Recalculate the bias after adjustment
                hyperplaneBias = Vector3.Dot(hyperplaneNormal, improbablePoint.position);
            }
        }

        Debug.Log("Model Refined with Improbable Points.");
    }

    void Update()
    {
        // Example usage: Check if a point is classified correctly using the current model
        Vector3 testPoint = new Vector3(4, 4, 0); // Example test point

        int predictedLabel = ClassifyPoint(testPoint);
        Debug.Log("Predicted Label for Test Point: " + predictedLabel);
    }

    // Classify a point based on the current model
    int ClassifyPoint(Vector3 point)
    {
        // Calculate the result of the decision function
        float decisionValue = Vector3.Dot(point, hyperplaneNormal) - hyperplaneBias;

        // If the value is positive, classify as class 1, else class 0
        return decisionValue >= 0 ? 1 : 0;
    }
}

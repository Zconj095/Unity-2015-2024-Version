using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class InferentialStatistics : MonoBehaviour
{
    public List<float> dataSet = new List<float>();  // Store measurements
    public float predictedValue;  // Predicted value for new data point

    void Start()
    {
        // Example: Sample data (this could be collected dynamically)
        dataSet.AddRange(new float[] { 10, 12, 14, 11, 13, 15, 10, 14, 12, 13 });

        // Calculate inferential statistics
        CalculateStatistics();

        // Make a prediction (for example, next value prediction)
        MakePrediction();
    }

    // Method to calculate mean and standard deviation
    public void CalculateStatistics()
    {
        float mean = CalculateMean(dataSet);
        float standardDeviation = CalculateStandardDeviation(dataSet, mean);
        
        Debug.Log("Mean: " + mean);
        Debug.Log("Standard Deviation: " + standardDeviation);
    }

    // Method to calculate the mean of the dataset
    public float CalculateMean(List<float> data)
    {
        return data.Average();  // Using LINQ for simplicity
    }

    // Method to calculate the standard deviation of the dataset
    public float CalculateStandardDeviation(List<float> data, float mean)
    {
        float sumOfSquares = data.Sum(d => Mathf.Pow(d - mean, 2));
        return Mathf.Sqrt(sumOfSquares / data.Count);
    }

    public void MakePrediction()
    {
        if (dataSet.Count < 2)
        {
            Debug.LogWarning("Not enough data to make a prediction.");
            return;
        }

        // Use a basic prediction model: Predicted value = Mean + Random deviation based on std deviation
        float mean = CalculateMean(dataSet);
        float stdDev = CalculateStandardDeviation(dataSet, mean);
        
        // Predict a new value by adding a small random noise (using UnityEngine.Random for Unity)
        predictedValue = mean + UnityEngine.Random.Range(-stdDev, stdDev);  // Explicitly use UnityEngine.Random

        Debug.Log("Predicted Value: " + predictedValue);
    }


	// Method to make prediction with confidence interval
    public void MakePredictionWithConfidenceInterval(float confidenceLevel = 0.95f)
    {
        float mean = CalculateMean(dataSet);
        float stdDev = CalculateStandardDeviation(dataSet, mean);
        float zScore = GetZScoreForConfidenceInterval(confidenceLevel); // Find z-score for the confidence level
        
        // Calculate margin of error
        float marginOfError = zScore * (stdDev / Mathf.Sqrt(dataSet.Count));

        // Predict the range (confidence interval)
        float lowerBound = mean - marginOfError;
        float upperBound = mean + marginOfError;

        // Use string.Format instead of interpolated strings
        Debug.Log(string.Format("Predicted Range with {0}% confidence: [{1}, {2}]", confidenceLevel * 100, lowerBound, upperBound));
    }


	// Helper method to get the Z-score for a given confidence level
	private float GetZScoreForConfidenceInterval(float confidenceLevel)
	{
		if (confidenceLevel == 0.95f)
			return 1.96f;  // Z-score for 95% confidence
		else
			return 1.64f;  // Z-score for 90% confidence
	}

}

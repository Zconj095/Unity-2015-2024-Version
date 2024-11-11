using UnityEngine;
using System.Linq;

public class CalculativeMeanAdjustment : MonoBehaviour
{
    // A list or array of data points that you want to adjust
    public float[] dataPoints;

    // The target mean value you want to achieve
    public float targetMean;

    void Start()
    {
        if (dataPoints.Length > 0)
        {
            // Calculate the current mean
            float currentMean = CalculateMean(dataPoints);

            // Adjust the data points to match the target mean
            AdjustDataPointsToMean(ref dataPoints, currentMean, targetMean);

            // Log the results
            Debug.Log("Current Mean: " + currentMean);
            Debug.Log("Adjusted Mean: " + targetMean);
            Debug.Log("Adjusted Data: " + string.Join(", ", dataPoints.Select(d => d.ToString()).ToArray()));
        }
        else
        {
            Debug.LogError("No data points provided!");
        }
    }

    // Function to calculate the mean of an array of data points
    float CalculateMean(float[] values)
    {
        return values.Average(); // Use LINQ to calculate the mean
    }

    // Function to adjust the data points to the target mean
    void AdjustDataPointsToMean(ref float[] values, float currentMean, float targetMean)
    {
        float adjustment = targetMean - currentMean;

        // Adjust all values by the difference between the target and current mean
        for (int i = 0; i < values.Length; i++)
        {
            values[i] += adjustment;
        }
    }
}

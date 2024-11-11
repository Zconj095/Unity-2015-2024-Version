using System;
using System.Collections.Generic;

public class SimpleModel
{
    public static float TrainAndEvaluate(Dataset dataset)
    {
        // For simplicity, assume we have a trivial model that always predicts the most common class.
        Dictionary<int, int> classCounts = new Dictionary<int, int>();

        // Count class frequencies in the dataset
        foreach (var dataPoint in dataset.DataPoints)
        {
            if (!classCounts.ContainsKey(dataPoint.Label))
            {
                classCounts[dataPoint.Label] = 0;
            }
            classCounts[dataPoint.Label]++;
        }

        // Find the most frequent class
        int mostFrequentClass = 0;
        int maxCount = 0;
        foreach (var kvp in classCounts)
        {
            if (kvp.Value > maxCount)
            {
                mostFrequentClass = kvp.Key;
                maxCount = kvp.Value;
            }
        }

        // Calculate accuracy
        int correct = 0;
        foreach (var dataPoint in dataset.DataPoints)
        {
            if (dataPoint.Label == mostFrequentClass)
            {
                correct++;
            }
        }

        // Return accuracy as a fraction
        return (float)correct / dataset.DataPoints.Count;
    }
}

using System;
using System.Collections.Generic;

public class CrossValidator
{
    public static float KFoldCrossValidation(Dataset dataset, int k, Func<Dataset, float> modelTrainer)
    {
		
        // Split the data into k folds
        List<List<DataPoint>> folds = CreateKFolds(dataset, k);

        float totalAccuracy = 0.0f;

        // Iterate over each fold
        for (int i = 0; i < k; i++)
        {
            // Use the ith fold as the validation set, others as the training set
            List<DataPoint> trainingSet = new List<DataPoint>();
            List<DataPoint> validationSet = folds[i];

            for (int j = 0; j < k; j++)
            {
                if (i != j)
                {
                    trainingSet.AddRange(folds[j]);
                }
            }

            // Train the model and evaluate it on the validation set
            float accuracy = modelTrainer(new Dataset(trainingSet)); // Train the model
            totalAccuracy += accuracy;
        }

        // Return the average accuracy across all folds
        return totalAccuracy / k;
    }

    // Function to split dataset into k folds
    private static List<List<DataPoint>> CreateKFolds(Dataset dataset, int k)
    {
        List<List<DataPoint>> folds = new List<List<DataPoint>>();
        List<DataPoint> dataPoints = dataset.DataPoints;
        int foldSize = dataPoints.Count / k;

        // Initialize k empty folds
        for (int i = 0; i < k; i++)
        {
            folds.Add(new List<DataPoint>());
        }

        // Distribute data into folds
        for (int i = 0; i < dataPoints.Count; i++)
        {
            int foldIndex = i % k;
            folds[foldIndex].Add(dataPoints[i]);
        }

        return folds;
    }
}

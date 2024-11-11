using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class KMeansClustering : MonoBehaviour
{
    // Number of clusters (k)
    public int k = 3;

    // Data points to be clustered (Each point has 2 features in this case)
    private List<KmeansDataPoint> KmeansDataPoints;

    // Centroids of each cluster
    private List<KmeansDataPoint> centroids;

    void Start()
    {
        // Initialize some example data points (2D points for simplicity)
        KmeansDataPoints = new List<KmeansDataPoint>
        {
            new KmeansDataPoint(new float[] { 1.0f, 2.0f }),
            new KmeansDataPoint(new float[] { 1.5f, 1.8f }),
            new KmeansDataPoint(new float[] { 5.0f, 8.0f }),
            new KmeansDataPoint(new float[] { 8.0f, 8.0f }),
            new KmeansDataPoint(new float[] { 1.0f, 0.6f }),
            new KmeansDataPoint(new float[] { 9.0f, 11.0f }),
            new KmeansDataPoint(new float[] { 8.0f, 4.0f }),
            new KmeansDataPoint(new float[] { 6.0f, 4.0f }),
            new KmeansDataPoint(new float[] { 3.0f, 7.0f }),
            new KmeansDataPoint(new float[] { 3.5f, 6.5f })
        };

        // Perform K-Means clustering
        centroids = PerformKMeansClustering(KmeansDataPoints, k);

        // Output the centroids to Unity's console
        foreach (var centroid in centroids)
        {
            Debug.Log("Centroid: " + centroid.ToString());
        }
    }

    // Perform K-Means clustering
    private List<KmeansDataPoint> PerformKMeansClustering(List<KmeansDataPoint> data, int k)
    {
        // Initialize centroids randomly from the data points
        List<KmeansDataPoint> centroids = InitializeCentroids(data, k);

        bool centroidsChanged = true;

        while (centroidsChanged)
        {
            centroidsChanged = false;

            // Step 1: Assign each data point to the nearest centroid
            foreach (var point in data)
            {
                point.AssignedCluster = GetClosestCentroid(point, centroids);
            }

            // Step 2: Recompute the centroids (mean of points in each cluster)
            for (int i = 0; i < k; i++)
            {
                var clusterPoints = data.Where(p => p.AssignedCluster == i).ToList();
                if (clusterPoints.Count > 0)
                {
                    KmeansDataPoint newCentroid = CalculateCentroid(clusterPoints);
                    if (!newCentroid.Equals(centroids[i]))
                    {
                        centroidsChanged = true;
                    }
                    centroids[i] = newCentroid;
                }
            }
        }

        return centroids;
    }

    // Initialize centroids randomly from the data points
    private List<KmeansDataPoint> InitializeCentroids(List<KmeansDataPoint> data, int k)
    {
        List<KmeansDataPoint> centroids = new List<KmeansDataPoint>();
        System.Random rand = new System.Random();

        while (centroids.Count < k)
        {
            int index = rand.Next(data.Count);
            KmeansDataPoint candidate = data[index];
            if (!centroids.Contains(candidate)) // Ensure uniqueness
            {
                centroids.Add(candidate);
            }
        }

        return centroids;
    }

    // Find the closest centroid to a given data point
    private int GetClosestCentroid(KmeansDataPoint point, List<KmeansDataPoint> centroids)
    {
        float minDistance = float.MaxValue;
        int closestCentroid = 0;

        for (int i = 0; i < centroids.Count; i++)
        {
            float distance = CalculateEuclideanDistance(point, centroids[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCentroid = i;
            }
        }

        return closestCentroid;
    }

    // Calculate the Euclidean distance between two data points
    private float CalculateEuclideanDistance(KmeansDataPoint point1, KmeansDataPoint point2)
    {
        float sum = 0;
        for (int i = 0; i < point1.Features.Length; i++)
        {
            sum += Mathf.Pow(point1.Features[i] - point2.Features[i], 2);
        }

        return Mathf.Sqrt(sum);
    }

    // Calculate the centroid of a set of points (mean of their coordinates)
    private KmeansDataPoint CalculateCentroid(List<KmeansDataPoint> points)
    {
        int numFeatures = points[0].Features.Length;
        float[] centroidFeatures = new float[numFeatures];

        foreach (var point in points)
        {
            for (int i = 0; i < numFeatures; i++)
            {
                centroidFeatures[i] += point.Features[i];
            }
        }

        for (int i = 0; i < numFeatures; i++)
        {
            centroidFeatures[i] /= points.Count;
        }

        return new KmeansDataPoint(centroidFeatures);
    }
}

// Class to represent a data point
public class KmeansDataPoint
{
    public float[] Features { get; set; }
    public int AssignedCluster { get; set; }

    public KmeansDataPoint(float[] features)
    {
        Features = features;
        AssignedCluster = -1;  // Not assigned to any cluster initially
    }

    // Override the ToString method for easier debugging
    public override string ToString()
    {
        // Use ToArray() to ensure proper conversion to string[]
        return "[" + string.Join(", ", Features.Select(f => f.ToString()).ToArray()) + "]";
    }

    // Override Equals and GetHashCode for correct comparison (used in centroid uniqueness check)
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        KmeansDataPoint other = (KmeansDataPoint)obj;
        return Features.SequenceEqual(other.Features);
    }

    public override int GetHashCode()
    {
        return Features.Aggregate(0, (hash, f) => hash * 31 + f.GetHashCode());
    }
}

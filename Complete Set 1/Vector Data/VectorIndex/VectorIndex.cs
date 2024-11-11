using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class VectorIndex : MonoBehaviour
{
    // The list of vectors to be indexed
    private List<VectorDataPoint> dataPoints;

    void Start()
    {
        // Initialize the vector index with some example data
        dataPoints = new List<VectorDataPoint>
        {
            new VectorDataPoint(new float[] { 1.0f, 2.0f }),
            new VectorDataPoint(new float[] { 1.5f, 1.8f }),
            new VectorDataPoint(new float[] { 5.0f, 8.0f }),
            new VectorDataPoint(new float[] { 8.0f, 8.0f }),
            new VectorDataPoint(new float[] { 1.0f, 0.6f }),
            new VectorDataPoint(new float[] { 9.0f, 11.0f }),
            new VectorDataPoint(new float[] { 8.0f, 4.0f }),
            new VectorDataPoint(new float[] { 6.0f, 4.0f }),
            new VectorDataPoint(new float[] { 3.0f, 7.0f }),
            new VectorDataPoint(new float[] { 3.5f, 6.5f })
        };

        // Perform a nearest neighbor search for a sample vector
        VectorDataPoint queryPoint = new VectorDataPoint(new float[] { 5.0f, 5.0f });
        int nearestNeighborIndex = FindNearestNeighbor(queryPoint);

        // Output the nearest neighbor to Unity's console
        Debug.Log("Nearest Neighbor: " + dataPoints[nearestNeighborIndex].ToString());
    }

    // Find the nearest neighbor using Euclidean distance
    public int FindNearestNeighbor(VectorDataPoint queryPoint)
    {
        int nearestIndex = 0;
        float minDistance = float.MaxValue;

        for (int i = 0; i < dataPoints.Count; i++)
        {
            float distance = CalculateEuclideanDistance(queryPoint, dataPoints[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestIndex = i;
            }
        }

        return nearestIndex;
    }

    // Calculate the Euclidean distance between two data points
    private float CalculateEuclideanDistance(VectorDataPoint point1, VectorDataPoint point2)
    {
        float sum = 0;
        for (int i = 0; i < point1.Features.Length; i++)
        {
            sum += Mathf.Pow(point1.Features[i] - point2.Features[i], 2);
        }

        return Mathf.Sqrt(sum);
    }

    // Optionally, you could add a method for multiple nearest neighbors
    public List<int> FindNearestNeighbors(VectorDataPoint queryPoint, int numNeighbors)
    {
        // Sort the data points based on distance to the query point
        var sortedDataPoints = dataPoints.Select((point, index) => new { point, index })
                                          .OrderBy(x => CalculateEuclideanDistance(queryPoint, x.point))
                                          .Take(numNeighbors)
                                          .Select(x => x.index)
                                          .ToList();

        return sortedDataPoints;
    }
}



using UnityEngine;
using System.Collections.Generic;

public class CoordinalInferentialMeans : MonoBehaviour
{
    public struct SpatialPoint
    {
        public Vector3 position; // 3D position in space
        public float likelihood; // Likelihood of an event at this coordinate

        public SpatialPoint(Vector3 pos, float lik)
        {
            position = pos;
            likelihood = lik;
        }
    }

    private List<SpatialPoint> spatialPoints;

    // Thresholds for inference
    public float maxDistance = 10f; // Maximum distance to consider in inference
    public float likelihoodThreshold = 0.5f; // Threshold for event likelihood

    void Start()
    {
        // Initialize some spatial points (for example, randomly generated points in the scene)
        spatialPoints = new List<SpatialPoint>
        {
            new SpatialPoint(new Vector3(1, 2, 3), 0.3f),
            new SpatialPoint(new Vector3(5, 1, 4), 0.8f),
            new SpatialPoint(new Vector3(10, 0, 0), 0.2f)
        };
    }

    // Update function to make inferences
    void Update()
    {
        Vector3 playerPosition = transform.position; // Assuming the object has a position in the game world

        // Inference logic based on spatial distances
        foreach (var point in spatialPoints)
        {
            float distance = Vector3.Distance(playerPosition, point.position);

            // If the point is within a certain threshold distance, infer the likelihood of an event
            if (distance <= maxDistance)
            {
                float inferredLikelihood = InferLikelihood(distance, point.likelihood);
                Debug.Log("Inferred likelihood for point at " + point.position + " is: " + inferredLikelihood);
                
                if (inferredLikelihood >= likelihoodThreshold)
                {
                    // Event is likely to occur (or some action needs to be taken)
                    Debug.Log("Event likely to occur at this point!");
                }
            }
        }
    }

    // Example of an inference function that adjusts likelihood based on distance and original likelihood
    float InferLikelihood(float distance, float baseLikelihood)
    {
        // Simple inverse distance weighting for inference (decay with distance)
        float distanceFactor = Mathf.Max(0, 1 - (distance / maxDistance));
        float inferredLikelihood = baseLikelihood * distanceFactor;
        return Mathf.Clamp01(inferredLikelihood); // Ensure the likelihood stays between 0 and 1
    }
}

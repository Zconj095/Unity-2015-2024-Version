using UnityEngine;

public class DimensionalOriginalityBetweenMultipleOriginLocations : MonoBehaviour
{
    // Array to store multiple origin points (can be set in inspector)
    public Transform[] originPoints;

    // Number of origins to compare
    public int comparisonCount = 3;

    // To store the distance between origins
    private float[,] distances;

    // To store the angles between origins
    private float[,] angles;

    // Optionally, store custom transformations for "dimensional" uniqueness
    public Transform customTransform;

    void Start()
    {
        // Initialize the distance and angle arrays
        distances = new float[originPoints.Length, originPoints.Length];
        angles = new float[originPoints.Length, originPoints.Length];

        // Compare distances and angles between all origins
        CompareOrigins();
    }

    void Update()
    {
        // Optional: Update positions or transforms during gameplay
        if (Input.GetKeyDown(KeyCode.U)) // For example, move the first origin point
        {
            originPoints[0].position += new Vector3(1, 0, 0); // Move the first origin on X-axis
            CompareOrigins();  // Recalculate distances and angles after update
        }
    }

    void CompareOrigins()
    {
        // Loop through all pairs of origins to calculate distance and angle
        for (int i = 0; i < originPoints.Length; i++)
        {
            for (int j = i + 1; j < originPoints.Length; j++)
            {
                // Calculate the distance between two origins in world space
                distances[i, j] = Vector3.Distance(originPoints[i].position, originPoints[j].position);

                // Calculate the angle between two origins in world space
                Vector3 directionI = originPoints[i].position - transform.position;
                Vector3 directionJ = originPoints[j].position - transform.position;
                angles[i, j] = Vector3.Angle(directionI, directionJ);

                // Output the results to the console
                Debug.Log("Distance between Origin " + i + " and Origin " + j + ": " + distances[i, j]);
                Debug.Log("Angle between Origin " + i + " and Origin " + j + ": " + angles[i, j]);
            }
        }
    }

    // Method to handle dynamic updating of origin points
    public void UpdateOrigin(int index, Transform newOrigin)
    {
        if (index >= 0 && index < originPoints.Length)
        {
            originPoints[index] = newOrigin;
            CompareOrigins();  // Recalculate distances and angles after the update
        }
    }

    // This method applies a transformation to all origins as if they exist in a custom "dimensional space"
    void ApplyDimensionalTransformation()
    {
        if (customTransform != null)
        {
            for (int i = 0; i < originPoints.Length; i++)
            {
                // Apply the custom transformation to each origin
                originPoints[i].position = customTransform.TransformPoint(originPoints[i].position);
            }
        }
    }

    // Gizmos for visualization in the Scene view
    void OnDrawGizmos()
    {
        if (originPoints != null && originPoints.Length > 0)
        {
            Gizmos.color = Color.red;

            // Draw a Gizmo at each origin point
            foreach (Transform origin in originPoints)
            {
                Gizmos.DrawSphere(origin.position, 0.1f);
            }

            // Optionally draw lines between origins
            for (int i = 0; i < originPoints.Length; i++)
            {
                for (int j = i + 1; j < originPoints.Length; j++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(originPoints[i].position, originPoints[j].position);
                }
            }
        }
    }
}

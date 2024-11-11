using UnityEngine;

public class CosineSimilarity : MonoBehaviour
{
    // Vectors for comparison (these could represent object directions, positions, or other properties)
    public Vector3 vectorA = new Vector3(1, 0, 0); // Example vector A
    public Vector3 vectorB = new Vector3(1, 1, 0); // Example vector B

    void Start()
    {
        // Call the function to compute cosine similarity
        float similarity = CalculateCosineSimilarity(vectorA, vectorB);
        Debug.Log("Cosine Similarity: " + similarity);
    }

    // Function to calculate cosine similarity
    float CalculateCosineSimilarity(Vector3 A, Vector3 B)
    {
        // Dot product of A and B
        float dotProduct = Vector3.Dot(A, B);

        // Magnitudes (lengths) of A and B
        float magnitudeA = A.magnitude;
        float magnitudeB = B.magnitude;

        // Cosine similarity formula
        float similarity = dotProduct / (magnitudeA * magnitudeB);

        return similarity;
    }
}

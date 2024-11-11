using UnityEngine;

public class TrilinearAnalysis : MonoBehaviour
{
    // Store input vectors representing the 3D directions of the analysis
    public Vector3 inputDirection1;
    public Vector3 inputDirection2;
    public Vector3 inputDirection3;

    // Multidirectional feedback coefficients (weights for each direction's influence)
    public float feedbackCoefficient1 = 1.0f;
    public float feedbackCoefficient2 = 1.0f;
    public float feedbackCoefficient3 = 1.0f;

    // Output feedback results
    private Vector3 feedbackResult;

    // For visualization, we may want to draw vectors representing the directions
    void OnDrawGizmos()
    {
        // Draw input directions as lines
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + inputDirection1); // Draw input 1

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + inputDirection2); // Draw input 2

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + inputDirection3); // Draw input 3

        // Draw the feedback result
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + feedbackResult); // Draw feedback direction
    }

    void Update()
    {
        // Perform Trilinear Analysis to compute feedback
        PerformTrilinearAnalysis();

        // Optionally apply the feedback to an object (e.g., move an object based on feedback)
        transform.position += feedbackResult * Time.deltaTime; // Apply feedback to position (for example)
    }

    void PerformTrilinearAnalysis()
    {
        // For simplicity, let's perform a linear combination of the directions with feedback coefficients
        Vector3 weightedDirection1 = inputDirection1 * feedbackCoefficient1;
        Vector3 weightedDirection2 = inputDirection2 * feedbackCoefficient2;
        Vector3 weightedDirection3 = inputDirection3 * feedbackCoefficient3;

        // Sum the weighted directions to compute the total feedback
        feedbackResult = weightedDirection1 + weightedDirection2 + weightedDirection3;

        // Optionally, apply a normalization or further transformation based on the analysis
        feedbackResult = feedbackResult.normalized; // Normalize feedback for consistent magnitude
    }

    // Method to dynamically update input directions
    public void UpdateInputDirections(Vector3 newDirection1, Vector3 newDirection2, Vector3 newDirection3)
    {
        inputDirection1 = newDirection1;
        inputDirection2 = newDirection2;
        inputDirection3 = newDirection3;
    }

    // Method to adjust feedback coefficients dynamically
    public void UpdateFeedbackCoefficients(float newCoefficient1, float newCoefficient2, float newCoefficient3)
    {
        feedbackCoefficient1 = newCoefficient1;
        feedbackCoefficient2 = newCoefficient2;
        feedbackCoefficient3 = newCoefficient3;
    }
}

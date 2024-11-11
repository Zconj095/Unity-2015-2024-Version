using UnityEngine;

public class HyperDimensionalVectorDotProduct : MonoBehaviour
{
    [SerializeField] private HyperDimensionalVector hv1;  // First HyperDimensionalVector reference
    [SerializeField] private HyperDimensionalVector hv2;  // Second HyperDimensionalVector reference

    private void Start()
    {
        // Check if both HyperDimensionalVector references are assigned
        if (hv1 == null || hv2 == null)
        {
            Debug.LogError("HyperDimensionalVectors are not assigned for dot product calculation.");
            return;
        }

        // Perform the dot product calculation and log the result
        float dotProduct = CalculateDotProduct();
        Debug.Log("Dot Product: " + dotProduct);
    }

    // Calculate dot product between hv1 and hv2 based on available dimensions
    private float CalculateDotProduct()
    {
        float dot = 0;
        int limit = Mathf.Min(hv1.components.Length, hv2.components.Length) * 4;  // Assumes 4D vectors per component

        for (int i = 0; i < limit; i++)
        {
            dot += GetDimension(hv1, i) * GetDimension(hv2, i);
        }

        return dot;
    }

    // Helper function to access individual dimension values in each HyperDimensionalVector
    private float GetDimension(HyperDimensionalVector vector, int index)
    {
        int vectorIndex = index / 4;
        int componentIndex = index % 4;

        if (vectorIndex >= vector.components.Length)
            return 0;

        Vector4 vec = vector.components[vectorIndex];

        // Standard switch-case for C# 7.3
        switch (componentIndex)
        {
            case 0: return vec.x;
            case 1: return vec.y;
            case 2: return vec.z;
            case 3: return vec.w;
            default: return 0;
        }
    }
}

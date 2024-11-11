using UnityEngine;

public class HyperDimensionalVectorManager : MonoBehaviour
{
    [SerializeField] private HyperDimensionalVector hv1;  // Expose to Inspector
    [SerializeField] private HyperDimensionalVector hv2;  // Expose to Inspector

    private void Awake()
    {
        // Add HyperDimensionalVector components and expose them in the Inspector
        hv1 = gameObject.AddComponent<HyperDimensionalVector>();
        hv2 = gameObject.AddComponent<HyperDimensionalVector>();

        // Initialize each HyperDimensionalVector with a specified number of dimensions
        hv1.Initialize(8);
        hv2.Initialize(8);
    }

    private void Start()
    {
        // Set some example values for hv1 and hv2
        hv1.SetDimension(0, 1.0f);
        hv1.SetDimension(4, 0.5f);
        hv1.Normalize();

        hv2.SetDimension(0, 0.7f);
        hv2.SetDimension(4, 0.8f);

        // Calculate the dot product of hv1 and hv2
        float dotProduct = hv1.DotProduct(hv2);
        Debug.Log("Dot Product: " + dotProduct);
    }
}

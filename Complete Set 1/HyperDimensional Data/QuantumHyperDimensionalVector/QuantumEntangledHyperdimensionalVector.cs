using UnityEngine;

public class QuantumEntangledHyperdimensionalVector : MonoBehaviour
{
    [SerializeField] private HyperDimensionalVector masterVector;  // Reference to the main vector
    private HyperDimensionalVector entangledVector;                // The entangled vector that mirrors masterVector

    private void Awake()
    {
        if (masterVector == null)
        {
            Debug.LogError("Master Vector is not assigned.");
            return;
        }

        // Initialize the entangled vector as a clone of the master vector's data
        entangledVector = CloneHyperDimensionalVector(masterVector);
    }

    private void Update()
    {
        // Continuously update entangledVector to stay in sync with masterVector
        SyncVectors();
    }

    // Method to clone HyperDimensionalVector data (not referencing the same object)
    private HyperDimensionalVector CloneHyperDimensionalVector(HyperDimensionalVector source)
    {
        HyperDimensionalVector clone = new GameObject("EntangledVector").AddComponent<HyperDimensionalVector>();
        clone.components = new Vector4[source.components.Length];

        for (int i = 0; i < source.components.Length; i++)
        {
            clone.components[i] = source.components[i];
        }

        return clone;
    }

    // Synchronize entangled vector with master vector to simulate entanglement
    private void SyncVectors()
    {
        if (masterVector == null || entangledVector == null)
        {
            Debug.LogWarning("One of the vectors is not properly initialized.");
            return;
        }

        for (int i = 0; i < masterVector.components.Length; i++)
        {
            entangledVector.components[i] = masterVector.components[i];
        }
    }

    // Access entangled vector for external use
    public HyperDimensionalVector GetEntangledVector()
    {
        return entangledVector;
    }
}

using UnityEngine;

public class SemiconductorSimulation : MonoBehaviour
{
    private SemiconductorGrid grid;
    private SemiConductorHopfieldNetwork hopfieldNetwork;
    private SemiConductorBayesianUpdater bayesianUpdater;

    public float externalFactor = 0.5f; // Simulated external factor
    private bool isInitialized = false;

    void Start()
    {
        grid = GetComponent<SemiconductorGrid>();
        if (grid == null)
        {
            Debug.LogError("SemiconductorGrid component is missing.");
            return;
        }

        hopfieldNetwork = gameObject.AddComponent<SemiConductorHopfieldNetwork>();
        bayesianUpdater = gameObject.AddComponent<SemiConductorBayesianUpdater>();
    }

    void Update()
    {
        // Wait for grid nodes to initialize before proceeding
        if (!isInitialized)
        {
            if (grid.nodes == null || grid.nodes.Count == 0)
            {
                Debug.LogError("Waiting for SemiconductorGrid nodes to initialize.");
                return;
            }

            // Initialize the network and updater once grid nodes are ready
            hopfieldNetwork.Initialize(grid.nodes);
            bayesianUpdater.Initialize(hopfieldNetwork);
            isInitialized = true;
            Debug.Log("SemiconductorSimulation initialized successfully.");
        }

        // Perform updates after initialization
        hopfieldNetwork.UpdateNetwork();
        bayesianUpdater.UpdateWeights(externalFactor);
    }
}

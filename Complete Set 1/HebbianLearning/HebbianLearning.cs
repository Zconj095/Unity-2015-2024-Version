using UnityEngine;

public class HebbianLearning : MonoBehaviour
{
    public int numNeurons = 5; // Number of neurons
    public float learningRate = 0.1f; // Learning rate (η)
    private float[,] weights; // Weights matrix for neuron-to-neuron connections
    private int[] neuronActivations; // Array of neuron activations (binary)
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the neuron activations and weights
        neuronActivations = new int[numNeurons];
        weights = new float[numNeurons, numNeurons];
        
        // Initialize weights to small random values
        InitializeWeights();
    }

    // Update is called once per frame
    void Update()
    {
        // Generate random neuron activations (you can replace this with actual data)
        for (int i = 0; i < numNeurons; i++)
        {
            neuronActivations[i] = Random.Range(0, 2); // Binary activation (0 or 1)
        }

        // Apply Hebbian learning rule to update the weights
        ApplyHebbianLearning();

        // Debug: Print the activations and weights
        PrintNeurons();
    }

    // Initialize weights to small random values
    void InitializeWeights()
    {
        for (int i = 0; i < numNeurons; i++)
        {
            for (int j = 0; j < numNeurons; j++)
            {
                if (i != j) // No self-connections
                {
                    weights[i, j] = Random.Range(-0.1f, 0.1f); // Small random initial weight
                }
            }
        }
    }

    // Apply Hebbian learning to update weights
    void ApplyHebbianLearning()
    {
        for (int i = 0; i < numNeurons; i++)
        {
            for (int j = 0; j < numNeurons; j++)
            {
                if (i != j) // No self-connections
                {
                    // Hebbian learning rule: Δw_ij = η * x_i * x_j
                    weights[i, j] += learningRate * neuronActivations[i] * neuronActivations[j];
                }
            }
        }
    }

    // Debug function to print the activations and weights
    void PrintNeurons()
    {
        string activationsStr = "Neuron Activations: ";
        for (int i = 0; i < numNeurons; i++) activationsStr += neuronActivations[i] + " ";
        Debug.Log(activationsStr);

        string weightsStr = "Weights Matrix: \n";
        for (int i = 0; i < numNeurons; i++)
        {
            for (int j = 0; j < numNeurons; j++)
            {
                weightsStr += string.Format("{0:F2} ", weights[i, j]);
            }
            weightsStr += "\n";
        }
        Debug.Log(weightsStr);
    }
}

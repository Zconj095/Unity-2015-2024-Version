using UnityEngine;

public class NeuralNetworkBackPropagation : MonoBehaviour
{
    // Network architecture parameters
    public int numInputNodes = 3;  // Number of input nodes (features)
    public int numHiddenNodes = 4; // Number of hidden nodes
    public int numOutputNodes = 1; // Number of output nodes (target)

    // Learning rate (η)
    public float learningRate = 0.1f;

    // Weights and biases
    private float[,] inputHiddenWeights;  // Weights between input and hidden layers
    private float[] hiddenBiases;         // Biases for hidden layer
    private float[,] hiddenOutputWeights; // Weights between hidden and output layers
    private float[] outputBiases;        // Biases for output layer

    // Activation function (Sigmoid)
    private float Sigmoid(float x)
    {
        return 1f / (1f + Mathf.Exp(-x));
    }

    // Derivative of Sigmoid
    private float SigmoidDerivative(float x)
    {
        return x * (1f - x);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize weights and biases
        InitializeNetwork();

        // Training loop (using dummy data)
        TrainNetwork();
    }

    // Initialize the weights and biases with small random values
    void InitializeNetwork()
    {
        inputHiddenWeights = new float[numInputNodes, numHiddenNodes];
        hiddenBiases = new float[numHiddenNodes];
        hiddenOutputWeights = new float[numHiddenNodes, numOutputNodes];
        outputBiases = new float[numOutputNodes];

        // Initialize weights with small random values
        for (int i = 0; i < numInputNodes; i++)
        {
            for (int j = 0; j < numHiddenNodes; j++)
            {
                inputHiddenWeights[i, j] = Random.Range(-0.5f, 0.5f);
            }
        }

        for (int i = 0; i < numHiddenNodes; i++)
        {
            hiddenBiases[i] = Random.Range(-0.5f, 0.5f);
        }

        for (int i = 0; i < numHiddenNodes; i++)
        {
            for (int j = 0; j < numOutputNodes; j++)
            {
                hiddenOutputWeights[i, j] = Random.Range(-0.5f, 0.5f);
            }
        }

        for (int i = 0; i < numOutputNodes; i++)
        {
            outputBiases[i] = Random.Range(-0.5f, 0.5f);
        }
    }

    // Train the network with dummy data (for testing purposes)
    void TrainNetwork()
    {
        // Dummy training data: inputs (3 features) and target outputs
        float[,] inputs = {
            { 0f, 0f, 1f },
            { 1f, 0f, 1f },
            { 0f, 1f, 1f },
            { 1f, 1f, 1f }
        };
        float[] targetOutputs = { 0f, 1f, 1f, 0f }; // XOR pattern

        int epochs = 10000; // Number of training iterations
        for (int epoch = 0; epoch < epochs; epoch++)
        {
            // Loop through the training examples
            for (int i = 0; i < inputs.GetLength(0); i++)
            {
                // Perform forward pass
                float[] inputLayer = new float[numInputNodes];
                for (int j = 0; j < numInputNodes; j++)
                {
                    inputLayer[j] = inputs[i, j];
                }

                // Hidden layer activations
                float[] hiddenLayer = new float[numHiddenNodes];
                for (int j = 0; j < numHiddenNodes; j++)
                {
                    float sum = hiddenBiases[j];
                    for (int k = 0; k < numInputNodes; k++)
                    {
                        sum += inputLayer[k] * inputHiddenWeights[k, j];
                    }
                    hiddenLayer[j] = Sigmoid(sum);
                }

                // Output layer activations
                float[] outputLayer = new float[numOutputNodes];
                for (int j = 0; j < numOutputNodes; j++)
                {
                    float sum = outputBiases[j];
                    for (int k = 0; k < numHiddenNodes; k++)
                    {
                        sum += hiddenLayer[k] * hiddenOutputWeights[k, j];
                    }
                    outputLayer[j] = Sigmoid(sum);
                }

                // Calculate error (difference between predicted and target)
                float[] outputErrors = new float[numOutputNodes];
                for (int j = 0; j < numOutputNodes; j++)
                {
                    outputErrors[j] = targetOutputs[i] - outputLayer[j];
                }

                // Backpropagation: calculate the gradients and update weights
                // Calculate output layer delta (error * derivative of sigmoid)
                float[] outputLayerDeltas = new float[numOutputNodes];
                for (int j = 0; j < numOutputNodes; j++)
                {
                    outputLayerDeltas[j] = outputErrors[j] * SigmoidDerivative(outputLayer[j]);
                }

                // Calculate hidden layer delta (propagate the error backwards)
                float[] hiddenLayerDeltas = new float[numHiddenNodes];
                for (int j = 0; j < numHiddenNodes; j++)
                {
                    float errorSum = 0f;
                    for (int k = 0; k < numOutputNodes; k++)
                    {
                        errorSum += outputLayerDeltas[k] * hiddenOutputWeights[j, k];
                    }
                    hiddenLayerDeltas[j] = errorSum * SigmoidDerivative(hiddenLayer[j]);
                }

                // Update output layer weights and biases
                for (int j = 0; j < numOutputNodes; j++)
                {
                    for (int k = 0; k < numHiddenNodes; k++)
                    {
                        hiddenOutputWeights[k, j] += learningRate * hiddenLayer[k] * outputLayerDeltas[j];
                    }
                    outputBiases[j] += learningRate * outputLayerDeltas[j];
                }

                // Update hidden layer weights and biases
                for (int j = 0; j < numHiddenNodes; j++)
                {
                    for (int k = 0; k < numInputNodes; k++)
                    {
                        inputHiddenWeights[k, j] += learningRate * inputLayer[k] * hiddenLayerDeltas[j];
                    }
                    hiddenBiases[j] += learningRate * hiddenLayerDeltas[j];
                }
            }
        }

        Debug.Log("Training complete!");
    }
}

using UnityEngine;
using System.Collections.Generic;

public class QuantumSuperposition : MonoBehaviour
{
    public class QuantumState
    {
        public string stateName;
        public ComplexNumber amplitude;

        public QuantumState(string name, ComplexNumber amp)
        {
            stateName = name;
            amplitude = amp;
        }
    }

    private List<QuantumState> states;
    private ComplexNumber[,] unitaryMatrix;
    private bool isCollapsed = false;
    private QuantumState collapsedState;

    void Start()
    {
        // Initialize states with complex amplitudes
        states = new List<QuantumState>
        {
            new QuantumState("State A", ComplexNumber.Create(0.5, 0.5)),
            new QuantumState("State B", ComplexNumber.Create(0.5, -0.5)),
            new QuantumState("State C", ComplexNumber.Create(0.5, 0.5)),
            new QuantumState("State D", ComplexNumber.Create(0.5, -0.5))
        };

        NormalizeStateVector();

        // Define a unitary matrix (4x4 Hadamard-like transformation)
        unitaryMatrix = new ComplexNumber[4, 4];
        unitaryMatrix[0, 0] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[0, 1] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[0, 2] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[0, 3] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[1, 0] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[1, 1] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[1, 2] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[1, 3] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[2, 0] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[2, 1] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[2, 2] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[2, 3] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[3, 0] = ComplexNumber.Create(0.5, 0);
        unitaryMatrix[3, 1] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[3, 2] = ComplexNumber.Create(-0.5, 0);
        unitaryMatrix[3, 3] = ComplexNumber.Create(0.5, 0);
    }

    void NormalizeStateVector()
    {
        double totalAmplitudeSquared = 0;
        foreach (var state in states)
        {
            totalAmplitudeSquared += state.amplitude.GetMagnitude() * state.amplitude.GetMagnitude();
        }

        double normalizationFactor = Mathf.Sqrt((float)totalAmplitudeSquared);
        for (int i = 0; i < states.Count; i++)
        {
            ComplexNumber normalizedAmplitude = ComplexNumber.Divide(states[i].amplitude, normalizationFactor);
            states[i] = new QuantumState(states[i].stateName, normalizedAmplitude);
        }
    }

    void ApplyUnitaryTransformation()
    {
        List<ComplexNumber> newAmplitudes = new List<ComplexNumber>();

        for (int i = 0; i < states.Count; i++)
        {
            ComplexNumber newAmplitude = ComplexNumber.Create(0, 0);
            for (int j = 0; j < states.Count; j++)
            {
                ComplexNumber product = ComplexNumber.Multiply(unitaryMatrix[i, j], states[j].amplitude);
                newAmplitude = ComplexNumber.Add(newAmplitude, product);
            }
            newAmplitudes.Add(newAmplitude);
        }

        for (int i = 0; i < states.Count; i++)
        {
            states[i] = new QuantumState(states[i].stateName, newAmplitudes[i]);
        }

        NormalizeStateVector();
        Debug.Log("Applied unitary transformation, new amplitudes calculated.");
    }

    void Collapse()
    {
        if (isCollapsed) return;

        double cumulativeProbability = 0;
        double randomValue = UnityEngine.Random.Range(0f, 1f);

        foreach (var state in states)
        {
            double probability = state.amplitude.GetMagnitude() * state.amplitude.GetMagnitude();
            cumulativeProbability += probability;

            if (randomValue <= cumulativeProbability)
            {
                collapsedState = state;
                isCollapsed = true;
                Debug.Log("Collapsed to state: " + collapsedState.stateName);
                break;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collapse();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            ApplyUnitaryTransformation();
        }
    }
}

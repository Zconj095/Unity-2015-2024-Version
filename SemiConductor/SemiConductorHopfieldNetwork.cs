using System.Collections.Generic;
using UnityEngine;

public class SemiConductorHopfieldNetwork : MonoBehaviour
{
    public int nodeCount;
    public float[,] weights;
    public List<SemiConductorNode> nodes;

    public void Initialize(List<SemiConductorNode> gridNodes)
    {
        nodes = gridNodes;
        nodeCount = nodes.Count;
        weights = new float[nodeCount, nodeCount];

        for (int i = 0; i < nodeCount; i++)
        {
            for (int j = 0; j < nodeCount; j++)
            {
                if (i != j)
                    weights[i, j] = Random.Range(-1f, 1f); // Initialize weights randomly
            }
        }
    }

    public void UpdateNetwork()
    {
        for (int i = 0; i < nodeCount; i++)
        {
            float input = 0;
            for (int j = 0; j < nodeCount; j++)
            {
                input += weights[i, j] * nodes[j].chargeState;
            }
            nodes[i].UpdateChargeState(input);
        }
    }
}

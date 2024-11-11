using System.Collections.Generic;
using UnityEngine;

public class SemiconductorGrid : MonoBehaviour
{
    public int gridSize = 10;
    public GameObject nodePrefab;
    public List<SemiConductorNode> nodes;

    void Awake() // Changed from Start to Awake
    {
        nodes = new List<SemiConductorNode>();
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject nodeObj = Instantiate(nodePrefab, new Vector3(x, y, 0), Quaternion.identity);
                SemiConductorNode node = nodeObj.AddComponent<SemiConductorNode>();
                node.Initialize(Random.Range(-1f, 1f)); // Random initial charge
                nodes.Add(node);
            }
        }
    }
}

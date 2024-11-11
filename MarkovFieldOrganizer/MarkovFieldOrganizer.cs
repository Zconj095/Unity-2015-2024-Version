using UnityEngine;

public class MarkovFieldOrganizer : MonoBehaviour
{
    public GameObject nodePrefab;
    public int gridWidth = 5;
    public int gridHeight = 5;
    public int numStates = 3;
    private MarkovFieldNode[,] nodes;

    void Start()
    {
        nodes = new MarkovFieldNode[gridWidth, gridHeight];
        GenerateGrid();
        ConnectNeighbors();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector2 position = new Vector2(x, y);
                GameObject nodeObject = Instantiate(nodePrefab, position, Quaternion.identity);
                MarkovFieldNode node = nodeObject.GetComponent<MarkovFieldNode>();
                node.InitializeProbabilities(numStates);  // Pass numStates here
                nodes[x, y] = node;
            }
        }
    }

    void ConnectNeighbors()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                MarkovFieldNode node = nodes[x, y];
                
                // Connect with neighboring nodes
                if (x > 0) node.AddNeighbor(nodes[x - 1, y]);
                if (x < gridWidth - 1) node.AddNeighbor(nodes[x + 1, y]);
                if (y > 0) node.AddNeighbor(nodes[x, y - 1]);
                if (y < gridHeight - 1) node.AddNeighbor(nodes[x, y + 1]);
            }
        }
    }
}

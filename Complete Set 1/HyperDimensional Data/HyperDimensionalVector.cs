using UnityEngine;

public class HyperDimensionalVector : MonoBehaviour
{
    public Vector4[] components;
    private int dimensions;
    
    // Initialize the HyperDimensionalVector with the desired number of dimensions
    public void Initialize(int dimensions)
    {
        this.dimensions = dimensions;
        int numVectors = Mathf.CeilToInt(dimensions / 4.0f); // Calculate required Vector4 components
        components = new Vector4[numVectors];
    }

    public void SetDimension(int index, float value)
    {
        int vectorIndex = index / 4;
        int componentIndex = index % 4;

        if (vectorIndex >= components.Length)
            return;

        Vector4 vec = components[vectorIndex];
        switch (componentIndex)
        {
            case 0: vec.x = value; break;
            case 1: vec.y = value; break;
            case 2: vec.z = value; break;
            case 3: vec.w = value; break;
        }
        components[vectorIndex] = vec;
    }

    public float GetDimension(int index)
    {
        int vectorIndex = index / 4;
        int componentIndex = index % 4;

        if (vectorIndex >= components.Length)
            return 0;

        Vector4 vec = components[vectorIndex];
        switch (componentIndex)
        {
            case 0: return vec.x;
            case 1: return vec.y;
            case 2: return vec.z;
            case 3: return vec.w;
            default: return 0;
        }
    }

    public void Normalize()
    {
        float magnitude = 0;
        foreach (Vector4 vec in components)
        {
            magnitude += vec.sqrMagnitude;
        }
        magnitude = Mathf.Sqrt(magnitude);

        if (magnitude > 0)
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i] /= magnitude;
            }
        }
    }

    public float DotProduct(HyperDimensionalVector other)
    {
        float dot = 0;
        int limit = Mathf.Min(this.dimensions, other.dimensions);

        for (int i = 0; i < limit; i++)
        {
            dot += this.GetDimension(i) * other.GetDimension(i);
        }
        return dot;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Class to represent a data point (vector) in the index
public class VectorDataPoint
{
    public float[] Features { get; set; }

    public VectorDataPoint(float[] features)
    {
        Features = features;
    }

    // Override the ToString method for easier debugging
    public override string ToString()
    {
        return "[" + string.Join(", ", Features.Select(f => f.ToString()).ToArray()) + "]";
    }
}
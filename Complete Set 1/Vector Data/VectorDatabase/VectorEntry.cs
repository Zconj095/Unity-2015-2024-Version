using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorEntry
{
    public Vector3 Position;
    public string ID;  // Unique identifier for each vector

    public VectorEntry(Vector3 position, string id)
    {
        Position = position;
        ID = id;
    }
}

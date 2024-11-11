using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorDatabase : MonoBehaviour
{
    private List<VectorEntry> vectorEntries;

    void Start()
    {
        vectorEntries = new List<VectorEntry>();
    }

    // Add a vector to the database
    public void AddVector(Vector3 position, string id)
    {
        vectorEntries.Add(new VectorEntry(position, id));
    }

    // Remove a vector by ID
    public bool RemoveVector(string id)
    {
        var entry = vectorEntries.Find(v => v.ID == id);
        if (entry != null)
        {
            vectorEntries.Remove(entry);
            return true;
        }
        return false;
    }

    // Query vectors within a certain radius
    public List<VectorEntry> QueryNearbyVectors(Vector3 queryPoint, float radius)
    {
        List<VectorEntry> result = new List<VectorEntry>();
        foreach (var entry in vectorEntries)
        {
            if (Vector3.Distance(entry.Position, queryPoint) <= radius)
            {
                result.Add(entry);
            }
        }
        return result;
    }
}

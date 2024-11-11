using UnityEngine;
using System.Collections.Generic;

public class MultiLocationalOriginBasis : MonoBehaviour
{
    // Structure for representing an origin in space
    [System.Serializable]
    public struct Origin
    {
        public string name; // Name of the origin (e.g., "Origin1", "Origin2", etc.)
        public Vector3 position; // Position of the origin in world space
        public Quaternion rotation; // Rotation of the origin (i.e., orientation)
    }

    public List<Origin> origins; // List of multiple origins in the scene
    public Transform objectToTransform; // Object to transform based on origin
    public string currentOriginName = "Origin1"; // The origin we're working with

    void Start()
    {
        // Initialize origins at different locations for testing purposes
        origins = new List<Origin>
        {
            new Origin { name = "Origin1", position = new Vector3(0, 0, 0), rotation = Quaternion.identity },
            new Origin { name = "Origin2", position = new Vector3(10, 0, 0), rotation = Quaternion.Euler(0, 45, 0) },
            new Origin { name = "Origin3", position = new Vector3(-10, 0, 10), rotation = Quaternion.Euler(0, 90, 0) }
        };

        // Initially set the object to be relative to the first origin
        SetObjectRelativeToOrigin(currentOriginName);
    }

    void Update()
    {
        // Update the position and rotation of the object relative to the chosen origin every frame
        SetObjectRelativeToOrigin(currentOriginName);
    }

    // Function to set the object's position and rotation relative to a specific origin
    void SetObjectRelativeToOrigin(string originName)
    {
        // Fetch the origin by name
        Origin? origin = GetOriginByName(originName);

        // Check if the origin was found
        if (origin.HasValue)
        {
            // Get the position and rotation of the found origin
            Vector3 originPosition = origin.Value.position;
            Quaternion originRotation = origin.Value.rotation;

            // Set the position of the object relative to the chosen origin
            Vector3 relativePosition = objectToTransform.position - originPosition;

            // Transform the object's position into the new coordinate system of the origin
            objectToTransform.position = originPosition + relativePosition;

            // Apply the rotation of the origin to the object
            objectToTransform.rotation = originRotation;

            // Log the successful transformation of the object
            Debug.Log("Object set relative to " + originName + ": Position " + objectToTransform.position + ", Rotation " + objectToTransform.rotation);
        }
        else
        {
            // Log an error if the origin is not found
            Debug.LogError("Origin not found: " + originName);
        }
    }

    // Get origin by name
    Origin? GetOriginByName(string name)
    {
        // Search for the origin with the given name
        foreach (Origin origin in origins)
        {
            if (origin.name == name)
            {
                return origin; // Return the origin if found
            }
        }
        return null; // Return null if the origin is not found
    }

    // Method to add a new origin
    public void AddOrigin(string name, Vector3 position, Quaternion rotation)
    {
        // Add the new origin to the list
        origins.Add(new Origin { name = name, position = position, rotation = rotation });

        // Log the addition of the new origin
        Debug.Log("New origin added: " + name);
    }

    // Method to switch to another origin
    public void SwitchOrigin(string newOriginName)
    {
        // Check if the origin exists
        if (GetOriginByName(newOriginName).HasValue)
        {
            // If the origin is valid, switch to it
            currentOriginName = newOriginName;

            // Log the successful switch
            Debug.Log("Switched to origin: " + newOriginName);
        }
        else
        {
            // If the origin is not found, log an error
            Debug.LogError("Origin not found: " + newOriginName);
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class FuzzyRecognition : MonoBehaviour
{
    public struct FuzzySet
    {
        public string name;
        public float degreeOfMembership; // Fuzzy membership degree (0 to 1)

        public FuzzySet(string n, float degree)
        {
            name = n;
            degreeOfMembership = Mathf.Clamp01(degree); // Ensure that degree is always between 0 and 1
        }
    }

    private List<FuzzySet> fuzzySets; // To hold fuzzy recognition states
    private float recognitionThreshold = 0.7f; // Threshold to consider something recognized
    private float distanceFactor = 5f; // Maximum distance for recognition
    private float similarityFactor = 0.8f; // Factor for comparing similarity of fuzzy states

    // List to simulate objects or states in the scene (using their tags and distance)
    private List<GameObject> objectsInScene;

    void Start()
    {
        // Initialize objects in the scene (as an example, we will use random distances and similarity values)
        objectsInScene = new List<GameObject>
        {
            new GameObject("Object1"),
            new GameObject("Object2"),
            new GameObject("Object3")
        };

        // Assign some random positions to the objects
        foreach (var obj in objectsInScene)
        {
            obj.transform.position = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(0f, 10f),
                Random.Range(-10f, 10f)
            );

            // Assign a tag based on some logic (ensure tags "Recognizable" and "Unrecognizable" are defined in Unity Editor)
            obj.tag = (Random.Range(0, 2) == 0) ? "Recognizable" : "Unrecognizable";
        }

        // Initialize fuzzy sets for recognition with predefined fuzzy logic
        fuzzySets = new List<FuzzySet>
        {
            new FuzzySet("Recognizable", 0.0f),
            new FuzzySet("Unrecognizable", 0.0f)
        };

        // Ensure that the number of fuzzy sets matches the number of objects in the scene
        if (fuzzySets.Count < objectsInScene.Count)
        {
            // Add more fuzzy sets if needed
            int additionalSets = objectsInScene.Count - fuzzySets.Count;
            for (int i = 0; i < additionalSets; i++)
            {
                fuzzySets.Add(new FuzzySet("Undefined", 0.0f));
            }
        }
    }

    void Update()
    {
        // Check fuzzy recognition for objects
        for (int i = 0; i < objectsInScene.Count; i++)
        {
            var obj = objectsInScene[i];
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            float similarity = CalculateSimilarity(obj);

            // Update the degree of membership (fuzzy recognition) based on distance and similarity
            fuzzySets[i] = new FuzzySet(obj.name, GetRecognitionDegree(distance, similarity));

            // Log the fuzzy set recognition for this object using string concatenation (for compatibility)
            Debug.Log("Recognition degree for " + obj.name + " (Tag: " + obj.tag + ") is: " + fuzzySets[i].degreeOfMembership);
        }

        // Check for recognition results
        CheckRecognition();
    }

    // Function to calculate recognition degree (fuzzy membership degree)
    float GetRecognitionDegree(float distance, float similarity)
    {
        float distanceDegree = Mathf.Clamp01(1 - (distance / distanceFactor)); // Distance effect
        float similarityDegree = Mathf.Clamp01(similarity); // Similarity effect

        // Combine distance and similarity to get the fuzzy membership degree
        return Mathf.Min(distanceDegree, similarityDegree); // Combine the effects using the minimum
    }

    // Function to calculate similarity based on the tag (or other properties)
    float CalculateSimilarity(GameObject obj)
    {
        // Here, we're using the tag to determine similarity
        if (obj.tag == "Recognizable")
        {
            return 1f; // Fully similar if the tag matches
        }
        else if (obj.tag == "Unrecognizable")
        {
            return 0.3f; // Low similarity if tag is "Unrecognizable"
        }

        return 0.0f; // Default similarity if no tag matches
    }

    // Function to check if an object is recognized (based on the fuzzy recognition degree)
    void CheckRecognition()
    {
        foreach (var fuzzySet in fuzzySets)
        {
            if (fuzzySet.degreeOfMembership >= recognitionThreshold)
            {
                // Log recognition using string concatenation
                Debug.Log(fuzzySet.name + " is recognized with a degree of " + fuzzySet.degreeOfMembership);
                // Perform recognition action (e.g., triggering an event)
                // Here, we can add any behavior based on recognized objects
            }
        }
    }
}

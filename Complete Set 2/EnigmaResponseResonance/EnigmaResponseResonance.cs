using UnityEngine;

public class EnigmaResponseResonance : MonoBehaviour
{
    // Resonance parameters (could represent frequencies, intensity, etc.)
    public float resonanceFrequency = 1.0f;  // Resonance base frequency
    private float currentResonanceStrength = 0f;  // Strength of resonance effect
    private bool isResonating = false;  // Whether resonance is active

    // Materials or Game Objects that react to resonance
    public Material resonanceMaterial;  // Material to change during resonance (for visual feedback)
    public AudioSource resonanceAudioSource;  // Audio source to play sound effects
    private Renderer objectRenderer;  // Renderer to access material

    // Enigma puzzle elements (could be anything that triggers the resonance)
    public GameObject puzzleObject;  // The object that will start the resonance
    private float puzzleInteractionThreshold = 0.75f;  // Threshold when interaction is strong enough to trigger resonance

    // Timer for controlling resonance
    private float resonanceTimer = 0f;
    private float resonanceDuration = 5f;  // How long the resonance effect lasts

    void Start()
    {
        // Initialize the object renderer to change materials and start with default state
        objectRenderer = GetComponent<Renderer>();

        if (resonanceAudioSource != null)
        {
            resonanceAudioSource.Stop();
        }
    }

    void Update()
    {
        // Simulate resonance based on puzzle object interaction (replace with actual logic)
        float interactionStrength = GetPuzzleInteractionStrength();

        // Check if the interaction crosses the threshold to activate resonance
        if (interactionStrength > puzzleInteractionThreshold)
        {
            isResonating = true;
        }
        else
        {
            isResonating = false;
        }

        // Update resonance strength if resonance is active
        if (isResonating)
        {
            // Increase resonance strength based on time
            currentResonanceStrength = Mathf.PingPong(Time.time * resonanceFrequency, 1.0f);

            // Change the material properties based on resonance strength
            ApplyResonanceEffects(currentResonanceStrength);

            // Optionally, play a sound or make other dynamic changes
            if (!resonanceAudioSource.isPlaying)
            {
                resonanceAudioSource.Play();
            }

            // Keep track of the duration of resonance
            resonanceTimer += Time.deltaTime;
            if (resonanceTimer >= resonanceDuration)
            {
                // Stop the resonance after a set duration
                isResonating = false;
                resonanceTimer = 0f;
                resonanceAudioSource.Stop();
            }
        }
        else
        {
            // If not resonating, reset the material effects and stop audio
            ResetResonanceEffects();
        }
    }

    // Simulate the puzzle interaction (replace with actual game logic)
    float GetPuzzleInteractionStrength()
    {
        // Example logic: Return a value between 0 and 1 based on interaction or input
        // This could be based on proximity, user input, or any other mechanic
        return Mathf.PingPong(Time.time * 0.5f, 1.0f);  // For demo purposes, simulate interaction with time
    }

    // Apply effects like changing materials or visuals during resonance
    void ApplyResonanceEffects(float strength)
    {
        if (resonanceMaterial != null)
        {
            // Adjust material properties based on the resonance strength
            resonanceMaterial.SetFloat("_Glossiness", strength);
            resonanceMaterial.SetColor("_Color", Color.Lerp(Color.white, Color.blue, strength));
        }

        // Other potential resonance effects could include manipulating lighting, sound, or physics
    }

    // Reset the effects when resonance stops
    void ResetResonanceEffects()
    {
        if (resonanceMaterial != null)
        {
            // Reset material properties to the default state
            resonanceMaterial.SetFloat("_Glossiness", 0f);
            resonanceMaterial.SetColor("_Color", Color.white);
        }
    }
}

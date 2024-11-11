using UnityEngine;

public class BioHapticGenreCreation : MonoBehaviour
{
    // Simulated bio-signal data (heart rate for example)
    private float heartRate = 70f;  // Example initial value in beats per minute
    private bool isGameIntense = false;

    // Haptic feedback strength (for vibration or wearable feedback)
    private float hapticStrength = 0f;

    // The target range for heart rate that triggers a change in game dynamics
    private const float LowHeartRateThreshold = 60f;
    private const float HighHeartRateThreshold = 100f;

    // Haptic vibration controller (assuming we use Unity's vibration system)
    private HapticController hapticController;

    void Start()
    {
        // Try to get the HapticController component attached to this GameObject
        hapticController = GetComponent<HapticController>(); 

        // Ensure the hapticController is correctly initialized, otherwise print an error
        if (hapticController == null)
        {
            Debug.LogError("HapticController component not found. Please make sure it's attached to the GameObject.");
        }
    }

    void Update()
    {
        // Simulate bio-signal input (replace this with actual bio-signal acquisition)
        heartRate = GetHeartRateData();

        // Check if the game should change based on the player's bio-feedback
        AdjustGameDynamicsBasedOnHeartRate();

        // Provide haptic feedback based on current intensity (game tension level)
        ProvideHapticFeedback();
    }

    // Simulate the acquisition of heart rate (replace with actual sensor input)
    float GetHeartRateData()
    {
        // In a real case, this would be replaced with actual data from a sensor
        return Mathf.PingPong(Time.time * 10f, HighHeartRateThreshold * 2);  // Simulated fluctuation
    }

    // Adjust the game dynamics based on heart rate
    void AdjustGameDynamicsBasedOnHeartRate()
    {
        if (heartRate < LowHeartRateThreshold)
        {
            // Relax the game mechanics if heart rate is low
            isGameIntense = false;
            // Slow down character speed or make the game more relaxing
        }
        else if (heartRate > HighHeartRateThreshold)
        {
            // Increase tension if heart rate is high
            isGameIntense = true;
            // Speed up character or trigger stressful events
        }
        else
        {
            // Normal gameplay mode
            isGameIntense = false;
            // Keep normal gameplay dynamics
        }
    }

    // Provide haptic feedback based on intensity level
    void ProvideHapticFeedback()
    {
        // Check if hapticController is null before proceeding
        if (hapticController == null)
        {
            Debug.LogError("HapticController is null. Cannot provide haptic feedback.");
            return; // Exit if hapticController is not available
        }

        // If the game is intense (high heart rate), provide stronger haptic feedback
        if (isGameIntense)
        {
            hapticStrength = Mathf.InverseLerp(HighHeartRateThreshold, 120f, heartRate);
        }
        else
        {
            hapticStrength = Mathf.InverseLerp(LowHeartRateThreshold, HighHeartRateThreshold, heartRate);
        }

        // Apply haptic feedback (vibration or wearable feedback)
        hapticController.Vibrate(hapticStrength);
    }
}

// Placeholder class for haptic feedback controller (you would need actual code for devices)
public class HapticController : MonoBehaviour
{
    // Function to vibrate the device based on a strength value
    public void Vibrate(float strength)
    {
        // Example for mobile vibration (or can be adapted to other devices)
        if (Application.isMobilePlatform)
        {
            Handheld.Vibrate();
        }

        // If using external haptic device, send the 'strength' value to trigger vibration accordingly
        // Example: device.SendVibration(strength);
    }
}

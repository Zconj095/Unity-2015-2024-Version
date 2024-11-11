using UnityEngine;
using System;
using System.Threading;
using System.Collections;
public class TripleFrequencyRelayDelay : MonoBehaviour
{
    // Frequency thresholds for detection
    private const float FrequencyA_Min = 50f;
    private const float FrequencyA_Max = 60f;

    private const float FrequencyB_Min = 100f;
    private const float FrequencyB_Max = 120f;

    private const float FrequencyC_Min = 150f;
    private const float FrequencyC_Max = 170f;

    // Time delay between frequency checks (in seconds)
    private const float DelayTime = 2.0f;

    // States for detecting each frequency
    private bool isFrequencyADetected = false;
    private bool isFrequencyBDetected = false;
    private bool isFrequencyCDetected = false;

    // Track elapsed time to simulate actual real-time delay
    private DateTime lastCheckedTime;
    
    // Relay state
    private bool isRelayActivated = false;

    void Start()
    {
        // Initialize the last checked time to the current time
        lastCheckedTime = DateTime.Now;
    }

    // Method to simulate the detection of frequencies
    public void DetectFrequencies(float freqA, float freqB, float freqC)
    {
        // Check if the required delay time has passed since the last check
        if ((DateTime.Now - lastCheckedTime).TotalSeconds >= DelayTime)
        {
            Debug.Log("Time for frequency check has passed, starting detection...");

            // Check if Frequency A is within the defined range
            if (!isFrequencyADetected && freqA >= FrequencyA_Min && freqA <= FrequencyA_Max)
            {
                isFrequencyADetected = true;
                Debug.Log("Frequency A detected: " + freqA + " Hz");
                lastCheckedTime = DateTime.Now;  // Reset the time after detecting the first frequency
            }

            // Check if Frequency B is within the defined range, and only after A has been detected
            if (isFrequencyADetected && !isFrequencyBDetected && freqB >= FrequencyB_Min && freqB <= FrequencyB_Max)
            {
                isFrequencyBDetected = true;
                Debug.Log("Frequency B detected: " + freqB + " Hz");
                lastCheckedTime = DateTime.Now;  // Reset the time after detecting the second frequency
            }

            // Check if Frequency C is within the defined range, and only after B has been detected
            if (isFrequencyBDetected && !isFrequencyCDetected && freqC >= FrequencyC_Min && freqC <= FrequencyC_Max)
            {
                isFrequencyCDetected = true;
                Debug.Log("Frequency C detected: " + freqC + " Hz");
                lastCheckedTime = DateTime.Now;  // Reset the time after detecting the third frequency
            }

            // After all frequencies have been detected, activate the relay
            if (isFrequencyADetected && isFrequencyBDetected && isFrequencyCDetected && !isRelayActivated)
            {
                ActivateRelay();
            }
        }
        else
        {
            Debug.Log("Waiting for delay... " + (DelayTime - (DateTime.Now - lastCheckedTime).TotalSeconds).ToString("F2") + " seconds remaining.");
        }
    }

    // Method to simulate the activation of the relay
    private void ActivateRelay()
    {
        // Simulate the activation of the relay by setting the flag to true and printing a message
        isRelayActivated = true;
        Debug.Log("Relay activated after all frequencies detected with delay!");
        // Add your logic for what happens when the relay is activated (e.g., triggering an event, sending a signal)
    }

    // Method to reset the detection process (in case we want to start fresh)
    public void ResetDetection()
    {
        isFrequencyADetected = false;
        isFrequencyBDetected = false;
        isFrequencyCDetected = false;
        isRelayActivated = false;
        Debug.Log("Detection process has been reset.");
    }

    void Update()
    {
        // Simulate continuous frequency updates and detection (this loop can be modified as needed)
        for (int i = 0; i < 10; i++) // Limit the number of iterations for testing purposes
        {
            // Generate random frequency values for testing
            float freqA = UnityEngine.Random.Range(40f, 80f); // Simulate frequency A between 40 Hz and 80 Hz
            float freqB = UnityEngine.Random.Range(90f, 130f); // Simulate frequency B between 90 Hz and 130 Hz
            float freqC = UnityEngine.Random.Range(140f, 180f); // Simulate frequency C between 140 Hz and 180 Hz

            Debug.Log("Iteration " + (i + 1) + " - Detected Frequencies: A = " + freqA + " Hz, B = " + freqB + " Hz, C = " + freqC + " Hz");

            // Detect frequencies
            DetectFrequencies(freqA, freqB, freqC);

            // Sleep for 1 second to simulate real-time processing
            Thread.Sleep(1000);  // This is the line that caused the error, now fixed
        }

        Debug.Log("End of simulation.");
    }
}

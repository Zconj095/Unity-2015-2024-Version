using UnityEngine;

public class CrossCorrelation1D : MonoBehaviour
{
    // Example signals (or image data)
    public float[] signal1 = { 1f, 2f, 3f, 4f, 5f };   // First signal
    public float[] signal2 = { 5f, 4f, 3f, 2f, 1f };   // Second signal (to correlate with the first)

    void Start()
    {
        // Compute Cross-Correlation for the example signals
        float[] result = ComputeCrossCorrelation1D(signal1, signal2);

        // Output the result
        Debug.Log("Cross Correlation Result:");
        foreach (float value in result)
        {
            Debug.Log(value);
        }
    }

    // Function to compute the cross-correlation between two 1D signals
    public float[] ComputeCrossCorrelation1D(float[] signal1, float[] signal2)
    {
        int length = signal1.Length + signal2.Length - 1;  // Length of the output correlation
        float[] correlation = new float[length];            // Array to store the result

        // Normalize the second signal (optional)
        signal2 = NormalizeSignal(signal2);

        // Compute the cross-correlation (signal1 * signal2)
        for (int i = 0; i < length; i++)
        {
            correlation[i] = 0f;

            for (int j = 0; j < signal1.Length; j++)
            {
                int signal2Index = i - j;

                // Check for index validity (i.e., within bounds of signal2)
                if (signal2Index >= 0 && signal2Index < signal2.Length)
                {
                    correlation[i] += signal1[j] * signal2[signal2Index];
                }
            }
        }

        return correlation;
    }

    // Function to normalize a signal (optional)
    private float[] NormalizeSignal(float[] signal)
    {
        float sum = 0f;
        foreach (float val in signal)
        {
            sum += val;
        }

        // Normalize to the mean of the signal
        float mean = sum / signal.Length;

        for (int i = 0; i < signal.Length; i++)
        {
            signal[i] -= mean;
        }

        return signal;
    }
}

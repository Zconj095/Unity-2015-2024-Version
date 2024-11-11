using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CrossCorrelation2D : MonoBehaviour
{
    // Using List<List<float>> for 2D data, which is serialized and visible in the Inspector.
    [Header("2D Image")]
    public List<List<float>> image1 = new List<List<float>> {
        new List<float>{ 1f, 2f, 3f },
        new List<float>{ 4f, 5f, 6f },
        new List<float>{ 7f, 8f, 9f }
    };

    [Header("2D Kernel")]
    public List<List<float>> kernel = new List<List<float>> {
        new List<float>{ 1f, 0f },
        new List<float>{ 0f, -1f }
    };

    void Start()
    {
        // Convert Lists to 2D arrays for processing
        float[,] image1Array = ConvertTo2DArray(image1);
        float[,] kernelArray = ConvertTo2DArray(kernel);

        // Compute Cross-Correlation for the 2D image and kernel
        float[,] result = ComputeCrossCorrelation2D(image1Array, kernelArray);

        // Output the result to the console (optional)
        Debug.Log("2D Cross Correlation Result:");
        for (int i = 0; i < result.GetLength(0); i++)
        {
            string row = "";
            for (int j = 0; j < result.GetLength(1); j++)
            {
                row += result[i, j] + " ";
            }
            Debug.Log(row);
        }
    }

    // Convert List<List<float>> to 2D array for processing
    private float[,] ConvertTo2DArray(List<List<float>> list)
    {
        int rows = list.Count;
        int cols = list[0].Count;
        float[,] array = new float[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = list[i][j];
            }
        }

        return array;
    }

    // Function to compute the 2D cross-correlation between an image and a kernel
    public float[,] ComputeCrossCorrelation2D(float[,] image, float[,] kernel)
    {
        int imageRows = image.GetLength(0);
        int imageCols = image.GetLength(1);
        int kernelRows = kernel.GetLength(0);
        int kernelCols = kernel.GetLength(1);

        int outputRows = imageRows - kernelRows + 1;
        int outputCols = imageCols - kernelCols + 1;

        float[,] correlation = new float[outputRows, outputCols];

        // Perform the 2D cross-correlation
        for (int i = 0; i < outputRows; i++)
        {
            for (int j = 0; j < outputCols; j++)
            {
                float sum = 0f;
                for (int m = 0; m < kernelRows; m++)
                {
                    for (int n = 0; n < kernelCols; n++)
                    {
                        sum += image[i + m, j + n] * kernel[m, n];
                    }
                }
                correlation[i, j] = sum;
            }
        }

        return correlation;
    }
}

using UnityEngine;

public class BilinearInterpolation : MonoBehaviour
{
    // Reference to the texture you want to sample from
    public Texture2D texture;

    void Start()
    {
        // Example of how to use bilinear interpolation
        Vector2 samplePoint = new Vector2(0.5f, 0.5f); // Coordinates to sample (could be any floating-point value)
        Color color = BilinearSample(texture, samplePoint);
        Debug.Log("Sampled color: " + color);
    }

    // Bilinear interpolation function
    Color BilinearSample(Texture2D texture, Vector2 point)
    {
        // Get the integer coordinates of the four surrounding pixels
        int x0 = Mathf.FloorToInt(point.x * texture.width);
        int y0 = Mathf.FloorToInt(point.y * texture.height);
        int x1 = Mathf.Min(x0 + 1, texture.width - 1); // Ensure we don't go out of bounds
        int y1 = Mathf.Min(y0 + 1, texture.height - 1);

        // Get the fractional offsets within the pixel (relative to the pixel grid)
        float u = point.x * texture.width - x0;
        float v = point.y * texture.height - y0;

        // Sample the four neighboring pixels
        Color c00 = texture.GetPixel(x0, y0);
        Color c10 = texture.GetPixel(x1, y0);
        Color c01 = texture.GetPixel(x0, y1);
        Color c11 = texture.GetPixel(x1, y1);

        // Perform the bilinear interpolation
        Color interpolatedColor = 
            (1 - u) * (1 - v) * c00 +
            u * (1 - v) * c10 +
            (1 - u) * v * c01 +
            u * v * c11;

        return interpolatedColor;
    }
}

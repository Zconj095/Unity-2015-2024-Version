using System;

public class DataPoint
{
    public float[] Features;  // Example: Array of input features
    public int Label;         // Example: Integer representing class label

    // Constructor for easy creation
    public DataPoint(float[] features, int label)
    {
        Features = features;
        Label = label;
    }
}

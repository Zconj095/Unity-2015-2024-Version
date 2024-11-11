using System.Collections.Generic;
using UnityEngine;

public class SVMTrainingSample
{
    public float[] Features { get; private set; }
    public int Label { get; private set; }

    public SVMTrainingSample(float[] features, int label)
    {
        Features = features;
        Label = label;
    }
}

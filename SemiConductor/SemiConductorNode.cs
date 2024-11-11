using UnityEngine;

public class SemiConductorNode : MonoBehaviour
{
    public float chargeState; // -1 or 1 for binary Hopfield

    public void Initialize(float initialCharge)
    {
        chargeState = Mathf.Sign(initialCharge);
    }

    public void UpdateChargeState(float input)
    {
        chargeState = Mathf.Sign(input); // Simplified activation
    }
}

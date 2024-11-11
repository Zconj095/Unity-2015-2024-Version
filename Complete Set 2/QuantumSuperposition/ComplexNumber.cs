using UnityEngine;

public class ComplexNumber : MonoBehaviour
{
    public double Real;
    public double Imaginary;

    // Initialize method to set values
    public void Initialize(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    // Static method to create new ComplexNumber instance as GameObject
    public static ComplexNumber Create(double real, double imaginary)
    {
        GameObject go = new GameObject("ComplexNumber");
        ComplexNumber complex = go.AddComponent<ComplexNumber>();
        complex.Initialize(real, imaginary);
        return complex;
    }

    // Addition operation
    public static ComplexNumber Add(ComplexNumber a, ComplexNumber b)
    {
        return Create(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    // Multiplication operation
    public static ComplexNumber Multiply(ComplexNumber a, ComplexNumber b)
    {
        return Create(
            a.Real * b.Real - a.Imaginary * b.Imaginary,
            a.Real * b.Imaginary + a.Imaginary * b.Real
        );
    }

    // Division by scalar
    public static ComplexNumber Divide(ComplexNumber a, double scalar)
    {
        return Create(a.Real / scalar, a.Imaginary / scalar);
    }

    // Get the magnitude for probability calculations
    public double GetMagnitude()
    {
        return Mathf.Sqrt((float)(Real * Real + Imaginary * Imaginary));
    }

    public override string ToString()
    {
        return "(" + Real + " + " + Imaginary + "i)";
    }
}

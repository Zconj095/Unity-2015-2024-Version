using UnityEngine;
using System;

public class GradientAscent : MonoBehaviour
{
    // Learning rate (step size)
    public float learningRate = 0.1f;

    // The number of iterations to run
    public int iterations = 100;

    // The starting point (initial guess)
    public float startingPoint = -5.0f;

    // The function we want to maximize
    private Func<float, float> objectiveFunction;

    void Start()
    {
        // Define the objective function: f(x) = - (x^2 - 4x + 3) (We aim to maximize this function)
        objectiveFunction = (x) => -(x * x - 4 * x + 3);

        // Perform Gradient Ascent
        float maxPoint = PerformGradientAscent(objectiveFunction, startingPoint, learningRate, iterations);

        // Output the result
        Debug.Log("The point of maximum value is: " + maxPoint);
        Debug.Log("The maximum value is: " + objectiveFunction(maxPoint));
    }

    // Perform Gradient Ascent on the given function
    private float PerformGradientAscent(Func<float, float> func, float startPoint, float learningRate, int iterations)
    {
        float currentPoint = startPoint;

        for (int i = 0; i < iterations; i++)
        {
            // Compute the gradient (derivative of the function)
            float gradient = ComputeGradient(func, currentPoint);

            // Update the current point
            currentPoint += learningRate * gradient;

            // Optionally, output the current point and its value
            Debug.Log("Iteration " + (i + 1) + ": x = " + currentPoint + ", f(x) = " + func(currentPoint));
        }

        return currentPoint;
    }

    // Compute the gradient of the function (numerical approximation of the derivative)
    private float ComputeGradient(Func<float, float> func, float point)
    {
        float h = 0.0001f; // A small delta for numerical differentiation
        return (func(point + h) - func(point)) / h;
    }
}

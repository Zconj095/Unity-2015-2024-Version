using UnityEngine;

public class BayesianLearning : MonoBehaviour
{
    public float priorClass0 = 0.5f; // Prior probability for Class 0
    public float priorClass1 = 0.5f; // Prior probability for Class 1

    public float likelihoodClass0 = 0.8f; // Likelihood of evidence given Class 0
    public float likelihoodClass1 = 0.2f; // Likelihood of evidence given Class 1

    public float evidence = 0.7f; // Some observed evidence (could be continuous, like a sensor reading)

    private float posteriorClass0; // Posterior probability for Class 0
    private float posteriorClass1; // Posterior probability for Class 1

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the priors and likelihoods
        CalculatePosterior();
        Debug.Log("Class 0 Posterior: " + posteriorClass0);
        Debug.Log("Class 1 Posterior: " + posteriorClass1);
    }

    // Update is called once per frame
    void Update()
    {
        // Generate new evidence (you could replace this with real data)
        evidence = Random.Range(0f, 1f); // Random evidence between 0 and 1
        Debug.Log("New Evidence: " + evidence);

        // Recalculate the posterior probabilities
        CalculatePosterior();

        // Debug: Output the updated posterior probabilities
        Debug.Log("Updated Class 0 Posterior: " + posteriorClass0);
        Debug.Log("Updated Class 1 Posterior: " + posteriorClass1);

        // Decide which class has the highest posterior probability
        if (posteriorClass0 > posteriorClass1)
        {
            Debug.Log("Class 0 is more probable.");
        }
        else
        {
            Debug.Log("Class 1 is more probable.");
        }
    }

    // Calculate the posterior probabilities using Bayes' Theorem
    void CalculatePosterior()
    {
        // Calculate the marginal likelihood P(E) (normalization factor)
        float marginalLikelihood = (likelihoodClass0 * priorClass0) + (likelihoodClass1 * priorClass1);

        // Calculate posterior for Class 0
        posteriorClass0 = (likelihoodClass0 * priorClass0) / marginalLikelihood;

        // Calculate posterior for Class 1
        posteriorClass1 = (likelihoodClass1 * priorClass1) / marginalLikelihood;
    }
}

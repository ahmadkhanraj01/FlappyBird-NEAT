using UnityEngine;
using UnityEngine.UI;

public class TrainingUI : MonoBehaviour
{
    public Text generationText;
    public Text aliveText;
    public Text bestFitnessText;

    private void Update()
    {
        if (EvolutionManager.Instance == null) return;

        generationText.text = "Generation: " + EvolutionManager.Instance.Generation;
        aliveText.text = "Alive: " + EvolutionManager.Instance.AliveCount;
        bestFitnessText.text = "Best Fitness: " + EvolutionManager.Instance.BestFitness.ToString("F0");
    }
}

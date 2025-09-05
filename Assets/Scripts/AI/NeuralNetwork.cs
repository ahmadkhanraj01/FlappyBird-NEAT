using System;
using UnityEngine;

[Serializable]
public class NeuralNetwork
{
    public int inputCount;
    public int hiddenCount;
    public int outputCount;

    // Flattened arrays for easy serialization and copying
    public float[] weightsIH; // size = inputCount * hiddenCount
    public float[] biasH;     // size = hiddenCount
    public float[] weightsHO; // size = hiddenCount * outputCount (outputCount is 1)
    public float[] biasO;     // size = outputCount

    public NeuralNetwork(int inputCount, int hiddenCount, int outputCount, bool randomize = true)
    {
        this.inputCount = inputCount;
        this.hiddenCount = hiddenCount;
        this.outputCount = outputCount;

        weightsIH = new float[inputCount * hiddenCount];
        biasH = new float[hiddenCount];
        weightsHO = new float[hiddenCount * outputCount];
        biasO = new float[outputCount];

        if (randomize)
            Randomize();
    }

    public void Randomize()
    {
        for (int i = 0; i < weightsIH.Length; i++) weightsIH[i] = UnityEngine.Random.Range(-1f, 1f);
        for (int i = 0; i < biasH.Length; i++) biasH[i] = UnityEngine.Random.Range(-1f, 1f);
        for (int i = 0; i < weightsHO.Length; i++) weightsHO[i] = UnityEngine.Random.Range(-1f, 1f);
        for (int i = 0; i < biasO.Length; i++) biasO[i] = UnityEngine.Random.Range(-1f, 1f);
    }

    // Feedforward - returns single output (0..1)
    public float FeedForward(float[] inputs)
    {
        // hidden activations
        float[] hidden = new float[hiddenCount];
        for (int h = 0; h < hiddenCount; h++)
        {
            float sum = 0f;
            for (int i = 0; i < inputCount; i++)
            {
                sum += inputs[i] * weightsIH[i * hiddenCount + h];
            }
            sum += biasH[h];
            hidden[h] = (float)Math.Tanh(sum); // hidden activation
        }

        // output
        float[] outputs = new float[outputCount];
        for (int o = 0; o < outputCount; o++)
        {
            float sum = 0f;
            for (int h = 0; h < hiddenCount; h++)
            {
                sum += hidden[h] * weightsHO[h * outputCount + o];
            }
            sum += biasO[o];
            // Sigmoid for output (0..1)
            outputs[o] = 1f / (1f + (float)Math.Exp(-sum));
        }

        return outputs[0];
    }

    public NeuralNetwork Clone()
    {
        NeuralNetwork clone = new NeuralNetwork(inputCount, hiddenCount, outputCount, false);
        Array.Copy(weightsIH, clone.weightsIH, weightsIH.Length);
        Array.Copy(biasH, clone.biasH, biasH.Length);
        Array.Copy(weightsHO, clone.weightsHO, weightsHO.Length);
        Array.Copy(biasO, clone.biasO, biasO.Length);
        return clone;
    }

    // Simple uniform crossover
    public static NeuralNetwork Crossover(NeuralNetwork a, NeuralNetwork b)
    {
        NeuralNetwork child = new NeuralNetwork(a.inputCount, a.hiddenCount, a.outputCount, false);

        System.Random rnd = new System.Random();
        for (int i = 0; i < a.weightsIH.Length; i++)
            child.weightsIH[i] = (rnd.NextDouble() < 0.5) ? a.weightsIH[i] : b.weightsIH[i];
        for (int i = 0; i < a.biasH.Length; i++)
            child.biasH[i] = (rnd.NextDouble() < 0.5) ? a.biasH[i] : b.biasH[i];
        for (int i = 0; i < a.weightsHO.Length; i++)
            child.weightsHO[i] = (rnd.NextDouble() < 0.5) ? a.weightsHO[i] : b.weightsHO[i];
        for (int i = 0; i < a.biasO.Length; i++)
            child.biasO[i] = (rnd.NextDouble() < 0.5) ? a.biasO[i] : b.biasO[i];

        return child;
    }

    // Mutate - small gaussian/random perturbations
    public void Mutate(float mutationRate, float mutationStrength)
    {
        for (int i = 0; i < weightsIH.Length; i++)
            if (UnityEngine.Random.value < mutationRate)
                weightsIH[i] += UnityEngine.Random.Range(-mutationStrength, mutationStrength);

        for (int i = 0; i < biasH.Length; i++)
            if (UnityEngine.Random.value < mutationRate)
                biasH[i] += UnityEngine.Random.Range(-mutationStrength, mutationStrength);

        for (int i = 0; i < weightsHO.Length; i++)
            if (UnityEngine.Random.value < mutationRate)
                weightsHO[i] += UnityEngine.Random.Range(-mutationStrength, mutationStrength);

        for (int i = 0; i < biasO.Length; i++)
            if (UnityEngine.Random.value < mutationRate)
                biasO[i] += UnityEngine.Random.Range(-mutationStrength, mutationStrength);
    }
}

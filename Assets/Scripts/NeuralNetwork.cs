using UnityEngine;

public class NeuralNetwork
{
    public int inputNodes = 3;
    public int hiddenNodes = 5;
    public int outputNodes = 3; // 0: Shoot, 1: Move, 2: Dodge

    public float[,] weightsInputHidden;
    public float[,] weightsHiddenOutput;
    public float learningRate = 0.1f;

    // NEW: Setup function instead of a constructor (fixes the Random error)
    public void Initialize()
    {
        weightsInputHidden = new float[inputNodes, hiddenNodes];
        weightsHiddenOutput = new float[hiddenNodes, outputNodes];
        InitializeWeights();
    }

    public void InitializeWeights()
    {
        for (int i = 0; i < inputNodes; i++)
            for (int j = 0; j < hiddenNodes; j++)
                weightsInputHidden[i, j] = Random.Range(-1f, 1f);

        for (int i = 0; i < hiddenNodes; i++)
            for (int j = 0; j < outputNodes; j++)
                weightsHiddenOutput[i, j] = Random.Range(-1f, 1f);
    }

    public float[] ForwardPass(float[] inputs)
    {
        float[] hidden = new float[hiddenNodes];
        for (int j = 0; j < hiddenNodes; j++)
        {
            float sum = 0;
            for (int i = 0; i < inputNodes; i++)
                sum += inputs[i] * weightsInputHidden[i, j];
            hidden[j] = (float)System.Math.Tanh(sum);
        }

        float[] outputs = new float[outputNodes];
        for (int k = 0; k < outputNodes; k++)
        {
            float sum = 0;
            for (int j = 0; j < hiddenNodes; j++)
                sum += hidden[j] * weightsHiddenOutput[j, k];
            outputs[k] = (float)System.Math.Tanh(sum);
        }
        return outputs;
    }

    public void Learn(float[] inputs, int chosenAction, float reward)
    {
        float[] hidden = new float[hiddenNodes];
        for (int j = 0; j < hiddenNodes; j++)
        {
            float sum = 0;
            for (int i = 0; i < inputNodes; i++)
                sum += inputs[i] * weightsInputHidden[i, j];
            hidden[j] = (float)System.Math.Tanh(sum);
        }

        for (int j = 0; j < hiddenNodes; j++)
        {
            float update = learningRate * reward * inputs[0] * hidden[j]; 
            weightsHiddenOutput[j, chosenAction] += update;
            weightsHiddenOutput[j, chosenAction] = Mathf.Clamp(weightsHiddenOutput[j, chosenAction], -5f, 5f);
        }
    }
    
    public float[] FlattenWeights() 
    { 
        float[] flat = new float[(inputNodes * hiddenNodes) + (hiddenNodes * outputNodes)];
        int index = 0;
        for (int i = 0; i < inputNodes; i++)
            for (int j = 0; j < hiddenNodes; j++)
                flat[index++] = weightsInputHidden[i, j];

        for (int i = 0; i < hiddenNodes; i++)
            for (int j = 0; j < outputNodes; j++)
                flat[index++] = weightsHiddenOutput[i, j];

        return flat;
    }
    
    public void LoadWeights(float[] flat) 
    { 
        int index = 0;
        for (int i = 0; i < inputNodes; i++)
            for (int j = 0; j < hiddenNodes; j++)
                weightsInputHidden[i, j] = flat[index++];

        for (int i = 0; i < hiddenNodes; i++)
            for (int j = 0; j < outputNodes; j++)
                weightsHiddenOutput[i, j] = flat[index++];
    }
}
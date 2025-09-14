using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Accord;
using Accord.Neuro;
using Accord.MachineLearning;
using Accord.Neuro.Learning;
using System;

public class NeuralNetworkAccord : MonoBehaviour
{

    private void Start()
    {
        // Dane wej�ciowe i wyj�ciowe (prawda logiczna XOR)
        double[][] inputs =
        {
            new double[] { 0, 0 },
            new double[] { 0, 1 },
            new double[] { 1, 0 },
            new double[] { 1, 1 }
        };

        double[][] outputs =
        {
            new double[] { 0 },
            new double[] { 1 },
            new double[] { 1 },
            new double[] { 0 }
        };

        // Tworzenie sieci neuronowej: 2 wej�cia -> 2 neurony w warstwie ukrytej -> 1 wyj�cie
        var network = new ActivationNetwork(
            function: new SigmoidFunction(),
            inputsCount: 2,
            neuronsCount: new[] { 2, 1 });

        // Inicjalizacja wag losowymi warto�ciami
        new NguyenWidrow(network).Randomize();

        // Tworzymy obiekt ucz�cy
        var teacher = new BackPropagationLearning(network)
        {
            LearningRate = 0.1
        };

        // Trening
        double error;
        int epoch = 0;
        do
        {
            error = teacher.RunEpoch(inputs, outputs);
            epoch++;
            Debug.Log($"Epoch {epoch} - Error: {error:F4}");
        }
        while (error > 0.01 && epoch < 10000);

        double[][] testInput =
        {
            new double[] { 1, 0 }
        };

        // Testowanie wynik�w
        Debug.Log("\n== Test Results ==");
        foreach (var input in testInput)
        {
            double[] result = network.Compute(input);
            Debug.Log($"{input[0]} XOR {input[1]} = {Math.Round(result[0])} ({result[0]:F4})");
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Accord;
using Accord.Neuro;
using Accord.MachineLearning;
using Accord.Neuro.Learning;
using System;
using Accord.Math;

public class HeroNeuralNetwork : MonoBehaviour
{
    ActivationNetwork network;

    private GameObject _hero;
    private HeroInfo _heroInfo;
    private HeroStateManager _heroStateManager;
    private void Start()
    {
        _hero = gameObject;
        _heroInfo = _hero.GetComponent<HeroInfo>();
        _heroStateManager = _hero.GetComponent<HeroStateManager>();

        // Dane wejœciowe i wyjœciowe 
        // Atak, Strona, Pozycja, Odleglosc
        // Atak: 0, 1, 2, 3
        // Strona: lewo - 0, prawo - 1
        // Pozycja: za - 0, przed - 1
        // Odleglosc: dotyk - 0, blisko - 1, daleko - 2
        double[][] inputs =
        {
            // gdy nie atakuje
            // atak
            new double[] { 0, 0, 0, 0 },
            new double[] { 0, 1, 0, 0 },
            new double[] { 0, 0, 1, 0 },
            new double[] { 0, 1, 1, 0 },
            // ruch w lewo
            new double[] { 0, 1, 0, 1 },
            new double[] { 0, 1, 1, 1 },
            new double[] { 0, 1, 0, 2 },
            new double[] { 0, 1, 1, 2 },
            // ruch w prawo
            new double[] { 0, 0, 0, 1 },
            new double[] { 0, 0, 1, 1 },
            new double[] { 0, 0, 0, 2 },
            new double[] { 0, 0, 1, 2 },

            // gdy atak 1
            // atak
            new double[] { 1, 0, 0, 0 },
            new double[] { 1, 1, 0, 0 },
            // stanie
            new double[] { 1, 0, 1, 2 }, // nauka
            new double[] { 1, 1, 1, 2 }, // nauka
            // lewo
            
            new double[] { 1, 1, 0, 2 },
            new double[] { 1, 1, 1, 0 }, // nauka
            // prawo
           
            new double[] { 1, 0, 0, 2 },
            new double[] { 1, 0, 1, 0 }, // nauka
            // unik L
            new double[] { 1, 0, 1, 1 }, // nauka
            // unik P
            new double[] { 1, 1, 1, 1 }, // nauka
            
            // gdy atak 2
            // atak
            new double[] { 2, 0, 0, 1 },
            new double[] { 2, 1, 0, 1 },
            // skok
            new double[] { 2, 0, 1, 1 }, // nauka
            new double[] { 2, 0, 1, 0 }, // nauka
            new double[] { 2, 0, 1, 2 }, // nauka
            new double[] { 2, 1, 1, 1 }, // nauka
            // stanie
            new double[] { 2, 1, 1, 0 }, // nauka
            new double[] { 2, 1, 1, 2 }, // nauka
            // lewo
            new double[] { 2, 1, 0, 2 },
            // prawo
            new double[] { 2, 0, 0, 2 },

            // gdy atak 3 
            // stanie
            new double[] { 3, 0, 0, 2 }, // nauka
            new double[] { 3, 0, 1, 2 }, // nauka
            new double[] { 3, 1, 0, 2 }, // nauka
            new double[] { 3, 1, 1, 2 }, // nauka
            // unik L
            new double[] { 3, 0, 0, 1 }, // nauka
            new double[] { 3, 0, 1, 1 }, // nauka
            // unik P 
            new double[] { 3, 1, 0, 1 }, // nauka
            new double[] { 3, 1, 1, 1 }, // nauka
        };


        // stanie lewo prawo unikL unikP skok atak 
        double[][] outputs =
        {
            // gdy atak 0
            // atak
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            // lewo
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            // prawo
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            
            // gdy atak 1
            // atak
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            // stanie
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            // lewo
           
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            // prawo
            
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            // unik L
            new double[] { 0, 0, 0, 1, 0, 0, 0 },
            // unik P
            new double[] { 0, 0, 0, 0, 1, 0, 0 },
            
            // gdy atak 2
            // atak
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            // skok
            new double[] { 0, 0, 0, 0, 0, 1, 0 },
            new double[] { 0, 0, 0, 0, 0, 1, 0 },
            new double[] { 0, 0, 0, 0, 0, 1, 0 },
            new double[] { 0, 0, 0, 0, 0, 1, 0 },
            // stanie
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            // lewo
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            // prawo
            new double[] { 0, 0, 1, 0, 0, 0, 0 },

            // gdy atak 3
            // stanie
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            // unik L
            new double[] { 0, 0, 0, 1, 0, 0, 0 },
            new double[] { 0, 0, 0, 1, 0, 0, 0 },
            // unik P
            new double[] { 0, 0, 0, 0, 1, 0, 0 },
            new double[] { 0, 0, 0, 0, 1, 0, 0 },
        };

        // Tworzenie sieci neuronowej: 4 wejscia -> 10 neuronow w 3 warstwach ukrytych -> 7 wyjsc
        network = new ActivationNetwork(
            function: new SigmoidFunction(),
            inputsCount: 4,
            neuronsCount: new[] { 10, 10, 10 , 7 });

        // Inicjalizacja wag losowymi wartoœciami (Gaussian distribution)
        new NguyenWidrow(network).Randomize();

        // Tworzymy obiekt ucz¹cy
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
        }
        while (error > 0.01 && epoch < 10000);
        Debug.Log($"Epoch {epoch} - Error: {error:F4}");

    }

    private void Update()
    {
        // tablica z danymi wejsciowymi do przetworzenia
        double[] gameInput = _heroInfo.GetHeroInfo();

        // obliczenie wyniku za pomoca sieci
        double[] result = network.Compute(gameInput);

        // zaokraglanie
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = Math.Round(result[i]);
        }
        // ktory neuron wyjsciowy zostal aktywowany
        int res = result.IndexOf(1);
        //Debug.Log($"{gameInput[0]} {gameInput[1]} {gameInput[2]} {gameInput[3]}");

        // wypisanie danych wejsciowych i wyjsciowych
        string txt = "";
        foreach (var item in result)
        {
            txt += item.ToString() + " ";
        }
        //print(txt);

        // przeslanie do npc akcji do wykonania
        _heroStateManager.SetActions(result);

    }
}

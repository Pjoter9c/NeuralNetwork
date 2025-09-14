using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Accord;
using Accord.Neuro;
using Accord.MachineLearning;
using Accord.Neuro.Learning;
using System;
using Accord.Math;
using JetBrains.Annotations;

public class HeroNeuralNetwork : MonoBehaviour
{
    ActivationNetwork network;

    private GameObject _hero;
    private HeroInfo _heroInfo;
    private HeroStateManager _heroStateManager;
    private List<List<Data>> data;
    List<double[]> inputsList;
    List<double[]> outputsList;
    private void Start()
    {
        _hero = gameObject;
        _heroInfo = _hero.GetComponent<HeroInfo>();
        _heroStateManager = _hero.GetComponent<HeroStateManager>();
        data = gameObject.GetComponent<ReadData>().datas;

        inputsList = new List<double[]>();
        outputsList = new List<double[]>();

        StartLearning();
        
        /*
        // Output and Input data
        // AttackType, Side, Orientation, Distance
        // AttackType: 0, 1, 2, 3
        // Side: left - 0, right - 1
        // Orientation: behind - 0, front - 1
        // Distance: near - 0, close - 1, far - 2

        // Inputs
        double[][] inputs =
        {
            // when attack 0
            // attack
            new double[] { 0, 0, 0, 0 },
            new double[] { 0, 1, 0, 0 },
            new double[] { 0, 0, 1, 0 },
            new double[] { 0, 1, 1, 0 },
            // left
            new double[] { 0, 1, 0, 1 },
            new double[] { 0, 1, 1, 1 },
            new double[] { 0, 1, 0, 2 },
            new double[] { 0, 1, 1, 2 },
            // right
            new double[] { 0, 0, 0, 1 },
            new double[] { 0, 0, 1, 1 },
            new double[] { 0, 0, 0, 2 },
            new double[] { 0, 0, 1, 2 },

            // attack 1
            // attack
            new double[] { 1, 0, 0, 0 },
            new double[] { 1, 1, 0, 0 },
            // stay
            new double[] { 1, 0, 1, 2 },
            new double[] { 1, 1, 1, 2 },
            // left
            
            new double[] { 1, 1, 0, 2 },
            new double[] { 1, 1, 1, 0 },
            // right
           
            new double[] { 1, 0, 0, 2 },
            new double[] { 1, 0, 1, 0 },
            // dodge L
            new double[] { 1, 0, 1, 1 },
            // dodge R
            new double[] { 1, 1, 1, 1 },
            
            // attack 2
            // attack
            new double[] { 2, 0, 0, 1 },
            new double[] { 2, 1, 0, 1 },
            // jump
            new double[] { 2, 0, 1, 1 },
            new double[] { 2, 0, 1, 0 },
            new double[] { 2, 0, 1, 2 },
            new double[] { 2, 1, 1, 1 },
            // stay
            new double[] { 2, 1, 1, 0 },
            new double[] { 2, 1, 1, 2 },
            // left
            new double[] { 2, 1, 0, 2 },
            // right
            new double[] { 2, 0, 0, 2 },

            // attack 3
            // stay
            new double[] { 3, 0, 0, 2 },
            new double[] { 3, 0, 1, 2 },
            new double[] { 3, 1, 0, 2 },
            new double[] { 3, 1, 1, 2 },
            // dodge L
            new double[] { 3, 0, 0, 1 },
            new double[] { 3, 0, 1, 1 },
            new double[] { 3, 0, 0, 0 },
            // dodge R
            new double[] { 3, 1, 0, 1 },
            new double[] { 3, 1, 1, 1 },
            new double[] { 3, 1, 0, 0 }
        };

        // Outputs
        // stay, left, right, dodgeL, dodgeR, Jump, Attack
        double[][] outputs =
        {
            // attack 0
            // attack
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            // left
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            // right
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            
            // attack 1
            // attack
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            // stay
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            // left
           
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            // right
            
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            new double[] { 0, 0, 1, 0, 0, 0, 0 },
            // dodge L
            new double[] { 0, 0, 0, 1, 0, 0, 0 },
            // dodge R
            new double[] { 0, 0, 0, 0, 1, 0, 0 },
            
            // attack 2
            // attack
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            new double[] { 0, 0, 0, 0, 0, 0, 1 },
            // jump
            new double[] { 0, 0, 0, 0, 0, 1, 0 },
            new double[] { 0, 0, 0, 0, 0, 1, 0 },
            new double[] { 0, 0, 0, 0, 0, 1, 0 },
            new double[] { 0, 0, 0, 0, 0, 1, 0 },
            // stay
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            // left
            new double[] { 0, 1, 0, 0, 0, 0, 0 },
            // right
            new double[] { 0, 0, 1, 0, 0, 0, 0 },

            // attack 3
            // stay
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            new double[] { 1, 0, 0, 0, 0, 0, 0 },
            // dodge L
            new double[] { 0, 0, 0, 1, 0, 0, 0 },
            new double[] { 0, 0, 0, 1, 0, 0, 0 },
            new double[] { 0, 0, 0, 1, 0, 0, 0 },
            // dodge R
            new double[] { 0, 0, 0, 0, 1, 0, 0 },
            new double[] { 0, 0, 0, 0, 1, 0, 0 },
            new double[] { 0, 0, 0, 0, 1, 0, 0 }
        };
        */

        /*
        // Creating Neural Network: 5 inputs -> 10 neurons in 3 hidden layers -> 7 outputs
        network = new ActivationNetwork(
            function: new SigmoidFunction(), // activation function
            inputsCount: 5, // how many input neurons
            // how many neurons in hiddens and output layers
            // for gradient
            //neuronsCount: new[] { 10, 10, 10, 7 }
            // for marquardta
            neuronsCount: new[] { 9, 7 }
        );

        // Random weight values (Gaussian distribution)
        new NguyenWidrow(network).Randomize();
        */

        // Creating learning object
        // gradient method
        /*
        var teacher = new BackPropagationLearning(network)
        {
            LearningRate = 0.1
        };
        */

        /*
        // Marquardt method
        var teacher = new LevenbergMarquardtLearning(network);

        // Training
        double error;
        int epoch = 0;
        do
        {
            error = teacher.RunEpoch(inputs, outputs);
            epoch++;
        }
        while (error > 0.01 && epoch < 10000);
        Debug.Log($"Epoch {epoch} - Error: {error:F4}");
        */
    }

    private void Update()
    {
        // Input values from game
        double[] gameInput = _heroInfo.GetHeroInfo();

        // Compute output values
        double[] result = network.Compute(gameInput);

        // Rounding
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = Math.Round(result[i]);
        }

        // Which neuron is activated
        int res = result.IndexOf(1);

        // Output computed values in console
        /*
        string txt = "Out: ";
        foreach (var item in result)
        {
            txt += item.ToString() + " ";
        }
        print(txt);
        */
        
        // Send actions to NPC
        _heroStateManager.SetActions(result);
        

    }
    public void StartLearning() { StartCoroutine(Learn()); }
    public IEnumerator Learn()
    {
        inputsList.Clear();
        outputsList.Clear();

        foreach (var attackType in data)
        {
            foreach (var condition in attackType)
            {
                // can NPC do this move
                if (condition.learned == 0)
                    continue;

                // create and add new move condition
                double[] val = new double[]
                {
                    condition.attackType,
                    condition.side,
                    condition.orientation,
                    condition.distance,
                    condition.inDmg
                };
                inputsList.Add(val);

                // create and add new move reaction
                double[] val2 = new double[]
                {
                    condition.action[0],
                    condition.action[1],
                    condition.action[2],
                    condition.action[3],
                    condition.action[4],
                    condition.action[5],
                    condition.action[6],
                };
                outputsList.Add(val2);
            }
        }

        // conver to array
        double[][] inputs = inputsList.ToArray();
        double[][] outputs = outputsList.ToArray();


        // Creating Neual Network:
        // 5 inputs -> 10 neurons in 3 hidden layers -> 7 outputs
        network = new ActivationNetwork(
            function: new SigmoidFunction(), // activation function
            inputsCount: 5, // how many input neurons
            
            // neurons in hidden and output layers

            // for gradient
            //neuronsCount: new[] { 10, 10, 10, 7 }
            
            // for marquardta
            neuronsCount: new[] { 9, 7 }
        );

        // Random weight values (Gaussian distribution)
        new NguyenWidrow(network).Randomize();

        // Creating new learning object
        // gradient method
        /*
        var teacher = new BackPropagationLearning(network)
        {
            LearningRate = 0.1
        };
        */
        // Marquardt method
        var teacher = new LevenbergMarquardtLearning(network);

        // Training
        double error;
        int epoch = 0;
        do
        {
            error = teacher.RunEpoch(inputs, outputs);
            epoch++;
            yield return null;
        }
        while (error > 0.01 && epoch < 10000);
        Debug.Log($"Epoch {epoch} - Error: {error:F4}");
    }
}

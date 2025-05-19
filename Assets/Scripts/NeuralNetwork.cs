using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    private void Start()
    {

        // Inicjowanie danych XOR z powtórzeniami
        MatrixMxN<int> X = new MatrixMxN<int>(10, 2);
        for(int i = 0;  i < 10; i++)
        {
            X[0 + i, 0] = 0;
            X[0 + i, 1] = 0;

            X[1 + i, 0] = 0;
            X[1 + i, 1] = 1;

            X[2 + i, 0] = 1;
            X[2 + i, 1] = 0;

            X[3 + i, 0] = 1;
            X[3 + i, 1] = 1;
        }

        MatrixMxN<int> Y = new MatrixMxN<int>(10, 1);
        for(int i = 0; i < 10; i++)
        {
            Y[0 + 1, 0] = 0;
            Y[1 + 1, 0] = 1;
            Y[2 + 1, 0] = 1;
            Y[3 + 1, 0] = 0;
        }

        // Inicjowanie wag i biasów
        int InputSize = X.Columns;
        int HiddenSize = 2;
        int OutputSize = Y.Columns;

        MatrixMxN<float> W1 = new MatrixMxN<float>(InputSize, HiddenSize);
        for(int i = 0; i < W1.Rows; i++)
        {
            for(int j = 0; j < W1.Columns; j++)
            {
                W1[i, j] = UnityEngine.Random.Range(0f, 1f); // Jak bêdzie mi siê chcia³o to dam tu Gaussa
            }
        }
        MatrixMxN<float> W2 = new MatrixMxN<float>(HiddenSize, OutputSize);
        for (int i = 0; i < W2.Rows; i++)
        {
            for (int j = 0; j < W2.Columns; j++)
            {
                W2[i, j] = UnityEngine.Random.Range(0f, 1f);
            }
        }
        MatrixMxN<float> B1 = new MatrixMxN<float>(1, HiddenSize);
        for (int i = 0; i < B1.Columns; i++)
            B1[0, i] = 0;
        MatrixMxN<float> B2 = new MatrixMxN<float>(1, OutputSize);
        for(int i = 0; i < B2.Columns; i++)
            B2[0, i] = 0;

        // Optymalizacja za pomoc¹ Levenberga-Marquardta
        //MatrixMxN<float> Result = LeastSquares(
        //    CostFunction(W1, B1, W2, B2, X, Y), W1, B1, W2, B2, X, Y
        //);

        // Testowanie wyuczonej sieci
        //MatrixMxN<float> Output = Forward(X, W)

    }

    // Funkcja aktywacji sigmoid
    public MatrixMxN<T> Sigmoid<T>(MatrixMxN<T> x) where T : struct
    {
        for (int i = 0; i < x.Rows; i++)
        {
            for(int j = 0; j < x.Columns; j++)
            {
                x[i, j] = (T)(object)(1 / (1 + Mathf.Exp(Convert.ToSingle(x[i, j]))));
            }
        }

        return x;
    }

    // Funkcja obliczaj¹ca wyjœcie sieci
    public MatrixMxN<T> Forward<T>(
        MatrixMxN<T> x, MatrixMxN<T> w1, MatrixMxN<T>b1, MatrixMxN<T>w2, MatrixMxN<T>b2
        ) where T : struct
    {
        MatrixMxN<T> hidden = Sigmoid(x * w1 + b1);
        MatrixMxN<T> output = Sigmoid(hidden * w2 + b2);

        return output;
    }

    // Funkcja kosztu
    public MatrixMxN<T> CostFunction<T>(
        MatrixMxN<T> w1, MatrixMxN<T> b1, MatrixMxN<T> w2, MatrixMxN<T> b2,
        MatrixMxN<T> x, MatrixMxN<T> y
        ) where T : struct
    {
        int inputSize = x.Columns;
        int hiddenSize = 2; // Zapytaæ siê czy to zawsze mo¿e byæ dowolna liczba
        int outputSize = y.Columns;

        MatrixMxN<T> output = Forward(x, w1, b1, w2, b2);
        return (output - y);
    }
}


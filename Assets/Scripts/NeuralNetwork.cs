using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AngouriMath;
using AngouriMath.Extensions;

using MathNet.Numerics.Distributions;

public class NeuralNetwork : MonoBehaviour
{
    int repeat = 1;
    private void Start()
    {

        // Macierz 40x2 zawierajaca wejscia xor powtorzone 10 razy
        var inputVal = MathS.ZeroMatrix(4 * repeat, 2);
        for(int i = 0; i < 4 * repeat; i+=4)
        {
            inputVal = inputVal.WithRow(i + 1, "[0, 1]T");
            inputVal = inputVal.WithRow(i + 2, "[1, 0]T");
            inputVal = inputVal.WithRow(i + 3, "[1, 1]T");
        }

        //Debug.Log(inputVal.ToString(multilineFormat: true));

        // Macierz 40x1 zawierajaca wyjsca xor powtorzone 10 razy
        var outputVal = MathS.ZeroMatrix(4 * repeat, 1);
        for(int i = 0; i < repeat; i++)
        {
            outputVal = outputVal.WithElement(4 * i + 1, 0, 1).WithElement(4 * i + 2, 0, 1);
        }

        //Debug.Log(outputVal.ToString(multilineFormat: true));

        int inputSize = inputVal.ColumnCount;
        int hiddenSize = 2;
        int outputSize = outputVal.ColumnCount;


        // Wagi wejsciowe losowane poprzez rozklad normalny
        var inputWeights = MathS.ZeroMatrix(inputSize, hiddenSize);
        // domyslny rozklad normalny
        var normal = new Normal(0, 1);

        for(int i = 0; i < inputSize; i++)
        {
            for(int j = 0; j < hiddenSize; j++)
            {
                // kazdy element ma losowana wartosc z rozkladu normalnego
                inputWeights = inputWeights.WithElement(i, j, normal.Sample());
            }
        }
        //Debug.Log(inputWeights.ToString(multilineFormat: true));

        // wejsciowe biasy z wartoscia 0
        var inputBias = MathS.ZeroMatrix(1, hiddenSize);

        // wagi wyjsciowe losowane poprzez rozklad normalny
        var outputWeights = MathS.ZeroMatrix(hiddenSize, outputSize);
        for(int i = 0; i < hiddenSize; i++)
        {
            for(int j = 0; j < outputSize; j++)
            {
                outputWeights = outputWeights.WithElement(i, j, normal.Sample());
            }
        }

        // wyjsciowe biasy z wartoscia 0
        var outbutBias = MathS.ZeroMatrix(1, outputSize);


        var output = Forward(inputVal, inputWeights, inputBias, outputWeights, outbutBias);

        Debug.Log(output.ToString(multilineFormat: true));

    }


    public float Sigmoid(float x)
    {
        return 1 / (1 + Mathf.Exp(-x));
    }

    public Entity.Matrix Forward(Entity.Matrix X, Entity.Matrix W1, Entity.Matrix b1, Entity.Matrix W2, Entity.Matrix b2)
    {
        var hidden = X * W1;
        //print(hidden.RowCount + " " + hidden.ColumnCount);
        for (int i = 0; i < hidden.RowCount; i++)
        {
            var row = MathS.ZeroMatrix(1, hidden.ColumnCount);
            for( int j = 0; j < hidden.ColumnCount; j++)
            {
                row = row.WithElement(0, j, hidden[i, j]);
            }

            //Debug.Log(row.ToString());
            //print(row.RowCount + " " + row.ColumnCount + " " + b1.ColumnCount);
            hidden = hidden.WithRow(i, row + b1);
        }
        for(int i = 0; i < hidden.RowCount; i++)
        {
            for(int j = 0; j < hidden.ColumnCount; j++)
            {
                hidden = hidden.WithElement(i, j, Sigmoid((float)hidden[i,j].EvalNumerical()));
            }
        }


        var output = hidden * W2;
        for(int i = 0; i < output.RowCount; i++)
        {
            var row = MathS.ZeroMatrix(1, output.ColumnCount);
            for (int j = 0; j < output.ColumnCount; j++)
            {
                row = row.WithElement(0, j, output[i, j]);
            }
            output = output.WithRow(i, row + b2);
        }
        for (int i = 0; i < output.RowCount; i++)
        {
            for (int j = 0; j < output.ColumnCount; j++)
            {
                output = output.WithElement(i, j, Sigmoid((float)output[i, j].EvalNumerical()));
            }
        }

        return output;
    }

    public Entity.Matrix CostFunction(Entity.Matrix W1, Entity.Matrix b1, Entity.Matrix W2, Entity.Matrix b2, Entity.Matrix X, Entity.Matrix y)
    {
        int inputSize = X.ColumnCount;
        int hiddenSize = 2;
        int outputSize = y.ColumnCount;

        var output = Forward(X, W1, b1, W2, b2);

        for (int i = 0; i < output.RowCount; i++)
        {
            var row = MathS.Vector(output[i]);
            output = output.WithRow(i, row - y);
        }

        return output;
    }



    /*
    private void Start()
    {

        // Inicjowanie danych XOR z powtórzeniami

        // Dane wejsciowe
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

        // Wyniki do tych danych wejsciowych
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
    */
}


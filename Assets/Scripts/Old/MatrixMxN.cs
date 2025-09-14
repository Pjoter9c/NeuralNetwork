using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using UnityEngine;

public struct MatrixMxN<T>
{
    private T[,] _matrix;

    public MatrixMxN(int rows, int columns)
    {
        _matrix = new T[rows, columns];
    }

    public T this[int row, int column]
    {
        get
        {
            if (row < 0 || row >= _matrix.GetLength(0) || column < 0 || column >= _matrix.GetLength(1))
            {
                throw new IndexOutOfRangeException("Index out of range.");
            }
            return _matrix[row, column];
        }

        set
        {
            if (row < 0 || row >= _matrix.GetLength(0) || column < 0 || column >= _matrix.GetLength(1))
            {
                throw new IndexOutOfRangeException("Indeks poza zakresem.");
            }
            _matrix[row, column] = value;
        }
    }

    public static MatrixMxN<T> operator *(MatrixMxN<T> matrix1, MatrixMxN<T> matrix2)
    {
        // Sprawdzamy, czy liczba kolumn pierwszej macierzy jest równa liczbie wierszy drugiej macierzy
        if (matrix1.Columns != matrix2.Rows)
        {
            throw new InvalidOperationException("Nie mo¿na pomno¿yæ macierzy o tych wymiarach.");
        }

        // Tworzymy wynikow¹ macierz o odpowiednich wymiarach
        MatrixMxN<T> result = new MatrixMxN<T>(matrix1.Rows, matrix2.Columns);

        // Mno¿ymy macierze
        for (int i = 0; i < matrix1.Rows; i++)
        {
            for (int j = 0; j < matrix2.Columns; j++)
            {
                T sum = (T)(object)0; // U¿ywamy dynamicznego typu, aby dodaæ i pomno¿yæ ró¿ne typy
                for (int k = 0; k < matrix1.Columns; k++)
                {
                    sum = Multiply(matrix1[i, k],matrix2[k, j]);
                }
                result[i, j] = sum; // Przypisujemy wynik do odpowiedniego miejsca w macierzy
            }
        }

        return result;
    }


    public static MatrixMxN<T> operator + (MatrixMxN<T> matrix1, MatrixMxN<T> matrix2)
    {
        if(matrix1.Columns != matrix2.Columns || (matrix1.Rows != matrix2.Rows && matrix2.Rows != 1))
            throw new InvalidOperationException("Nie mo¿na dodaæ macierzy o tych wymiarach.");
    
        MatrixMxN<T> result = new MatrixMxN<T>(matrix1.Columns, matrix1.Rows);
        if(matrix2.Rows == 1)
        {
            for(int i = 0; i < matrix1.Rows; i++)
            {
                for(int j = 0; j < matrix2.Columns; j++)
                {
                    result[i, j] = Add(matrix1[i, j], matrix2[0, j]);
                }
            }
            return result;
        }

        for (int i = 0; i < matrix1.Rows; i++)
        {
            for (int j = 0; j < matrix2.Columns; j++)
            {
                result[i, j] = Add(matrix1[i, j], matrix2[i, j]);
            }
        }
        return result;
    }

    public static MatrixMxN<T> operator - (MatrixMxN<T> matrix1, MatrixMxN<T> matrix2)
    {
        if (matrix1.Columns != matrix2.Columns || (matrix1.Rows != matrix2.Rows && matrix2.Rows != 1))
            throw new InvalidOperationException("Nie mo¿na odj¹æ macierzy o tych wymiarach.");

        MatrixMxN<T> result = new MatrixMxN<T>(matrix1.Columns, matrix1.Rows);
        if (matrix2.Rows == 1)
        {
            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix2.Columns; j++)
                {
                    result[i, j] = Substract(matrix1[i, j], matrix2[0, j]);
                }
            }
            return result;
        }

        for (int i = 0; i < matrix1.Rows; i++)
        {
            for (int j = 0; j < matrix2.Columns; j++)
            {
                result[i, j] = Substract(matrix1[i, j], matrix2[i, j]);
            }
        }
        return result;
    }

    public T[] GetRow(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= Rows)
        {
            throw new IndexOutOfRangeException("Indeks wiersza poza zakresem.");
        }

        T[] row = new T[Columns];
        for (int i = 0; i < Columns; i++)
        {
            row[i] = _matrix[rowIndex, i];
        }

        return row;
    }

    public int Rows => _matrix.GetLength(0);
    public int Columns => _matrix.GetLength(1);
    private static T Add(T a, T b)
    {
        if(typeof(T) == typeof(int))
            return (T)(object)(Convert.ToInt32(a) + Convert.ToInt32(b));
        if (typeof(T) == typeof(float))
            return (T)(object)(Convert.ToSingle(a) + Convert.ToSingle(b));
        if (typeof(T) == typeof(double))
            return (T)(object)(Convert.ToDouble(a) + Convert.ToDouble(b));
        throw new InvalidOperationException("Unsupported type for addition.");
    }
    private static T Substract(T a, T b)
    {
        if (typeof(T) == typeof(int))
            return (T)(object)(Convert.ToInt32(a) - Convert.ToInt32(b));
        if (typeof(T) == typeof(float))
            return (T)(object)(Convert.ToSingle(a) - Convert.ToSingle(b));
        if (typeof(T) == typeof(double))
            return (T)(object)(Convert.ToDouble(a) - Convert.ToDouble(b));
        throw new InvalidOperationException("Unsupported type for addition.");
    }
    private static T Multiply(T a, T b)
    {
        if (typeof(T) == typeof(int))
            return (T)(object)(Convert.ToInt32(a) * Convert.ToInt32(b));
        if (typeof(T) == typeof(float))
            return (T)(object)(Convert.ToSingle(a) * Convert.ToSingle(b));
        if (typeof(T) == typeof(double))
            return (T)(object)(Convert.ToDouble(a) * Convert.ToDouble(b));
        throw new InvalidOperationException("Unsupported type for addition.");
    }

}


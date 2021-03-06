﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Matrix {
    
    public int rows;
    public int cols;
    public double[][] data;

    public Matrix (int rows, int cols) {
        this.rows = rows;
        this.cols = cols;

        this.data = new double[this.rows][];

        for (int i = 0; i < this.rows; i++)  {
            double[] arr = new double[this.cols];

            for (int j = 0; j < this.cols; j++)  {
                arr[j] = Mathf.Floor(UnityEngine.Random.Range(0f, 10f));
            }

            data[i] = new double[arr.Length];
            data[i] = arr;
        }
    }

    public static Matrix arrayToMatrix(double[] arr) {
        Matrix matrix = new Matrix(arr.Length, 1);
        matrix.map((num, i, j) => {
            return arr[(int)i];
        });

        return matrix;
    }

    public static double[] matrixToArray(Matrix m) {
        List<double> arr = new List<double>();

        m.map((num, i, j) => {
            arr.Add(num);
            return arr[arr.Count - 1];
        });

        return arr.ToArray();
    }

    public void randomize() {
        map((num, i, j) => {
            return UnityEngine.Random.Range(-1000f, 1000f);
        });
    }

    public static Matrix crossover(Matrix a, Matrix b) {
        Matrix matrix = new Matrix(a.rows, a.cols);

		matrix.map((num, i, j) => {
            double mut = 0;
			int rnd = UnityEngine.Random.Range(0, 2);

            double elm1 = a.data[(int)i][(int)j];
            double elm2 = b.data[(int)i][(int)j];

            if (rnd == 0) {
				mut = elm1;
			} else {
				mut = elm2;
			}

            return mut;
        });

        return matrix;
    }

	public static Matrix mutation(Matrix a, Matrix b) {
		Matrix matrix = new Matrix(a.rows, a.cols);
        int rndMut = UnityEngine.Random.Range(0, 3);
        int rowIndex = UnityEngine.Random.Range(0, a.rows);
        int colIndex = UnityEngine.Random.Range(0, a.cols);

        matrix.map((num, i, j) => {
            double mut = a.data[(int)i][(int)j];

            if ((int)i == rowIndex && (int)j == colIndex) {
                switch (rndMut) {
                    case 0:
                        double elm1 = a.data[(int)i][(int)j];
                        double elm2 = b.data[(int)i][(int)j];

                        if (elm1 > elm2) {
                            mut = elm1 * UnityEngine.Random.Range((float)elm1, (float)elm2);
                        } else {
                            mut = elm2 * UnityEngine.Random.Range((float)elm2, (float)elm1);
                        }
                        break;
                    case 1:
                        mut = UnityEngine.Random.Range(-10f, 10f); // Random Value Multiply
                        if (mut > 1000f) mut = 1000f;
                        if (mut < -1000f) mut = -1000f;
                        break;
                    case 2:
                        mut += UnityEngine.Random.Range(-1000f, 1000f); // Random Value Sum
                        if (mut > 1000f) mut = 1000f;
                        if (mut < -1000f) mut = -1000f;
                        break;
                }
            }

            return mut;
        });

		return matrix;
	}

	public static Matrix map(Matrix m, Func<double, double, double, double> func) {
        Matrix matrix = new Matrix(m.rows, m.cols);

        matrix.data = m.data.Select((arr, i) => {
            return arr.Select((num, j) => {
                return func(num, i, j);
            }).ToArray();
        }).ToArray();

        return matrix;
    }

    public Matrix map(Func<double, double, double, double> func) {
        data = data.Select((arr, i) => {
            return arr.Select((num, j) => {
                return func(num, i, j);
            }).ToArray();
        }).ToArray();

        return this;
    }

    public static Matrix transpose(Matrix a) {
        Matrix matrix = new Matrix(a.cols, a.rows);
        matrix.map((num, i, j) => {
            return a.data[(int)j][(int)i];
        });

        return matrix;
    }

    //Escalar Multiply
    public static Matrix escalarMultiply(Matrix a, double escalar) {
        Matrix matrix = new Matrix(a.rows, a.cols);
        matrix.map((num, i, j) => {
            return a.data[(int)i][(int)j] * escalar;
        });

        return matrix;
    }

    //Hadamard
    public static Matrix hadamard(Matrix a, Matrix b) {
        Matrix matrix = new Matrix(a.rows, a.cols);
        matrix.map((num, i, j) => {
            return a.data[(int)i][(int)j] * b.data[(int)i][(int)j];
        });

        return matrix;
    }

    public static Matrix add(Matrix a, Matrix b) {
        Matrix matrix = new Matrix(a.rows, a.cols);
        matrix.map((num, i, j) => {
            return a.data[(int)i][(int)j] + b.data[(int)i][(int)j];
        });

        return matrix;
    }

    public static Matrix subtract(Matrix a, Matrix b) {
        Matrix matrix = new Matrix(a.rows, a.cols);
        matrix.map((num, i, j) => {
            return a.data[(int)i][(int)j] - b.data[(int)i][(int)j];
        });

        return matrix;
    }

    public static Matrix multiply (Matrix a, Matrix b) {
        Matrix matrix = new Matrix(a.rows, b.cols);

        matrix.map((num, i, j) => {
            double sum = 0;
            
            for (int k = 0; k < a.cols; k++) {
                
                double elm1 = a.data[(int)i][k];
                double elm2 = b.data[k][(int)j];

                sum += elm1 * elm2;
            }

            return sum;
        });

        return matrix;
    }
}
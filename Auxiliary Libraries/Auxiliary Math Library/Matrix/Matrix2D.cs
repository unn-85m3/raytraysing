using System;

namespace Auxiliary.MathTools
{
    /// <summary> Вещественная 2 x 2 матрица. </summary>
    [Serializable]
    public class Matrix2D : Matrix.IMatrix
    {
        #region Private Fields

        private static int size = 2;

        private float[,] values = new float[size, size];

        #endregion

        #region Constructor and Destructor

        public Matrix2D(float value)
        {
            for (int i = 0; i < size; i++)
            {
                values[i, i] = value;
            }
        }

        public Matrix2D(float[] diagonal)
        {
            for (int i = 0; i < size; i++)
            {
                values[i, i] = diagonal[i];
            }
        }

        public Matrix2D(float[,] matrix)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    values[i, j] = matrix[i, j];
                }
            }
        }

        public Matrix2D(Matrix2D matrix)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    values[i, j] = matrix[i, j];
                }
            }
        }

        #endregion

        #region Operators

        public static Matrix2D operator -(Matrix2D source)
        {
            float[,] result = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = -source[i, j];
                }
            }

            return new Matrix2D(result);
        }

        public static Matrix2D operator -(Matrix2D left, Matrix2D right)
        {
            float[,] result = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = left[i, j] - right[i, j];
                }
            }

            return new Matrix2D(result);
        }

        public static Matrix2D operator +(Matrix2D left, Matrix2D right)
        {
            float[,] result = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = left[i, j] + right[i, j];
                }
            }

            return new Matrix2D(result);
        }

        public static Matrix2D operator *(Matrix2D left, float right)
        {
            float[,] result = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = left[i, j] * right;
                }
            }

            return new Matrix2D(result);
        }

        public static Matrix2D operator *(Matrix2D left, Matrix2D right)
        {
            float[,] result = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = 0;

                    for (int k = 0; k < size; k++)
                    {
                        result[i, j] += left[i, k] * right[k, j];
                    }
                }
            }

            return new Matrix2D(result);
        }

        public static Vector2D operator *(Matrix2D left, Vector2D right)
        {
            float[] result = new float[size];

            for (int i = 0; i < size; i++)
            {
                result[i] = 0;

                for (int k = 0; k < size; k++)
                {
                    result[i] += left[i, k] * right[k];
                }
            }

            return new Vector2D(result);
        }

        public static Matrix2D operator /(Matrix2D left, float right)
        {
            float[,] result = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = left[i, j] / right;
                }
            }

            return new Matrix2D(result);
        }

        #endregion

        #region Public Methods

        public float[] ToArray()
        {
            float[] result = new float[values.Length];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i * size + j] = values[i, j];
                }
            }

            return result;
        }

        public static Matrix2D Inverse(Matrix2D source)
        {
            Matrix2D result = new Matrix2D(source);

            Matrix2D temp = Matrix2D.Zero;

            for (int k = 0; k < size; k++)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (i == k)
                        {
                            if (j == k)
                            {
                                temp[i, j] = 1.0f / result[i, j];
                            }
                            else
                            {
                                temp[i, j] = -result[i, j] / result[k, k];
                            }
                        }
                        else
                        {
                            if (j == k)
                            {
                                temp[i, j] = result[i, k] / result[k, k];
                            }
                            else
                            {
                                temp[i, j] = result[i, j] - result[k, j] * result[i, k] / result[k, k];
                            }
                        }
                    }
                }

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        result[i, j] = temp[i, j];
                    }
                }
            }

            return result;
        }

        public static Matrix2D Transpose(Matrix2D source)
        {
            Matrix2D matrix = Matrix2D.Zero;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = matrix[j, i];
                }
            }

            return matrix;
        }

        #endregion

        #region Properties

        public float this[int row, int col]
        {
            get
            {
                return values[row, col];
            }

            set
            {
                values[row, col] = value;
            }
        }

        public static Matrix2D Zero
        {
            get
            {
                return new Matrix2D(0);
            }
        }

        public static Matrix2D Identity
        {
            get
            {
                return new Matrix2D(1);
            }
        }

        #endregion
    }
}

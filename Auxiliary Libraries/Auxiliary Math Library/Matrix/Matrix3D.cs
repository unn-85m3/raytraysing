using System;

namespace Auxiliary.MathTools
{
    /// <summary> Вещественная 3 x 3 матрица. </summary>
    [Serializable]
    public class Matrix3D : Matrix.IMatrix
    {
        #region Private Fields

        private static int size = 3;

        private float[,] values = new float[size, size];

        #endregion

        #region Constructor and Destructor

        public Matrix3D(float value)
        {
            for (int i = 0; i < size; i++)
            {
                values[i, i] = value;
            }
        }

        public Matrix3D(float[] diagonal)
        {
            for (int i = 0; i < size; i++)
            {
                values[i, i] = diagonal[i];
            }
        }

        public Matrix3D(float[,] matrix)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    values[i, j] = matrix[i, j];
                }
            }
        }

        public Matrix3D(Matrix3D matrix)
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

        public static Matrix3D operator -(Matrix3D source)
        {
            float[,] result = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = -source[i, j];
                }
            }

            return new Matrix3D(result);
        }

        public static Matrix3D operator -(Matrix3D left, Matrix3D right)
        {
            float[,] result = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = left[i, j] - right[i, j];
                }
            }

            return new Matrix3D(result);
        }

        public static Matrix3D operator +(Matrix3D left, Matrix3D right)
        {
            float[,] result = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = left[i, j] + right[i, j];
                }
            }

            return new Matrix3D(result);
        }

        public static Matrix3D operator *(Matrix3D left, float right)
        {
            float[,] result = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = left[i, j] * right;
                }
            }

            return new Matrix3D(result);
        }

        public static Matrix3D operator *(Matrix3D left, Matrix3D right)
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

            return new Matrix3D(result);
        }

        public static Vector3D operator *(Matrix3D left, Vector3D right)
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

            return new Vector3D(result);
        }

        public static Matrix3D operator /(Matrix3D left, float right)
        {
            float[,] result = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = left[i, j] / right;
                }
            }

            return new Matrix3D(result);
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

        public static Matrix3D Inverse(Matrix3D source)
        {
            Matrix3D result = new Matrix3D(source);

            Matrix3D temp = Matrix3D.Zero;

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

        public static Matrix3D Transpose(Matrix3D source)
        {
            Matrix3D matrix = Matrix3D.Identity;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = matrix[j, i];
                }
            }

            return matrix;
        }

        public static Matrix3D MatrixRotateX(float angle)
        {
            Matrix3D matrix = Matrix3D.Identity;

            float cos = (float) Math.Cos(angle);
            float sin = (float) Math.Sin(angle);

            matrix[1, 1] = cos;  matrix[1, 2] = sin;
            matrix[2, 1] = -sin; matrix[2, 2] = cos;

            return matrix;
        }

        public static Matrix3D MatrixRotateY(float angle)
        {
            Matrix3D matrix = Matrix3D.Identity;

            float cos = (float) Math.Cos(angle);
            float sin = (float) Math.Sin(angle);

            matrix[0, 0] = cos; matrix[0, 2] = -sin;
            matrix[2, 0] = sin; matrix[2, 2] = cos;

            return matrix;
        }

        public static Matrix3D MatrixRotateZ(float angle)
        {
            Matrix3D matrix = Matrix3D.Identity;

            float cos = (float) Math.Cos(angle);
            float sin = (float) Math.Sin(angle);

            matrix[0, 0] = cos;  matrix[0, 1] = sin;
            matrix[1, 0] = -sin; matrix[1, 1] = cos;

            return matrix;
        }

        public static Matrix3D MatrixRotateAxes(Vector3D orientation)
        {
            return MatrixRotateZ(orientation.Z) * MatrixRotateY(orientation.Y) * MatrixRotateX(orientation.X);
        }

        public static Matrix3D MatrixScale(Vector3D factor)
        {
            Matrix3D matrix = Matrix3D.Identity;

            for (int i = 0; i < size; i++)
            {
                matrix[i, i] = factor[i];
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

        public static Matrix3D Zero
        {
            get
            {
                return new Matrix3D(0);
            }
        }

        public static Matrix3D Identity
        {
            get
            {
                return new Matrix3D(1);
            }
        }

        #endregion
    }
}

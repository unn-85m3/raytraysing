using System;

namespace Auxiliary.MathTools
{
    /// <summary> Четырехкомпонентный вещественный вектор. </summary>
    [Serializable]
    public class Vector4D
    {
        #region Public Fields

        public float X;

        public float Y;

        public float Z;

        public float W;
        
        public static readonly Vector4D Zero = new Vector4D(0.0f, 0.0f, 0.0f, 0.0f);

        public static readonly Vector4D Unit = new Vector4D(1.0f, 1.0f, 1.0f, 1.0f);

        public static readonly Vector4D I = new Vector4D(1.0f, 0.0f, 0.0f, 0.0f);

        public static readonly Vector4D J = new Vector4D(0.0f, 1.0f, 0.0f, 0.0f);

        public static readonly Vector4D K = new Vector4D(0.0f, 0.0f, 1.0f, 0.0f);
        
        public static readonly Vector4D L = new Vector4D(0.0f, 0.0f, 0.0f, 1.0f);
        
        public static readonly Vector4D Epsilon = new Vector4D(Util.Epsilon, Util.Epsilon,
                                                                  Util.Epsilon, Util.Epsilon);

        #endregion

        #region Constructor and Destructor

        public Vector4D(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4D(float[] values)
        {
            X = values[0];
            Y = values[1];
            Z = values[2];
            W = values[3];
        }

        public Vector4D(Vector4D vector)
        {
            X = vector[0];
            Y = vector[1];
            Z = vector[2];
            W = vector[3];
        }

        #endregion

        #region Operators

        public static Vector4D operator -(Vector4D value)
        {
            return new Vector4D(-value.X, -value.Y, -value.Z, -value.W);
        }

        public static Vector4D operator +(Vector4D left, Vector4D right)
        {
            return new Vector4D(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static Vector4D operator -(Vector4D left, Vector4D right)
        {
            return new Vector4D(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public static Vector4D operator *(float left, Vector4D right)
        {
            return new Vector4D(left * right.X, left * right.Y, left * right.Z, left * right.W);
        }

        public static Vector4D operator *(Vector4D left, float right)
        {
            return new Vector4D(left.X * right, left.Y * right, left.Z * right, left.W * right);
        }

        public static Vector4D operator *(Vector4D left, Vector4D right)
        {
            return new Vector4D(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
        }

        public static Vector4D operator /(Vector4D left, float right)
        {
            return new Vector4D(left.X / right, left.Y / right, left.Z / right, left.W / right);
        }

        public static Vector4D operator /(Vector4D left, Vector4D right)
        {
            return new Vector4D(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
        }

        public static bool operator <(Vector4D left, Vector4D right)
        {
            return left.X < right.X && left.Y < right.Y && left.Z < right.Z && left.W < right.W;
        }

        public static bool operator >(Vector4D left, Vector4D right)
        {
            return left.X > right.X && left.Y > right.Y && left.Z > right.Z && left.W > right.W;
        }

        public static bool operator <=(Vector4D left, Vector4D right)
        {
            return left.X <= right.X && left.Y <= right.Y && left.Z <= right.Z && left.W <= right.W;
        }

        public static bool operator >=(Vector4D left, Vector4D right)
        {
            return left.X >= right.X && left.Y >= right.Y && left.Z >= right.Z && left.W >= right.W;
        }

        public static bool operator !=(Vector4D left, Vector4D right)
        {
            return left.X != right.X || left.Y != right.Y || left.Z != right.Z || left.W != right.W;
        }

        public static bool operator ==(Vector4D left, Vector4D right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;
        }

        #endregion

        #region Public Methods

        #region Общие функции

        public static Vector4D Abs(Vector4D source)
        {
            return new Vector4D(Math.Abs(source.X), Math.Abs(source.Y),
                                Math.Abs(source.Z), Math.Abs(source.W));
        }

        public static Vector4D Sign(Vector4D source)
        {
            return new Vector4D(Math.Sign(source.X), Math.Sign(source.Y),
                                Math.Sign(source.Z), Math.Sign(source.W));
        }

        public static Vector4D Floor(Vector4D source)
        {
            return new Vector4D((float)Math.Floor(source.X), (float)Math.Floor(source.Y),
                                (float)Math.Floor(source.Z), (float)Math.Floor(source.W));
        }

        public static Vector4D Fract(Vector4D source)
        {
            return new Vector4D(source.X - (float)Math.Floor(source.X),
        	                    source.Y - (float)Math.Floor(source.Y),
                                source.Z - (float)Math.Floor(source.Z),
                                source.W - (float)Math.Floor(source.W));
        }

        public static Vector4D Ceiling(Vector4D source)
        {
            return new Vector4D((float)Math.Ceiling(source.X), (float)Math.Ceiling(source.Y),
                                (float)Math.Ceiling(source.Z), (float)Math.Ceiling(source.W));
        }

        public static Vector4D Mod(Vector4D source, float val)
        {
            return source - val * Floor(source / val);
        }

        public static Vector4D Mod(Vector4D left, Vector4D right)
        {
            return left - right * Floor(left / right);
        }

        public static Vector4D Min(Vector4D source, float val)
        {
            return new Vector4D(Math.Min(source.X, val), Math.Min(source.Y, val),
                                Math.Min(source.Z, val), Math.Min(source.W, val));
        }

        public static Vector4D Min(Vector4D left, Vector4D right)
        {
            return new Vector4D(Math.Min(left.X, right.X), Math.Min(left.Y, right.Y),
                                Math.Min(left.Z, right.Z), Math.Min(left.W, right.W));
        }

        public static Vector4D Max(Vector4D source, float val)
        {
            return new Vector4D(Math.Max(source.X, val), Math.Max(source.Y, val),
                                Math.Max(source.Z, val), Math.Max(source.W, val));
        }

        public static Vector4D Max(Vector4D left, Vector4D right)
        {
            return new Vector4D(Math.Max(left.X, right.X), Math.Max(left.Y, right.Y),
                                Math.Max(left.Z, right.Z), Math.Max(left.W, right.W));
        }

        public static Vector4D Clamp(Vector4D source, float min, float max)
        {
            Vector4D result = source;

            if (result.X < min) result.X = min;

            if (result.X > max) result.X = max;

            if (result.Y < min) result.Y = min;

            if (result.Y > max) result.Y = max;

            if (result.Z < min) result.Z = min;

            if (result.Z > max) result.Z = max;

            if (result.W < min) result.W = min;

            if (result.W > max) result.W = max;

            return result;
        }

        public static Vector4D Mix(Vector4D left, Vector4D right, float val)
        {
            return left * (1 - val) + right * val;
        }

        public static Vector4D Mix(Vector4D left, Vector4D right, Vector4D val)
        {
            return new Vector4D(left.X * (1 - val.X) + right.X * val.X,
                                left.Y * (1 - val.Y) + right.Y * val.Y,
                                left.Z * (1 - val.Z) + right.Z * val.Z,
                                left.W * (1 - val.W) + right.W * val.W);
        }
        
        public static Vector4D Step(Vector4D source, float edge)
        {
            return new Vector4D(Util.Step(source.X, edge), Util.Step(source.Y, edge),
                                Util.Step(source.Z, edge), Util.Step(source.W, edge));
        }

        public static Vector4D Step(Vector4D source, Vector4D edge)
        {
            return new Vector4D(Util.Step(source.X, edge.X), Util.Step(source.Y, edge.Y),
                                Util.Step(source.Z, edge.Z), Util.Step(source.W, edge.W));
        }
        
        public static Vector4D SmoothStep(Vector4D source, float left, float right)
        {
            return new Vector4D(Util.SmoothStep(source.X, left, right),
                                Util.SmoothStep(source.Y, left, right),
                                Util.SmoothStep(source.Z, left, right),
                                Util.SmoothStep(source.W, left, right));
        }        

        public static Vector4D SmoothStep(Vector4D source, Vector4D left, Vector4D right)
        {
            return new Vector4D(Util.SmoothStep(source.X, left.X, right.X),
                                Util.SmoothStep(source.Y, left.Y, right.Y),
                                Util.SmoothStep(source.Z, left.Z, right.Z),
                                Util.SmoothStep(source.W, left.W, right.W));
        }

        #endregion

        #region Геометрические функции

        public static float Length(Vector4D source)
        {
            return (float)Math.Sqrt(source.X * source.X + source.Y * source.Y +
        	                        source.Z * source.Z + source.W * source.W);
        }

        public static float SquareLength(Vector4D source)
        {
            return source.X * source.X + source.Y * source.Y +
                   source.Z * source.Z + source.W * source.W;
        }

        public static float Distance(Vector4D left, Vector4D right)
        {
            return Length(left - right);
        }

        public static Vector4D Normalize(Vector4D source)
        {
            return source / Length(source);
        }

        public static float Dot(Vector4D left, Vector4D right)
        {
            return left.X * right.X + left.Y * right.Y +
                   left.Z * right.Z + left.W * right.W;
        }

        public static Vector4D Reflect(Vector4D incident, Vector4D normal)
        {
            return incident - 2 * Dot(normal, incident) * normal;
        }

        public static Vector4D Refract(Vector4D incident, Vector4D normal, float index)
        {
            float dot = Dot(incident, normal);

            float square = 1 - index * index * (1 - dot * dot);

            if (square < 0)
            {
                return Reflect(incident, normal);
            }
            else
            {
                float cos = (float)Math.Sqrt(square);

                return index * incident - (cos + index * dot) * normal;
            }
        }

        #endregion

        #region Тригонометрические функции

        public static Vector4D Sin(Vector4D source)
        {
            return new Vector4D((float)Math.Sin(source.X), (float)Math.Sin(source.Y),
                                (float)Math.Sin(source.Z), (float)Math.Sin(source.W));
        }

        public static Vector4D Cos(Vector4D source)
        {
            return new Vector4D((float)Math.Cos(source.X), (float)Math.Cos(source.Y),
                                (float)Math.Cos(source.Z), (float)Math.Cos(source.W));
        }

        public static Vector4D Tan(Vector4D source)
        {
            return new Vector4D((float)Math.Tan(source.X), (float)Math.Tan(source.Y),
                                (float)Math.Tan(source.Z), (float)Math.Tan(source.W));
        }

        public static Vector4D Asin(Vector4D source)
        {
            return new Vector4D((float)Math.Asin(source.X), (float)Math.Asin(source.Y),
                                (float)Math.Asin(source.Z), (float)Math.Asin(source.W));
        }

        public static Vector4D Acos(Vector4D source)
        {
            return new Vector4D((float)Math.Acos(source.X), (float)Math.Acos(source.Y),
                                (float)Math.Acos(source.Z), (float)Math.Acos(source.W));
        }

        public static Vector4D Atan(Vector4D source)
        {
            return new Vector4D((float)Math.Atan(source.X), (float)Math.Atan(source.Y),
                                (float)Math.Atan(source.Z), (float)Math.Atan(source.W));
        }

        #endregion

        #region Экспоненциальные функции

        public static Vector4D Pow(Vector4D left, Vector4D right)
        {
            return new Vector4D((float)Math.Pow(left.X, right.X), (float)Math.Pow(left.Y, right.Y),
                                (float)Math.Pow(left.Z, right.Z), (float)Math.Pow(left.W, right.W));
        }

        public static Vector4D Exp(Vector4D source)
        {
            return new Vector4D((float)Math.Exp(source.X), (float)Math.Exp(source.Y),
                                (float)Math.Exp(source.Z), (float)Math.Exp(source.W));
        }

        public static Vector4D Log(Vector4D source)
        {
            return new Vector4D((float)Math.Log(source.X), (float)Math.Log(source.Y),
                                (float)Math.Log(source.Z), (float)Math.Log(source.W));
        }

        public static Vector4D Sqrt(Vector4D source)
        {
            return new Vector4D((float)Math.Sqrt(source.X), (float)Math.Sqrt(source.Y),
                                (float)Math.Sqrt(source.Z), (float)Math.Sqrt(source.W));
        }

        #endregion

        #region Дополнительные функции

        public float[] ToArray()
        {
            return new float[] { X, Y, Z, W };
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector4D)
            {
                return this == (Vector4D)obj;
            }

            return false;
        }
        
		public override string ToString()
		{
			return "[" + String.Format("{0:0.00}", X) + "; " +
				String.Format("{0:0.00}", Y) + "; " +
				String.Format("{0:0.00}", Z) + "; " +
				String.Format("{0:0.00}", W) + "]";
		}         

        #endregion

        #endregion

        #region Properties

        public float this[int index]
        {
            get
            {
                if (0 == index)
                    return X;
                else
                    if (1 == index)
                        return Y;
                    else
                        if (2 == index)
                            return Z;
                        else
                            return W;
            }

            set
            {
                if (0 == index)
                    X = value;
                else
                    if (1 == index)
                        Y = value;
                    else
                        if (2 == index)
                            Z = value;
                        else
                            W = value;
            }
        }
        
        #endregion
    }
}

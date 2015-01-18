using System;

namespace Auxiliary.MathTools
{
    /// <summary>Двухкомпонентный вещественный вектор. </summary>
    [Serializable]
    public class Vector2D
    {
        #region Public Fields

        public float X;

        public float Y;
        
        public static readonly Vector2D Zero = new Vector2D(0.0f, 0.0f);

        public static readonly Vector2D Unit = new Vector2D(1.0f, 1.0f);

        public static readonly Vector2D I = new Vector2D(1.0f, 0.0f);

        public static readonly Vector2D J = new Vector2D(0.0f, 1.0f);

        public static readonly Vector2D K = new Vector2D(0.0f, 0.0f);
        
        public static readonly Vector2D Epsilon = new Vector2D(Util.Epsilon, Util.Epsilon);

        #endregion

        #region Constructor and Destructor

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2D(float[] values)
        {
            X = values[0];
            Y = values[1];
        }

        public Vector2D(Vector2D vector)
        {
            X = vector[0];
            Y = vector[1];
        }

        #endregion

        #region Operators

        public static Vector2D operator -(Vector2D value)
        {
            return new Vector2D(-value.X, -value.Y);
        }

        public static Vector2D operator +(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2D operator -(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2D operator *(float left, Vector2D right)
        {
            return new Vector2D(left * right.X, left * right.Y);
        }

        public static Vector2D operator *(Vector2D left, float right)
        {
            return new Vector2D(left.X * right, left.Y * right);
        }

        public static Vector2D operator *(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X * right.X, left.Y * right.Y);
        }

        public static Vector2D operator /(Vector2D left, float right)
        {
            return new Vector2D(left.X / right, left.Y / right);
        }

        public static Vector2D operator /(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X / right.X, left.Y / right.Y);
        }

        public static bool operator <(Vector2D left, Vector2D right)
        {
            return left.X < right.X && left.Y < right.Y;
        }

        public static bool operator >(Vector2D left, Vector2D right)
        {
            return left.X > right.X && left.Y > right.Y;
        }

        public static bool operator <=(Vector2D left, Vector2D right)
        {
            return left.X <= right.X && left.Y <= right.Y;
        }

        public static bool operator >=(Vector2D left, Vector2D right)
        {
            return left.X >= right.X && left.Y >= right.Y;
        }

        public static bool operator !=(Vector2D left, Vector2D right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        public static bool operator ==(Vector2D left, Vector2D right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        #endregion

        #region Public Methods

        #region Общие функции

        public static Vector2D Abs(Vector2D source)
        {
            return new Vector2D(Math.Abs(source.X), Math.Abs(source.Y));
        }

        public static Vector2D Sign(Vector2D source)
        {
            return new Vector2D(Math.Sign(source.X), Math.Sign(source.Y));
        }

        public static Vector2D Floor(Vector2D source)
        {
        	return new Vector2D((float)Math.Floor(source.X), (float)Math.Floor(source.Y));
        }

        public static Vector2D Fract(Vector2D source)
        {
        	return new Vector2D(source.X - (float)Math.Floor(source.X),
        	                    source.Y - (float)Math.Floor(source.Y));
        }

        public static Vector2D Ceiling(Vector2D source)
        {
            return new Vector2D((float)Math.Ceiling(source.X), (float)Math.Ceiling(source.Y));
        }

        public static Vector2D Mod(Vector2D source, float val)
        {
            return source - val * Floor(source / val);
        }

        public static Vector2D Mod(Vector2D left, Vector2D right)
        {
            return left - right * Floor(left / right);
        }

        public static Vector2D Min(Vector2D source, float val)
        {
            return new Vector2D(Math.Min(source.X, val), Math.Min(source.Y, val));
        }

        public static Vector2D Min(Vector2D left, Vector2D right)
        {
            return new Vector2D(Math.Min(left.X, right.X), Math.Min(left.Y, right.Y));
        }

        public static Vector2D Max(Vector2D source, float val)
        {
            return new Vector2D(Math.Max(source.X, val), Math.Max(source.Y, val));
        }

        public static Vector2D Max(Vector2D left, Vector2D right)
        {
            return new Vector2D(Math.Max(left.X, right.X), Math.Max(left.Y, right.Y));
        }

        public static Vector2D Clamp(Vector2D source, float min, float max)
        {
            Vector2D result = source;

            if (result.X < min) result.X = min;

            if (result.X > max) result.X = max;

            if (result.Y < min) result.Y = min;

            if (result.Y > max) result.Y = max;

            return result;
        }

        public static Vector2D Mix(Vector2D left, Vector2D right, float val)
        {
            return left * (1 - val) + right * val;
        }

        public static Vector2D Mix(Vector2D left, Vector2D right, Vector2D val)
        {
            return new Vector2D(left.X * (1 - val.X) + right.X * val.X,
                                left.Y * (1 - val.Y) + right.Y * val.Y);
        }
        
        public static Vector2D Step(Vector2D source, float edge)
        {
            return new Vector2D(Util.Step(source.X, edge),
                                Util.Step(source.Y, edge));
        }

        public static Vector2D Step(Vector2D source, Vector2D edge)
        {
            return new Vector2D(Util.Step(source.X, edge.X),
                                Util.Step(source.Y, edge.Y));
        }
        
        public static Vector2D SmoothStep(Vector2D source, float left, float right)
        {
            return new Vector2D(Util.SmoothStep(source.X, left, right),
                                Util.SmoothStep(source.Y, left, right));
        }

        public static Vector2D SmoothStep(Vector2D source, Vector2D left, Vector2D right)
        {
            return new Vector2D(Util.SmoothStep(source.X, left.X, right.X),
                                Util.SmoothStep(source.Y, left.Y, right.Y));
        }
        
        #endregion

        #region Геометрические функции

        public static float Length(Vector2D source)
        {
            return (float)Math.Sqrt(source.X * source.X + source.Y * source.Y);
        }

        public static float SquareLength(Vector2D source)
        {
            return source.X * source.X + source.Y * source.Y;
        }

        public static float Distance(Vector2D left, Vector2D right)
        {
            return Length(left - right);
        }

        public static Vector2D Normalize(Vector2D source)
        {
            return source / Length(source);
        }

        public static float Dot(Vector2D left, Vector2D right)
        {
            return left.X * right.X + left.Y * right.Y;
        }

        public static Vector2D Reflect(Vector2D incident, Vector2D normal)
        {
            return incident - 2 * Dot(normal, incident) * normal;
        }

        public static Vector2D Refract(Vector2D incident, Vector2D normal, float index)
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

        public static Vector2D Sin(Vector2D source)
        {
            return new Vector2D((float)Math.Sin(source.X), (float)Math.Sin(source.Y));
        }

        public static Vector2D Cos(Vector2D source)
        {
            return new Vector2D((float)Math.Cos(source.X), (float)Math.Cos(source.Y));
        }

        public static Vector2D Tan(Vector2D source)
        {
            return new Vector2D((float)Math.Tan(source.X), (float)Math.Tan(source.Y));
        }

        public static Vector2D Asin(Vector2D source)
        {
            return new Vector2D((float)Math.Asin(source.X), (float)Math.Asin(source.Y));
        }

        public static Vector2D Acos(Vector2D source)
        {
            return new Vector2D((float)Math.Acos(source.X), (float)Math.Acos(source.Y));
        }

        public static Vector2D Atan(Vector2D source)
        {
            return new Vector2D((float)Math.Atan(source.X), (float)Math.Atan(source.Y));
        }

        #endregion

        #region Экспоненциальные функции

        public static Vector2D Pow(Vector2D left, Vector2D right)
        {
            return new Vector2D((float)Math.Pow(left.X, right.X), (float)Math.Pow(left.Y, right.Y));
        }

        public static Vector2D Exp(Vector2D source)
        {
            return new Vector2D((float)Math.Exp(source.X), (float)Math.Exp(source.Y));
        }

        public static Vector2D Log(Vector2D source)
        {
            return new Vector2D((float)Math.Log(source.X), (float)Math.Log(source.Y));
        }

        public static Vector2D Sqrt(Vector2D source)
        {
            return new Vector2D((float)Math.Sqrt(source.X), (float)Math.Sqrt(source.Y));
        }

        #endregion

        #region Дополнительные функции

        public float[] ToArray()
        {
            return new float[] { X, Y };
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2D)
            {
                return this == (Vector2D)obj;
            }

            return false;
        }

		public override string ToString()
		{
			return "[" + String.Format("{0:0.00}", X) + "; " +
				String.Format("{0:0.00}", Y) + "]";
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
                    return Y;
            }

            set
            {
                if (0 == index)
                    X = value;
                else
                    Y = value;
            }
        }

        #endregion
    }
}

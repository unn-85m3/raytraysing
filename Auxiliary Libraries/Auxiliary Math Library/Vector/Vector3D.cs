using System;

namespace Auxiliary.MathTools
{
    /// <summary> Трехкомпонентный вещественный вектор. </summary>
    [Serializable]
    public class Vector3D
    {
        #region Public Fields

        public float X;

        public float Y;

        public float Z;
        
        public static readonly Vector3D Zero = new Vector3D(0.0f, 0.0f, 0.0f);

        public static readonly Vector3D Unit = new Vector3D(1.0f, 1.0f, 1.0f);

        public static readonly Vector3D I = new Vector3D(1.0f, 0.0f, 0.0f);

        public static readonly Vector3D J = new Vector3D(0.0f, 1.0f, 0.0f);

        public static readonly Vector3D K = new Vector3D(0.0f, 0.0f, 1.0f);

        public static readonly Vector3D Epsilon = new Vector3D(Util.Epsilon, Util.Epsilon, Util.Epsilon);

        #endregion

        #region Constructor and Destructor

        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D(float[] values)
        {
            X = values[0];
            Y = values[1];
            Z = values[2];
        }
        
        public Vector3D(Vector3D vector)
        {
            X = vector[0];
            Y = vector[1];
            Z = vector[2];
        }

        #endregion

        #region Operators

        public static Vector3D operator -(Vector3D value)
        {
            return new Vector3D(-value.X, -value.Y, -value.Z);
        }

        public static Vector3D operator +(Vector3D left, Vector3D right)
        {
            return new Vector3D(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector3D operator -(Vector3D left, Vector3D right)
        {
            return new Vector3D(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3D operator *(float left, Vector3D right)
        {
            return new Vector3D(left * right.X, left * right.Y, left * right.Z);
        }

        public static Vector3D operator *(Vector3D left, float right)
        {
            return new Vector3D(left.X * right, left.Y * right, left.Z * right);
        }

        public static Vector3D operator *(Vector3D left, Vector3D right)
        {
            return new Vector3D(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        public static Vector3D operator /(Vector3D left, float right)
        {
            return new Vector3D(left.X / right, left.Y / right, left.Z / right);
        }

        public static Vector3D operator /(Vector3D left, Vector3D right)
        {
            return new Vector3D(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
        }

        public static bool operator <(Vector3D left, Vector3D right)
        {
            return left.X < right.X && left.Y < right.Y && left.Z < right.Z;
        }

        public static bool operator >(Vector3D left, Vector3D right)
        {
            return left.X > right.X && left.Y > right.Y && left.Z > right.Z;
        }

        public static bool operator <=(Vector3D left, Vector3D right)
        {
            return left.X <= right.X && left.Y <= right.Y && left.Z <= right.Z;
        }

        public static bool operator >=(Vector3D left, Vector3D right)
        {
            return left.X >= right.X && left.Y >= right.Y && left.Z >= right.Z;
        }

        public static bool operator !=(Vector3D left, Vector3D right)
        {
            return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
        }

        public static bool operator ==(Vector3D left, Vector3D right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        #endregion

        #region Public Methods

        #region Общие функции

        public static Vector3D Abs(Vector3D source)
        {
            return new Vector3D(Math.Abs(source.X), Math.Abs(source.Y), Math.Abs(source.Z));
        }

        public static Vector3D Sign(Vector3D source)
        {
            return new Vector3D(Math.Sign(source.X), Math.Sign(source.Y), Math.Sign(source.Z));
        }

        public static Vector3D Floor(Vector3D source)
        {
            return new Vector3D((float)Math.Floor(source.X),
                                (float)Math.Floor(source.Y),
                                (float)Math.Floor(source.Z));
        }

        public static Vector3D Fract(Vector3D source)
        {
            return new Vector3D(source.X - (float)Math.Floor(source.X),
                                source.Y - (float)Math.Floor(source.Y),
                                source.Z - (float)Math.Floor(source.Z));
        }

        public static Vector3D Ceiling(Vector3D source)
        {
            return new Vector3D((float)Math.Ceiling(source.X),
                                (float)Math.Ceiling(source.Y),
                                (float)Math.Ceiling(source.Z));
        }

        public static Vector3D Mod(Vector3D source, float val)
        {
            return source - val * Floor(source / val);
        }

        public static Vector3D Mod(Vector3D left, Vector3D right)
        {
            return left - right * Floor(left / right);
        }

        public static Vector3D Min(Vector3D source, float val)
        {
            return new Vector3D(Math.Min(source.X, val),
        	                    Math.Min(source.Y, val),
        	                    Math.Min(source.Z, val));
        }

        public static Vector3D Min(Vector3D left, Vector3D right)
        {
            return new Vector3D(Math.Min(left.X, right.X),
        	                    Math.Min(left.Y, right.Y),
        	                    Math.Min(left.Z, right.Z));
        }

        public static Vector3D Max(Vector3D source, float val)
        {
            return new Vector3D(Math.Max(source.X, val),
        	                    Math.Max(source.Y, val),
        	                    Math.Max(source.Z, val));
        }

        public static Vector3D Max(Vector3D left, Vector3D right)
        {
            return new Vector3D(Math.Max(left.X, right.X),
        	                    Math.Max(left.Y, right.Y),
        	                    Math.Max(left.Z, right.Z));
        }

        public static Vector3D Clamp(Vector3D source, float min, float max)
        {
            Vector3D result = source;

            if (result.X < min) result.X = min;

            if (result.X > max) result.X = max;

            if (result.Y < min) result.Y = min;

            if (result.Y > max) result.Y = max;

            if (result.Z < min) result.Z = min;

            if (result.Z > max) result.Z = max;

            return result;
        }

        public static Vector3D Mix(Vector3D left, Vector3D right, float val)
        {
            return left * (1.0f - val) + right * val;
        }

        public static Vector3D Mix(Vector3D left, Vector3D right, Vector3D val)
        {
            return new Vector3D(left.X * (1.0f - val.X) + right.X * val.X,
                                left.Y * (1.0f - val.Y) + right.Y * val.Y,
                                left.Z * (1.0f - val.Z) + right.Z * val.Z);
        }
        
        public static Vector3D Step(Vector3D source, float edge)
        {
            return new Vector3D(Util.Step(source.X, edge),
                                Util.Step(source.Y, edge),
                                Util.Step(source.Z, edge));
        }

        public static Vector3D Step(Vector3D source, Vector3D edge)
        {
            return new Vector3D(Util.Step(source.X, edge.X),
                                Util.Step(source.Y, edge.Y),
                                Util.Step(source.Z, edge.Z));
        }
        
        public static Vector3D SmoothStep(Vector3D source, float left, float right)
        {
            return new Vector3D(Util.SmoothStep(source.X, left, right),
                                Util.SmoothStep(source.Y, left, right),
                                Util.SmoothStep(source.Z, left, right));
        }

        public static Vector3D SmoothStep(Vector3D source, Vector3D left, Vector3D right)
        {
            return new Vector3D(Util.SmoothStep(source.X, left.X, right.X),
                                Util.SmoothStep(source.Y, left.Y, right.Y),
                                Util.SmoothStep(source.Z, left.Z, right.Z));
        }

        #endregion

        #region Геометрические функции

        public static float Length(Vector3D source)
        {
            return (float)Math.Sqrt(source.X * source.X + source.Y * source.Y + source.Z * source.Z);
        }

        public static float SquareLength(Vector3D source)
        {
            return source.X * source.X + source.Y * source.Y + source.Z * source.Z;
        }

        public static float Distance(Vector3D left, Vector3D right)
        {
            return Length(left - right);
        }

        public static Vector3D Normalize(Vector3D source)
        {
            return source / Length(source);
        }

        public static float Dot(Vector3D left, Vector3D right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        public static Vector3D Cross(Vector3D left, Vector3D right)
        {
            return new Vector3D(left.Y * right.Z - left.Z * right.Y,
                                left.Z * right.X - left.X * right.Z,
                                left.X * right.Y - left.Y * right.X);
        }

        public static Vector3D Reflect(Vector3D incident, Vector3D normal)
        {
            return incident - 2 * Dot(normal, incident) * normal;
        }

        public static Vector3D Refract(Vector3D incident, Vector3D normal, float index)
        {
            float ndoti = Dot(incident, normal);

            float square = 1 - index * index * (1 - ndoti * ndoti);

            if (square < 0)
            {
                return Reflect(incident, normal);
            }
            else
            {
                float cos = (float)Math.Sqrt(square);

                return index * incident - (cos + index * ndoti) * normal;
            }
        }

        #endregion

        #region Тригонометрические функции

        public static Vector3D Sin(Vector3D source)
        {
            return new Vector3D((float)Math.Sin(source.X),
        	                    (float)Math.Sin(source.Y),
        	                    (float)Math.Sin(source.Z));
        }

        public static Vector3D Cos(Vector3D source)
        {
            return new Vector3D((float)Math.Cos(source.X),
        	                    (float)Math.Cos(source.Y),
        	                    (float)Math.Cos(source.Z));
        }

        public static Vector3D Tan(Vector3D source)
        {
            return new Vector3D((float)Math.Tan(source.X),
        	                    (float)Math.Tan(source.Y),
        	                    (float)Math.Tan(source.Z));
        }

        public static Vector3D Asin(Vector3D source)
        {
            return new Vector3D((float)Math.Asin(source.X),
        	                    (float)Math.Asin(source.Y),
        	                    (float)Math.Asin(source.Z));
        }

        public static Vector3D Acos(Vector3D source)
        {
            return new Vector3D((float)Math.Acos(source.X),
        	                    (float)Math.Acos(source.Y),
        	                    (float)Math.Acos(source.Z));
        }

        public static Vector3D Atan(Vector3D source)
        {
            return new Vector3D((float)Math.Atan(source.X),
        	                    (float)Math.Atan(source.Y),
        	                    (float)Math.Atan(source.Z));
        }

        #endregion

        #region Экспоненциальные функции

        public static Vector3D Pow(Vector3D left, Vector3D right)
        {
            return new Vector3D((float)Math.Pow(left.X, right.X),
        	                    (float)Math.Pow(left.Y, right.Y),
        	                    (float)Math.Pow(left.Z, right.Z));
        }

        public static Vector3D Exp(Vector3D source)
        {
            return new Vector3D((float)Math.Exp(source.X),
        	                    (float)Math.Exp(source.Y),
        	                    (float)Math.Exp(source.Z));
        }

        public static Vector3D Log(Vector3D source)
        {
            return new Vector3D((float)Math.Log(source.X),
        	                    (float)Math.Log(source.Y),
        	                    (float)Math.Log(source.Z));
        }

        public static Vector3D Sqrt(Vector3D source)
        {
            return new Vector3D((float)Math.Sqrt(source.X),
        	                    (float)Math.Sqrt(source.Y),
        	                    (float)Math.Sqrt(source.Z));
        }

        #endregion

        #region Дополнительные функции

        public float[] ToArray()
        {
            return new float[] { X, Y, Z };
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3D)
            {
                return this == (Vector3D)obj;
            }

            return false;
        }

		public override string ToString()
		{
			return "[" + String.Format("{0:0.00}", X) + "; " +
				String.Format("{0:0.00}", Y) + "; " +
				String.Format("{0:0.00}", Z) + "]";
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
                        return Z;
            }

            set
            {
                if (0 == index)
                    X = value;
                else
                    if (1 == index)
                        Y = value;
                    else
                        Z = value;
            }
        }

        public Vector2D XY
        {
        	get
        	{
        		return new Vector2D(X, Y);
        	}
        }
        
        public Vector2D YX
        {
        	get
        	{
        		return new Vector2D(Y, X);
        	}
        }
        
        public Vector2D XZ
        {
        	get
        	{
        		return new Vector2D(X, Z);
        	}
        }   
        
        public Vector2D ZX
        {
        	get
        	{
        		return new Vector2D(Z, X);
        	}
        }      
        
        public Vector2D YZ
        {
        	get
        	{
        		return new Vector2D(Y, Z);
        	}
        }   
        
        public Vector2D ZY
        {
        	get
        	{
        		return new Vector2D(Z, Y);
        	}
        }         
        
        #endregion
    }
}

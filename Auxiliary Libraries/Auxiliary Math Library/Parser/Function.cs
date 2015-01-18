using System;
using System.Collections.Generic;

namespace Auxiliary.MathTools
{
    /// <summary> Базовый класс для всех функций. </summary>
    [Serializable]
    public abstract class BaseFunction
    {
    	#region Private Fields
    	
    	/// <summary> Символьное представление функции. </summary>
        private string text = null;

        #endregion
        
        #region Operators
        
        /// <summary> Вычисляет сумму двух функций. </summary>
        public static BaseFunction operator +(BaseFunction left, BaseFunction right)
        {
            return new AddFunction(left, right);
        }

        /// <summary> Вычисляет разность двух функций. </summary>
        public static BaseFunction operator -(BaseFunction left, BaseFunction right)
        {
            return new SubFunction(left, right);
        }

        /// <summary> Вычисляет произведение двух функций. </summary>
        public static BaseFunction operator *(BaseFunction left, BaseFunction right)
        {
            return new MulFunction(left, right);
        }

        /// <summary> Вычисляет частное двух функций. </summary>
        public static BaseFunction operator /(BaseFunction left, BaseFunction right)
        {
            return new DivFunction(left, right);
        }

        /// <summary> Вычисляет функцию, противоположную к данной. </summary>
        public static BaseFunction operator -(BaseFunction funct)
        {
            return new NegativeFunction(funct);
        }
        
        #endregion        
        
    	#region Public Methods
    	
        /// <summary> Вычисляет значение функции. </summary>
        public abstract float CalcFunction(Dictionary<string, float> arg);

        /// <summary> Возвращает символьное представление функции. </summary>
		public override string ToString()
		{
			return text;
		}
        
        #endregion       
    }
    
    /// <summary> Корневая функция дерева функций. </summary>
    [Serializable]
    public class RootFunction : BaseFunction
    {
    	#region Private Fields
    	
    	/// <summary> Символьное представление функции. </summary>
        private string text = null;
        
    	/// <summary> Функция для вычисления. </summary>
        private BaseFunction function = null;        

        #endregion
        
        #region Constructors
        
        /// <summary> Создает корневую функцию дерева функций. </summary>
        public RootFunction(string text, BaseFunction function)
        {
        	this.text = text;
        	
        	this.function = function;
        }
        
        #endregion
        
        #region Public Methods
    	
        /// <summary> Вычисляет значение функции. </summary>
        public override float CalcFunction(Dictionary<string, float> arg)
        {
        	return function.CalcFunction(arg);
        }

        /// <summary> Возвращает символьное представление функции. </summary>
		public override string ToString()
		{
			return text;
		}
        
        #endregion       
    }    

    /// <summary> Определяет сумму двух функций. </summary>
    [Serializable]
    public class AddFunction : BaseFunction
    {
    	#region Private Fields
    	
        private BaseFunction left;
        
        private BaseFunction right;

        #endregion
        
        #region Constructors
        
        public AddFunction(BaseFunction left, BaseFunction right)
        {
            this.left = left;
            this.right = right;
        }
        
        #endregion
        
        #region Public Methods

        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return left.CalcFunction(arg) + right.CalcFunction(arg);
        }
        
        #endregion
    }

    /// <summary> Определяет разность двух функций. </summary>
    [Serializable]
    public class SubFunction : BaseFunction
    {
    	#region Private Fields
    	
        private BaseFunction left;
        
        private BaseFunction right;

        #endregion
        
        #region Constructors
        
        public SubFunction(BaseFunction left, BaseFunction right)
        {
            this.left = left;
            this.right = right;
        }
        
        #endregion

        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return left.CalcFunction(arg) - right.CalcFunction(arg);
        }
        
		public override string ToString()
		{
			return "(" + left.ToString() + " - " + right.ToString() + ")";
		}        
        
        #endregion
    }

    /// <summary> Определяет произведение двух функций. </summary>
    [Serializable]
    public class MulFunction : BaseFunction
    {
    	#region Private Fields
    	
        private BaseFunction left;
        
        private BaseFunction right;

        #endregion
        
        #region Constructors
        
        public MulFunction(BaseFunction left, BaseFunction right)
        {
            this.left = left;
            this.right = right;
        }

        #endregion
        
        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return left.CalcFunction(arg) * right.CalcFunction(arg);
        }
        
        #endregion
    }
    
    /// <summary> Определяет частное двух функций. </summary>
    [Serializable]
    public class DivFunction : BaseFunction
    {
        #region Private Fields
        
    	private BaseFunction left;
        
        private BaseFunction right;

        #endregion
        
        #region Constructors
                
        public DivFunction(BaseFunction left, BaseFunction right)
        {
            this.left = left;
            this.right = right;
        }
        
        #endregion

        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return left.CalcFunction(arg) / right.CalcFunction(arg);
        }
        
        #endregion
    }

    /// <summary> Определяет функцию, противоположную к данной. </summary>
    [Serializable]
    public class NegativeFunction : BaseFunction
    {
    	#region Private Fields
    	
        private BaseFunction funct;
        
        #endregion

        #region Constructors
        
        public NegativeFunction(BaseFunction funct)
        {
            this.funct = funct;
        }
        
        #endregion

        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return -funct.CalcFunction(arg);
        }
        
        #endregion
    }

    /// <summary> Определяет функцию, обратную к данной. </summary>
    [Serializable]
    public class ReverseFunction : BaseFunction
    {
    	#region Private Fields
    	   	
        private BaseFunction funct;
        
        #endregion 

        #region Constructors
              
        public ReverseFunction(BaseFunction funct)
        {
            this.funct = funct;
        }
        
        #endregion  
        
        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return 1 / funct.CalcFunction(arg);
        }
        
        #endregion
    }

    /// <summary> Определяет синус от некоторой функции. </summary>
    [Serializable]
    public class SinFunction : BaseFunction
    {
    	#region Private Fields
    	   	
        private BaseFunction funct;
        
        #endregion 

        #region Constructors
              
        public SinFunction(BaseFunction funct)
        {
            this.funct = funct;
        }
        
        #endregion  

        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
        	return (float)Math.Sin(funct.CalcFunction(arg));
        }
        
        #endregion
    }

    /// <summary> Определяет косинус от некоторой функции. </summary>
    [Serializable]
    public class CosFunction : BaseFunction
    {
    	#region Private Fields
    	  	
        private BaseFunction funct;
        
        #endregion  

        #region Constructors
              
        public CosFunction(BaseFunction funct)
        {
            this.funct = funct;
        }
        
        #endregion  
        
        #region Public Methods       

        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return (float)Math.Cos(funct.CalcFunction(arg));
        }
        
        #endregion
    }

    /// <summary> Определяет экспоненту от некоторой функции. </summary>
    [Serializable]
    public class ExpFunction : BaseFunction
    {
    	#region Private Fields
    	   	
        private BaseFunction funct;
        
        #endregion 

        #region Constructors
               
        public ExpFunction(BaseFunction funct)
        {
            this.funct = funct;
        }
        
        #endregion 

        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return (float)Math.Exp(funct.CalcFunction(arg));
        }
        
        #endregion
    }

    /// <summary> Определяет натуральный логарифм от некоторой функции. </summary>
    [Serializable]
    public class LogFunction : BaseFunction
    {
    	#region Private Fields
    	   	
        private BaseFunction funct;
        
        #endregion 

        #region Constructors
                
        public LogFunction(BaseFunction funct)
        {
            this.funct = funct;
        }
        
        #endregion

        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return (float)Math.Log(funct.CalcFunction(arg));
        }
        
        #endregion
    }

    /// <summary> Определяет квадратный корень из некоторой функции. </summary>
    [Serializable]
    public class SqrtFunction : BaseFunction
    {
    	#region Private Fields
    	   	
        private BaseFunction funct;
        
        #endregion 

        #region Constructors
               
        public SqrtFunction(BaseFunction funct)
        {
            this.funct = funct;
        }
        
        #endregion 

        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return (float)Math.Sqrt(funct.CalcFunction(arg));
        }
        
        #endregion
    }

    /// <summary> Определяет модуль некоторой функции. </summary>
    [Serializable]
    public class AbsFunction : BaseFunction
    {
    	#region Private Fields
    	   	
        private BaseFunction funct;
        
        #endregion 

        #region Constructors
             
        public AbsFunction(BaseFunction funct)
        {
            this.funct = funct;
        }
        
        #endregion   

        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return Math.Abs(funct.CalcFunction(arg));
        }
        
        #endregion
    }

    /// <summary> Постоянная функция. </summary>
    [Serializable]
    public class ConstFunction : BaseFunction
    {
    	#region Private Fields
    	   	
        private float value;
        
        #endregion 

        #region Constructors
               
        public ConstFunction(float value)
        {
            this.value = value;
        }
        
        #endregion
        
        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
            return value;
        }
        
        #endregion
    }

    /// <summary> Тождественная функция. </summary>
    [Serializable]
    public class EqualFunction : BaseFunction
    {
    	#region Private Fields
    	   	
        private string name;
        
        #endregion 

        #region Constructors
              
        public EqualFunction(string name)
        {
            this.name = name;
        }
        
        #endregion
        
        #region Public Methods
        
        public override float CalcFunction(Dictionary<string, float> arg)
        {
        	return arg[name];
        }
        
        #endregion
    }
}

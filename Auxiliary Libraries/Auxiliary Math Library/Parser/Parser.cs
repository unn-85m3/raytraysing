using System;
using System.Collections.Generic;

namespace Auxiliary.MathTools
{
    /// <summary> Осуществляет анализ входной строки и строит суперпозицю вложенных функций. </summary>
    public static class Parser
    {
    	#region Public Fields
    	
    	/// <summary> Список имен переменных функции. </summary>
    	private static string [] Variables = new string[] { "U", "V", "X", "Y", "Z", "T" };
    	
    	#endregion    	
    	
    	#region Private Fields
    	
    	/// <summary> Постоянная функция для представления числа PI. </summary>
    	private static BaseFunction PiFunction = new ConstFunction((float)Math.PI);
    	
    	#endregion
    	
    	#region Public Methods
    	
    	/// <summary> Осуществляет разбор входной строки и строит суперпозицию вложенных функций. </summary>
        public static BaseFunction ParseString(string formula)
        {
            int index = 0;

            return new RootFunction(formula,
                                    Expression(formula.Replace(" ", String.Empty).ToUpper() + " ", ref index));
        }
        
        #endregion
        
        #region Private Methods
        
        private static BaseFunction Expression(string str, ref int index)
        {
            BaseFunction funct = Addend(str, ref index);

            while (str[index] == '+' || str[index] == '-')
            {
            	if (str[index] == '+')
            	{
            		index++;
            		funct += Addend(str, ref index);
            	}
            	else
            	{
            		if (str[index] == '-')
            		{
            			index++;
            			funct -= Addend(str, ref index);
            		}
            	}
            }
           
            return funct;
        }

        private static BaseFunction Addend(string str, ref int index)
        {
            BaseFunction funct = Multiplier(str, ref index);

            while (str[index] == '*' || str[index] == '/')
            {
            	if (str[index] == '*')
            	{
            		index++;
            		funct *= Multiplier(str, ref index);
            	}
            	else
            	{
            		if (str[index] == '/')
            		{
            			index++;
            			funct /= Multiplier(str, ref index);
            		}
            	}
            }

            return funct;
        }

        private static BaseFunction Multiplier(string str, ref int index)
        {
            BaseFunction funct = null;
            
            if (Char.IsDigit(str[index]))
            {
                int start = index;
                
                index++;

                while (Char.IsDigit(str[index]) || (str[index] == ',')) 
                    index++;

                string substr = str.Substring(start, index - start);
                
                funct = new ConstFunction(Convert.ToSingle(substr));
            }
            else
                if (str[index] == '-')
                {
                    index++;
                    funct = -Multiplier(str, ref index);
                }
                else
                    if (str[index] == '(')
                    {
                        index++;
                    
                        funct = Expression(str, ref index);

                        if (str[index] == ')')
                        {
                            index++;
                        }
                        else
                        {
                        	throw new Exception("Ошибка! Отсутствует закрывающаяся скобка.");
                        }
                    }
                    else
                        if (Char.IsLetter(str[index]))
                        {
                            funct = Function(str, ref index);
                        }
            
            return funct;
        }

        private static BaseFunction Function(string str, ref int index)
        {
            BaseFunction funct = null;

            int start = index;
            
            index++;
            
            while (Char.IsLetter(str[index])) 
                index++;

            string substr = str.Substring(start, index - start);

            if (str[index] == '(')
            {
                index++;

                if (substr == "SIN")
                    funct = new SinFunction(Expression(str, ref index));
                else
                    if (substr == "COS")
                        funct = new CosFunction(Expression(str, ref index));
                    else
                        if (substr == "LOG")
                            funct = new LogFunction(Expression(str, ref index));
                        else
                            if (substr == "EXP")
                                funct = new ExpFunction(Expression(str, ref index));
                            else
                                if (substr == "SQRT")
                                    funct = new SqrtFunction(Expression(str, ref index));
                                else
                                    if (substr == "ABS")
                                        funct = new AbsFunction(Expression(str, ref index));
                                	else
                                		throw new Exception("Ошибка! Неизвестная функция. ");
            
                if (str[index] == ')')
                    index++;
                else
                	throw new Exception("Ошибка! Отсутствует закрывающаяся скобка.");
            }
            else
            {
            	if (substr == "PI")
            	{
            		funct = PiFunction;
            	}
            	else
            	{
            		foreach (string var in Variables)
            		{
            			if (substr == var)
            				funct = new EqualFunction(var);
            		}            		
            		
            		if (null == funct)
            			throw new Exception("Ошибка! Неизвестная переменная. ");
            	}
                        
            }
            
            return funct;
        }
    
    	#endregion    
    }
}

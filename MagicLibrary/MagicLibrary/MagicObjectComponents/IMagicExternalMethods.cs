using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Magic.MO
{
    public class MagicExternalMethods
    {
        static readonly string[] externalMethodsNames = { "scan","distance","debug" };
        static string regexPattern;
        public static string SubstituteExternalMethods(string expresion, ref List<MagicVariable>[] variables, MagicObject parentMagicObject)
        {
            if (parentMagicObject.externalMethods == null) return expresion;
            if (regexPattern == null) externalMethodsNames.ToList().ForEach(n => regexPattern += n);
            //stardant format: "var name = MethodName(p1,p2)"
            if (!externalMethodsNames.Any(s => expresion.ToLower().Contains(s))) return expresion;
            
            string[] content = expresion.Split(new string[]{@"scan\(",@"distance\(",@"debug\("}, StringSplitOptions.None); 
            //Regex.Matches(expresion, @"").Cast<Match>().Select(m => m.Value).ToArray();

            int bracketCounter = 0;

            string memory = "";
            string returnExpresion = "";

            for (int i = 0; i < content.Count(); i++)
            {
                string s = content[i];

                if (s.Length == 0) continue;

                if (s.Contains("("))
                {
                    bracketCounter += s.Count(c => c == '(');
                }
                if (s.Contains(")"))
                {
                    bracketCounter -= s.Count(c => c == ')');
                    if (bracketCounter == 0)
                    {
                        memory += s;
                        s = memory;
                        memory = "";
                    }
                }

                if (bracketCounter > 0)
                {
                    memory += s;
                    continue;
                }
                returnExpresion += SubstituteExternalMethod(s, ref variables, parentMagicObject);
                //position += s.Length;
            }

            return returnExpresion;
        }
        private static string SubstituteExternalMethod(string expresion, ref List<MagicVariable>[] variables, MagicObject parentMagicObject)
        {
            if (!expresion.Contains('(') || !expresion.Contains(')')) return expresion;

            IMagicExternalMethods externalMethods = parentMagicObject.externalMethods;
            float[] parameters;
            string[] methodParameters;
            string s = expresion.Substring(expresion.IndexOf('(') + 1);
            methodParameters = s.Remove(s.LastIndexOf(')')).Split(',');
            switch (expresion.Remove(expresion.IndexOf('(')).ToLower())
            {
                case "scan": //format: scan(float,float)"
                    if (methodParameters.Length != 2) return expresion;
                    parameters = new float[2];
                    for(int i = 0; i < parameters.Length; i++) 
                        parameters[i] = MagicVariable.ParseExpresionStringToFloat(methodParameters[i], ref variables, parentMagicObject);
                    expresion = externalMethods.Scan(parameters[0], parameters[1]).ToString();
                    break;
                case "distance":
                    if (methodParameters.Length != 1) return expresion;
                    parameters = new float[1];
                    for (int i = 0; i < parameters.Length; i++)
                        parameters[i] = MagicVariable.ParseExpresionStringToFloat(methodParameters[i], ref variables, parentMagicObject);
                    expresion = externalMethods.Distance(parameters[0]).ToString();
                    break;
                case "debug":
                    externalMethods.Debug(expresion);
                    break;
                default:
                    return expresion;
            }
            return expresion;
        }
    }
    public interface IMagicExternalMethods
    {
        /// <summary>
        /// Scan's in given direction, and return's 0 if detects nothing, 1 if ground, 2 if enemy.
        /// </summary>
        int Scan(float angle, float distance);
        /// <summary>
        /// Return's distance to nearest object. Return's -1 if no object is detected.
        /// </summary>
        float Distance(float angle);
        /// <summary>
        /// Writes out message to console. Debuging only.
        /// </summary>
        void Debug(string message);
    }
}

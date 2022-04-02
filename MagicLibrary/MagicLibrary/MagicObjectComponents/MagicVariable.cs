using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Magic.Editor;

namespace Magic.MO
{
    public class MagicVariable : MagicContent
    {
        public string name;
        public float value;
        public static bool ParseStringToMagicVariableExpresion(string content, int position, ref List<MagicVariable>[] variables, MagicObject parentMagicObject)
        {
            float value = 0;
            string name;
            //stardant format: "var name = expresion"
            string variableContent = Regex.Replace(content, @" +", " ");

            if (variableContent.Contains("++") || variableContent.Contains("--"))
            {
                name = variableContent.Remove(variableContent.IndexOfAny(new[] {'+','-'}));
                name = Regex.Replace(name, @"[\s;]", "");
                ChangeVariableValue(variableContent.Contains("++") ? "++" : "--", name, value, ref variables);
                return true;
            }
            if (!variableContent.Contains("=") && !variableContent.TrimStart().StartsWith("var "))
            {
                //test for external methods:
                if (new string[] { "scan", "distance", "debug" }.Any(variableContent.Contains))
                {
                    MagicExternalMethods.SubstituteExternalMethods(Regex.Replace(variableContent, @"[\s;]", ""), ref variables, parentMagicObject);
                    return true;
                }
                return false;
            }

            if (variableContent.Contains("var "))
            {
                name = variableContent.Split(new string[] {"var "}, StringSplitOptions.None)[1];
                name = Regex.Replace(name, @"[\s;]", ""); //removes whitespaces and command end sign
                if (name == "") return false;

                variableContent = Regex.Replace(variableContent, @"[\s;]", "");

                foreach (List<MagicVariable> l in variables) if (l.Any(v => v.name == name)) return false; // return false if variable is already declared
                if (variableContent.Contains("="))
                {
                    name = name.Remove(name.IndexOf('='));
                    string expresion = variableContent.Substring(variableContent.IndexOf('=') + 1);
                    value = ParseExpresionStringToFloat(expresion, ref variables, parentMagicObject);
                }
                //declare variable
                variables[variables.Length - 1].Add(new MagicVariable(name, value)); //adds variable to lowest variable list
                if (position != -1) CodeEditor.highlightings.Add(new Highlighting(position, content.IndexOf("var") + 3, Highlighting.Type.var, null));
            }
            else
            {
                variableContent = Regex.Replace(variableContent, @"[\s;]", "");
                string[] signs = new string[] { "+=", "-=", "*=", "/="};
                string sign = signs.Any(variableContent.Contains) ? variableContent.ElementAt(variableContent.IndexOf('=') -1) + "=" : "=";
                name = variableContent.Remove(variableContent.IndexOf(sign));
                string expresion = variableContent.Substring(variableContent.IndexOf(sign) + sign.Length);
                value = ParseExpresionStringToFloat(expresion, ref variables, parentMagicObject);
                ChangeVariableValue(sign, name, value, ref variables);
            }
            if (name == null) return false;
            return true;
        }
        private static void ChangeVariableValue(string sign, string name, float value, ref List<MagicVariable>[] variables)
        {
            for (int i = 0; i < variables.Length; i++)
            {
                for (int x = 0; x < variables[i].Count; x++)
                {
                    if (variables[i].ElementAt(x).name == name)
                    {
                        switch (sign)
                        {
                            case "=": variables[i].ElementAt(x).value = value; break;
                            case "+=": variables[i].ElementAt(x).value += value; break;
                            case "-=": variables[i].ElementAt(x).value -= value; break;
                            case "*=": variables[i].ElementAt(x).value *= value; break;
                            case "/=": variables[i].ElementAt(x).value /= value; break;
                            case "++": variables[i].ElementAt(x).value++; break;
                            case "--": variables[i].ElementAt(x).value--; break;
                        };
                    }
                }
            }
        }
        public MagicVariable(string name, float value)
        {
            this.name = name;
            this.value = value;
        }
        public MagicVariable(MagicVariable magicVariable)
        {
            this.name = magicVariable.name;
            this.value = magicVariable.value;
        }

        public static float ParseExpresionStringToFloat(string expresion, ref List<MagicVariable>[] variables, MagicObject parentMagicObject)
        {
            expresion = MagicExternalMethods.SubstituteExternalMethods(expresion, ref variables, parentMagicObject);
            AdjustContent(ref expresion);
            Parser(ref expresion, ref variables);
            return ParseBracketToFloat(expresion);
        }
        static float ParseBracketToFloat(string expresion)
        {
            float value = 0;
            int bracketCount = 0;
            string memory = "";
            string mainMemory = "";
            expresion = Regex.Replace(expresion, @"(?<=[0-9])\(", "*("); //adds multiplication symbol before brackets.
            string[] brackets = Regex.Split(expresion, @"(\()|(\))");
            if (!expresion.Contains("("))
            {
                return ParseBracketContentToFloat(expresion);
            }
            foreach (string bracketContent in brackets)
            {
                string s = bracketContent;
                if (s.Length == 0) continue;
                if (s.Contains("("))
                {
                    bracketCount += s.Count(c => c == '(');
                }
                if (s.Contains(")"))
                {
                    bracketCount -= s.Count(c => c == ')');
                    if (bracketCount == 0)
                    {
                        memory += s;
                        s = memory;
                        memory = "";
                    }
                }
                if (bracketCount > 0)
                {
                    memory += s;
                    continue;
                }
                if (s.Contains(")"))
                {
                    s = Regex.Replace(s, @"(^\()|(\)$)", "");
                    mainMemory += ParseBracketToFloat(s);
                }
                else
                {
                    mainMemory += s;
                }
            }
            value = ParseBracketContentToFloat(mainMemory);
            return value;
        }
        public static float ParseBracketContentToFloat(string content)
        {
            AdjustContent(ref content);
            string patern = @"(\*)|(\+)|(/)|(?<=\d)(-)";
            string[] values = Regex.Split(content, patern);

            string sign = "";
            double lastValue = 0;
            content = "";
            bool containsSign;

            string[] compareStrings = new string[] { "*", "/" };
            for (int i = 0; i < 2; i++)
            {
                foreach (string value in values)
                {
                    if (value.Length == 0) continue;
                    containsSign = value.Length == 1 && compareStrings.Contains(value);
                    if (containsSign) sign = value;
                    else
                    {
                        if (!double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
                        {
                            content += lastValue; content += value; continue;
                        }

                        if (sign == "")
                        {
                            lastValue = number;
                        }
                        else
                        {
                            switch (sign)
                            {
                                case "*": lastValue *= number; break;
                                case "/": lastValue /= number; break;
                                case "+": lastValue += number; break;
                                case "-": lastValue -= number; break;
                            }
                            sign = "";  //resets sign
                        }
                    }
                }
                content += lastValue;
                if (i == 1) continue;

                AdjustContent(ref content);

                patern = @"(\+)|(-)";
                values = Regex.Split(content, patern).Where(c => c != "").ToArray();
                if (values.Length == 2 && values[0] == "-") goto end;

                compareStrings = new string[] { "+", "-" };
                sign = "";
                content = "";
            }
        end:
            double.TryParse(content, out double result);
            return (float)Math.Round(result, 6); //return's float rounded to 6 decimal places to fix most floating-point errors.
        }
        internal static void AdjustContent(ref string content)
        {
            while (content.Contains("--"))
                content = content.Replace("--", "+");
            content = Regex.Replace(content, @"\++", "+");
            content = Regex.Replace(content, @"\+\-", "-");
            content = Regex.Replace(content, @"\-\+", "-");
            while (content.Contains("--"))
                content = content.Replace("--", "+");

            content = content.Replace(',', '.');
        }
        internal static void Parser(ref string content, ref List<MagicVariable>[] variables) //returns string with variables names substituted with their values
        {
            foreach (List<MagicVariable> l in variables)
            {
                foreach (MagicVariable v in l)
                {
                    string patern = @"\b" + v.name + @"\b";
                    content = Regex.Replace(content, patern, v.value.ToString());
                }
            }
        }
    }
}

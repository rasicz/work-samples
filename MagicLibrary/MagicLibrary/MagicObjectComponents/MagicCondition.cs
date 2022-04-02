using Magic.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Magic.MO
{
    public class MagicCondition
    {
        public List<object> content;
        Statement statement;
        public enum ConditionType
        {
            magicIf,
            magicWhile,
        }
        public ConditionType conditionType;
        public void SetMagicCondition(List<object> content, string statement)
        {
            this.content = content;
            this.statement = new Statement(statement);
        }
        public bool Activate(MagicObject parentMagicObject, ref List<MagicVariable>[] variables)
        {
            //makes array from variables, and adds one new list to it;
            List<MagicVariable>[] currentVariables = new List<MagicVariable>[variables.Length + 1];
            Array.Copy(variables, currentVariables, variables.Length);
            currentVariables[currentVariables.Length - 1] = new List<MagicVariable>();

            int conditionStartTick = Environment.TickCount; //this int prevent's from infinite loop and freezing
            switch (conditionType)
            {
                case ConditionType.magicIf:
                    if(!parentMagicObject.manaManager.UseMana(ConditionsManaCost["if"])) break;
                    if (statement.EvaluateStatement(ref currentVariables, parentMagicObject)) ActivateContent(parentMagicObject, ref variables);
                    else return false;
                    break;
                case ConditionType.magicWhile:
                    if (!parentMagicObject.manaManager.UseMana(ConditionsManaCost["while"])) break;
                    while (statement.EvaluateStatement(ref currentVariables, parentMagicObject) && Environment.TickCount - conditionStartTick < 20)
                    {
                        if (!parentMagicObject.manaManager.UseMana(ConditionsManaCost["whileloop"])) break;
                        ActivateContent(parentMagicObject, ref variables);
                    };
                    break;
            }
            return true;
        }
        private void ActivateContent(MagicObject parentMagicObject, ref List<MagicVariable>[] variables)
        {
            foreach (object o in content)
            {
                if (parentMagicObject.manaManager.Mana == 0) return;
                if (o.GetType() == typeof(MagicContent))
                {
                    ((MagicContent)o).EvalueateContent(ref parentMagicObject.tags, ref variables, parentMagicObject);
                }
                if (o.GetType() == typeof(MagicCondition))
                {
                    ((MagicCondition)o).Activate(parentMagicObject, ref variables);
                }
            }
        }

        public bool ParseStringToMagicCondition(string content, int position, ref List<MagicVariable>[] editorVariables, ref MagicTags editorTags, MagicObject parentMagicObject)
        {
            if (content.Length == 0) return false;
            if (!(content.Contains('[') && content.Contains(']') && content.Contains('(') && content.Contains(')'))) return false;
            
            try
            {
                List<MagicVariable>[] currentEditorVariables = new List<MagicVariable>[editorVariables.Length + 1];
                Array.Copy(editorVariables, currentEditorVariables, editorVariables.Length);
                currentEditorVariables[currentEditorVariables.Length - 1] = new List<MagicVariable>();

                string conditionName = content.Split('(')[0];

                string conditionStatement = content.Substring(content.IndexOf('(') + 1);
                conditionStatement = conditionStatement.Remove(conditionStatement.IndexOf(')'));

                string conditionContent = content.Substring(content.IndexOf('[') + 1);
                conditionContent = conditionContent.Remove(conditionContent.LastIndexOf(']'));

                switch (conditionName.Trim().ToLower())
                {
                    case "if":
                        conditionType = ConditionType.magicIf;
                        break;
                    case "while":
                        conditionType = ConditionType.magicWhile;
                        break;
                    default:
                        return false;
                }
                SetMagicCondition(Tools.ParseStringToMagicMethodContent(conditionContent, position + content.IndexOf('[') + 1, ref currentEditorVariables, ref editorTags, parentMagicObject), conditionStatement);

                CodeEditor.highlightings.Add(new Highlighting(position, conditionName.TrimEnd().Length, Highlighting.Type.method, null));

            }
            catch (Exception e) //TO DO: tohle je taky celkem prasárna
            {
                UnityEngine.Debug.Log("e: " + e);
                return false;
            }
            return true;
        }

        private static readonly Dictionary<string, int> ConditionsManaCost = new Dictionary<string, int>()
        {
            { "if", 10 },
            { "while", 10 },
            { "whileloop", 1 },
        };
    }
    internal class Statement
    {
        List<Expresion> expresions = new List<Expresion>();
        List<LogicalOperator> logicalOperators = new List<LogicalOperator>();
        public enum LogicalOperator
        {
            and,
            or,
            xor,
        }
        public Statement(string content)
        {
            string patern = @"(?i)(xor)|(and)|(or)";
            string[] values = Regex.Split(content, patern);
            for (int i = 0; i < values.Length; i++)
            {
                if (i % 2 == 0) expresions.Add(new Expresion(values[i]));
                else logicalOperators.Add(values[i].ToLower() == "and" ? LogicalOperator.and : values[i].ToLower() == "or" ? LogicalOperator.or : LogicalOperator.xor);
            }
        }
        public bool EvaluateStatement(ref List<MagicVariable>[] variables, MagicObject parentMagicObject)
        {
            bool returnBool;
            bool a = expresions.ElementAt(0).EvaluateExpresion(ref variables, parentMagicObject);
            bool b;
            bool c = false;
            if (expresions.Count == 1) return a;
            for (int i = 0; i < expresions.Count - 1; i++)
            {
                b = expresions.ElementAt(i + 1).EvaluateExpresion(ref variables, parentMagicObject);
                LogicalOperator lo = logicalOperators.ElementAt(i);
                switch (lo)
                {
                    case LogicalOperator.and:
                        c = a && b;
                        break;
                    case LogicalOperator.or:
                        c = a || b;
                        break;
                    case LogicalOperator.xor:
                        c = a != b;
                        break;
                }
                a = c;
            }
            returnBool = c;
            return returnBool;
        }
    }
    internal class Expresion
    {
        public Term termA;
        public Term termB;
        public enum Operator
        {
            equals,
            notEquals,
            greater,
            less,
            greaterOREqual,
            lessOrEqual
        }
        public Operator @operator;
        public Expresion(string content)
        {
            string patern = @"(==)|(!=)|(>=)|(<=)|(>)|(<)";
            string[] values = Regex.Split(content, patern);
            if(values.Length == 3)
            {
                termA = new Term(values[0]);
                switch (values[1])
                {
                    case "==": @operator = Operator.equals; break;
                    case "!=": @operator = Operator.notEquals; break;
                    case ">": @operator = Operator.greater; break;
                    case "<": @operator = Operator.less; break;
                    case ">=": @operator = Operator.greaterOREqual; break;
                    case "<=": @operator = Operator.lessOrEqual; break;
                }
                termB = new Term(values[2]);
            }
        }
        public bool EvaluateExpresion(ref List<MagicVariable>[] variables, MagicObject parentMagicObject)
        {
            float a = termA.EvaluateTerm(ref variables, parentMagicObject);
            float b = termB.EvaluateTerm(ref variables, parentMagicObject);
            bool returnBool = false;
            switch (@operator)
            {
                case Operator.equals: returnBool = a == b; break;
                case Operator.notEquals: returnBool = a != b; break;
                case Operator.greater: returnBool = a > b; break;
                case Operator.less: returnBool = a < b; break;
                case Operator.greaterOREqual: returnBool = a >= b; break;
                case Operator.lessOrEqual: returnBool = a <= b; break;
            }
            return returnBool;
        }
    }
    internal class Term
    {
        string content;
        public Term(string content)
        {
            this.content = content;
        }
        public float EvaluateTerm(ref List<MagicVariable>[] variables, MagicObject parentMagicObject)
        {
            return MagicVariable.ParseExpresionStringToFloat(content, ref variables, parentMagicObject);
        } 
    }
}

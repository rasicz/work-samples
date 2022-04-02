using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Magic.Editor;

namespace Magic.MO
{
    /// <summary>
    /// Class containing all methods of MagicObject.
    /// </summary>
    public class MagicMethods
    {
        public MagicMethod OnStart;
        public MagicMethod EveryTick;
        public MagicMethod OnCollision;
        public MagicObject parentMagicObject;
        public struct MagicMethod
        {
            public MagicObject parentMagicObject;
            public List<object> content;
            public MagicMethod(List<object> content, MagicObject parentMagicObject)
            {
                this.content = content;
                this.parentMagicObject = parentMagicObject;
            }
            /// <summary>
            /// This method is called by external event's from unity.
            /// </summary>
            public bool Activate(ref List<MagicVariable> variables)
            {
                List<MagicVariable>[] currentVariables = { variables, new List<MagicVariable>() };
                foreach(object o in content)
                {
                    if (parentMagicObject.manaManager.Mana == 0) return false;
                    if (o.GetType() == typeof(MagicContent))
                    {
                        ((MagicContent)o).EvalueateContent(ref parentMagicObject.tags, ref currentVariables, parentMagicObject);
                    }
                    if(o.GetType() == typeof(MagicCondition))
                    {
                        ((MagicCondition)o).Activate(parentMagicObject, ref currentVariables);
                    }
                }
                return true;
            }
        }
        private static readonly string[] keyWords = new string[] { "onstart", "everytick", "oncollision" };
        public bool ParseStringToMagicMethod(string content, int position, ref List<MagicVariable> editorVariables, ref MagicTags editorTags)    //Tries to create method from content, returns true if successful.
        {
            List<MagicVariable>[] currentEditorVariables = { editorVariables, new List<MagicVariable>() };
            if (!content.Contains('[') || !content.Contains(']')) return false;
            if (content.Length == 0) return false;
            string methodName = content.Split('[')[0];
            string methodContent = content.Substring(methodName.Length + 1);
            methodContent = methodContent.Remove(methodContent.LastIndexOf(']'));

            if (!keyWords.Any(k => k == methodName.Trim().ToLower())) return false;
            MagicMethod magicMethod = new MagicMethod(Tools.ParseStringToMagicMethodContent(methodContent, position + methodName.Length + 1, ref currentEditorVariables, ref editorTags, parentMagicObject), parentMagicObject);
            switch (methodName.Trim().ToLower())
            {
                case "onstart": OnStart = magicMethod; break;
                case "everytick": EveryTick = magicMethod; break;
                case "oncollision": OnCollision = magicMethod; break;
                default: return false; //TO DO (možná): add user defined methods
            }

            CodeEditor.highlightings.Add(new Highlighting(position, methodName.TrimEnd().Length, Highlighting.Type.method, null));
            return true;
        }
    }

}

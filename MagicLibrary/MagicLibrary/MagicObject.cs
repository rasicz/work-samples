using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Magic.Editor;
using System.Net;
using System.Text.RegularExpressions;

namespace Magic.MO
{
    // TO DO:
    // child storage and spawn system

    public class MagicObject
    {
        public MagicObjectType moType;
        public MagicTags tags = new MagicTags();
        //public MagicTags newTags = new MagicTags();
        public MagicMethods methods = new MagicMethods();
        public List<MagicVariable> variables = new List<MagicVariable>();
        public IMagicExternalMethods externalMethods;

        public ManaManager manaManager = new ManaManager();
        
        public void SetMagicObject(string content, int position, IMagicExternalMethods externalMethodsScript)
        {
            this.externalMethods = externalMethodsScript;
            List<MagicContent> contents = new List<MagicContent>();
            manaManager.editor = true;

            //Editor-only variables - these are used only for editor to test if code is valid.
            MagicTags editorTags = new MagicTags();
            List<MagicVariable> editorVariables = new List<MagicVariable>();

            string[] magicObjectContent = Regex.Split(content, @"([^;]+;)");

            int methodBracketCounter = 0;
            string memory = "";

            //references MagicObject to methods and contents
            methods.parentMagicObject = this;

            for (int i = 0; i < magicObjectContent.Count(); i++)
            {
                string s = magicObjectContent[i];
                if (s.Length == 0) continue;
                if (s.Contains("["))
                {
                    methodBracketCounter += s.Count(c => c == '[');
                }
                if (s.Contains("]"))
                {
                    methodBracketCounter -= s.Count(c => c == ']');
                    if (methodBracketCounter == 0)
                    {
                        memory += s;
                        s = memory;
                    }
                }
                if (methodBracketCounter > 0)
                {
                    memory += s;
                    continue;
                }

                if (methods.ParseStringToMagicMethod(s, position, ref editorVariables, ref editorTags))
                {
                    position += s.Length;
                    continue;
                }
                //passes informations to codeeditor
                if (CodeEditor.caretPosition >= position && CodeEditor.caretPosition < position + s.Length)
                {
                    CodeEditor.caretVariables = new List<MagicVariable>[] { variables.ToList() };
                    CodeEditor.caretCurrentStructure = CodeEditor.CaretCurrentStructure.insideObject;
                }
                MagicContent magicContent = new MagicContent();
                List<MagicVariable>[] currentVariables = new List<MagicVariable>[] { variables };
                if (magicContent.ParseStringToMagicContent(s, position, ref currentVariables, ref tags, this, true))
                {
                    variables = currentVariables[0];
                    contents.Add(magicContent);

                    for (int v = 0; v < variables.Count; v++)
                    { //adds new variables into editorVariables.
                        if (editorVariables.Where(e => e.name == variables[v].name).Count() == 0)
                        {
                            editorVariables.Add(new MagicVariable(variables[v]));
                        }
                    }
                    editorTags += tags;
                    position += s.Length;
                    continue;
                }
                position += s.Length;

                CodeEditor.highlightings.Add(new Highlighting(position, s.Length, Highlighting.Type.error));
            }
        }
    }
    public enum MagicObjectType
    {
        sphere,
        placeHolder,
    }

    public class Tools
    {
        /// <summary>
        /// Clamps the given float value to be between min and max.
        /// </summary>
        public static float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        /// <summary>
        /// Clamps the given float value to be between min and max.
        /// </summary>
        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        /// <summary>
        /// Return's all variables needed for MagicMethod or MagicCondition creation.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static List<object> ParseStringToMagicMethodContent(string content, int position, ref List<MagicVariable>[] editorVariables, ref MagicTags editorTags, MagicObject magicObject)
        {
            IMagicExternalMethods externalMethods = magicObject.externalMethods;

            string[] magicObjectContent = Regex.Split(content, @"([^;]+;)");
            int methodBracketCounter = 0;
            
            string memory = "";

            List<object> methodContent = new List<object>();

            for (int i = 0; i < magicObjectContent.Count(); i++)
            {
                string s = magicObjectContent[i];
                if (s.Length == 0) continue;

                if (s.Contains("["))
                {
                    methodBracketCounter += s.Count(c => c == '[');
                }
                if (s.Contains("]"))    //TO DO: vyřešit co se stane když je počet zavřených závorek > otevřených
                {
                    methodBracketCounter -= s.Count(c => c == ']');
                    if (methodBracketCounter == 0)
                    {
                        memory += s;
                        s = memory;
                        memory = "";
                    }
                }

                if (methodBracketCounter > 0)
                {
                    memory += s;
                    continue;
                }
                MagicCondition magicCondition = new MagicCondition();
                if (magicCondition.ParseStringToMagicCondition(s, position, ref editorVariables, ref editorTags, magicObject))
                {
                    methodContent.Add(magicCondition);
                    position += s.Length;
                    continue;
                }

                //passes informations to codeeditor
                if (CodeEditor.caretPosition >= position && CodeEditor.caretPosition < position + s.Length){
                    CodeEditor.caretVariables = editorVariables.Select(l => l.ToList()).ToArray();
                    CodeEditor.caretCurrentStructure = CodeEditor.CaretCurrentStructure.insideMethod;
                }

                MagicContent magicContent = new MagicContent();
                if (magicContent.ParseStringToMagicContent(s, position, ref editorVariables, ref editorTags, magicObject))
                {
                    methodContent.Add(magicContent);
                    position += s.Length;
                    continue;
                }
                position += s.Length;
            }
            return methodContent;
        }
    }
}

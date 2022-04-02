using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Magic.Editor;

namespace Magic.MO
{
    public class MagicContent
    {
        string content;
        public bool ParseStringToMagicContent(string content, int position, ref List<MagicVariable>[] editorVariables, ref MagicTags editorTags, MagicObject parentMagicObject, bool outsideMethod = false)
        {
            if (content.Length == 0) {return false; } 
            //test if content is tag:
            if(MagicTags.tagKeyWords.ToList().Any(k => k == content.Replace(" ","").Split(new char[] {':'})[0].ToLower()))
            {
                if (editorTags.ParseStringToMagicTag(content, position, ref editorVariables, parentMagicObject, outsideMethod))
                {
                    this.content = content;
                    return true;
                }
            }
            //test if content is variable:
            if(MagicVariable.ParseStringToMagicVariableExpresion(content, position, ref editorVariables, parentMagicObject))
            {
                this.content = content;
                return true;
            }
            return false;
        }
        public bool EvalueateContent(ref MagicTags magicTags, ref List<MagicVariable>[] variables, MagicObject parentMagicObject)
        {
            //variables:
            if (MagicVariable.ParseStringToMagicVariableExpresion(content, -1, ref variables, parentMagicObject)) return true;
            //tags:
            if (magicTags.ParseStringToMagicTag(content, -1, ref variables, parentMagicObject)) return true;

            return false;
        }
    }
}

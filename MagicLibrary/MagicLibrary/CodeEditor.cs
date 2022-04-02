using Magic.MO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magic.Editor
{
    public class CodeEditor
    {
        public static List<Highlighting> highlightings = new List<Highlighting>();
        public static List<MagicVariable>[] caretVariables;
        public static int caretPosition;

        public enum CaretCurrentStructure {
            outside,
            insideObject,
            insideMethod
        }

        public static CaretCurrentStructure caretCurrentStructure = CaretCurrentStructure.outside;

        /*public static bool CheckSyntax()
        {
            return false;
        } */
    }
    public class Highlighting{
        public enum Type
        {
            normalText = 0,
            magicObject = 1,
            method = 2,
            tag = 3,
            var = 4,
            error = 5,
        }
        public int start;
        public int length;
        public Type type;
        public string message;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="type">1 - object, 2 - s</param>
        public Highlighting(int start, int length, Type type)
        {
            new Highlighting(start, length, type, null);
        }
        public Highlighting(int start, int length, Type type, string message)
        {
            this.start = start;
            this.length = length;
            this.type = type;
            this.message = message;
        }
    }
}

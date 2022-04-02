using Magic.Editor;
using System;
using System.Collections.Generic;

namespace Magic.MO
{
    /// <summary>
    /// Class containing all tags of MagicObject.
    /// </summary>
    public class MagicTags : MagicContent
    {
        public readonly static string[] tagKeyWords = new string[] { "fire", "ice", "angle", "size", "velocity", "gravity", "resist" };
        public bool[] tagChanged = new bool[7];
        MagicObject parentMagicObject;
        //static tags:
        private bool fire = false;
        public bool Fire
        {
            get { return fire; }
            set { if (value) ChangeMutalTag(); fire = value; tagChanged[0] = true; }
        }
        private bool ice = false;
        public bool Ice
        {
            get { return ice; }
            set { if (value) ChangeMutalTag(); ice = value; tagChanged[1] = true; }
        }
        private float angle = 0;
        public float Angle
        {
            get { return angle; }
            set { angle = value; tagChanged[2] = true; }
        }
        //dynamic tags:
        private int size = 0;
        public int Size
        {
            get { return size; }
            set { size = value; tagChanged[3] = true; }
        }
        private float velocity = 0;
        public float Velocity
        {
            get { return velocity; }
            set { velocity = value; tagChanged[4] = true; }
        }
        public bool gravity = false;
        public bool Gravity
        {
            get { return gravity; }
            set { gravity = value; tagChanged[5] = true; }
        }
        public bool resist = false;
        public bool Resist
        {
            get { return resist; }
            set { resist = value; tagChanged[6] = true; }
        }
        public bool ParseStringToMagicTag(string content, int position, ref List<MagicVariable>[] variables, MagicObject parentMagicObject, bool outsideMethod = false)
        {
            this.parentMagicObject = parentMagicObject;
            this.outsideMethod = outsideMethod; 

            content = content.Replace(";", "");
            string[] tagsPart = content.Split(':');
            string tagContent = "";
            int tagContentStartIndex = content.IndexOf(':') + 1;
            if (tagContentStartIndex >= 1) {
                tagContent = content.Substring(content.IndexOf(':') + 1);
            }

            float value = MagicVariable.ParseExpresionStringToFloat(tagContent, ref variables, parentMagicObject);

            switch (tagsPart[0].ToLower().Trim())
            {
                case "fire":
                    if ((value == 1) == Fire) break; //break if the value is same
                    if (!UseManaBool("fire")) break; //break if there isn't enough mana
                    Fire = !Fire;
                    break;
                case "ice":
                    if ((value == 1) == Ice) break;
                    if (!UseManaBool("ice")) break;
                    Ice = !Ice;
                    break;
                case "angle":
                    float angle = value;
                    if (angle >= 360) angle -= 360 * (float)Math.Floor(angle / 360);
                    if (angle < 0) angle += 360 * (float)Math.Floor(-angle / 360);
                    if (angle == Angle) break;
                    if (!UseManaBool("angle")) break;
                    Angle = angle;
                    break;
                case "size":
                    int size = (int)Math.Floor(Tools.Clamp(value, 1, 5));
                    if (size == Size) break;
                    if (!UseManaLinear("size", size)) break;
                    Size = size;
                    break;
                case "velocity":
                    float velocity = Tools.Clamp(value, -5, 5);
                    if (!UseManaLinear("velocity", Math.Abs(velocity + 1))) break;
                    Velocity = velocity;
                    break;
                case "gravity":
                    if ((value == 1) == Gravity) break;
                    if (!UseManaBool("gravity")) break;
                    Gravity = !Gravity;
                    break;
                case "resist":
                    if ((value == 1) == Resist) break;
                    if (!UseManaBool("resist")) break;
                    Resist = !Resist;
                    break;
                default:
                    return false;
            }
            if(position != -1) CodeEditor.highlightings.Add(new Highlighting(position, tagsPart[0].Length, Highlighting.Type.tag, null));
            return true;
        }
        private bool outsideMethod;
        private bool UseManaBool(string name)
        {
            if (outsideMethod) return parentMagicObject.manaManager.UseSecretMana(TagsManaCost[name]);
            return parentMagicObject.manaManager.UseMana(TagsManaCost[name]);
        }
        private bool UseManaLinear(string name, float value)
        {
            int usedMana = (int)Math.Round((double)TagsManaCost[name] * (double)value);
            if (outsideMethod) return parentMagicObject.manaManager.UseSecretMana(usedMana);
            return parentMagicObject.manaManager.UseMana(usedMana);
        }
        //merge two MagicTags:
        public static MagicTags operator +(MagicTags a, MagicTags b)
        {
            if (b == null) return a;
            var properties = a.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                if (!b.tagChanged[i]) continue;
                var property = properties[i];
                var oldValue = property.GetValue(a, null);
                var newValue = property.GetValue(b, null);
                // this will handle the scenario where either value is null
                if (!object.Equals(oldValue, newValue))
                {
                    property.SetValue(a, newValue);
                    a.tagChanged[i] = true;
                }
            }
            return a;
        }

        static readonly Dictionary<string, int> TagsManaCost = new Dictionary<string, int>()
        {
            { "fire", 20 },
            { "ice", 20 },
            { "angle", 1 },
            { "size", 20 },
            { "velocity", 20 },
            { "gravity", 50 },
            { "resist", 50 }
        };

        private void ChangeMutalTag()
        {
            fire = false;
            tagChanged[0] = false;
            ice = false;
            tagChanged[1] = false;
        }
    }
}

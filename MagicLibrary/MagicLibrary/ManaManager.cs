using System;
using System.Collections.Generic;
using System.Text;

namespace Magic.MO
{
    public class ManaManager
    {
        public bool editor;
        int mana = 2000;
        int secretMana = 2000;
        public int Mana { get { return mana; } set { mana = Tools.Clamp(value, 0, 2000); } }
        public int SecretMana { get { return secretMana; } set { secretMana = Tools.Clamp(value, 0, 2000); } }
        public bool UseMana(int cost)
        {
            UnityEngine.Debug.Log("current mana: " + mana + " cost: " + cost);
            if (editor) return true;
            if(Mana - cost > 0)
            {
                Mana -= cost;
                return true;
            }
            else
            {
                Mana = 0;
                return false;
            }
        }
        public bool UseSecretMana(int cost)
        {
            UnityEngine.Debug.Log("current secret mana: " + mana + " cost: " + cost);
            if (secretMana - cost > 0)
            {
                secretMana -= cost;
                return true;
            }
            else
            {
                secretMana = 0;
                return false;
            }
        }

        public static readonly Dictionary<string, int> ManaCost = new Dictionary<string, int>()
        {
            { "MagicObject", 200 },
            //External Methods
            { "Distance", 20 },
            { "Scan", 20 },
        };
    }
}

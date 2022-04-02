using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(int))]
public class test : PropertyDrawer
{
    float verticalPostition;
    float horizontalPosition;
    float positionX;
    Rect positionSave;
    /**
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.name.Contains("test"))
        {
            string name = property.name.Replace("test", "");
            byte counter = 0;
            counter = byte.Parse(name);
            counter -= 1;

            if (counter == 0) { 
                verticalPostition = position.position.y;
                horizontalPosition = position.position.x;
            }
            //position.position = new Vector2(position.position.x, verticalPostition);// + position.height * counter;
            position.position = new Vector2(horizontalPosition + position.x / 2 * counter, position.position.y);// + position.width * counter;

            Debug.Log("counter: " + counter);

            EditorGUI.BeginProperty(position, label, property);
            if (counter == 0)
            {
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                positionSave = position;// + position.width;
            }
            else 
            {
                //position.position = new Vector2(positionX, position.position.y);
                position = positionSave;
                position.x += 100;
            }
            GUI.backgroundColor = Color.cyan;
            Rect newPosition = new Rect(position.position, new Vector2(position.x / 2 ,position.height));
            EditorGUI.PropertyField(newPosition, property, GUIContent.none);
            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            EditorGUI.PropertyField(position, property, GUIContent.none);
            EditorGUI.EndProperty();
        }
    }
    // Start is called before the first frame update
    **/
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

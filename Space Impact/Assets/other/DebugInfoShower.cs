using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfoShower : MonoBehaviour
{
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;

    public Text text;
    public Text text2;
    void Start()
    {
        GameObject g = GameObject.Find("MainUi");
        text = g.transform.GetChild(0).GetComponent<Text>();
        text2 = g.transform.GetChild(1).GetComponent<Text>();
        text.text = "Difficulty: " + GlobalVariables.GameDifficulty;
    }
    void Update()
    {
        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            m_lastFramerate = (float)m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }

        text2.text = "fps: " + m_lastFramerate;
    }
}

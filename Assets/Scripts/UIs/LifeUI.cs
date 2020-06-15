using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    public int m_Life = 3;
    public string m_Mask = "00";
    private Text m_Text;

    private void Start()
    {
        m_Text = GetComponent<Text>();
        UpdateUI();
    }

    public void LifeUp()
    {
        m_Life++;
        UpdateUI();
    }

    public void LifeDown()
    {
        m_Life--;
    	UpdateUI();
    }

    private void UpdateUI()
    {
        m_Text.text = $"<size=32>x</size>{m_Life.ToString(m_Mask)}";
    }
}


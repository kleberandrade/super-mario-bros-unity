using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    public int m_Life = 3;
    public string m_Mask = "00";
    private Text m_Text;
    private const string m_Key = "life";

    private void OnEnable()
    {
        m_Life = PlayerPrefs.GetInt(m_Key, m_Life);
    }

    private void OnDestroy()
    {
        if (m_Life != 0)
            PlayerPrefs.SetInt(m_Key, m_Life);
        else
            PlayerPrefs.DeleteKey(m_Key);
    }

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


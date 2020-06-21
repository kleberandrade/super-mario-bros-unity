using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public int m_Value;
    public string m_Mask = "00";
    private Text m_Text;

    private void Start()
    {
        m_Text = GetComponent<Text>();
        UpdateUI();
    }

    public void AddCoins(int value)
    {
        m_Value += value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_Text.text = m_Value.ToString(m_Mask);
    }
}
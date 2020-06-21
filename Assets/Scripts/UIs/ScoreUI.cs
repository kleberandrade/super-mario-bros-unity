using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public int m_Score;
    public string m_Mask = "000000000";
    private Text m_Text;
    private const string m_Key = "score";

    private void OnEnable()
    {
        m_Score = PlayerPrefs.GetInt(m_Key, m_Score);
    }

    private void OnDestroy()
    {
        if (m_Score != 0)
            PlayerPrefs.SetInt(m_Key, m_Score);
        else
            PlayerPrefs.DeleteKey(m_Key);
    }

    private void Start()
    {
        m_Text = GetComponent<Text>();
        UpdateUI();
    }

    public void AddScore(int score)
    {
        m_Score += score;
    }

    private void UpdateUI()
    {
        m_Text.text = m_Score.ToString(m_Mask);
    }
}


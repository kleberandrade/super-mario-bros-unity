using UnityEngine;
using UnityEngine.UI;

public class ChronometerUI : MonoBehaviour
{
    public float m_MaxTime = 300;
    public float m_ElapsedTime;
    public bool m_FinishLevel;
    public string m_Mask = "000";
    private Text m_Text;

    private void Start()
    {
        m_Text = GetComponent<Text>();
        UpdateUI();
    }

    private void Update()
    {
        if (m_FinishLevel)
            return;

        m_ElapsedTime += Time.deltaTime;
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_Text.text = Mathf.Ceil(m_MaxTime - m_ElapsedTime).ToString(m_Mask);
    }
}


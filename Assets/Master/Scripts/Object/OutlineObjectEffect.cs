using UnityEngine;

public class OutlineObjectEffect : MonoBehaviour
{
    [SerializeField] private Outline m_Outline;
    [SerializeField] private float m_Speed = 10f;
    private int m_MaxLoop = 2;
    private float m_DefaultWidht = 0;
    private float m_MaxValueWidth = 6f;
    private int m_CurrentLoop = 0;
    private bool m_IsActiveOutLineEffector = false;
    public bool IsFinished => m_CurrentLoop >= m_MaxLoop;
    private void Start()
    {
        ResetOutLineEffect();
        ActiveOutLineEffect(false);
    }
    private void Update()
    {
        RunEffectOutline();
    }
    public void ResetOutLineEffect()
    {
        m_CurrentLoop = 0;
        m_Outline.OutlineWidth = m_DefaultWidht;
    }
    public void RunEffectOutline()
    {
        if (m_CurrentLoop >= m_MaxLoop || !m_IsActiveOutLineEffector) return;
        m_Outline.OutlineWidth += Time.deltaTime * m_Speed;
        if (m_Outline.OutlineWidth >= m_MaxValueWidth)
        {
            m_Outline.OutlineWidth = 0;
            m_CurrentLoop++;
        }
    }
    public void StopEffectOutLine()
    {
        ActiveOutLineEffect(false);
        ResetOutLineEffect();
    }
    public void ActiveOutLineEffect(bool isActiveOutLineEffect)
    {
        m_IsActiveOutLineEffector = isActiveOutLineEffect;
    }
  
}


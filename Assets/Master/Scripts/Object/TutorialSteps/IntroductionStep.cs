using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroductionStep : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI m_TextTitle;
    [SerializeField] private TextMeshProUGUI m_TextIntroduction;
    [SerializeField] private List<IntroductionStepData> m_ListIntroductionSteps;
    private Coroutine m_RunningCoroutine;

    private void OnEnable()
    {
        m_TextTitle.text = Config.Text_Title_Introduction;
        m_RunningCoroutine = StartCoroutine(CoroutineRunStepIntroduction());
    }
    private void OnDisable()
    {
        if (m_RunningCoroutine != null)
        {
            StopStepIntroduction();
            m_RunningCoroutine = null;
            m_TextTitle.gameObject.SetActive(false);
        }
    }
    private IEnumerator CoroutineRunStepIntroduction()
    {
        foreach (var step in m_ListIntroductionSteps)
        {
            m_TextIntroduction.gameObject.SetActive(true);
            step.m_Outline.ResetOutLineEffect();
            step.m_Outline.ActiveOutLineEffect(true);
            m_TextIntroduction.text = step.m_TextNameDevice;
            m_TextTitle.gameObject.SetActive(true);
            yield return new WaitUntil(() => step.m_Outline.IsFinished);
            step.m_Outline.ActiveOutLineEffect(false);
        }
        //Debug.Log("Introduction completed!");
    }
    public void StopStepIntroduction()
    {
        StopCoroutine(m_RunningCoroutine);
        foreach (var step in m_ListIntroductionSteps) step.m_Outline.StopEffectOutLine();
    }
}
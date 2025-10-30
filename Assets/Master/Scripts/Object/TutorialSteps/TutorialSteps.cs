using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
public class TutorialSteps : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<StepBase> m_ListStepBaseObject;
    private int m_CurrentStep = 0;
    private float m_DelayTimeToShowNextStep = 2f;

    private void OnEnable()
    {
        StartCoroutine(DelayShowNextStep());
    }
    private IEnumerator DelayShowNextStep()
    {
        yield return new WaitForSeconds(m_DelayTimeToShowNextStep);
        ShowNextStep();
    }
    private void Start()
    {
        foreach (var step in m_ListStepBaseObject) step.OnStepFinished += HandleStepFinished;
    }

    public void ResetAllStep()
    {
        AudioMainManager.Instance.StopLoop();
        foreach (var step in m_ListStepBaseObject) 
        {
            step.ResetStep();
        }
    }
    private void HandleStepFinished(StepBase finishedStep) => ShowNextStep();
    private void ShowNextStep()
    {
        m_ListStepBaseObject[m_CurrentStep].StartStep();
        m_CurrentStep++;
    }
}

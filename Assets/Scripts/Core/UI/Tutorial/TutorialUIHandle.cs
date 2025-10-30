using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUIHandle : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private TutorialSO tutorialData;
    [Header("Reference")]
    [SerializeField] private TutorialUI m_tutorialUI;
    [SerializeField] private TextMeshProUGUI m_buttonNameText;
    [SerializeField] private TextMeshProUGUI m_buttonContenText;
    [SerializeField] private GameObject m_ButtonPrevious;
    [SerializeField] private Canvas canvas;

    private void OnEnable()
    {
        SetUp();
        ShowStep(0);
        ActiveButtonPrev(false);
    }

    private void SetUp()
    {
        canvas.worldCamera = Camera.main;
    }
    public void NextStep()
    {
        int index = m_tutorialUI.ShowNextStep();
        //ket thuc tutoral
        ShowStep(index);
        ActiveButtonPrev(true);
        Debug.Log("index:" + index);
    }
    public void Skip()
    {
        MainManager.Instance.LoadMenu();
    }
    public void PreViousStep()
    {
        int index = m_tutorialUI.ShowPreViousStep();
        ShowStep(index);
        Debug.Log("index:" + index);
    }
    private void ShowStep(int index)
    {

        if (index < 0 || index >= tutorialData.Data.Count)
        {
            MainManager.Instance.LoadExp();
            return;
        }
        if (index == 0) ActiveButtonPrev(false);
        var data = tutorialData.Data[index];
        m_buttonNameText.text = data.buttonName;
        m_buttonContenText.text = data.buttonContent;
        AudioMainManager.Instance.PlayAudioIntroduction(data.buttonAudio);
    }
    public void TutorialEnd()
    {
        MainManager.Instance.LoadMenu();
    }

    private void ActiveButtonPrev(bool isActive)
    {
        m_ButtonPrevious.SetActive(isActive);
    }
}

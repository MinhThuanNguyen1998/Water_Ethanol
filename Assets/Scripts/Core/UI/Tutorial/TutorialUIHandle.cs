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
    [SerializeField] private Canvas canvas;
    public event Action OnEndTutorial;
    private void OnEnable()
    {
        SetUp();
        ShowStep(0);

    }
    private void SetUp()
    {
        canvas.worldCamera = Camera.main;
    }
    public void NextStep()
    {
        //Debug.Log("index:" + index);
        int index = m_tutorialUI.ShowNextStep();
        //ket thuc tutoral
        if (index == -1) MainManager.Instance.LoadExp();
        //if (index == -1) TutorialEnd();
        ShowStep(index);
    }
    private void ShowStep(int index)
    {
        if (index < 0 || index >= tutorialData.Data.Count)
        {
            MainManager.Instance.LoadMenu();
            return;
        }
        var data = tutorialData.Data[index];
        m_buttonNameText.text = data.buttonName;
        m_buttonContenText.text = data.buttonContent;
        AudioMainManager.Instance.PlayAudioIntroduction(data.buttonAudio);
            
    }
    public void TutorialEnd()
    {
        OnEndTutorial?.Invoke();
        MainManager.Instance.LoadMenu();
    }
}

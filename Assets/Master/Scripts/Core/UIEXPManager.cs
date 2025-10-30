using System;
using UnityEngine;
using UnityEngine.UI;

public class UIEXPManager : MonoBehaviour
{
    [SerializeField] private GameObject m_ScrollViewDoc;
    [SerializeField] private GameObject m_ScrollViewStep;

    [SerializeField] private Button m_OnButtonDoc;
    [SerializeField] private Button m_OnButtonStep;

    private GameObject m_CurrentActivePanel = null;

    private void Start()
    {
        m_OnButtonDoc.onClick.AddListener(() => TogglePanel(m_ScrollViewDoc));
        m_OnButtonStep.onClick.AddListener(() => TogglePanel(m_ScrollViewStep));

        m_ScrollViewDoc.SetActive(false);
        m_ScrollViewStep.SetActive(false);
    }
    private void TogglePanel(GameObject targetPanel)
    {
        if (m_CurrentActivePanel == targetPanel)
        {
            targetPanel.SetActive(false);
            m_CurrentActivePanel = null;
        }
        else
        {
            if (m_CurrentActivePanel != null) m_CurrentActivePanel.SetActive(false);
            targetPanel.SetActive(true);
            m_CurrentActivePanel = targetPanel;
        }
    }
}

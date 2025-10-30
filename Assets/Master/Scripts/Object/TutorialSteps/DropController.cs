using System;
using System.Collections;
using UnityEngine;

public class DropController : StepBase
{
    [SerializeField] private Transform m_TargetPosObject;
    [SerializeField] private Transform m_ParentDropPos;
    [SerializeField] GameObject m_DropPrefab;
    [SerializeField] private LevelLiquidControl m_LevelLiquidVaseControl;
    private float m_TimeToCloneDropPrefab = 5f;
    private float m_TimeToDone = 15f;
    private Coroutine m_EndPopupCoroutine;
    public override void StartStep()
    {
        base.StartStep();
        if (m_ArrowDirectionObject != null && m_TargetPosObject != null) m_ArrowDirectionObject.SetPosArrowDirection(m_TargetPosObject);
        m_EndPopupCoroutine = StartCoroutine(ShowEndPopupAfterDelay(m_TimeToDone));
        InvokeRepeating(nameof(DropObject), 0f, m_TimeToCloneDropPrefab);
    }
    private IEnumerator ShowEndPopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Completed, Config.Button_Yes);
    }

    public override void FinishStep() => base.FinishStep();
    public override void ResetStep()
    {
        base.ResetStep();
        CancelInvoke(nameof(DropObject));
        m_LevelLiquidVaseControl.ResetFillLevel();
        if (m_EndPopupCoroutine != null)
        {
            StopCoroutine(m_EndPopupCoroutine);
            m_EndPopupCoroutine = null;
        }
    }
    private void DropObject()
    {
        if (m_DropPrefab != null && m_ParentDropPos != null)
        {
            GameObject drop = Instantiate(m_DropPrefab, m_ParentDropPos.position, Quaternion.identity);
            drop.transform.SetParent(m_ParentDropPos, true);
        }
    } 
}

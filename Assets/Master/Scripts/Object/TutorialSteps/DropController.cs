using System;
using System.Collections;
using UnityEngine;

public class DropController : StepBase
{
    [SerializeField] private Transform m_TargetPosObject;
    [SerializeField] private Transform m_ParentDropPos;
    [SerializeField] GameObject m_DropPrefab;
    [SerializeField] private LevelLiquidControl m_LevelLiquidVaseControl;
    private float m_TimeToCloneDropPrefab = 1f;
    private float m_TimeToDone = 15f;
    private Coroutine m_EndPopupCoroutine;

    private void Start() => VolumeSliderController.OnTimeSliderValueChanged += SetDefaultDropTime;

    public override void StartStep()
    {
        base.StartStep();
        VolumeSliderController.OnTimeSliderValueChanged += UpdateDropTime;
        if (m_ArrowDirectionObject != null && m_TargetPosObject != null) m_ArrowDirectionObject.SetPosArrowDirection(m_TargetPosObject);
        m_EndPopupCoroutine = StartCoroutine(ShowEndPopupAfterDelay(m_TimeToDone));
        InvokeRepeating(nameof(DropObject), 0f, m_TimeToCloneDropPrefab);
    }
    private IEnumerator ShowEndPopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PopupManager.Instance.ShowPopup(PopupType.Success, Config.Completed, Config.Button_Yes);
    }
    public override void FinishStep() => base.FinishStep();
    public override void ResetStep()
    {
        base.ResetStep();
        VolumeSliderController.OnTimeSliderValueChanged -= UpdateDropTime;
        VolumeSliderController.OnTimeSliderValueChanged -= SetDefaultDropTime;
        CancelInvoke(nameof(DropObject));
        m_LevelLiquidVaseControl.ResetFillLevel();
        if (m_EndPopupCoroutine != null)
        {
            StopCoroutine(m_EndPopupCoroutine);
            m_EndPopupCoroutine = null;
        }
    }
    private void UpdateDropTime(float value)
    {
        SetDefaultDropTime(value);
        CancelInvoke(nameof(DropObject));
        InvokeRepeating(nameof(DropObject), m_TimeToCloneDropPrefab, m_TimeToCloneDropPrefab);
    }

    private void SetDefaultDropTime(float value)
    {
        m_TimeToCloneDropPrefab = value;
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

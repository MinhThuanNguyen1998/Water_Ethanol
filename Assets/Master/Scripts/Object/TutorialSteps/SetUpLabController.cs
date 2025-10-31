using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class SetUpLabController : StepBase
{
    [SerializeField] private List<MovingObjectByMouse> m_ListObject;
    [SerializeField] private List<OutlineHoverEffect> m_ListOutLineHoverObject;
    [SerializeField] private List<TriggerPlacer> m_ListTriggerPlacer;
    private int m_PlacedCount = 0;
    private float m_TimeToFinishStep = 2f;

    protected override void OnEnable() => TutorialUIHandle.OnStartIntroductionMode += ResetOutLineHoverObject;
    protected override void OnDisable() => TutorialUIHandle.OnStartIntroductionMode -= ResetOutLineHoverObject;
   
    private void Start()
    {
        CheckListObjectCanMoveByMouse(false);
        foreach (var placer in m_ListTriggerPlacer)
        {
            placer.OnObjectPlaced += HandleObjectPlaced;
        }
    }
    public override void StartStep()
    {
        base.StartStep();
        CheckListObjectCanMoveByMouse(true);
        ResetOriginalPosMovingObject();
        ResetOutLineHoverObject(true);
        m_PlacedCount = 0;
    }
    public override void FinishStep()
    {
        base.FinishStep();
        CheckListObjectCanMoveByMouse(false);
    }
    public override void ResetStep()
    {
        base.ResetStep();
        CheckListObjectCanMoveByMouse(false);
        ResetOriginalPosMovingObject();
        ResetOutLineHoverObject(true);
        m_PlacedCount = 0;
    }
    private void HandleObjectPlaced(TriggerPlacer placer)
    {
        m_PlacedCount++;
        if (m_PlacedCount >= m_ListTriggerPlacer.Count)
        {
            StartCoroutine(CoroutineWaitingToFinishStep());
        }
    }
    private IEnumerator CoroutineWaitingToFinishStep()
    {
        yield return new WaitForSeconds(m_TimeToFinishStep);
        PopupManager.Instance.ShowPopup(PopupType.Success, Config.Right, Config.Button_Yes);
        FinishStep();
        
    }
    private void CheckListObjectCanMoveByMouse(bool isMovingByMouse)
    {
        if (m_ListObject != null) 
        { 
            foreach (var objectMoving in m_ListObject) objectMoving.CheckMovingObjectByMouse(isMovingByMouse);
        }
    }
    private void ResetOriginalPosMovingObject()
    {
        foreach (var objectMoving in m_ListObject) objectMoving.ResetTransform();
    }
    private void ResetOutLineHoverObject(bool isHover)
    {
        foreach(var objectOutline in m_ListOutLineHoverObject) objectOutline.IsHover = isHover;
    }
}

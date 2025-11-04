using System;
using UnityEngine;


public abstract class StepBase : MonoBehaviour, IStep
{
    [Header("Step ID")]
    [SerializeField] protected int StepID;
    [SerializeField] protected ArrowDirectionController m_ArrowDirectionObject;
    public bool IsFinished { get; protected set; }
    public event Action<StepBase> OnStepFinished;
    public static event Action<int> OnGotoState;
    protected virtual void OnEnable(){}
    protected virtual void OnDisable(){}
   
    public virtual void StartStep()
    {
        IsFinished = false;
        OnGotoState?.Invoke(StepID);

    }
    public virtual void FinishStep()
    {
        IsFinished = true;
        OnStepFinished?.Invoke(this);
    }
    public virtual void ActiveArrowDirectionObject(bool isActive)
    {
        m_ArrowDirectionObject.ActiveArrowDirectionObject(isActive);
    }
}

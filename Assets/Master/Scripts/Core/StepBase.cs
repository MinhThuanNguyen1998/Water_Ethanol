using System;
using UnityEngine;


public abstract class StepBase : MonoBehaviour, IStep
{
    [SerializeField] protected ArrowDirectionController m_ArrowDirectionObject;
    public bool IsFinished { get; protected set; }
    public event Action<StepBase> OnStepFinished;

    protected virtual void OnEnable(){}
    protected virtual void OnDisable(){}
   
    public virtual void StartStep()
    {
        IsFinished = false;
    }
    public virtual void FinishStep()
    {
        IsFinished = true;
        OnStepFinished?.Invoke(this);
    }
    public virtual void ResetStep() 
    {
        IsFinished = false;
        ActiveArrowDirectionObject(false);
    }
    public virtual void ActiveArrowDirectionObject(bool isActive)
    {
        m_ArrowDirectionObject.ActiveArrowDirectionObject(isActive);
    }
}

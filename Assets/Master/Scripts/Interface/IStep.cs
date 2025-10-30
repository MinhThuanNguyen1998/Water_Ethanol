using UnityEngine;

public interface IStep 
{
    bool IsFinished { get; }
    void StartStep() { }
    void ResetStep() { }  
    void FinishStep() { }
}

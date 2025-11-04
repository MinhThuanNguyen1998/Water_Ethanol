using System.Collections;
using UnityEngine;

public class BurnerController : StepBase
{
    [SerializeField] private Transform m_TargetPosObject;
    [SerializeField] private GameObject m_Knob;
    [SerializeField] private Animator m_AnimKnobRotation;
    [SerializeField] private ParticleSystem m_ParticleSystemFire;

    private string KnobTag = "Knob";
    private string ParamIsRotating = "IsRotating";
    private string ParamIsReset = "IsReset";

    private bool m_IsKnobRotated = false;
    private bool m_IsCanClickKnob = false;

    private float m_TimeWaitToBurn = 1.35f;
    private float m_TimeWaitToFinishStep = 10f;
    private void Start() => ResetKnob();
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    public override void StartStep()
    {
        base.StartStep();
        if (m_ArrowDirectionObject != null && m_TargetPosObject != null) m_ArrowDirectionObject.SetPosArrowDirection(m_TargetPosObject);
        m_ParticleSystemFire?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        m_IsCanClickKnob = true;
    }
    public override void FinishStep() => base.FinishStep();
    private void Update()
    {
        if (m_IsKnobRotated || !Input.GetMouseButtonDown(0) || !m_IsCanClickKnob) return;
        if (TryHitKnob(out _))
        {
            AudioMainManager.Instance.PlayOnShot(SoundType.GasIgnition);
            RotateKnob();
            ActiveArrowDirectionObject(false);
        }
    }
    private bool TryHitKnob(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
            return hit.collider.CompareTag(KnobTag);
        return false;
    }
    private void RotateKnob()
    {
        if (m_AnimKnobRotation != null)
        {
            m_AnimKnobRotation.SetBool(ParamIsRotating, true);
            m_AnimKnobRotation.SetBool(ParamIsReset, false);
            m_IsKnobRotated = true;
            StartCoroutine(CoroutineWaitingToBurn());
        }
    }
    private void ResetKnob()
    {
        if (m_AnimKnobRotation != null)
        {
            m_AnimKnobRotation.SetBool(ParamIsReset, true);
            m_AnimKnobRotation.SetBool(ParamIsRotating, false);
        }
        m_ParticleSystemFire?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        m_IsKnobRotated = false;
        m_IsCanClickKnob = false;
    }
    private IEnumerator CoroutineWaitingToBurn()
    {
        yield return new WaitForSeconds(m_TimeWaitToBurn);
        m_ParticleSystemFire?.Play();
        yield return new WaitForSeconds(m_TimeWaitToFinishStep);
        FinishStep();
    }
}

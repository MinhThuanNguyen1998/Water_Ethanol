using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AirController : StepBase
{
    [SerializeField] private Transform m_TargetPosObject;
    [SerializeField] private ParticleSystem m_ParticleSystemSmoke;
    [SerializeField] private List<Renderer> m_ListRenderer;        
    [SerializeField] private Material m_OriginMaterial;
    [SerializeField] private Material m_EffectAirMaterial;

    private Coroutine m_ChangeMaterialCoroutine;
    private float m_TimeToChangeMaterial = 8f;
    private float m_LerpDuration = 6f;
    private void Start()
    {
        ResetEffect();
    }
    public override void StartStep()
    {
        base.StartStep();
        if (m_ArrowDirectionObject != null && m_TargetPosObject != null) m_ArrowDirectionObject.SetPosArrowDirection(m_TargetPosObject);
        AudioMainManager.Instance.PlayLoop(SoundType.BoilingWater);
        m_ParticleSystemSmoke.Play();
        if (m_ChangeMaterialCoroutine != null) StopCoroutine(m_ChangeMaterialCoroutine);
        m_ChangeMaterialCoroutine = StartCoroutine(ChangeMaterialAfterDelay(m_TimeToChangeMaterial , m_LerpDuration));
    }
    private IEnumerator ChangeMaterialAfterDelay(float delay, float lerpDuration)
    {
        yield return new WaitForSeconds(delay);
        yield return LerpMaterial(m_OriginMaterial, m_EffectAirMaterial, lerpDuration);
        m_ChangeMaterialCoroutine = null;
    }
    private IEnumerator LerpMaterial(Material fromMat, Material toMat, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            float t = time / duration;
            foreach (var renderer in m_ListRenderer) 
            {
                if (renderer != null) renderer.material.Lerp(fromMat, toMat, t);
            }
            time += Time.deltaTime;
            yield return null;
        }
        SetMaterial(toMat);
        yield return new WaitForSeconds(2f);
        FinishStep();
    }
    public override void FinishStep() => base.FinishStep();
    private void SetMaterial(Material material)
    {
        if (m_ListRenderer == null) return;
        foreach (var renderer in m_ListRenderer)
        {
            if (renderer != null) renderer.material = material;
        }
    }
    private void ResetEffect()
    {
        m_ParticleSystemSmoke.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        SetMaterial(m_OriginMaterial);
        AudioMainManager.Instance.StopLoop();
    }
}

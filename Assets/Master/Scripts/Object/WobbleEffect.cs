using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleEffect : MonoBehaviour
{
   
    Vector3 m_LastPos;
    Vector3 m_Velocity;
    Vector3 m_LastRot;  
    Vector3 m_AngularVelocity;

    [SerializeField] private float MaxWobble = 0.03f;
    [SerializeField] private float WobbleSpeed = 1f;
    [SerializeField] private float Recovery = 1f;
    [SerializeField] private Renderer m_Renderer;

    float m_WobbleAmountX;
    float m_WobbleAmountZ;
    float m_WobbleAmountToAddX;
    float m_WobbleAmountToAddZ;
    float m_Pulse;
    float m_Time = 0.5f;
    
    // Use this for initialization
    void Start()
    {
        
        
    }
    private void Update()
    {
        
    }


    private void CreateWobbelEffect()
    {
        if (m_Renderer == null) return;
        m_Time += Time.deltaTime;
        // decrease wobble over time
        m_WobbleAmountToAddX = Mathf.Lerp(m_WobbleAmountToAddX, 0, Time.deltaTime * (Recovery));
        m_WobbleAmountToAddZ = Mathf.Lerp(m_WobbleAmountToAddZ, 0, Time.deltaTime * (Recovery));

        // make a sine wave of the decreasing wobble
        m_Pulse = 2 * Mathf.PI * WobbleSpeed;
        m_WobbleAmountX = m_WobbleAmountToAddX * Mathf.Sin(m_Pulse * m_Time);
        m_WobbleAmountZ = m_WobbleAmountToAddZ * Mathf.Sin(m_Pulse * m_Time);

        // send it to the shader
        m_Renderer.sharedMaterial.SetFloat("_WobbleX", m_WobbleAmountX);
        m_Renderer.sharedMaterial.SetFloat("_WobbleZ", m_WobbleAmountZ);

        // velocity
        m_Velocity = (m_LastPos - transform.position) / Time.deltaTime;
        m_AngularVelocity = transform.rotation.eulerAngles - m_LastRot;


        // add clamped velocity to wobble
        m_WobbleAmountToAddX += Mathf.Clamp((m_Velocity.x + (m_AngularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
        m_WobbleAmountToAddZ += Mathf.Clamp((m_Velocity.z + (m_AngularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);

        // keep last position
        m_LastPos = transform.position;
        m_LastRot = transform.rotation.eulerAngles;
    }
}
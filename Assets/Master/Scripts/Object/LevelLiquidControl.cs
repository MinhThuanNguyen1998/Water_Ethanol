
using UnityEngine;

public class LevelLiquidControl : MonoBehaviour
{
    [SerializeField] private Renderer m_Renderer;
    private Material m_Material;
    private float m_FillValue = 0f;
    public float CurrentFill
    {
        get => m_FillValue;
        set
        {
            m_FillValue = Mathf.Clamp01(value);
            UpdateFillLevel();
        }
    }
    private void Start()
    {
        if (m_Renderer != null) m_Material = m_Renderer.sharedMaterial;
        ResetFillLevel();
    }

    public void ControlFillLevel(float value) => CurrentFill = value;
    public void ResetFillLevel() => ControlFillLevel(0f);
    
    private void UpdateFillLevel() => m_Material?.SetFloat("_Fill", m_FillValue);
 
}


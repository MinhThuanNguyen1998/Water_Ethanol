using UnityEngine;

public class DropDestroyer : MonoBehaviour
{
    [SerializeField] private LevelLiquidControl m_LevelLiquidVaseControl;
    private float m_FillValue = 0.01f;
    private float m_MaxFillValue = 0.2f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drop"))
        {
            Transform parent = other.transform.parent;
            if (parent != null) 
            {
                if (m_LevelLiquidVaseControl != null)
                {
                    float newFill = Mathf.Clamp(m_LevelLiquidVaseControl.CurrentFill + m_FillValue, 0f, m_MaxFillValue);
                    m_LevelLiquidVaseControl.ControlFillLevel(newFill);
                }
                Destroy(parent.gameObject);
            }
        }
        
    }
}

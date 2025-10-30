using UnityEngine;

public class ChangeFillValue : MonoBehaviour
{
    [SerializeField] private LevelLiquidControl m_LevelLiquidControl;
    private float m_ValueFillLevel = 0.4f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Debug.Log("UpArrow");
            m_ValueFillLevel += Time.deltaTime * 0.1f;
            m_LevelLiquidControl.ControlFillLevel(m_ValueFillLevel);

        }
    }
}


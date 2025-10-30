using UnityEngine;

public class ArrowDirectionController : MonoBehaviour
{
    [SerializeField] private float m_Speed = 5f;
    [SerializeField] private GameObject m_ObjectArrowDirection;

    private void Start()
    {
        ActiveArrowDirectionObject(false);
    }
    private void Update()
    {
        if (m_ObjectArrowDirection == null || !m_ObjectArrowDirection.activeSelf) return;
        m_ObjectArrowDirection.transform.Rotate(m_Speed * Time.deltaTime, 0,0);
    }
    public void SetPosArrowDirection(Transform pos)
    {
        ActiveArrowDirectionObject(true);
        m_ObjectArrowDirection.transform.position = pos.position;
    }
    public void ActiveArrowDirectionObject(bool isActive)
    {
        m_ObjectArrowDirection.gameObject.SetActive(isActive);
    }
}

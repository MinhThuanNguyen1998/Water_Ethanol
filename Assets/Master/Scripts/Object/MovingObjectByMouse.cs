using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingObjectByMouse : MonoBehaviour
{
    [SerializeField] private Renderer m_BoundaryCube;
    private Vector3 m_Offset;
    private Vector3 m_InitialPosition;

    private bool m_IsCanMoveByMouse = false;
    private bool m_IsDragging = false;

    private Bounds m_Bounds;

    private void Start()
    {
        if (m_BoundaryCube != null) m_Bounds = m_BoundaryCube.bounds;
        m_InitialPosition = transform.position;
    }
    private void OnMouseDown()
    {
        if (!m_IsCanMoveByMouse) return;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; 
        m_Offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
        m_IsDragging = true;
    }
    private void OnMouseDrag()
    {
        if (!m_IsCanMoveByMouse) return;
        if (m_IsDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition) + m_Offset;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(newPosition);
            newPosition = Camera.main.ScreenToWorldPoint(screenPos);
            newPosition.x = Mathf.Clamp(newPosition.x, m_Bounds.min.x, m_Bounds.max.x);
            newPosition.y = Mathf.Clamp(newPosition.y, m_Bounds.min.y, m_Bounds.max.y);
            transform.position = newPosition;
        }
    }
    private void OnMouseUp() => m_IsDragging = true;
    public void CheckMovingObjectByMouse(bool isMoving) =>m_IsCanMoveByMouse = isMoving;
    public void ResetTransform() => transform.position = m_InitialPosition;   
}


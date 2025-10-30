using System.Collections;
using UnityEngine;

public class TriggerPlacer : MonoBehaviour
{
    [SerializeField] private string m_TagName;   
    [SerializeField] private Transform m_Target;

    private bool m_IsPlaced = false;
    public event System.Action<TriggerPlacer> OnObjectPlaced;
    private float m_TimeToMoveToPlacer = 0.2f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_TagName))
        {
            //Debug.Log("TriggerPlacer: OnTriggerEnter" + m_TagName);
            var mover = other.GetComponent<MovingObjectByMouse>();
            StartCoroutine(CoroutineSmoothMove(other.transform, m_Target.position, m_TimeToMoveToPlacer));
            mover.CheckMovingObjectByMouse(false);
            m_IsPlaced = true;
            OnObjectPlaced?.Invoke(this);
        }
        
    }
    private IEnumerator CoroutineSmoothMove(Transform obj, Vector3 targetPos, float duration)
    {
        Vector3 startPos = obj.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t);
            obj.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
        obj.position = targetPos;
    }
}

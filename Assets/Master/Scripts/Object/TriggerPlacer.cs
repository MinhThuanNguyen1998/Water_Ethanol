using System.Collections;
using UnityEngine;

public class TriggerPlacer : MonoBehaviour
{
    [SerializeField] private string m_TagName;   
    [SerializeField] private Transform m_Target;

    public event System.Action<TriggerPlacer> OnObjectPlaced;
    private float m_TimeToMoveToPlacer = 0.2f;
    private float m_TimeToStopOutline = 0.5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_TagName))
        {
            //Debug.Log("TriggerPlacer: OnTriggerEnter" + m_TagName);
            var mover = other.GetComponent<MovingObjectByMouse>();
            var outline = other.GetComponent<OutlineHoverEffect>();
            StartCoroutine(CoroutineSmoothMove(other.transform, m_Target.position, m_TimeToMoveToPlacer));
            mover.CheckMovingObjectByMouse(false);
            StartCoroutine(CoroutineDisableOutlineAfterDelay(other));
            OnObjectPlaced?.Invoke(this);
        }
    }
    private IEnumerator CoroutineDisableOutlineAfterDelay(Collider other)
    {
        yield return new WaitForSeconds(m_TimeToStopOutline);

        var outline = other.GetComponent<Outline>();
        outline.OutlineWidth = 0;
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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextContent;
    [SerializeField] private TextMeshProUGUI m_TextButtonYES;
    public void SetContent(string content, string buttonTextYES)
    {
        //Debug.Log("SetContent");
        m_TextContent.text = content;
        m_TextButtonYES.text = buttonTextYES;
        
    }
    public void OnButtonYES()
    {
        Destroy(gameObject);
    }
    public void OnButtonNO()
    {
        Destroy(gameObject);
    }
}

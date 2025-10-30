using UnityEngine;

public enum PopupType
{
    Fail,
    Success,
}
public class PopupManager : SingletonNotBaseSource<PopupManager>
{
    [SerializeField] private Popup m_FailPopupPrefab;
    [SerializeField] private Popup SuccessPopupPrefab;

    public void ShowPopup(PopupType type, string content, string buttonTextYES)
    {
        //Debug.Log("ShowPopup:" + Equals(content));
        Popup prefab = null;

        switch (type)
        {
            case PopupType.Fail:
                prefab = m_FailPopupPrefab;
                AudioMainManager.Instance.PlayOnShot(SoundType.Popup);
                break;
            case PopupType.Success:
                prefab = SuccessPopupPrefab;
                AudioMainManager.Instance.PlayOnShot(SoundType.Popup);
                break;
        }

        if (prefab != null)
        {
            Popup popup = Instantiate(prefab);
            popup.SetContent(content, buttonTextYES);
            
        }
        else 
        {
            Debug.LogError("Prefab not found");
        }
    }
   
}

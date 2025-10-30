using UnityEngine;

public enum PopupType
{
    Notification,
    Tutorial,
}
public class PopupManager : SingletonNotBaseSource<PopupManager>
{
    [SerializeField] private Popup m_NotificationPopupPrefab;
    [SerializeField] private Popup TutorialPopupPrefab;

    public void ShowPopup(PopupType type, string content, string buttonTextYES)
    {
        //Debug.Log("ShowPopup:" + Equals(content));
        Popup prefab = null;

        switch (type)
        {
            case PopupType.Notification:
                prefab = m_NotificationPopupPrefab;
                AudioMainManager.Instance.PlayOnShot(SoundType.Popup);
                break;
            case PopupType.Tutorial:
                prefab = TutorialPopupPrefab;
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

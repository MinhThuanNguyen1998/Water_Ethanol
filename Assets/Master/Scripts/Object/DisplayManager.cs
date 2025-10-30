using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    [SerializeField] private Sprite m_FullScreenIcon;
    [SerializeField] private Sprite m_MiniScreenIcon;
    [SerializeField] private Image m_ImageZoom;
    void Start()
    {
        SetQualityDisplay();
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetWindowed(int width, int height)
    {
        Screen.SetResolution(width, height, false);
    }
    public void ToggleFullScreen()
    {
        SetQualityDisplay();

        if (Screen.fullScreen)
        {
          
            SetWindowed(1280, 720);
            m_ImageZoom.sprite = m_FullScreenIcon;
        }
        else
        {
            SetFullScreen(true);
            m_ImageZoom.sprite = m_MiniScreenIcon;
        }
    }
    private void SetQualityDisplay()
    {
       
        Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, true);
        QualitySettings.antiAliasing = 8;
    }
}
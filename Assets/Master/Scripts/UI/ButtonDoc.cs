using UnityEngine;
using UnityEngine.UI;

public class ButtonDoc : MonoBehaviour
{
    [SerializeField] private Image m_DefaultSoundImage;
    [SerializeField] private Image m_PauseImage;
    [SerializeField] private Button m_SoundDocButton;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioClip;
    private bool isSoundOn = true;

    private void Start()
    {
        m_SoundDocButton.onClick.AddListener(ToggleSoundIcon);
        ResetButtonDocState();
    }
    private void ToggleSoundIcon()
    {
        isSoundOn = !isSoundOn;
        if (!isSoundOn)
        {
            m_AudioSource.clip = m_AudioClip;
            m_AudioSource.Play();
        }
        else m_AudioSource.Pause();
       
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_DefaultSoundImage.gameObject.SetActive(isSoundOn);
        m_PauseImage.gameObject.SetActive(!isSoundOn);
    }
    private void ResetButtonDocState()
    {
        isSoundOn = true;
        UpdateUI();
    }
}

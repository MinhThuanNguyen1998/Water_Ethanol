using UnityEngine;

[DisallowMultipleComponent]
public class OutlineHoverEffect: MonoBehaviour
{
    [SerializeField] private Outline m_outline;

    [Header("Object Type")]
    [Tooltip("Nếu bật, object này là loại có thể di chuyển.")]
    [SerializeField] private bool isMovable = false;

    private bool m_IsHover = false;
    public bool IsHover
    {
        get { return m_IsHover; } 
        set { m_IsHover = value; }
    }

    // Shared settings cho toàn project
    private static OutlineSettingsSO sharedSettings;

    private void Awake()
    {
        IsHover = true;
        // Lấy reference Outline
        if (m_outline == null)
            m_outline = GetComponent<Outline>();

        // Load ScriptableObject chỉ 1 lần
        if (sharedSettings == null)
        {
            sharedSettings = Resources.Load<OutlineSettingsSO>("Settings/OutlineSettings");
            if (sharedSettings == null)
            {
                Debug.LogWarning("⚠️ OutlineSettings.asset not found in Resources/Settings/");
                return;
            }
        }

        if (m_outline == null)
        {
            Debug.LogWarning($"⚠️ Outline component missing on {name}");
            return;
        }

        // Áp dụng cài đặt Outline tương ứng
        ApplyOutlineSettings();

        // Tắt mặc định để tiết kiệm performance
        m_outline.enabled = false;
    }

    private void ApplyOutlineSettings()
    {
        if (isMovable)
        {
            m_outline.OutlineColor = sharedSettings.movableColor;
            m_outline.OutlineWidth = sharedSettings.movableWidth;
            m_outline.OutlineMode = sharedSettings.movableMode;
        }
        else
        {
            m_outline.OutlineColor = sharedSettings.staticColor;
            m_outline.OutlineWidth = sharedSettings.staticWidth;
            m_outline.OutlineMode = sharedSettings.staticMode;
        }
    }
    private void OnMouseEnter()
    {
        if (!m_IsHover) return;
        m_outline.enabled = true;
    }
    private void OnMouseExit()
    {
        if (!m_IsHover) return;
        m_outline.enabled = false;
    }
}



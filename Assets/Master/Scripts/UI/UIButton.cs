using System;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public void OnClick()
    {
        //Debug.Log("Click");
        AudioMainManager.Instance.PlayOnShot(SoundType.Button);
    }
}

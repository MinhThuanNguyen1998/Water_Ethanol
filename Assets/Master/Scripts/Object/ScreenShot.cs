using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    [ContextMenu("Take Screenshot")]
    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            TakeScreenshot();
        }
    }
    private void TakeScreenshot()
    {
        string path = Application.dataPath + "/../Screenshot.png";
        ScreenCapture.CaptureScreenshot(path);
        Debug.Log("Screenshot saved to: " + path);
    }



}

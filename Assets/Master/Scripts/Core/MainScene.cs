using UnityEngine;

public class MainScene : SingletonNotBaseSource<MainScene>
{
    public void QuitApp()
    {
        Application.Quit();
    }
}

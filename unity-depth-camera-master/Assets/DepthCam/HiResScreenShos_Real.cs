using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiResScreenShos_Real : MonoBehaviour
{
    private int resWidth = 1280;
    private int resHeight = 752;

    private bool takeHiResShot = false;

    private void Start()
    {
        ScreenShot();
    }
    void Awake()
    {
        resWidth = Screen.width;
        resHeight = Screen.height;
    }

    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/Image/screen_{1}_origin.png",
                             Application.dataPath,
                             //width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeHiResShot()
    {
        takeHiResShot = true;
    }

    void ScreenShot()
    {
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        GetComponent<Camera>().targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        GetComponent<Camera>().Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        GetComponent<Camera>().targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);

        // 파일로 저장.
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(resWidth, resHeight);
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
    }
}

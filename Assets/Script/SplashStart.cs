using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashStart : MonoBehaviour
{
    public Image SplashIMG0;
    public Image SplashIMG1;
    public Image SplashIMG2;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(FadeOutCorutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (SplashIMG2.color.a < 0f)
        {
            SceneManager.LoadScene("Main");
        }
        
    }

    IEnumerator FadeOutCorutine()
    {
        float fadeCount = 1.2f;
        while (fadeCount > 0f)
        {
            if (fadeCount >= 0.8f)
            {
                fadeCount -= 0.005f;
            }
            else
            {
                fadeCount -= 0.02f;
            }
            yield return new WaitForSeconds(0.005f);
            SplashIMG0.color = new Color(255,255,255, fadeCount);
            SplashIMG1.color = new Color(255,255,255, fadeCount);
            SplashIMG2.color = new Color(255,255,255, fadeCount);
        }
    }
}

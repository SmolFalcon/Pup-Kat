using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Menu : MonoBehaviour
{
    public Animator FadeToBlack;
    public Image Black;
    public void PlayGame()
    {
        StartCoroutine(Fade());
        // SceneManager.LoadScene("MainScene");
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    IEnumerator Fade()
    {
        FadeToBlack.SetBool("Fade", true);
        yield return new WaitUntil(() => Black.color.a == 1);
        SceneManager.LoadScene("MainScene");
    }
} 
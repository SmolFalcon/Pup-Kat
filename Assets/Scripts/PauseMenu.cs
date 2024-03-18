using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Animator FadeToBlack;
    public Image Black;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PlayerControl(true);
        GameIsPaused = false;
    }
    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PlayerControl(false);
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(Fade("Menu"));
        //SceneManager.LoadScene("Menu"); ;
    }
    public void LoadMainScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(Fade("MainScene"));
        //SceneManager.LoadScene("MainScene"); ;
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    IEnumerator Fade(string scene)
    {
        FadeToBlack.SetBool("Fade", true);
        yield return new WaitUntil(() => Black.color.a == 1);
        SceneManager.LoadScene(scene);
    }
    void PlayerControl(bool state)
    {
        GameObject.FindGameObjectWithTag("CatRoot").GetComponent<Cat>().enabled = state;
        GameObject.FindGameObjectWithTag("DogRoot").GetComponent<Dog>().enabled = state;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().enabled = state;
    }
}

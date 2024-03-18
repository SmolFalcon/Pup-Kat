using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class KillZone : MonoBehaviour
{
    public Animator Lose;
    public Image LoseScreen;
    bool CatIsIn = false, DogIsIn = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DogRoot")
        {
            DogIsIn = true;
        }
        if (other.gameObject.tag == "CatRoot")
        {
            CatIsIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DogRoot")
        {
            DogIsIn = false;
        }
        if (other.gameObject.tag == "CatRoot")
        {
            CatIsIn = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CatIsIn)
        {
            Lose.SetBool("Fade", true);
            //StartCoroutine(LoseFade());
            if (LoseScreen.color.a == 1)
            {
                SceneManager.LoadScene("MainScene");
            }
        }
        if (DogIsIn)
        {
            EnemyDog.Aggressive = false;
        }
        else
        {
            EnemyDog.Aggressive = true;
        }
        
    }

    IEnumerator LoseFade()
    {
        Lose.SetBool("Fade", true);
        yield return new WaitUntil(() => LoseScreen.color.a == 1);
        SceneManager.LoadScene("MainScene");
    }
}

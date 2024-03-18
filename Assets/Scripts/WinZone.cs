using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinZone : MonoBehaviour
{
    public Animator Win;
    public Image WinScreen;
    bool DogIn = false, CatIn = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "DogRoot")
        {
            DogIn = true;
        }
        if (other.gameObject.tag == "CatRoot")
        {
            CatIn = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DogIn&&CatIn)
        {
            Win.SetBool("Fade", true);
            //StartCoroutine(WinFade());
            if (WinScreen.color.a == 1)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
    IEnumerator WinFade()
    {
        Win.SetBool("Fade", true);
        yield return new WaitUntil(() => WinScreen.color.a == 1);
        SceneManager.LoadScene("Menu");
    }
}

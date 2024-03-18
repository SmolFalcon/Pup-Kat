using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovable : MonoBehaviour
{
    public GameObject Pull, Target;
    public AudioSource audioSourceBox;
    public Animator CatAnim;
    public static Vector3 PullPos,PullTarget;
    public static Quaternion PullRot;
    public static bool Moved; 

    // Start is called before the first frame update
    void Start()
    {
        PullPos = Pull.transform.position;
        PullRot = Pull.transform.rotation;
        PullTarget = Target.transform.position;
        Moved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CatAnim.GetBool("IsPulling") && audioSourceBox.isPlaying==false)
        {
            audioSourceBox.Play();
        }
        else if (CatAnim.GetBool("IsPulling") == false)
        {
            audioSourceBox.Stop();
        }
        
    }
}

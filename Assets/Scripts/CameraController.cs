using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{

    public static Vector3 CatPos, DogPos;
    public static Vector3 CamRot;
    public GameObject Cam1, Cam2, Cam3;
    public static Quaternion CamRotQ;
    public PostProcessVolume PVolume;
    private DepthOfField DoF;
    
    // Start is called before the first frame update
    void Start()
    {
        CharSwitch(true);
        DepthOfField temp;
        PVolume.profile.TryGetSettings<DepthOfField>(out temp);
        DoF = temp;
    }

    // Update is called once per frame
    void Update()
    {
        CharSwitch();
        CatPos = GameObject.FindGameObjectWithTag("CatHead").transform.position;
        DogPos = GameObject.FindGameObjectWithTag("DogHead").transform.position;
        transform.LookAt((CatPos + DogPos) / 2);//Points camera between the two character positions
        CamRotQ = this.transform.rotation;
        CamRot = CamRotQ.eulerAngles;
        if (Dog.UserControl == true)
        {
            DoF.focusDistance.value = Vector3.Distance(this.transform.position, DogPos);
        }
        else
        {
            DoF.focusDistance.value = Vector3.Distance(this.transform.position, CatPos);
        }
    }

    void CharSwitch(bool pass = false)
    {
        if((Input.GetKeyDown(KeyCode.E) || pass == true)&&Cat.Interacting==false)
        {
            if(Cat.UserControl==true)
            {
                Cat.UserControl = false;
                Dog.UserControl = true;
            }
            else
            {
                Cat.UserControl = true;
                Dog.UserControl = false;
            }

        }
    }

    public void SwitchCam(int indx)
    {
        switch(indx)
        {
            case 1:
                {
                    transform.position = Cam1.transform.position;
                    break;
                }
            case 2:
                {
                    transform.position = Cam2.transform.position;
                    break;
                }
            case 3:
                {
                    transform.position = Cam3.transform.position;
                    break;
                }
        }
    }

}

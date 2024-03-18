using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitcher : MonoBehaviour
{


    BoxCollider Zone;
    public GameObject MainCam;
    public int CamIndx;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DogRoot" || other.gameObject.tag == "CatRoot")//((other.gameObject.tag == "DogRoot" || other.gameObject.tag == "CatRoot") && Vector3.Distance(CameraController.CatPos, CameraController.DogPos) < 0.25f)
        {          
            MainCam.GetComponent<CameraController>().SwitchCam(CamIndx);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Zone = this.gameObject.GetComponent<BoxCollider>();       
    }

    // Update is called once per frame
    void Update()
    {

    }
}

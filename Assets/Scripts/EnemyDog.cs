using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : MonoBehaviour
{
    public GameObject Mesh;
    SkinnedMeshRenderer SKM;
    Quaternion LookTarget,temp, StartHeadRot;
    bool IsLooking = false;
    public static bool Aggressive;
    public GameObject HeadJoint,ViewConeJoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DogRoot")
        {
            IsLooking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DogRoot")
        {
            IsLooking = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SKM = Mesh.GetComponent<SkinnedMeshRenderer>();
        StartHeadRot = HeadJoint.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (IsLooking)

        {
            temp = HeadJoint.transform.rotation;
            HeadJoint.transform.LookAt(CameraController.DogPos);
            HeadJoint.transform.Rotate(50, 0, 0);
            LookTarget = HeadJoint.transform.rotation;
            HeadJoint.transform.rotation = temp;
            HeadJoint.transform.rotation = Quaternion.Slerp(HeadJoint.transform.rotation, LookTarget, Time.deltaTime * 2);
        }
        else
        {
            HeadJoint.transform.rotation = Quaternion.Slerp(HeadJoint.transform.rotation, StartHeadRot, Time.deltaTime * 2);
        }
        ViewConeJoint.transform.rotation = HeadJoint.transform.rotation;


        if (Aggressive)
        {
            SKM.SetBlendShapeWeight(0, Mathf.Lerp(SKM.GetBlendShapeWeight(0), 100, Time.deltaTime*5));
        }
        else
        {
            SKM.SetBlendShapeWeight(0, Mathf.Lerp(SKM.GetBlendShapeWeight(0), 0, Time.deltaTime*5));
        }
    }
}

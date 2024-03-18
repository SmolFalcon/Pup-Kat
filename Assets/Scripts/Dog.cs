using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public float WalkSpeed, RotInterp, JumpForce;
    public Animator Anim_Controller;
    Quaternion Target;
    Rigidbody rb;
    BoxCollider HitBox;
    float WalkAngle,RefAngle,TimeElapsed,Timer;
    Vector3 StartHitboxCenter;
    public static bool UserControl = true, IsFalling = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "BoxMovable")
        {            IsFalling = false;
            Anim_Controller.SetBool("IsFalling", false);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        HitBox = GetComponent<BoxCollider>();
        StartHitboxCenter = HitBox.center;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        WalkAngle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg; //Converts input to an angle to offset InterpRot
    }

    void Movement()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && UserControl == true)
        {
            InterpRot(Quaternion.Euler(0, RefAngle + WalkAngle, 0),RotInterp);
            transform.Translate(0,0,WalkSpeed * Time.deltaTime);
            Anim_Controller.SetBool("IsWalking", true);
        }
        else
        {
            Anim_Controller.SetBool("IsWalking", false);
            RefAngle = GameObject.FindGameObjectWithTag("CamDir").transform.rotation.eulerAngles[1];
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsFalling == false && UserControl == true)
        {
            Anim_Controller.SetBool("IsFalling", true);
            IsFalling = true;
            rb.AddForce(0,JumpForce,0, ForceMode.Force);
        }

    }

    void InterpRot(Quaternion Target, float Interp)//Interpolates Quaternion rotation to align with camera direction
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Target, Interp);
    }
}

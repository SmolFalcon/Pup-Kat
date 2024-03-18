using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public float WalkSpeed, RotInterp, PosInterp, JumpForce, FenceLength;
    public Animator Anim_Controller;
    public AudioSource audioSourceFence;
    Quaternion Target;
    Rigidbody rb;
    GameObject Interaction;
    BoxCollider HitBox;
    float WalkAngle,TimeElapsed,Timer,RefAngle;
    Vector3 StartHitboxCenter;
    public static bool IsFalling = false, CanJump = false, ReachedTop=false, PullBoxMode = false, Interacting = false, UserControl = true;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Floor" || collision.gameObject.tag == "BoxMovable")
        {            IsFalling = false;
            Anim_Controller.SetBool("IsFalling", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="BoxMovable")
        {
            PullBoxMode = true;
            Interaction = other.gameObject;
        }
        if (other.gameObject.tag == "Jumpable")
        {
            CanJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BoxMovable")
        {
            PullBoxMode = false;
        }
        if (other.gameObject.tag == "Jumpable")
        {
            CanJump = false;
        }

    }

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

        if(PullBoxMode && Input.GetKeyDown(KeyCode.LeftShift) && Anim_Controller.GetBool("PullMode") == false && BoxMovable.Moved == false &&UserControl == true)
        {
            Anim_Controller.SetBool("PullMode", true);
        }

        if (Anim_Controller.GetBool("PullMode"))
        {
            bool DoneInterp = false;

            if(Vector3.Distance(this.transform.position, BoxMovable.PullPos) < 0.01)
            {
                DoneInterp = true;
            }
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S)) && (DoneInterp || Interacting))
            {
                Interacting = true;
                Anim_Controller.SetBool("IsPulling", true);
                if(Vector3.Distance(this.transform.position, BoxMovable.PullTarget) < 0.1)
                {
                    Interacting = false;
                    Anim_Controller.SetBool("IsPulling", false);
                    Anim_Controller.SetBool("PullMode", false);
                    BoxMovable.Moved = true;
                    UserControl = true;
                }
                else
                {
                    transform.Translate(0, 0, -(WalkSpeed / 2) * Time.deltaTime);
                    Interaction.transform.Translate(0, 0, -(WalkSpeed / 2) * Time.deltaTime);
                }

            }
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) == false) && Interacting)
            {
                Anim_Controller.SetBool("IsPulling", false);
            }
            else if (DoneInterp == false && Interacting == false)
            {
                BoxMove(BoxMovable.PullPos, BoxMovable.PullRot);
            }
 
        }
    }

    void Movement()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && UserControl == true)
        {
            InterpRot(Quaternion.Euler(0, RefAngle + WalkAngle, 0), RotInterp);//Interp rotation to align with camera and direction.
            transform.Translate(0,0,WalkSpeed * Time.deltaTime);
            Anim_Controller.SetBool("IsWalking", true);
        }
        else
        {
            RefAngle = GameObject.FindGameObjectWithTag("CamDir").transform.rotation.eulerAngles[1];
            Anim_Controller.SetBool("IsWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsFalling == false && UserControl == true)
        {
            Anim_Controller.SetBool("IsFalling", true);
            IsFalling = true;
            rb.AddForce(0,JumpForce,0, ForceMode.Force);
        }

        if(Input.GetKeyDown(KeyCode.Q) && Anim_Controller.GetBool("JumpingFence") == false && CanJump == true && UserControl == true)
        {
            audioSourceFence.Play();
            UserControl = false;
            Anim_Controller.SetBool("JumpingFence", true);
            Timer = Time.time;
        }

        if (Anim_Controller.GetBool("JumpingFence") == true && Time.time - Timer > 1.5f)//Starts when Jumping Fence animation has played by 1.5 seconds (meaning it reached the top)
        {
            Anim_Controller.SetBool("JumpingFence", false);
            Anim_Controller.SetBool("IsFalling", true);
            rb.useGravity = false;
            HitBox.enabled = false;
            ReachedTop = true;
        }

        if (ReachedTop == true)//When Cat is on top, resets variables and lets animation interpolate back down after moving forward ahead of the fence.
        {     

            if (TimeElapsed < 0.5f)
            {
                transform.Translate(0, 0, FenceLength * Time.deltaTime);
                TimeElapsed += Time.deltaTime;
            }
            else
            {
                TimeElapsed = 0;
                Anim_Controller.SetBool("JumpingFence", false);
                Anim_Controller.SetBool("IsFalling", false);
                rb.useGravity = true;
                HitBox.enabled = true;
                UserControl = true;
                ReachedTop = false;
                CanJump = false;
            }

        }

    }


    void InterpRot(Quaternion Target, float Interp)//Interpolates Quaternion rotation to align with camera direction
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Target, Interp);
    }

    void InterpPos(Vector3 Target, float Interp)//Lerp character to position
    {
        this.transform.position = Vector3.Lerp(this.transform.position, Target, Interp);
    }

    void BoxMove(Vector3 Pos,Quaternion Rot)
    {
        UserControl = false;
        InterpRot(Rot,RotInterp);
        InterpPos(Pos,PosInterp);
    }

}

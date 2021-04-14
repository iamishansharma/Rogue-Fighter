using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    private Animator Anim;
    public float WalkSpeed = 0.05f;
    private bool isJumping = false;
    private bool isCrouch = false;
    private AnimatorStateInfo Player1Layer0;
    private bool CanWalkLeft = true;
    private bool CanWalkRight = true;
    public GameObject Player1;
    public GameObject Opponent;
    private Vector3 OppPosition;
    private bool FacingLeft = false;
    private bool FacingRight = true;
    public AudioClip LightPunch;
    public AudioClip HeavyPunch;
    public AudioClip LightKick;
    public AudioClip HeavyKick;
    private AudioSource MyPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        StartCoroutine(FaceRight());
        MyPlayer = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get Animator
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        // Screen Bounds
        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);

        if(ScreenBounds.x > Screen.width - 200)
        {
            CanWalkRight = false;
        }
        if(ScreenBounds.x < 0 + 200)
        {
            CanWalkLeft = false;
        }
        else if(ScreenBounds.x > 200 && ScreenBounds.x < Screen.width - 200)
        {
            CanWalkRight = true;
            CanWalkLeft = true;
        }

        // Flipping to Face Opponent
        OppPosition = Opponent.transform.position;

        //Facing Left or Right of the Opponent
        if(OppPosition.x > Player1.transform.position.x)
        {
            StartCoroutine(FaceLeft());
        }
        if(OppPosition.x < Player1.transform.position.x)
        {
            StartCoroutine(FaceRight());
        }

        // Horizontal Axis
        if(Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if(CanWalkRight == true)
                {
                    Anim.SetBool("Forward", true);
                    transform.Translate(WalkSpeed, 0, 0);
                }
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                if(CanWalkLeft == true)
                {
                    Anim.SetBool("Backward", true);
                    transform.Translate(-WalkSpeed, 0, 0);
                }
            }
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
        }

        // Vertical Axis
        if (Input.GetAxis("Vertical") > 0)
        {
            if(isJumping == false)
            {
                isJumping = true;
                Anim.SetTrigger("Jump"); 
                StartCoroutine(JumpPause());
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            if(isCrouch == false)
            {
                isCrouch = true;
                Anim.SetTrigger("Crouch");
                StartCoroutine(CrouchPause());
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("FistLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip = LightPunch;
            MyPlayer.Play();
        }
        if(other.gameObject.CompareTag("FistHeavy"))
        {
            Anim.SetTrigger("BigReact");
            MyPlayer.clip = HeavyPunch;
            MyPlayer.Play();
        }
        if(other.gameObject.CompareTag("KickLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip = LightPunch;
            MyPlayer.Play();
        }
        if(other.gameObject.CompareTag("KickHeavy"))
        {
            Anim.SetTrigger("BigReact");
            MyPlayer.clip = HeavyPunch;
            MyPlayer.Play();
        }
    }

    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        isJumping = false;
    }
    IEnumerator CrouchPause()
    {
        yield return new WaitForSeconds(1.0f);
        isCrouch = false;
    }
    IEnumerator FaceLeft()
    {
        if(FacingLeft == true)
        {
            FacingLeft = false;
            FacingRight = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0,-180,0);
            Anim.SetLayerWeight(1,0);
        }
    }
    IEnumerator FaceRight()
    {
        if(FacingRight == true)
        {
            FacingLeft = true;
            FacingRight = false;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0,180,0);
            Anim.SetLayerWeight(1,1);
        }
    }
}

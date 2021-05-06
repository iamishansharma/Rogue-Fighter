using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Actions : MonoBehaviour
{
    public float JumpSpeed = 1.5f;

    public GameObject Player1;

    private Animator Anim;

    private AnimatorStateInfo Player1Layer0;

    private AudioSource MyPlayer;

    public AudioClip PunchWoosh;

    public AudioClip KickWoosh;

    public static bool HitsP2 = false;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get Animator
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        if (Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetButtonDown("Fire1P2"))
            {
                Anim.SetTrigger("LightPunch");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("Fire2P2"))
            {
                Anim.SetTrigger("HeavyPunch");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("Fire3P2"))
            {
                Anim.SetTrigger("LightKick");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("Fire4P2"))
            {
                Anim.SetTrigger("HeavyKick");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("BlockP2"))
            {
                Anim.SetTrigger("BlockOn");
            }
        }
        if (Player1Layer0.IsTag("Block"))
        {
            if (Input.GetButtonUp("BlockP2"))
            {
                Anim.SetTrigger("BlockOff");
            }
        }
    }

    public void PunchWooshSound()
    {
        MyPlayer.clip = PunchWoosh;
        MyPlayer.Play();
    }

    public void KickWooshSound()
    {
        MyPlayer.clip = KickWoosh;
        MyPlayer.Play();
    }

    public void JumpUp()
    {
        //Player1.transform.Translate(0,JumpSpeed,0);
        //StartCoroutine(JumpDelay());
    }

    // IEnumerator JumpDelay()
    // {
    //     //yield return new WaitForSeconds(0.2f);
    //     //Player1.transform.Translate(0,-JumpSpeed,0);
    // }
}

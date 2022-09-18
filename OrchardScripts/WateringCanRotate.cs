using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanRotate : MonoBehaviour
{
    float rot;
    GameObject WF;
    GameObject Pool;
    public GameObject hand;

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip WaterSound;
    private AudioSource aSound;

    // Start is called before the first frame update
    void Start()
    {
        WF = GameObject.Find("WaterFall");
        Pool = GameObject.Find("GrowTrigger");
        aSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent != null)
        {
            rot = Mathf.Abs(hand.transform.rotation.x);
            if (rot > 0.1f && rot < 0.4f)
            {
                WF.GetComponent<ParticleSystem>().Play(true);
                aSound.clip = WaterSound;
                if (aSound.isPlaying == false)
                {
                    aSound.loop = true;
                    aSound.Play();
                }
                Pool.GetComponent<BoxCollider>().enabled = true;

            }
            else
            {
                if (aSound.isPlaying == true)
                {
                    aSound.Stop();
                }
                WF.GetComponent<ParticleSystem>().Pause(true);
                WF.GetComponent<ParticleSystem>().Clear(true);
                Pool.GetComponent<BoxCollider>().enabled = false;
            }
        }
        else
        {
            if (aSound.isPlaying == true)
            {
                aSound.Stop();
            }
            WF.GetComponent<ParticleSystem>().Pause(true);
            WF.GetComponent<ParticleSystem>().Clear(true);
            Pool.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

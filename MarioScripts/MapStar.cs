using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapStar : MonoBehaviour
{
    public static MapStar instance;
    public StartGetScript starGetScript;
    AudioSource getSound;
    public AudioClip soundCheck;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        getSound = gameObject.AddComponent<AudioSource>();
        getSound.clip = soundCheck;
    }

    void OnTriggerEnter(Collider other)
    {
        //���࿡ other�� �÷��̾���
        if (other.transform.tag == "Player")
        {
            Renderer[] renderer = gameObject.GetComponentsInChildren<Renderer>();
            gameObject.GetComponent<Collider>().enabled = false;
            for (int i = 0; i < renderer.Length; i++)
            {
                renderer[i].enabled = false;
            }
            GetStars gstar = other.GetComponent<GetStars>();
            getSound.Play();
            print("sound����");
            //UI�� ����
            gstar.AddSTAR(1);
            //Destroy(gameObject, 2);
            starGetScript.ScoreUp();


        }

    }
    
}

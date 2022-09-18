using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public Animator anim;

    CharacterController cc;
    public float dashTime = 2f;
    public float dashSpeed = 20f;
    public float gravity = -9.81f;//중력, 플레이를 달, 화성에서 할 수도 있으니까 조절가능하게
    float yVelocity;
    public float speed = 5;
    public float coolTime = 5;
    bool canDash = true;

    // Start is called before the first frame update
    void Start()
    {

        cc = GetComponent<CharacterController>();
    }
    private IEnumerator DashCoolTime()
    {
        canDash = false;
        yield return new WaitForSeconds(coolTime);
        canDash = true;
    }

    public int plusScore = 2000;
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Star")
        {
            ScoreManager.instance.SCORE += plusScore;

            anim.SetTrigger("GetStar");
        }

        if (other.CompareTag("Platform"))
        {
            platform = other.gameObject.GetComponent<Platform>();
        }

        if (other.transform.tag == "Enemy")
        {
            anim.SetTrigger("Death");
            GameManager.instance.GameOverUI.SetActive(true);
            GameManager.instance.GameOverCamera.SetActive(true);
            //GameManager.instance.MainCamera.SetActive(false);
            GameManager.instance.MainUI.SetActive(false);
        }
    }

    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time; // need to     remember this to know how long to dash
        while (Time.time < startTime + dashTime)
        {

            cc.Move(transform.forward * dashSpeed * Time.deltaTime);
            // or controller.Move(...), dunno about that script
            yield return null; // this will make Unity stop here and continue next frame
        }
    }


    void Update()
    {

        if (false == cc.isGrounded)
        {
            yVelocity += gravity * Time.deltaTime;
        }

        // 1. 사용자의 입력에따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        anim.SetFloat("h", h);
        anim.SetFloat("v", v);

        if (h == 0 && v == 0)
        {
            dustWK.Stop();
            dustDS.Stop();
        }   


        // 2. 앞뒤좌우로 방향을 만들고
        Vector3 dir = new Vector3(h, 0, v);

        // 방향(dir)을 카메라를 기준으로 변경하고싶다.
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();
        if (h != 0 || v != 0)
        {
            transform.forward = dir;

        }
        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;
        if (platform != null)
        {
            velocity += platform.velocity;
        }
        // 3. 그 방향으로 이동하고싶다.
        cc.Move(velocity * Time.deltaTime);
        
        
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            anim.SetBool("Dash", true);
            StartCoroutine(DashCoolTime());
            StartCoroutine(DashCoroutine());
        }
        else if (!canDash)
        {
            anim.SetBool("Dash", false);
        }
    }

    Platform platform;


    private void OnTriggerExit(Collider other)
    {
        platform = null;
    }

    public ParticleSystem dustWK;
    public ParticleSystem dustDS;

    public void OnWalkDust()
    {
        dustWK.Stop();
        dustWK.Play();
    }
    public void OnDashDust()
    {
        dustDS.Stop();
        dustDS.Play();
    }
}

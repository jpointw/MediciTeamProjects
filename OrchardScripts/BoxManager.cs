using System;
using UnityEngine;

//박스를 관리해주는 스크립트
//Box와의 거리가 0.5 이하일 때,
//핸드트리거 하나라도 때면은 박스는 디스트로이 되고 count를 1증가 시켜 setActive(true&false)해준다,
public class BoxManager : MonoBehaviour
{

    public GameObject FullAppleBoxOne;
    public GameObject FullAppleBoxTwo;
    public GameObject FullAppleBoxThree;
    public GameObject FullAppleBoxFour;

    void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;   
    }
    bool finish = false;
    float count;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Destroy(collision.gameObject);
            count++;
            if (count == 1)
            {
                finish = false;
                FullAppleBoxOne.SetActive(true);
            }
            if (count == 2)
            {
                finish = false;
                FullAppleBoxTwo.SetActive(true);
            }
            if (count == 3)
            {
                finish = false;
                FullAppleBoxThree.SetActive(true);
            }
            if (count == 4)
            {
                finish = true;
                FullAppleBoxFour.SetActive(true);
                GetComponent<Rigidbody>().isKinematic = false;
            }

        }
    }

}




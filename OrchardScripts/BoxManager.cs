using System;
using UnityEngine;

//�ڽ��� �������ִ� ��ũ��Ʈ
//Box���� �Ÿ��� 0.5 ������ ��,
//�ڵ�Ʈ���� �ϳ��� ������ �ڽ��� ��Ʈ���� �ǰ� count�� 1���� ���� setActive(true&false)���ش�,
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




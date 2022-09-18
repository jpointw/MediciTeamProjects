using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Position : MonoBehaviour
{
    public static Inventory_Position instance;

    public Transform[] positions; //위치들의 배열
    float oneAngle; //한번 돌릴때 각도 (평균 각)
    Quaternion targetAngle; //돌리는 방향
    private bool axisInUse = false; //조이스틱 입력 딱! 한번만 받아야할때 확인용 부울
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Quaternion originAngle = transform.localRotation;
        targetAngle = transform.localRotation;
        oneAngle = 360 / positions.Length; //한번 돌릴때 각도는 360/인벤토리 개수

        for (int i = 0; i < positions.Length; i++)
        {
            Quaternion addAngle = Quaternion.Euler(0, 0, i * oneAngle); //z축을 기준으로  oneAngle 만큼의 각속도
            Vector3 dir = addAngle * transform.up; //포지션들의 부모의 업벡터를 z축을 기준으로 addAngle

            positions[i].localPosition = transform.localPosition + dir;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetAngle, Time.deltaTime * 5);
        if (ActionController.instance.InventoryActivated == true)
        {
            Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            //if (Input.GetKeyDown(KeyCode.LeftArrow))
            if (thumbstick.x < 0)
            {
                if (axisInUse == false)
                {
                    axisInUse = true;
                    // 내용
                    targetAngle = targetAngle * Quaternion.Euler(0, 0, oneAngle);
                }
            }

            //if (Input.GetKeyDown(KeyCode.RightArrow))
            if (thumbstick.x > 0)
            {
                if (axisInUse == false)
                {
                    axisInUse = true;
                    // 내용
                    targetAngle = targetAngle * Quaternion.Euler(0, 0, -oneAngle);
                }
            }
            if (thumbstick.x == 0)
            {
                axisInUse = false;
            }
        }
    }
}

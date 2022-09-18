using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Position : MonoBehaviour
{
    public static Inventory_Position instance;

    public Transform[] positions; //��ġ���� �迭
    float oneAngle; //�ѹ� ������ ���� (��� ��)
    Quaternion targetAngle; //������ ����
    private bool axisInUse = false; //���̽�ƽ �Է� ��! �ѹ��� �޾ƾ��Ҷ� Ȯ�ο� �ο�
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Quaternion originAngle = transform.localRotation;
        targetAngle = transform.localRotation;
        oneAngle = 360 / positions.Length; //�ѹ� ������ ������ 360/�κ��丮 ����

        for (int i = 0; i < positions.Length; i++)
        {
            Quaternion addAngle = Quaternion.Euler(0, 0, i * oneAngle); //z���� ��������  oneAngle ��ŭ�� ���ӵ�
            Vector3 dir = addAngle * transform.up; //�����ǵ��� �θ��� �����͸� z���� �������� addAngle

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
                    // ����
                    targetAngle = targetAngle * Quaternion.Euler(0, 0, oneAngle);
                }
            }

            //if (Input.GetKeyDown(KeyCode.RightArrow))
            if (thumbstick.x > 0)
            {
                if (axisInUse == false)
                {
                    axisInUse = true;
                    // ����
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

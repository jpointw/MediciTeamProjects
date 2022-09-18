using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    public static ActionController instance;

    public GameObject InventoryBase;
    [SerializeField]
    private float range; //���� ������ �ִ�Ÿ�
    private bool pickupActivated = false; //���� �����ҽ� true

    private RaycastHit hitInfo; //�浹ü ���� ����
    private bool axisInUse = false; //���̽�ƽ �Է� ��! �ѹ��� �޾ƾ��Ҷ� Ȯ�ο� �ο�

    //������ ���̾�� �����ϵ��� ���̾� ����ũ�� ����
    [SerializeField]
    private LayerMask layerMask;

    //�ʿ��� ������Ʈ
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory_UI theInventory;

    int SelectedSlotNumber = 0;
    public bool InventoryActivated = false;


    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        SelectSlot(); //������ �����Ѵ�
        InventoryOnOff();// �κ��丮 on/off
    }

    private void LateUpdate()
    {
        CheckItem(); //Ray�� ���� �������� �±׸� �����Ͽ� ������üũ
        TryAction(); //EŰ�� ������ �������� Ȯ���Ѵ�
    }

    private void InventoryOnOff() //�κ��丮 ����ݱ�
    {
        //if(Input.GetKeyDown(KeyCode.I)) //I �� ������ �κ��丮�� ����
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            if (InventoryActivated == false)
            {
                InventoryBase.SetActive(true);
                InventoryActivated = true;
            }
            else
            {
                InventoryBase.SetActive(false);
                InventoryActivated = false;
            }
        }
    }
    private void SelectSlot()
    {
        Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        /* //����Ƽ
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectedSlotNumber += 1;
            if (SelectedSlotNumber > 5) 
            {
                SelectedSlotNumber = 0;
            }
        }
        */

        if (thumbstick.x > 0) //���̽�ƽ�� �������� ������ slot[i+1]
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                // ����
                SelectedSlotNumber += 1;
                if (SelectedSlotNumber > 5) // i�� 5�� ������ 0���� �ʱ�ȭ
                {
                    SelectedSlotNumber = 0;
                }
            }
        }

        /* //����Ƽ
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectedSlotNumber -= 1;
            if (SelectedSlotNumber < 0)
            {
                SelectedSlotNumber = 5;
            }
        }
        */

        if (thumbstick.x < 0) //���̽�ƽ�� ���������� ������ slot[i-1]
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                // ����
                SelectedSlotNumber -= 1;
                if (SelectedSlotNumber < 0) // i�� 0���� ������ 5���� �ʱ�ȭ
                {
                    SelectedSlotNumber = 5;
                }
            }
        }
        if (thumbstick.x == 0)
        {
            axisInUse = false;
        }

    }

    private void TryAction() //�԰������
    {
        //if(Input.GetKeyDown(KeyCode.E)) //EŰ�� ������ �������� �Դ´�
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            CheckItem(); //Ray�� ���� �������� �±׸� �����Ͽ� ������üũ
            CanPickUp(); //�ֿ�� �ִ°�?
        }

        //if(Input.GetKeyDown(KeyCode.Q)) //QŰ�� ������ �������� ������
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            if (Inventory_UI.instance.slots[SelectedSlotNumber].item != null)
            {
                Instantiate(Inventory_UI.instance.slots[SelectedSlotNumber].item.itemPrefab, gameObject.transform.position, Quaternion.identity);
                Inventory_UI.instance.RemoveItem(Inventory_UI.instance.slots[SelectedSlotNumber].item);
            }
        }

    }

    private void CheckItem() //Ray�� ���� �������� �±׸� �����Ͽ� ������üũ
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, layerMask))
        {
            if (hitInfo.transform.tag == "Fruit") //Ray�� ���� ��ü�� �±װ� Fruit �̶��
            {
                ItemInfoAppear(); //������ ���� Ȱ��ȭ
            }
        }
        else // �׷��� �ʴٸ�
        {
            ItemInfoDisappear(); //������ ���� ��Ȱ��ȭ
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated == true) //�ֿ�� �ִ°�?
        {
            if (hitInfo.transform != null) //Ray�� ���� ��ü�� null�� �ƴѰ�?
            {
                //Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ���߽��ϴ�");
                theInventory.AccquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item); //������ �ݱ�
                Destroy(hitInfo.transform.gameObject); //�ֿ� ������ ����
                //ItemInfoDisappear(); //������ ���� ��Ȱ��ȭ
            }
        }
    }


    private void ItemInfoAppear() //������ ���� Ȱ��ȭ
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName
            + "ȹ��" + "<color=yellow>" + "(E)" + "</color>";
    }
    private void ItemInfoDisappear() //������ ���� ��Ȱ��ȭ
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

}

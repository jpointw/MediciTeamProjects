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
    private float range; //습득 가능한 최대거리
    private bool pickupActivated = false; //습득 가능할시 true

    private RaycastHit hitInfo; //충돌체 정보 저장
    private bool axisInUse = false; //조이스틱 입력 딱! 한번만 받아야할때 확인용 부울

    //아이템 레이어에만 반응하도록 레이어 마스크를 설정
    [SerializeField]
    private LayerMask layerMask;

    //필요한 컴포넌트
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
        SelectSlot(); //슬롯을 선택한다
        InventoryOnOff();// 인벤토리 on/off
    }

    private void LateUpdate()
    {
        CheckItem(); //Ray의 맞은 아이템의 태그를 구분하여 아이템체크
        TryAction(); //E키를 누르면 아이템을 확인한다
    }

    private void InventoryOnOff() //인벤토리 열고닫기
    {
        //if(Input.GetKeyDown(KeyCode.I)) //I 를 누르면 인벤토리를 연다
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
        /* //유니티
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectedSlotNumber += 1;
            if (SelectedSlotNumber > 5) 
            {
                SelectedSlotNumber = 0;
            }
        }
        */

        if (thumbstick.x > 0) //조이스틱을 왼쪽으로 누르면 slot[i+1]
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                // 내용
                SelectedSlotNumber += 1;
                if (SelectedSlotNumber > 5) // i가 5가 넘으면 0으로 초기화
                {
                    SelectedSlotNumber = 0;
                }
            }
        }

        /* //유니티
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectedSlotNumber -= 1;
            if (SelectedSlotNumber < 0)
            {
                SelectedSlotNumber = 5;
            }
        }
        */

        if (thumbstick.x < 0) //조이스틱을 오른쪽으로 누르면 slot[i-1]
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                // 내용
                SelectedSlotNumber -= 1;
                if (SelectedSlotNumber < 0) // i가 0보다 작으면 5으로 초기화
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

    private void TryAction() //먹고버리기
    {
        //if(Input.GetKeyDown(KeyCode.E)) //E키를 누르면 아이템을 먹는다
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            CheckItem(); //Ray의 맞은 아이템의 태그를 구분하여 아이템체크
            CanPickUp(); //주울수 있는가?
        }

        //if(Input.GetKeyDown(KeyCode.Q)) //Q키를 누르면 아이템을 버린다
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            if (Inventory_UI.instance.slots[SelectedSlotNumber].item != null)
            {
                Instantiate(Inventory_UI.instance.slots[SelectedSlotNumber].item.itemPrefab, gameObject.transform.position, Quaternion.identity);
                Inventory_UI.instance.RemoveItem(Inventory_UI.instance.slots[SelectedSlotNumber].item);
            }
        }

    }

    private void CheckItem() //Ray의 맞은 아이템의 태그를 구분하여 아이템체크
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, layerMask))
        {
            if (hitInfo.transform.tag == "Fruit") //Ray에 맞은 물체의 태그가 Fruit 이라면
            {
                ItemInfoAppear(); //아이템 정보 활성화
            }
        }
        else // 그렇지 않다면
        {
            ItemInfoDisappear(); //아이템 정보 비활성화
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated == true) //주울수 있는가?
        {
            if (hitInfo.transform != null) //Ray에 맞은 물체가 null이 아닌가?
            {
                //Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득했습니다");
                theInventory.AccquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item); //아이템 줍기
                Destroy(hitInfo.transform.gameObject); //주운 아이템 제거
                //ItemInfoDisappear(); //아이템 정보 비활성화
            }
        }
    }


    private void ItemInfoAppear() //아이템 정보 활성화
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName
            + "획득" + "<color=yellow>" + "(E)" + "</color>";
    }
    private void ItemInfoDisappear() //아이템 정보 비활성화
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public static Inventory_UI instance;
    //public static bool inventoryActivated = false;

    //�ʿ��� ������Ʈ
    [SerializeField]
    //private GameObject Inventory_Base;
    private GameObject SlotsParent;

    // ���Ե�
    public Slot[] slots;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        slots = SlotsParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        //TryOpenInventory();
    }

    private void TryOpenInventory()
    {
         
    }

    private void OpenInventory()
    {

    }

    private void CloseInventory()
    {

    }

    //�������� �����ϴ� �Լ�
    public void RemoveItem(Item _item, int _count = -1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }
    }

    //���Կ� ������ ����(�̸�, ����)�� �������ִ� �Լ�
    public void AccquireItem(Item _item, int _count = 1)
    {
        if (_item.itemType != Item.ItemType.Equipment)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }      
        }

        //�� ������ ã�� �־��ִ� �Լ�
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}

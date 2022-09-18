using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Item item; //ȹ���� ������
    public int itemCount; //ȹ���� �������� ����
    public Image itemImage; // �������� �̹���

    //�ʿ��� ������Ʈ
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SetColor(float _alpha) //�κ��丮 ���İ����� �Լ�
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item, int _count = 1) // ������ ȹ�� �Լ�
    {
        //AddItem(_item, 1);
        item = _item;
        itemCount = _count;
        itemImage.sprite = _item.itemImage;

        if (item.itemType != Item.ItemType.Equipment) //���� ������ Ÿ���� ��� �ƴ϶��
        {
            //go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString(); //������ ������ ǥ���Ѵ�
        }
        else //�׷��� �ʴٸ�
        {
            //go_CountImage.SetActive(false);
            text_Count.text = null; //������ ������ ǥ�������ʴ´�
        }
        SetColor(1);
    }

    public void SetSlotCount(int _count) //�����۰��� �����Լ�
    {
        itemCount += _count; //�������� 1���� �þ��
        text_Count.text = itemCount.ToString(); //������ ���� ǥ��
        if (itemCount <= 0) //���� �������� 0�����
        {
            ClearSlot(); // ������ Ŭ�����Ѵ�
        }
    }

    private void ClearSlot() //���� �ʱ�ȭ
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = " ";
        //go_CountImage.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) //���콺 ������ Ŭ��
        {
            if (item != null) //�������� null�� �ƴ��� Ȯ��
            {
                //�Ҹ�
                SetSlotCount(-1);
                Debug.Log(item.itemName + "�� ����߽��ϴ�");

            }
        }
    }

    public void ReduceItem()
    {
        if (item != null) //�������� null�� �ƴ��� Ȯ��
        {
            //�Ҹ�
            SetSlotCount(-1);
            Debug.Log(item.itemName + "�� ����߽��ϴ�");

        }
    }
}
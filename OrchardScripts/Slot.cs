using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Item item; //획득한 아이템
    public int itemCount; //획득한 아이템의 개수
    public Image itemImage; // 아이템의 이미지

    //필요한 컴포넌트
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
    private void SetColor(float _alpha) //인벤토리 알파값변경 함수
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item, int _count = 1) // 아이템 획득 함수
    {
        //AddItem(_item, 1);
        item = _item;
        itemCount = _count;
        itemImage.sprite = _item.itemImage;

        if (item.itemType != Item.ItemType.Equipment) //만약 아이템 타입이 장비가 아니라면
        {
            //go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString(); //아이템 갯수를 표시한다
        }
        else //그렇지 않다면
        {
            //go_CountImage.SetActive(false);
            text_Count.text = null; //아이템 갯수를 표시하지않는다
        }
        SetColor(1);
    }

    public void SetSlotCount(int _count) //아이템개수 관리함수
    {
        itemCount += _count; //아이템은 1개씩 늘어난다
        text_Count.text = itemCount.ToString(); //아이템 개수 표시
        if (itemCount <= 0) //만약 아이템이 0개라면
        {
            ClearSlot(); // 슬롯을 클리어한다
        }
    }

    private void ClearSlot() //슬롯 초기화
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
        if (eventData.button == PointerEventData.InputButton.Right) //마우스 오른쪽 클릭
        {
            if (item != null) //아이템이 null이 아닌지 확인
            {
                //소모
                SetSlotCount(-1);
                Debug.Log(item.itemName + "를 사용했습니다");

            }
        }
    }

    public void ReduceItem()
    {
        if (item != null) //아이템이 null이 아닌지 확인
        {
            //소모
            SetSlotCount(-1);
            Debug.Log(item.itemName + "를 사용했습니다");

        }
    }
}
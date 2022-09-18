using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetStars : MonoBehaviour
{

    //플레이어가 별과 충돌하면 UI를 하나씩 활성화시키고싶다.
    public Animator anim;

    public int star;
    public GameObject[] starObjectList; //[] :배열
    public int maxSTAR = 0;
    public int STAR
    {       
        get
        { return star; }


        set
        {
            star = value;

            for (int i = 0; i < starObjectList.Length; i++)
            {
                starObjectList[i].SetActive(!(star > i));
                
            }
        }
    }

    public void ResetSTARObject() // ->     public int HP에 넣으면 계속 호출되서 데이터 낭비. 한번은 해야됨 -> 스타트에서 넣기
    {
        //최대체력 갯수만큼 체력UI를 활성화(전체를 켜는)해야한다.
        for (int i = 0; i < starObjectList.Length; i++) // i 는 hpObjectList 인덱스, 부모자식 인덱스 번호는 같음
        {
            starObjectList[i].transform.parent.gameObject.SetActive(i < maxSTAR); //<이 배열의 트랜스폼의 부모의 게임오브젝트를 활성화, 게임오브젝트에는 부모가 없음.
        }
    }
    public void AddSTAR(int value)
    {
        maxSTAR += value;
        if (maxSTAR > starObjectList.Length) //maxHP가 hpObjectList.Length보다 커질 수 없음. -> 막아주기!
        {
            maxSTAR = starObjectList.Length;
        }
        ResetSTARObject();
        STAR = STAR;

    }

    private void Start()
    {
        star = maxSTAR;
        ResetSTARObject();
    }

    private void Update()
    {
        updateClear();
    }

    public void updateClear()
    {
        if (maxSTAR == 5)
        {
            anim.SetTrigger("Clear");
            GameManager.instance.GameClearUI.SetActive(true);
            GameManager.instance.GameClearCamera.SetActive(true);
            //GameManager.instance.MainCamera.SetActive(false);
            GameManager.instance.MainUI.SetActive(false);
        }
    }
}

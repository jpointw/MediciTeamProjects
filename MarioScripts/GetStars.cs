using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetStars : MonoBehaviour
{

    //�÷��̾ ���� �浹�ϸ� UI�� �ϳ��� Ȱ��ȭ��Ű��ʹ�.
    public Animator anim;

    public int star;
    public GameObject[] starObjectList; //[] :�迭
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

    public void ResetSTARObject() // ->     public int HP�� ������ ��� ȣ��Ǽ� ������ ����. �ѹ��� �ؾߵ� -> ��ŸƮ���� �ֱ�
    {
        //�ִ�ü�� ������ŭ ü��UI�� Ȱ��ȭ(��ü�� �Ѵ�)�ؾ��Ѵ�.
        for (int i = 0; i < starObjectList.Length; i++) // i �� hpObjectList �ε���, �θ��ڽ� �ε��� ��ȣ�� ����
        {
            starObjectList[i].transform.parent.gameObject.SetActive(i < maxSTAR); //<�� �迭�� Ʈ�������� �θ��� ���ӿ�����Ʈ�� Ȱ��ȭ, ���ӿ�����Ʈ���� �θ� ����.
        }
    }
    public void AddSTAR(int value)
    {
        maxSTAR += value;
        if (maxSTAR > starObjectList.Length) //maxHP�� hpObjectList.Length���� Ŀ�� �� ����. -> �����ֱ�!
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

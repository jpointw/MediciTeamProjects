using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    // SCORE s===================================
    int score = 0;
    public Text textScore = null;
    int totalScore = 0;
    public Text textTotalScore = null;

    // property : �Լ��ε� ����ó�� ����� �� �ִ�.
    public int SCORE
    {
        get { return score; }
        set
        {
            score = value;
            textScore.text = "���� : " + score + "��";

        }
    }
    void Start()
    {
        // �¾ �� ������ 0������ �ϰ� UI���� 0�� �̶�� ǥ���ϰ�ʹ�.
        SCORE = 0;
    }

    int finalTime;

    // Update is called once per frame
    void FinalTimeScore()
    {
        TOTALSCORE = 10000;

        print("start");
        if (true == GameManager.instance.GameClearUI.activeSelf)
        {
            print("acting");

            finalTime = GameManager.instance.secondsLeft;
            TOTALSCORE += finalTime * 100;

        }
    }
    public int TOTALSCORE
    {
        get { return totalScore; }
        set
        {
            totalScore = value;
            
            textTotalScore.text = "�������� : " + totalScore + "��";

        }
    }
    void Update()
    {
        if (GameManager.instance.GameClearUI.activeSelf)
        {
            FinalTimeScore();
        }
    }

}

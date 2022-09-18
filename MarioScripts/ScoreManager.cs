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

    // property : 함수인데 변수처럼 사용할 수 있다.
    public int SCORE
    {
        get { return score; }
        set
        {
            score = value;
            textScore.text = "점수 : " + score + "점";

        }
    }
    void Start()
    {
        // 태어날 때 점수를 0점으로 하고 UI에도 0점 이라고 표현하고싶다.
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
            
            textTotalScore.text = "최종점수 : " + totalScore + "점";

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

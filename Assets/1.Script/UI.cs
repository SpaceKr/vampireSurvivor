using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text killCountTxt;
    [SerializeField] private TMP_Text timeTxt;
    [SerializeField] private TMP_Text levelTxt;
    [SerializeField] private Image expImage;
    [SerializeField] private GameObject lvPopUp;
    private int level;
    int maxExp = 1280;
    public int Level
    {
        get { return level; 
        }
        set
        {
            level = value;
            levelTxt.text = $"Lv.{level}";
        }
    }private int killCnt;
    public int KillCount
    {
        get { return killCnt; }
        set
        {
            killCnt = value;
            killCountTxt.text = $"{killCnt}";
        }
    }
    private int gameTimer;
    public int GameTime
    {
        get { return gameTimer; }
        set
        {
            gameTimer = value;
            System.DateTime dTime=new System.DateTime();
            
            dTime.AddTicks((long)gameTimer);
            timeTxt.text = $"{dTime.Minute.ToString("00")} : {dTime.Second.ToString("00")}";
        }
    }private float exp;
    private RectTransform rr;
    public float Exp
    {
        get { return exp; }
        set
        {
            exp = value;
            //1280=해상도x크기
            float SizeX = (exp / maxExp) * 1280f;
            expImage.rectTransform.sizeDelta = new Vector2(SizeX, 44);
            //레벨업
            if (exp >= maxExp)
            {
                Level++;
                expImage.rectTransform.sizeDelta = new Vector2(0, 44);
                exp = 0;
                maxExp += 100;
                lvPopUp.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    
    void Start()
    {
        Level = 1;
        Exp = 0;
        lvPopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnSpeedUp()
    {
        gameManager.Instance.p.speed += 1;
        Time.timeScale = 1;
        lvPopUp.SetActive(false);
    }
    public void OnAttackUp()
    {
        gameManager.Instance.p.power += 2;
        Time.timeScale = 1;
        lvPopUp.SetActive(false);
    }
}

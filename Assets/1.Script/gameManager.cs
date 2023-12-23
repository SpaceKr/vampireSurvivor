using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //static은 어디서든 모든 클래스가 접근이 편하게 함
    public static gameManager Instance;
    public Player p;

    // Update is called once per frame
    void Awake()
    {
        //변수의 값이 없으면
        if (Instance == null)
        {
            //this=자기 자신의 클래스, 를 넣음
            Instance = this;
        }
    }
}

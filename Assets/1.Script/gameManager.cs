using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //static�� ��𼭵� ��� Ŭ������ ������ ���ϰ� ��
    public static gameManager Instance;
    public Player p;
    private UI ui;
    public UI UI
    {
        get
        {
            if (ui = null)
            {
                ui = FindObjectOfType<UI>();
            }return ui;
        }
        set { }
        
    }

    // Update is called once per frame
    void Awake()
    {
        //������ ���� ������
        if (Instance == null)
        {
            //this=�ڱ� �ڽ��� Ŭ����, �� ����
            Instance = this;

        }
    }
}

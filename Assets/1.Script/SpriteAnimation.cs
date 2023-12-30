using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))] 
public class SpriteAnimation : MonoBehaviour
{
    private List<Sprite> sprites = new List<Sprite>();
    private SpriteRenderer sr;
    private Image image;

    //  sprite ������ Ÿ��
    private float spriteDelayTime;

    // ������Ÿ�� 0
    private float delayTime = 0f;
    private int spriteAnimationIndex = 0;
    private UnityAction action = null;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        // sprite�� ������ ó�� ���� ����
        if (sprites.Count == 0)
            return;
        
        // ������ ���� �ð��� ����
        delayTime += Time.deltaTime;
        if (delayTime >= spriteDelayTime)
        {
            delayTime = 0;

            sr.sprite = sprites[spriteAnimationIndex];
            spriteAnimationIndex++;

            // sprite�� ������ �Ѿ��
            if (spriteAnimationIndex >= sprites.Count)
            {
                // action �� ���� ��
                if (action == null)
                {
                    spriteAnimationIndex = 0;
                }
                else
                {
                    sprites.Clear();
                    CancelInvoke();
                    Invoke("ActionStart", spriteDelayTime);
                    
                }
            }
        }
    }

    void ActionStart()
    {
        action();
        action = null;
    }

    void Init(List<Sprite> argSprites, float delayTime)
    {
        this.delayTime = float.MaxValue;
        // ������ ���� ���� ��������Ʈ�� ���� ����
        sprites.Clear();

        spriteAnimationIndex = 0;
        sprites = argSprites.ToList();
        spriteDelayTime = delayTime;
    }

    public void SetSprite(List<Sprite> argSprites, float delayTime)
    {
        Init(argSprites, delayTime);
    }
                                                     
    public void SetSprite(List<Sprite> argSprites, float delayTime, UnityAction action)
    {
        Init(argSprites, delayTime);
        // ���� �׼�(��������Ʈ)�� ����Ƽ �׼� ��
        this.action = action;
    }
}

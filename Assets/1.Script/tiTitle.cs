using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tiTitle : MonoBehaviour
{
    [System.Serializable]
    //�ѹ��� ó���ϱ�
    private class CharacterRes
    {
        public Sprite[] sprites;
        public Image image;
        public float delayTime;
        [HideInInspector]
        public float delayTimer;
        [HideInInspector]
        public int aniCount;
    }[SerializeField] private List<CharacterRes> characters;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in characters)
        {
            //��������Ʈ�� ���ų�, �̹����� ���ų� üũ(�Ѵ� �־���� �۵���)
            if (item.sprites.Length != 0 && item.image != null)
            {
                //Ÿ�̸Ӹ� ��鼭 ������ ������ ����
                item.delayTimer+=Time.deltaTime;
                //Ÿ�̸��� ������ �ð��� ����� �ð����� ���ٸ�
                if (item.delayTimer >= item.delayTime)
                {
                    item.delayTimer = 0;
                    //�ش� ��������Ʈ�� �ٸ� ��������Ʈ�� ��ü
                    item.image.sprite = item.sprites[item.aniCount];
                    item.aniCount++;
                    //��������Ʈ ���̺��� Anicount�� ������ 0���� ����
                    if (item.aniCount >= item.sprites.Length)
                    {
                        item.aniCount = 0;
                    }
                }
            }
        }
    }
}

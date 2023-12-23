using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tiTitle : MonoBehaviour
{
    [System.Serializable]
    //한번에 처리하기
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
            //스프라이트가 없거나, 이미지가 없거나 체크(둘다 있어야지 작동함)
            if (item.sprites.Length != 0 && item.image != null)
            {
                //타이머를 놀리면서 프레임 시작을 누적
                item.delayTimer+=Time.deltaTime;
                //타이머의 누적된 시간이 저장된 시간보다 높다면
                if (item.delayTimer >= item.delayTime)
                {
                    item.delayTimer = 0;
                    //해당 스프라이트를 다른 스프라이트로 교체
                    item.image.sprite = item.sprites[item.aniCount];
                    item.aniCount++;
                    //스프라이트 길이보다 Anicount가 높으면 0으로 변경
                    if (item.aniCount >= item.sprites.Length)
                    {
                        item.aniCount = 0;
                    }
                }
            }
        }
    }
}

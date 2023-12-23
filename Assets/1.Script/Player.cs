using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    enum PlayerState
    {
        Stand,
        Run,
        Dead
    }
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private Sprite[] standSprite;
    [SerializeField] private Sprite[] runSprite;
    [SerializeField] private Sprite[] deadSprite;
    private SpriteAnimation sa;
    private SpriteRenderer sr;
    private PlayerState state = PlayerState.Stand;
    void Start()
    {
        sa=GetComponent<SpriteAnimation>();
        sr=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //유니티에 지정된 키보드 값을 가지고 옴
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxisRaw("Vertical") *Time.deltaTime* speed;
        //값을 가지고 온것을 해당 범위만큼만 움직이게
        float clampX = Mathf.Clamp(transform.position.x + x, -19, 19);
        float clampY = Mathf.Clamp(transform.position.y + y, -19, 19);
        transform.position = new Vector2(clampX, clampY);
        //직후 키값이 들어올 경우만
        if (x != 0)
        {
            //만약 내가 왼쪽으로 누르면 x의 키값을 -1로 지정
            //-1일경우 이미지 좌우반전 시킴, 기준은 x의........
            sr.flipX = x < 0 ? true : false;
        }
        //스프라이트 애니메이션 적용
        //기존에 입력값이 없었을때(정지한 상태)
        if ((x != 0 || y != 0) && state != PlayerState.Run)
        {
            state = PlayerState.Run;
            sa.SetSprite(runSprite.ToList(), 0.3f/speed);
        }
        //기존에 윰직이고 있었던 상태
        else if ((x == 0 && y == 0) && state != PlayerState.Stand)
        {
            state = PlayerState.Stand;
            sa.SetSprite(standSprite.ToList(), 0.2f);
        }
    }
}

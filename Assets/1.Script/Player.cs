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
        //����Ƽ�� ������ Ű���� ���� ������ ��
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxisRaw("Vertical") *Time.deltaTime* speed;
        //���� ������ �°��� �ش� ������ŭ�� �����̰�
        float clampX = Mathf.Clamp(transform.position.x + x, -19, 19);
        float clampY = Mathf.Clamp(transform.position.y + y, -19, 19);
        transform.position = new Vector2(clampX, clampY);
        //���� Ű���� ���� ��츸
        if (x != 0)
        {
            //���� ���� �������� ������ x�� Ű���� -1�� ����
            //-1�ϰ�� �̹��� �¿���� ��Ŵ, ������ x��........
            sr.flipX = x < 0 ? true : false;
        }
        //��������Ʈ �ִϸ��̼� ����
        //������ �Է°��� ��������(������ ����)
        if ((x != 0 || y != 0) && state != PlayerState.Run)
        {
            state = PlayerState.Run;
            sa.SetSprite(runSprite.ToList(), 0.3f/speed);
        }
        //������ �����̰� �־��� ����
        else if ((x == 0 && y == 0) && state != PlayerState.Stand)
        {
            state = PlayerState.Stand;
            sa.SetSprite(standSprite.ToList(), 0.2f);
        }
    }
}

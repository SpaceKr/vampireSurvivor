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
    [SerializeField] Transform firePos;
    [SerializeField] private Bullet bullet;
    private SpriteAnimation sa;
    private SpriteRenderer sr;
    private float bulletTimer;
    private float bulletFireTime = 0.2f;
    private PlayerState state = PlayerState.Stand;
    private int HP { get; set; } = 200;
    void Start()
    {
        sa=GetComponent<SpriteAnimation>();
        sr=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletTimer += Time.deltaTime;
        Monster[] monsters = FindObjectsOfType<Monster>();
        if (monsters.Length != 0)
        {
            int findIndex = -1;
            float bulletDistance = 3f;
            for (int i = 0; i < monsters.Length; i++)
            {
                float dis = Vector3.Distance(transform.position, monsters[i].transform.position);
                if (dis <= bulletDistance)
                {
                    findIndex = i;
                    bulletDistance = dis;
                }
            }
            if (findIndex != -1)
            {
                TargetPosRotation(monsters[findIndex]);
                BulletFire();
            }
        }


        MovetoAnimation();
      
        
    }
    void MovetoAnimation()
    {
        //����Ƽ�� ������ Ű���� ���� ������ ��
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
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
            sa.SetSprite(runSprite.ToList(), 0.3f / speed);
        }
        //������ �����̰� �־��� ����
        else if ((x == 0 && y == 0) && state != PlayerState.Stand)
        {
            state = PlayerState.Stand;
            sa.SetSprite(standSprite.ToList(), 0.2f);
        }
    }
    void TargetPosRotation(Monster m)
    {
        //Ÿ���� ��� ���� ��ȯ
        Vector2 vec = transform.position - m.transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        firePos.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    }
    void BulletFire()
    {
        
        if (bulletTimer >= bulletFireTime)
        {
            bulletTimer = 0;
            //�Ѿ� ����, �θ� �ڽ� ���踦 ����
            Bullet b = Instantiate(bullet, firePos);
            b.transform.SetParent(null);
            b.Power = 10;
            
        }
    }
    public void Hit(int damage)
    {
        HP -= damage;
        Debug.Log(HP);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Exp>())
        {
            Destroy(collision.gameObject);
            
        }
    }
}

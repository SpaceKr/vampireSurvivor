using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Monster : MonoBehaviour
{
    public class MonsterData
    {
        public int HP { get; set; }
        public float Speed { get; set; }
        public float TargetDistance { get; set; }
        public float AttackDelay { get; set; }
        public int Power { get; set; }
        public MonsterData()
        {
            HP = 100;
            Speed = 2f;
            TargetDistance = 1f;
            AttackDelay = 1f;
            Power = 5;

        }
    }
    
    [SerializeField] private Sprite[] runSprite;
    [SerializeField] private Sprite[] hitSprite;
    [SerializeField] private Sprite[] deadSprite;
    private SpriteAnimation sa;
    private SpriteRenderer sr;
    public MonsterData data = new MonsterData();
    private float atkDelayTimer;

    

    
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        sr = GetComponent<SpriteRenderer>();
        //ToList() => �迭�� ���¸� ����Ʈ�� ��ȯ
        sa.SetSprite(runSprite.ToList(), 0.2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        //? << �ڵ����� ���� �ִ��� ������ üũ
        if (gameManager.Instance?.p == null)
        {
            return;
        }
        Vector3 targetPos = gameManager.Instance.p.transform.position;
        //Ÿ�ٰ� �Ÿ���
        float distance = Vector3.Distance(targetPos, transform.position);
        if (distance > data.TargetDistance)
        {
            Vector2 dis = targetPos - transform.position;
            Vector3 dir = dis.normalized * Time.deltaTime * data.Speed;

            transform.Translate(dir);
            //normalized x�� Ÿ���� ���� ���ؿ� ���ʿ� ������-, �����ʿ� ������ +
            if (dir.normalized.x > 0)
            {
                sr.flipX = false;
            }
            else if (dir.normalized.x < 0)
            {
                sr.flipX = true;

            }
            //���ݽð� �ʱ�ȭ
            atkDelayTimer = 0;


        }
        //Ÿ�ٰ� ����ﶧ
        else
        {
            atkDelayTimer += Time.deltaTime;
            if (atkDelayTimer >= data.AttackDelay)
            {
                atkDelayTimer = 0;
                //�÷��̾�� Ÿ��
                gameManager.Instance.p.Hit(data.Power);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet b = collision.GetComponent<Bullet>();
        if (b != null & data.HP > 0)
        {
            //������
            data.HP -= b.Power;
            //Ÿ�ݹ޴� �ִϸ��̼� ó��, ���ٽ����� �Լ� ����
            data.Speed = 0;

            sa.SetSprite(hitSprite.ToList(), 0.5f, () =>

            {
                sa.SetSprite(runSprite.ToList(), 0.2f);
                data.Speed = 2f;
            });
            
        
        }
        if (data.HP <= 0)
        {
            //�״� �ִϸ��̼� ó��
            sa.SetSprite(deadSprite.ToList(), 0.2f);
            Destroy(gameObject, 0.2f);
        }
        if (collision.GetComponent<Bullet>())
        {
            
            Destroy(collision.gameObject);
        }
    }
    
}

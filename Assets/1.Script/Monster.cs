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
        //ToList() => 배열의 형태를 리스트로 전환
        sa.SetSprite(runSprite.ToList(), 0.2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        //? << 자동으로 값이 있는지 없는지 체크
        if (gameManager.Instance?.p == null)
        {
            return;
        }
        Vector3 targetPos = gameManager.Instance.p.transform.position;
        //타겟과 거리값
        float distance = Vector3.Distance(targetPos, transform.position);
        if (distance > data.TargetDistance)
        {
            Vector2 dis = targetPos - transform.position;
            Vector3 dir = dis.normalized * Time.deltaTime * data.Speed;

            transform.Translate(dir);
            //normalized x는 타겟이 나의 기준에 왼쪽에 있으면-, 오른쪽에 있으면 +
            if (dir.normalized.x > 0)
            {
                sr.flipX = false;
            }
            else if (dir.normalized.x < 0)
            {
                sr.flipX = true;

            }
            //공격시간 초기화
            atkDelayTimer = 0;


        }
        //타겟과 가까울때
        else
        {
            atkDelayTimer += Time.deltaTime;
            if (atkDelayTimer >= data.AttackDelay)
            {
                atkDelayTimer = 0;
                //플레이어에게 타격
                gameManager.Instance.p.Hit(data.Power);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet b = collision.GetComponent<Bullet>();
        if (b != null & data.HP > 0)
        {
            //데미지
            data.HP -= b.Power;
            //타격받는 애니메이션 처리, 람다식으로 함수 실행
            data.Speed = 0;

            sa.SetSprite(hitSprite.ToList(), 0.5f, () =>

            {
                sa.SetSprite(runSprite.ToList(), 0.2f);
                data.Speed = 2f;
            });
            
        
        }
        if (data.HP <= 0)
        {
            //죽는 애니메이션 처리
            sa.SetSprite(deadSprite.ToList(), 0.2f);
            Destroy(gameObject, 0.2f);
        }
        if (collision.GetComponent<Bullet>())
        {
            
            Destroy(collision.gameObject);
        }
    }
    
}

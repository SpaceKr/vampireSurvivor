using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Monster : MonoBehaviour
{
    [SerializeField] private Sprite[] runSprite;
    [SerializeField] private Sprite[] hitSprite;
    [SerializeField] private Sprite[] deadSprite;
    private SpriteAnimation sa;
    private SpriteRenderer sr;

    protected float targetDistance;
    protected float speed;
    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent< SpriteAnimation> ();
        sr = GetComponent<SpriteRenderer>();
        //ToList() => 배열의 형태를 리스트로 전환
        sa.SetSprite(runSprite.ToList(), 0.2f);
        targetDistance = 1f;
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = gameManager.Instance.p.transform.position;
        //타겟과 거리값
        float distance = Vector3.Distance(targetPos,transform.position);
        if (distance > targetDistance)
        {
            Vector2 dis = targetPos - transform.position;
            Vector3 dir = dis.normalized * Time.deltaTime * speed;

            transform.Translate(dir);
            //normalized x는 타겟이 나의 기준에 왼쪽에 있으면-, 오른쪽에 있으면 +
            if (dir.normalized.x > 0)
            {
                sr.flipX = false;
            }else if (dir.normalized.x < 0)
            {
                sr.flipX = true;
            }
        }
    }
}

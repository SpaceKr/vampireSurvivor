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
        //ToList() => �迭�� ���¸� ����Ʈ�� ��ȯ
        sa.SetSprite(runSprite.ToList(), 0.2f);
        targetDistance = 1f;
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = gameManager.Instance.p.transform.position;
        //Ÿ�ٰ� �Ÿ���
        float distance = Vector3.Distance(targetPos,transform.position);
        if (distance > targetDistance)
        {
            Vector2 dis = targetPos - transform.position;
            Vector3 dir = dis.normalized * Time.deltaTime * speed;

            transform.Translate(dir);
            //normalized x�� Ÿ���� ���� ���ؿ� ���ʿ� ������-, �����ʿ� ������ +
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

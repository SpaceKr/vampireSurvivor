using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    //���� ����
    //������ ����� Plane�� �ڽ��� RespawnRange ������Ʈ
    [SerializeField] private BoxCollider2D[] rangeColliders;
    [SerializeField] private float spawnDelay;
    [SerializeField] private Monster[] monsters;
    [SerializeField] private Transform parent;
    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnDelay)
        {
            spawnTimer = 0;
            Monster m=Instantiate(monsters[0],Return_RandomPosition(),Quaternion.identity);
            m.transform.SetParent(parent);
        }
    }

    Vector3 Return_RandomPosition()
    {
        BoxCollider2D rangeCollider = rangeColliders[Random.Range(0, rangeColliders.Length)];
        Vector3 originPosition = rangeCollider.transform.position;
        //�ݶ��̴� ����� �������� bound.size ���
        float range_X = rangeCollider.bounds.size.x;
        float range_Y = rangeCollider.bounds.size.y;
        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector3 RandomPosition = new Vector3(range_X, range_Y, 0f);
        Vector3 respawnPosition = originPosition + RandomPosition;
        return respawnPosition;

    }
}

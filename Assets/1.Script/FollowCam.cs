using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GameManager.Instance=>null ���� üũ
        //p=>Player�� �ִ� ���� �϶���
        if (gameManager.Instance?.p != null)
        {
            //MoveTowards ù��° ���ڰ�=current ��ǥ,�ι�° ���ڰ�=��ǥ ��ǥ
            //��, ���� ��ǥ���� ��ǥ ��ǥȣ �̵���Ű�� �Լ�
            Vector3 nextPos = gameManager.Instance.p.transform.position;
            //z���� ���� ������ ����
            nextPos.z = transform.position.z;
            //ī�޶� �����µ��� ����,ī�޶��� ������������ ĳ���͸� �̵� ����
            nextPos.x = Mathf.Clamp(nextPos.x, -11, 11);
            nextPos.y = Mathf.Clamp(nextPos.y, -15, 15);
            //�ﰢ������ ���󰡴� �Լ�
            //transform.position = Vector3.MoveTowards(transform.position,nextPos, 0.5f);

            //���� ���󰡴� �ִϸ��̼��� �ʿ��ϴٸ�...
            transform.position = Vector3.Lerp(transform.position, nextPos, speed * Time.deltaTime);
        }
    }
}

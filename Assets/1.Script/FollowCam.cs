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
        //GameManager.Instance=>null 강제 체크
        //p=>Player가 있는 상태 일때만
        if (gameManager.Instance?.p != null)
        {
            //MoveTowards 첫번째 인자값=current 좌표,두번째 인자값=목표 좌표
            //즉, 현재 좌표에서 목표 좌표호 이동시키는 함수
            Vector3 nextPos = gameManager.Instance.p.transform.position;
            //z값을 원래 값으로 유지
            nextPos.z = transform.position.z;
            //카메라 범위는따로 지정,카메라의 끝지점에서는 캐릭터만 이동 ㄱㄴ
            nextPos.x = Mathf.Clamp(nextPos.x, -11, 11);
            nextPos.y = Mathf.Clamp(nextPos.y, -15, 15);
            //즉각적으로 따라가는 함수
            //transform.position = Vector3.MoveTowards(transform.position,nextPos, 0.5f);

            //만약 따라가는 애니매이션이 필요하다면...
            transform.position = Vector3.Lerp(transform.position, nextPos, speed * Time.deltaTime);
        }
    }
}

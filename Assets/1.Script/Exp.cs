using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite[] sprites;
    public Player P { get; set; } = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(P == null)
        {
            P = gameManager.Instance.p;
            return;
        }

        if (P != null)
        {
            transform.position = Vector3.Lerp(P.transform.position,transform.position, 1f);
            float distance=Vector3.Distance(P.transform.position, transform.position);
            if (distance <= 0.5f)
            {
                gameManager.Instance.UI.Exp += 200;
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)      //�浹�� �Ͼ���� ȣ�� �Ǵ� �Լ�
    {
        if(collision.gameObject.tag == "Groun")     //�浹�� �Ͼ ��ü�� Tag�� Ground�� ���

        {
            Debug.Log("���� �浹");                  //�浹�� �Ͼ�� ��� �α׷� Ȯ���Ѵ�.
        }


    }
}

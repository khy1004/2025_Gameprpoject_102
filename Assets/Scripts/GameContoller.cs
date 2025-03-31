using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContoller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))       //마우스 버튼을 눌렀을 때
        {
            RaycastHit hit;                                               //물리 Hit 선언
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // 카메라에서 Ray를

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    //Debug.Log($"hit : {hit .collider.name}");
                    //hit.collider
                }
            }
        }
    }
}

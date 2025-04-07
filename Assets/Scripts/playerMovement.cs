using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;     //이동 속도 변수 설정
    public float jumpForce = 5.0f;     //점프의 침 값을 준다.

    public bool isGrounded = true;     //땅에 있는지 체크 하는 변수

    public int coinCount = 0;          //코인 획득 변수 선언
    public int totalCoins = 5;         //

    public Rigidbody rb;                //플레이어 강체를 선언

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) //트리거 영역 안에 들어왔다 감시하는 함수
    {
        //코인 수집
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(other.gameObject);
            Debug.Log($"코인 수집 : {coinCount}/{totalCoins}");
        }


    } // Update is called once per frame
    void Update()
    {
        //움직임 입력
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //속도값으로 직접 이동
        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

        //점프 입력
        if (Input.GetButtonDown("Jump") && isGrounded) //&& 두 값이 True 일때 ->(Jump 버튼{보통 스페이스바} 와 땅 위에 있을 떄)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  //위쪽으로 설정한 힘만큼 강체에 힘을 전달한다.
            isGrounded = false;  //점프를 하는 순간 땅에서 떨어졌기 때문에 False 라고 한다.

        }
    }
}
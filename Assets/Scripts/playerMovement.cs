using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;     //이동 속도 변수 설정
    public float jumpForce = 5.0f;     //점프의 침 값을 준다.
    public float turnSpeed = 10f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplies = 2.0f;

    public float coyoteTime = 0.15f;
    public float coyoteTimeCounter;
    public bool realGrouned = true;

    public GameObject gliderObject;
    public float gliderFallSpeed = 1.0f;
    public float gliderMoveSpeed = 7.0f;
    public float gliderMaxTime = 5.0f;
    public float gliderTimeLeft;
    public bool isGliding = false;

    public bool isGrounded = true;     //땅에 있는지 체크 하는 변수

    public int coinCount = 0;          //코인 획득 변수 선언
    public int totalCoins = 5;         //

    public Rigidbody rb;                //플레이어 강체를 선언

    void Start()
    {
        if (gameObject != null)
        {
            gameObject.SetActive(false);
        }

        gliderTimeLeft = gliderMaxTime;

        coyoteTimeCounter = 0;
    }

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

        UpdateGroundedState();


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        if(movement.magnitude > 1.0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.G) && !isGrounded && gliderTimeLeft > 0)
        {
            if(!isGrounded)
            {
                //글라이더 비활성화 함수
            }
        }
        gliderTimeLeft -= Time.deltaTime;

        if (gliderTimeLeft <= 0)
        {
            //글라이더 비활성화 함수

        }
        else if (isGliding)
        {
            //G키를 때면 글라이더 비활성화
        }
        //속도값으로 직접 이동
        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if(rb.velocity.y > 0 && ! Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplies - 1) * Time.deltaTime;
        }
        //점프 입력
        if (Input.GetButtonDown("Jump") && isGrounded) //&& 두 값이 True 일때 ->(Jump 버튼{보통 스페이스바} 와 땅 위에 있을 떄)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  //위쪽으로 설정한 힘만큼 강체에 힘을 전달한다.
            isGrounded = false;  //점프를 하는 순간 땅에서 떨어졌기 때문에 False 라고 한다.

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    void UpdateGroundedState()
    {
        if(realGrouned)
        {
            coyoteTimeCounter = coyoteTime;
            isGrounded = true;
        }
        else
        {
            if (coyoteTimeCounter > 0)
            {
                coyoteTimeCounter -= Time.deltaTime;
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        
        void EnableGlider()
        {
            isGliding = true;

            if (gliderObject != null)
            {
                gliderObject.SetActive(true);
            }

            rb.velocity = new Vector3(rb.velocity.x, -gliderMoveSpeed, rb.velocity.z);
        }
        void DisableGlider()
        {
            isGliding = false;

            if (gliderObject != null)
            {
                gliderObject.SetActive(false);
            }
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
        void ApplyGliberMovement(float horizontal, float vertical)
        {
          Vector3 gliderVelocity = new Vector3(horizontal * gliderMoveSpeed,-gliderFallSpeed, vertical * gliderMoveSpeed);

            rb.velocity = gliderVelocity;
        }




    }
}
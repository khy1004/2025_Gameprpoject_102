using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;     //�̵� �ӵ� ���� ����
    public float jumpForce = 5.0f;     //������ ħ ���� �ش�.
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

    public bool isGrounded = true;     //���� �ִ��� üũ �ϴ� ����

    public int coinCount = 0;          //���� ȹ�� ���� ����
    public int totalCoins = 5;         //

    public Rigidbody rb;                //�÷��̾� ��ü�� ����

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
   
    void OnTriggerEnter(Collider other) //Ʈ���� ���� �ȿ� ���Դ� �����ϴ� �Լ�
    {
        //���� ����
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(other.gameObject);
            Debug.Log($"���� ���� : {coinCount}/{totalCoins}");
        }


    } // Update is called once per frame
    void Update()
    {
        //������ �Է�

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
                //�۶��̴� ��Ȱ��ȭ �Լ�
            }
        }
        gliderTimeLeft -= Time.deltaTime;

        if (gliderTimeLeft <= 0)
        {
            //�۶��̴� ��Ȱ��ȭ �Լ�

        }
        else if (isGliding)
        {
            //GŰ�� ���� �۶��̴� ��Ȱ��ȭ
        }
        //�ӵ������� ���� �̵�
        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if(rb.velocity.y > 0 && ! Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplies - 1) * Time.deltaTime;
        }
        //���� �Է�
        if (Input.GetButtonDown("Jump") && isGrounded) //&& �� ���� True �϶� ->(Jump ��ư{���� �����̽���} �� �� ���� ���� ��)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  //�������� ������ ����ŭ ��ü�� ���� �����Ѵ�.
            isGrounded = false;  //������ �ϴ� ���� ������ �������� ������ False ��� �Ѵ�.

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
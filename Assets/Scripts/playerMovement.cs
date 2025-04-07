using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;     //�̵� �ӵ� ���� ����
    public float jumpForce = 5.0f;     //������ ħ ���� �ش�.

    public bool isGrounded = true;     //���� �ִ��� üũ �ϴ� ����

    public int coinCount = 0;          //���� ȹ�� ���� ����
    public int totalCoins = 5;         //

    public Rigidbody rb;                //�÷��̾� ��ü�� ����

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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //�ӵ������� ���� �̵�
        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

        //���� �Է�
        if (Input.GetButtonDown("Jump") && isGrounded) //&& �� ���� True �϶� ->(Jump ��ư{���� �����̽���} �� �� ���� ���� ��)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  //�������� ������ ����ŭ ��ü�� ���� �����Ѵ�.
            isGrounded = false;  //������ �ϴ� ���� ������ �������� ������ False ��� �Ѵ�.

        }
    }
}
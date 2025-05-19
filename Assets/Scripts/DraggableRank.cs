using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableRank : MonoBehaviour
{

    public int rankLevel = 1;                   //����� ����
    public float dragSpeed = 10f;               //�巡�� �� �̵� �ӵ�
    public float snapBackSpeed = 20f;           //����ġ�� ���ư��� �ӵ�

    public bool isDragging = false;             //���� �巡�� ������
    public Vector3 originalPosition;            //���� ��ġ 
    public GridCell currentCell;                //���� ��ġ�� ĭ

    public Camera mainCamera;                   //���� ī�޶�
    public Vector3 dragOffset;                  //�巡�� �� ������ (������)
    public SpriteRenderer spriteRenderer;       //����� �̹��� ������
    public GameManager gamemanager;           //���� �޴���

    private void Awake()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        gamemanager = GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void StartDragging()
    {
        isDragging = true;
        dragOffset = transform.position - GetMouseWorldPosition();
        spriteRenderer.sortingOrder = 10;
    }
    void StopDragging()
    {
        isDragging = false;
        spriteRenderer.sortingOrder = 1;
        GridCell targetCell = gamemanager.FindClosestCell(transform.position);

        if (targetCell !=null)
        {
            if(targetCell.currentRank == null)
            {
                MoveToCell(targetCell);
            }
            else if (targetCell.currentRank != this && targetCell. currentRank.rankLevel == rankLevel)
            {
                MoveToCell(targetCell);
            }
            else
            {
                ReturnToOriginalPosition();
            }
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }
    public void MoveToCell(GridCell targetCell)     //Ư�� ĭ���� �̵�
    {
        if (currentCell != null)
        {
            currentCell.currentRank = null; //���� ĭ���� ����
        }

        currentCell = targetCell;           //�� ĭ���� �̵� 
        targetCell.currentRank = this;

        originalPosition = new Vector3(targetCell.transform.position.x, targetCell.transform.position.y, 0f);
        transform.position = originalPosition;
    }

    public void ReturnToOriginalPosition()//���� ��ġ�� ���ư��� �Լ� 
    {
        transform.position = originalPosition;
    }

    public void MergeWithCell(GridCell targetCell)
    {
        if (targetCell.currentRank == null || targetCell.currentRank.rankLevel != rankLevel) //���� �������� Ȯ��
        {
            ReturnToOriginalPosition(); //���� ��ġ�� ���ư���
            return;
        }

        if (currentCell != null)
        {
            currentCell.currentRank = null; //���� ĭ���� ����
        }

        //��ġ�� ���� MergeRanks �Լ��� ���ؼ� ���� 

    }

    public Vector3 GetMouseWorldPosition()                  //���콺 ���� ��ǥ ���ϱ� 
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    public void SetRankLevel(int level)
    {
        rankLevel = level;

        if (gamemanager != null && gamemanager.rankSprites.Length > level - 1)
        {
            spriteRenderer.sprite = gamemanager.rankSprites[level - 1];     //������ �´� ��������Ʈ�� ���� 
        }
    }


}

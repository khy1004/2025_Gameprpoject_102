using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject coiPrefabs;    //���� �������� ���� �Ѵ�.
    public GameObject MissilePrfabs;


    [Header("���� Ÿ�̹� ����")]
    public float minSpawnlnterval = 0.5f;  //�ּ� ���� ����(��)
    public float mixSpawnlnterval = 2.0f;  //�ִ� ���� ����(��)

    [Header("���� ���� Ȯ�� ����")]
    [Range(0, 100)]
    public int coinSoawnChance = 50;

    public float timer = 0.0f;            //Ÿ�̸�
    public float nextSpawnTime;           //���� ���� �ð�

    // Start is called before the first frame update
    void Start()
    {
        SetNextSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= nextSpawnTime)
        {
            SpawnObject();
            timer = 0.0f;
            SetNextSpawnTime();
        }
    }


    void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnlnterval, mixSpawnlnterval);

    }
    
    void SpawnObject()
    {
        Transform spawnTransform = transform;

        int randomValue = Random.Range(0, 100);
        if (randomValue < coinSoawnChance)
        {
            Instantiate(coiPrefabs, spawnTransform.position, spawnTransform.rotation);
        }
        else
        {
            Instantiate(coiPrefabs, spawnTransform.position, spawnTransform.rotation);
        }
          
    }
}





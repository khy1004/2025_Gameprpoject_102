using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject coiPrefabs;    //동전 프리팹을 선언 한다.
    public GameObject MissilePrfabs;


    [Header("스폰 타이밍 설정")]
    public float minSpawnlnterval = 0.5f;  //최소 생성 간견(초)
    public float mixSpawnlnterval = 2.0f;  //최대 생성 간견(초)

    [Header("동전 스폰 확률 설정")]
    [Range(0, 100)]
    public int coinSoawnChance = 50;

    public float timer = 0.0f;            //타이머
    public float nextSpawnTime;           //다음 생산 시간

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





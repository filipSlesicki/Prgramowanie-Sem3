using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject carPrefab;

    [SerializeField] float spawnDelayMin;
    [SerializeField] float spawnDelayMax;

    [SerializeField] float carSpeedMin = 5;
    [SerializeField] float carSpeedMax = 8;
    //float nextSpawnTime;
    float timer;

    void Update()
    {
        //if(Time.time >= nextSpawnTime)
        //{
        //    SpawnCar();
        //    nextSpawnTime = Time.time + spawnDelay;
        //}

        if(timer <= 0)
        {
            SpawnCar();
            timer = Random.Range(spawnDelayMin,spawnDelayMax);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void SpawnCar()
    {
        GameObject car = Instantiate(carPrefab, transform.position, transform.rotation);
        car.GetComponent<Car>().moveSpeed = Random.Range(carSpeedMin, carSpeedMax);
    }
}

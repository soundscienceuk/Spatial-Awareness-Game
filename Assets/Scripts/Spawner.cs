using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] Asteroid[] spawnableAsteroids;
    [SerializeField] float timeBetweenAsteroids = 1f;
    [SerializeField] float spawnRadius = 1f;

    public bool spawningDone = false;
    [SerializeField] int asteroidsPerWave;


    void Start()
    {

    }

    void Update()
    {
        //SpawnAsteroids();
    }

    public void SpawnNextWave(int toSpawn)
    {
        spawningDone = false;
        asteroidsPerWave = toSpawn;
        StartCoroutine(SpawnAsteroids());
    }

    public bool GetSpawned()
    {
        return spawningDone;
    }

    private IEnumerator SpawnAsteroids()
    {
        for (int i = 0; i < asteroidsPerWave; i++)
        {
            int typeIndex = (int)UnityEngine.Random.Range(0, spawnableAsteroids.Length);
            Asteroid newAsteroid = Instantiate(spawnableAsteroids[typeIndex], transform.position, Quaternion.identity, transform);
            newAsteroid.transform.localPosition = UnityEngine.Random.insideUnitSphere * spawnRadius;
            yield return new WaitForSeconds(timeBetweenAsteroids);
        }

        spawningDone = true;
        //yield return new WaitForSeconds(5f);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}

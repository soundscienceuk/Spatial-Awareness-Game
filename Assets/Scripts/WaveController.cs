using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    [SerializeField] int[] asteroidPerWave;
    [SerializeField] int currentWave;
    [SerializeField] float waitTimeBetweenWaves = 2f;
    [SerializeField] bool waveInProgress = false;
    [SerializeField] Asteroid[] currentAsteroids;
    float time;
    Spawner spawner;
    Core core;

    void Start()
    {
        core = GetComponent<Core>();
        spawner = GetComponent<Spawner>();
    }

    void Update()
    {
        if (waveInProgress && spawner.GetSpawned()) //  Checks if wave more asteroids is in wave and only if all asteroids are done spawning
        {
            CheckForAsteroids();
        }
        
        if (!waveInProgress && !spawner.GetSpawned()) // If wave is done and spawner is done spawning start next wave
        {
            StartCoroutine(StartNextWave());
        }
    }

    private void CheckForAsteroids()
    {
        currentAsteroids = GetComponentsInChildren<Asteroid>();
        if (currentAsteroids.Length <= 0)
        {
            waveInProgress = false;
            spawner.spawningDone = false;
            currentWave++;
        }
    }

    private IEnumerator StartNextWave()
    {
        waveInProgress = true;

        yield return new WaitForSeconds(waitTimeBetweenWaves);

        core.SetWaveText(currentWave+1);
        spawner.SpawnNextWave(asteroidPerWave[currentWave]);



    }
}

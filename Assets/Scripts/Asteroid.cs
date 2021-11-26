using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 1.0f;
    [SerializeField] GameObject explosionFX;
    [SerializeField] AudioClip defaultSFX;
    [SerializeField] AudioClip explodingSFX;
    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource.clip = defaultSFX;
        audioSource.Play();
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    public void Explode()
    {
        var explosion = Instantiate(explosionFX, transform.position, Quaternion.identity);
        audioSource.Stop();
        audioSource.PlayOneShot(explodingSFX);
        Destroy(explosion, 3f);
        gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
        var renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        Destroy(gameObject,1f);
    }

    private void MoveTowardsPlayer()
    {
        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            // Swap the position of the cylinder.
            target.position *= -1.0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Transform cameraTransform;
    [SerializeField] float aimTime;
    [SerializeField] float aimedTime;
    [SerializeField] Image targetingImage;
    [SerializeField] AudioClip lockOnSFX;
    [SerializeField] AudioClip firingSFX;


    AudioSource audioSource;

    bool hitting = false;
    GameObject target;
    Core core;
    float distanceToTarget;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        core = FindObjectOfType<Core>();
    }

    void Update()
    {
        Raycasting();
        if (hitting)
        {
            if (aimedTime == 0)
            {
                audioSource.clip = lockOnSFX;
                audioSource.Play();
            }
            audioSource.volume = aimedTime / aimTime;
            aimedTime += Time.deltaTime;
            targetingImage.fillAmount = aimedTime / aimTime;
        }
        else
        {
            audioSource.Stop();
            aimedTime = 0f;
            targetingImage.fillAmount = 0f;
        }

        if (aimedTime >= aimTime)
        {
            if (target != null)
            {
                target.GetComponentInParent<Asteroid>().Explode();
                int points = (int)(1 * distanceToTarget);
                core.UpdateScore(points);
                target = null;
                audioSource.clip = firingSFX;
                audioSource.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Asteroid>())
        {
            print("dead");
            core.SetDead();
        }
    }

    private void Raycasting()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = LayerMask.GetMask("Asteroid");

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        // layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            hitting = true;
            distanceToTarget = hit.distance;
            target = hit.collider.gameObject;
        }
        else
        {
            Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
            hitting = false;
            distanceToTarget = 0f;

            target = null;
        }
    }
}

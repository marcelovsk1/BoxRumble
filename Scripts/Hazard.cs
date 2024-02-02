using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class Hazard : MonoBehaviour
{
    Vector3 rotation;

    public ParticleSystem breakingEffect;

    private void Start()
    {
        var xRotation = Random.Range(90f, 180f);
        rotation = new Vector3(-xRotation, 0);
    }

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Hazard"))
        {
            Destroy(gameObject);
            Instantiate(breakingEffect, transform.position, Quaternion.identity);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var distance = Vector3.Distance(transform.position, player.transform.position);
                var force = 1f / distance;
                Debug.Log(force);
            }
            else
            {
                Debug.LogError("Player not found!");
            }
        }
    }
}

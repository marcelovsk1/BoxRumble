using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public float forceMultiplier = 5f;
    public float maximumVelocity = 2f;
    public ParticleSystem deathParticles;
    public GameObject mainVCam;
    public GameObject zoomVCam;

    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;

    void Awake()
    {
      QualitySettings.vSyncCount = 0;
      Application.targetFrameRate = 140;
    }

    void Start()
    {
      rb = GetComponent<Rigidbody>();
      cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        if (GetComponent<Rigidbody>().velocity.magnitude <= 5f)
        {
          rb.AddForce(new Vector3(horizontalInput * forceMultiplier * Time.deltaTime,  0, 0));
        }
    }

      private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.GameOver();
            Destroy(gameObject); // Fixed typo here (changed from Destroy(gemeObject) to Destroy(gameObject))
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();

            mainVCam.SetActive(value: false);
            zoomVCam.SetActive(value: true);
        }
    }
}

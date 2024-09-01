using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    
    [SerializeField] Rigidbody rb;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip thrust;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;


    [SerializeField] float ThrustSpeed = 10f;
    [SerializeField] float turnSpeed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        restartLevel();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        thrustParticles.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * ThrustSpeed * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrust);
        }
        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }
        else
        {
            StopTurning();
        }
    }

    private void StopTurning()
    {
        if (rightThrust.isPlaying)
        {
            rightThrust.Stop();
        }
        if (leftThrust.isPlaying)
        {
            leftThrust.Stop();
        }
    }

    private void TurnRight()
    {
        if (leftThrust.isPlaying)
        {
            leftThrust.Stop();
        }
        applyRotation(-turnSpeed);
        rightThrust.Play();
    }

    private void TurnLeft()
    {
        if (rightThrust.isPlaying)
        {
            rightThrust.Stop();
        }
        applyRotation(turnSpeed);
        leftThrust.Play();
    }

    private void applyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation=false;
    }

    private void restartLevel()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

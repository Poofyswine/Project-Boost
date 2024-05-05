using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Movement : MonoBehaviour
{
    [SerializeField] float upForce = 1000f;
    [SerializeField] float rotateForce = 50f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem boosterleftParticles;
    [SerializeField] ParticleSystem boosterRightParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void OnDisable() {
        boosterleftParticles.Stop();
        boosterRightParticles.Stop();
        mainEngineParticles.Stop();
    }

    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * upForce * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

        private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void ProcessRotate(){
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else if(Input.GetKeyUp(KeyCode.A)){
            boosterRightParticles.Stop();
        }

        else if(Input.GetKeyUp(KeyCode.D)){
            boosterleftParticles.Stop();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotateForce);
        if (!boosterRightParticles.isPlaying)
        {
            boosterRightParticles.Play();
        }
    }
    void RotateRight()
    {
        ApplyRotation(-rotateForce);
        if (!boosterleftParticles.isPlaying)
        {
            boosterleftParticles.Play();
        }
    }



    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freesing roaation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //
    }
}

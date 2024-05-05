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


    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space)){
            rb.AddRelativeForce(Vector3.up * upForce * Time.deltaTime);
            if(!audioSource.isPlaying){
            audioSource.PlayOneShot(mainEngine);
            }
        }
        else if(Input.GetKeyUp(KeyCode.Space)){
            audioSource.Stop();
        }
    }

    void ProcessRotate(){
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateForce);
        }

        else if(Input.GetKey(KeyCode.D)){
            ApplyRotation(-rotateForce);
        }
        
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freesing roaation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //
    }
}

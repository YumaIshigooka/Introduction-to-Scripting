using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private Rigidbody rb;
    private Translate translate;
    private bool isHit = false;
    private float dropDelay = 3.0f;
    private float eatDelay = 12.0f;
    private bool slowdown = false;
    private int slowdownRate = 10;
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        translate = GetComponent<Translate>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slowdown) translate.translateSpeed *= Mathf.Pow(1.0f / slowdownRate, Time.deltaTime);
        if (isHit) {
             translate.translateSpeed.z += 20f * Time.deltaTime;
             translate.translateSpeed.z = Mathf.Clamp(translate.translateSpeed.z, -10f, 20f);
             if (transform.eulerAngles.y < 180f) transform.Rotate(0f, 180f * Time.deltaTime, 0f); 
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("DropSheep") && !isHit){
            rb.isKinematic = false;
            slowdown = true;
            Destroy(gameObject, dropDelay);
        }
        if (other.CompareTag("Hay") && !isHit){
            Destroy(other.gameObject);
            isHit = true;
            Destroy(gameObject, eatDelay);
            audioSource.pitch = Random.Range(1.0f, 1.5f);
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        }
    }
}

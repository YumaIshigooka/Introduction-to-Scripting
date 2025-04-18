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
    private MeshRenderer sheepRenderer;
    public AudioClip[] audioClips;


    public Summoner sheepSpawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        translate = GetComponent<Translate>();
        audioSource = GetComponent<AudioSource>();
    }

    public void InitializeSheep(float currTime)
    {
        translate = GetComponent<Translate>();
        sheepRenderer = GetComponentInChildren<MeshRenderer>();
        int sheepType = Random.Range(Mathf.Clamp(100 - 25 *(int)Mathf.Log(currTime + 0.001f), 0, 100), 100);
        if (sheepType < 5) {
            translate.translateSpeed *= 3f;
            sheepRenderer.material.SetColor("_Color", UnityEngine.Color.yellow);
            GetComponentInChildren<ParticleSystem>().startColor = UnityEngine.Color.yellow;
        }
        else if (sheepType < 25 ) {
            translate.translateSpeed *= 2f;
            sheepRenderer.material.SetColor("_Color", UnityEngine.Color.red);
            GetComponentInChildren<ParticleSystem>().enableEmission = false;
        } else GetComponentInChildren<ParticleSystem>().enableEmission = false;
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

    public void SetSpawner(Summoner spawner){
        sheepSpawner = spawner;
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("DropSheep") && !isHit){
            rb.isKinematic = false;
            slowdown = true;
            Destroy(gameObject, dropDelay);
            sheepSpawner.RemoveSheepFromList (gameObject);
            GameState.Instance.DroppedSheep();
        }
        if (other.CompareTag("Hay") && !isHit){
            Destroy(other.gameObject);
            isHit = true;
            Destroy(gameObject, eatDelay);
            audioSource.pitch = Random.Range(1.0f, 1.5f);
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
            sheepSpawner.RemoveSheepFromList (gameObject);
            GameState.Instance.SavedSheep();
            if (sheepRenderer.material.color != UnityEngine.Color.yellow) {
                sheepRenderer.material.SetColor("_Color", UnityEngine.Color.white);
            }
            
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HayMachineController : MonoBehaviour
{
    private float movementSpeed;
    public float acceleration = 50;
    public float friction = 10;
    public float limitX = 22;
    public float hayBaleCooldown = 1;
    private AudioSource audioSource;
    public AudioClip shootSound;
    private float hayBaleTimer;
    public GameObject spawnPos;
    public GameObject hayBale;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movementSpeed = 0;
        hayBaleTimer = hayBaleCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        hayBaleTimer += Time.deltaTime;
        movementSpeed *= Mathf.Pow(1/friction , Time.deltaTime);
        transform.Translate(movementSpeed * Time.deltaTime, 0, 0);

        if(Input.GetKey(KeyCode.LeftArrow)){
            movementSpeed -= acceleration * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.RightArrow)){
            movementSpeed += acceleration * Time.deltaTime;
        }

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -limitX, limitX);

        transform.position = position;


        if((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Space)) && hayBaleTimer > hayBaleCooldown){
            Instantiate(hayBale, spawnPos.transform.position, Quaternion.identity);
            hayBaleTimer = 0;
            audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(shootSound);
        }
    }
}

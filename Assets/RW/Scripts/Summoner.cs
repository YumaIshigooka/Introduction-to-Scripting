using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    public GameObject sheep;
    private float cooldown;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = Random.Range(4.0f, 8.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown) {
            if (Random.Range(0, 2) > 0.5f) Instantiate(sheep, transform.position, Quaternion.identity);
            timer = 0;
            cooldown *= 0.99f;
        }
    }
}

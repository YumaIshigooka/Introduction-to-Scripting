using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Deletor : MonoBehaviour
{
    public float timeToLive = 10;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToLive) Destroy(gameObject);
    }
}

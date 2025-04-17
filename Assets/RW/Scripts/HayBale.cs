using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayBale : MonoBehaviour
{
    private Rigidbody rb;
    private Translate translate;
    private bool slowdown = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        translate = GetComponent<Translate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slowdown) translate.translateSpeed *= Mathf.Pow(0.05f, Time.deltaTime);
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("DestroyHay")){
            Destroy(gameObject); 
        }
        if (other.CompareTag("River")){
            rb.isKinematic = false;
            slowdown = true;
        }
    }
}

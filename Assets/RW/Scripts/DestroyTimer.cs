using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float targetScale; // 1
    public float timeToReachTarget = 1f; // 2
    private float startScale;  // 3
    private float percentScaled; // 4

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale.x; 
        percentScaled = 0f; 
    }

    // Update is called once per frame
    void Update()
    {
        if (percentScaled < 1f) {
            percentScaled += Time.deltaTime / timeToReachTarget; 
            float scale = Mathf.Lerp(startScale, targetScale, percentScaled); 
            transform.localScale = new Vector3(scale, scale, scale); 
        } else {
            Destroy(gameObject); 
        }
    }
}

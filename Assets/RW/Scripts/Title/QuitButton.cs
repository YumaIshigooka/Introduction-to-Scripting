using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) 
    {
        Application.Quit(); 
        Debug.Log("Quit button clicked");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

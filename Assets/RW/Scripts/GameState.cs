using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameState : MonoBehaviour
{
    public static GameState Instance; // 1

    [HideInInspector]
    public int sheepSaved; // 2

    [HideInInspector]
    public int sheepDropped; // 3

    public int sheepDroppedBeforeGameOver; // 4
    public Summoner sheepSpawner;

    public TextMeshProUGUI  sheepSavedText; // 2
    public TextMeshProUGUI  sheepDroppedText;

    public GameObject gameOverPanel; 
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        sheepSaved = 0; 
        sheepDropped = 0; 

        sheepSavedText.text = "Saved: " + sheepSaved;
        sheepDroppedText.text = "Dropped: " + sheepDropped + " / " + sheepDroppedBeforeGameOver; 

        sheepSpawner.canSpawn = true; 
        gameOverPanel.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            RestartGame(); 
        }
    }

    public void SavedSheep()
    {
        sheepSaved++;
        sheepSavedText.text = "Saved: " + sheepSaved; 
    }

    private void GameOver()
    {
        sheepSpawner.canSpawn = false; // 1
        sheepSpawner.DestroyAllSheep(); // 2
        Music.Instance.audioSource.Stop();
        Music.Instance.audioSource.PlayOneShot(Music.Instance.media[1]);
        gameOverPanel.SetActive(true); 
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Title"); 
    }
    public void RestartGame()
    {
        sheepSpawner.canSpawn = true;
        sheepSpawner.DestroyAllSheep();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        gameOverPanel.SetActive(false); 

        sheepSaved = 0; 
        sheepDropped = 0; 

        sheepDroppedText.text = "Saved: " + sheepDropped; 
        sheepDroppedText.text = "Dropped: " + sheepDropped + " / " + sheepDroppedBeforeGameOver; 

        Music.Instance.audioSource.Stop();
        Music.Instance.audioSource.Play();
    }

    public void DroppedSheep()
    {
        sheepDropped++; 
        sheepDroppedText.text = "Dropped: " + sheepDropped + " / " + sheepDroppedBeforeGameOver; 

        if (sheepDropped == sheepDroppedBeforeGameOver) 
        {
            GameOver();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState Instance; // 1

    [HideInInspector]
    public int sheepSaved; // 2

    [HideInInspector]
    public int sheepDropped; // 3

    public int sheepDroppedBeforeGameOver; // 4
    public Summoner sheepSpawner;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
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
        Debug.Log("Sheep saved: " + sheepSaved); // 1
    }

    private void GameOver()
    {
        sheepSpawner.canSpawn = false; // 1
        sheepSpawner.DestroyAllSheep(); // 2
        Music.Instance.audioSource.Stop();
        Music.Instance.audioSource.PlayOneShot(Music.Instance.media[1]);
    }

    private void RestartGame()
    {
        sheepSpawner.canSpawn = true;
        sheepSpawner.DestroyAllSheep();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        Music.Instance.audioSource.Stop();
        Music.Instance.audioSource.Play();
    }

    public void DroppedSheep()
    {
        sheepDropped++; // 1
        Debug.Log("Sheep saved: " + sheepSaved);

        if (sheepDropped == sheepDroppedBeforeGameOver) // 2
        {
            GameOver();
        }
    }
}

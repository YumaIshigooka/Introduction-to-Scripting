using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    
    // Public variables
    public bool canSpawn = true;
    public List<Transform> spawnPositions = new List<Transform>();
    public float timeBetweenSpawns = 1f;
    private float originalTimeBetweenSpawns;
    public List<GameObject> sheepPrefabs;


    private float timeElapsed = 0f;

    // Private variables
    private List<GameObject> sheepList = new List<GameObject>();
    private Summoner sheepSpawner;

    // Start is called before the first frame update
    void Start()
    {
        originalTimeBetweenSpawns = Mathf.Clamp(timeBetweenSpawns, 0.5f, 10f);
        StartCoroutine(SpawnRoutine());
    }

    public void SetSpawner(Summoner spawner){
        sheepSpawner = spawner;
    }

    private void spawnSheep()
    {
        Vector3 randomPosition = spawnPositions[Random.Range(0, spawnPositions .Count)].position;
        GameObject sheepObject = Instantiate(sheepPrefabs[0], randomPosition, sheepPrefabs[0].transform.rotation);
        Sheep sheep = sheepObject.GetComponent<Sheep>();
        sheepList.Add(sheepObject); 
        sheep.SetSpawner(this); 
        sheep.InitializeSheep(timeElapsed); 
    }

    public void RemoveSheepFromList (GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }
        sheepList.Clear(); 
    }


    private IEnumerator SpawnRoutine() {
        while (canSpawn) {
            spawnSheep(); 
            timeBetweenSpawns *= 0.99f;
            timeBetweenSpawns = Mathf.Clamp(timeBetweenSpawns, 0.5f, originalTimeBetweenSpawns); 
            yield return new WaitForSeconds(Random.Range(timeBetweenSpawns, timeBetweenSpawns * 2f));
        }
    }


    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
    }
}

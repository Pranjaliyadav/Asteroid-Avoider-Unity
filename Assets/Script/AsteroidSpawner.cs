using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float secondBetweenAestroid = 1.5f;
    [SerializeField] private Vector2 forceRange;

    private Camera mainCamera;
    private float timer;


    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //every frame countdown the timer, if reaches zero, reset it to predefined timer
        timer = -Time.deltaTime;

        if(timer <= 0)
        {
            SpawnAsteroid();

            timer += secondBetweenAestroid;
        }
    }

    private void SpawnAsteroid()
    {
        int side = Random.Range(0, 4); //to spawn an asteroid, pick a random side of the screen


        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;


        switch (side)
        {
            case 0:
                //left
                spawnPoint.x = 0; //where the spawn points are, these spawn point are viewport points
                spawnPoint.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1f, 1f)); //what direction the force to be applied
                break;
            case 1:
                //right
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1f, 1f)); //-1f as we are on right of screen and we want the x direction to be left which is going to be -ve
                break;
            case 2:
                //bottom
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                direction = new Vector2(Random.Range(-1f, 1f),1f );
                break;
            case 3:
                //top
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }

        Vector3 worldSpawnPoint =  mainCamera.ViewportToWorldPoint(spawnPoint); //convert to world space of the device based on size and shape of screen
        worldSpawnPoint.z = 0; //this way they will appear side by side to player, not above and below it

        GameObject selectedAsteroid = asteroidPrefabs[Random.Range(0,asteroidPrefabs.Length)]; //pick random asteroid 
        GameObject asteroidInstance =  Instantiate(selectedAsteroid, worldSpawnPoint, Quaternion.Euler(0f,0f,Random.Range(0f,360f))); //spawn it on the screen at the position we calculated and rotate it on z axis randomly

        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>(); //grab the rigid body

        rb.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y); //make velocity between x and f randomly


    }
}

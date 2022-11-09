using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemies : MonoBehaviour
{
    //Enemy script
    public Enemy enemy;

    //GameObjects
    public GameObject enemyPrefab;
    public GameObject enemies;
    public GameObject[] predeterminedEnemies;

    float time;

    bool runOnce = true;

    // Update is called once per frame
    void Update()
    {
        //When the second doll is collected 1 ghoul is instantiated every 60 seconds
        if (GameManager.dolls == 2)
        {
            InstantiateEnemies(1, 60f);

            //Activates some more Ghouls
            if (runOnce)
            {
                foreach (GameObject ghoul in predeterminedEnemies)
                {
                    ghoul.SetActive(true);
                }
                runOnce = false;
            }
        }

        //When the third doll is collected 1 ghoul is instantiated every 10 seconds
        else if (GameManager.dolls == 3)
        {
            InstantiateEnemies(1, 10f);
        }

        //When the fourth doll is collected 1 ghoul is instantiated every 5 seconds
        else if (GameManager.dolls == 4)
        {
            InstantiateEnemies(1, 5f);

            //The new ghouls that are instantiated will run to the player
            enemy.alwaysRunToPlayer = true;
        }
    }

    //Instantiates amountEnemies every spawnTimer 
    public void InstantiateEnemies(int amountEnemies, float spawnTimer)
    {
        time += Time.deltaTime;

        if (time > spawnTimer)
        {
            for(int i = 0; i < amountEnemies; i++)
            {
                Vector3 spawnPosition = SpawnPosition();
                GameObject enemyObj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                //Sets the enemy as the child of the Enemies GameObject
                enemyObj.transform.parent = enemies.gameObject.transform;
            }
            //Resets the time
            time = 0;
        }
    }

    //Gets a random x and z position on the map
    public Vector3 SpawnPosition()
    {
        int spawnPointX = Random.Range(-150, 150);
        int spawnPointZ = Random.Range(-150, 150);

        Vector3 spawnPosition = new Vector3(spawnPointX, 5f, spawnPointZ);

        //Returns the Vector3
        return spawnPosition;
    }
}

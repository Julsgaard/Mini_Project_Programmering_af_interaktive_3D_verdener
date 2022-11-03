using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemies : MonoBehaviour
{
    public Enemy enemy;

    public GameObject enemyPrefab;
    public GameObject enemies;
    public GameObject[] predeterminedEnemies;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        //enemy = enemy.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.dolls == 2)
        {
            InstantiateEnemies(1, 60f);

            //Activates some more Ghouls
            foreach(GameObject ghoul in predeterminedEnemies)
            {
                ghoul.SetActive(true);
            }

        }
        else if (GameManager.dolls == 3)
        {
            InstantiateEnemies(1, 10f);
        }
        else if (GameManager.dolls == 4)
        {
            InstantiateEnemies(1, 5f);

            //The new ghouls that are instantiated will wun to the player
            enemy.alwaysRunToPlayer = true;
        }
    }


    public void InstantiateEnemies(int amountEnemies, float spawnTimer)
    {
        time += Time.deltaTime;
        //Debug.Log(time);


        if (time > spawnTimer)
        {
            for(int i = 0; i < amountEnemies; i++)
            {
                Vector3 spawnPosition = SpawnPosition();
                GameObject enemyObj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                enemyObj.transform.parent = enemies.gameObject.transform;
            }

            time = 0;
        }


    }

    public Vector3 SpawnPosition()
    {
        int spawnPointX = Random.Range(-150, 150);
        int spawnPointZ = Random.Range(-150, 150);

        Vector3 spawnPosition = new Vector3(spawnPointX, 5f, spawnPointZ);

        return spawnPosition;
    }

}

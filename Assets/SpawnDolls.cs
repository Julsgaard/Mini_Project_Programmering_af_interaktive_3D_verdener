using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDolls : MonoBehaviour
{
    public GameObject[] dolls;


    // Start is called before the first frame update
    void Start()
    {
        SpawnDoll();

    }

    void Update()
    {
        if (dolls[0].activeSelf && dolls[1].activeSelf)
        {
            int randomNumber = Random.Range(0, 2);

            //Debug.Log(randomNumber);
            if (randomNumber == 0)
                dolls[0].SetActive(false);
            else
                dolls[1].SetActive(false);

            RandomDoll();
        }
    }


    void SpawnDoll()
    {
        for (int i = 0; i < 3; i++)
            RandomDoll();
    }


    void RandomDoll()
    {
        int randomIndex = Random.Range(0, dolls.Length);

        if (dolls[randomIndex].activeSelf)
            RandomDoll();
        else
            dolls[randomIndex].SetActive(true);
    }
}

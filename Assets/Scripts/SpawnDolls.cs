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
        //So the first and second doll can not be active at the same time.
        /*if (dolls[0].activeSelf && dolls[1].activeSelf)
        {
            int randomNumber = Random.Range(0, 2);

            //Debug.Log(randomNumber);
            if (randomNumber == 0)
                dolls[0].SetActive(false);
            else
                dolls[1].SetActive(false);

            RandomDoll();
        }*/
    }

    //Runs the loop 3 times
    void SpawnDoll()
    {
        for (int i = 0; i < 3; i++)
            RandomDoll();
    }


    //Gets a randomIndex, if the doll is already active then a new randomIndex is choosen. If the doll is not active then it is SetActive(true)
    void RandomDoll()
    {
        int randomIndex = Random.Range(0, dolls.Length);

        if (dolls[randomIndex].activeSelf)
            RandomDoll();
        else
            dolls[randomIndex].SetActive(true);
    }
}

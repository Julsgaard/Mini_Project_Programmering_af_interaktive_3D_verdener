using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //Used for the players position
    public Transform Player;

    //Enemy components 
    Animation enemyAnimation;
    NavMeshAgent navMesh;
    Collider colliders;
    AudioSource audioSource;
   
    //Enemy death sound
    public AudioClip deathClip;
    
    //Used for checking if the enemy is dead
    bool dead = false;

    //If true then the enemy will run after the player even tough it is too far away 
    public bool alwaysRunToPlayer = false;

    //Enemy health
    float health = 2;

    //Distance between the player and the enemy
    float distancePlayerEnemy;


    void Awake()
    {
        //Gets the Players transform with the help of the tag "Player"
        Player = GameObject.FindWithTag("Player").transform;

        //Gets the enemy Animation component
        enemyAnimation = GetComponent<Animation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Gets components from the Enemy GameObject
        navMesh = GetComponent<NavMeshAgent>();
        colliders = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculates the distance between the player and the enemy 
        distancePlayerEnemy = Vector3.Distance(Player.transform.position, transform.position);

        //Enemy runs after the player
        RunAfterPlayer();
    }

    //TakeDamage is run when Players raycast hits the enemy collider
    public void TakeDamage(float Amount)
    {
        //Subtracting the health from the amount of damage
        health -= Amount;

        //Checks if the health is less than or equal to 0 and if the enemy is not dead
        if (health <= 0 && !dead)
        {
            //Sets the bool dead to true
            dead = true;

            //Sets the animation to only run once and plays the animation
            enemyAnimation["Death"].wrapMode = WrapMode.Once;
            enemyAnimation.Play("Death");
            
            //Disables the NavMesh and collider for the enemy
            navMesh.enabled = false;
            colliders.enabled = false;

            //Plays enemy death audio clip sets it to only play once
            audioSource.clip = deathClip;
            audioSource.Play();
            audioSource.loop = false;

            //Destroys the enemy gameobject after 20 seconds
            Object.Destroy(gameObject, 20.0f);
        }
    }

    //Makes the enemy run after the player
    void RunAfterPlayer()
    {
        //The bool alwaysRunToPlayer is used for getting all the new ghouls that are instantiated to run after the player
        //when the 4 dolls are collected and the player has to escape (to make the game harder to complete)
        if (!alwaysRunToPlayer)
        {
            //If the enemy is less than 20f from the player and it is not dead then it will run towards the player
            if (distancePlayerEnemy < 20f && distancePlayerEnemy > 0 && !dead)
            {
                //Updates the destination for the enemy NavMesh Agent
                navMesh.SetDestination(Player.position);

                //Plays the enemy animation "Run"
                enemyAnimation.Play("Run");
            }
        }
        else
        {
            if (!dead)
            {
                //Updates the destination for the enemy NavMesh Agent
                navMesh.SetDestination(Player.position);

                //Plays the enemy animation "Run"
                enemyAnimation.Play("Run");
            }
        }
    }

    //Checking if the flashlight is hitting the enemy
    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FlashLight"))
        {
            navMesh.SetDestination(Player.position);

            enemyAnimation.Play("Run");
            Debug.Log("HITTING!!!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FlashLight"))
        {
            navMesh.SetDestination(Player.position);

            enemyAnimation.Play("Run");
            Debug.Log("HITTING COLLISION!!!");
        }
    }*/
}

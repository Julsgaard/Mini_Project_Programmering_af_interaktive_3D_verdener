using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform Player;
    Animation anim;

    NavMeshAgent navMesh;
    Collider colliders;
    
    AudioSource audioSource;
    public AudioClip deathClip;

    bool dead = false;
    public bool alwaysRunToPlayer = false;

    float health = 2;

    void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animation>();
    }

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        colliders = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {

        RunAfterPlayer();

    }

    public void TakeDamage(float Amount)
    {
        health -= Amount;
        if (health <= 0 && !dead)
        {
            dead = true;
            anim["Death"].wrapMode = WrapMode.Once;
            anim.Play("Death");
            navMesh.enabled = false;
            colliders.enabled = false;
            //audioSource.enabled = false;

            //Play death audio clip
            audioSource.clip = deathClip;
            audioSource.Play();
            audioSource.loop = false;

            Object.Destroy(gameObject, 20.0f);
        }
    }

    void RunAfterPlayer()
    {
        //alwaysRunToPlayer is used for getting all the ghouls to run after the player when the 4 dolls are collected
        if (!alwaysRunToPlayer)
        {
            if (Vector3.Distance(Player.transform.position, transform.position) < 20f && Vector3.Distance(Player.transform.position, transform.position) > 0 && !dead)
            {
                navMesh.SetDestination(Player.position);

                anim.Play("Run");

                //Ghoul running after player audioclip
                //audioSource.clip = revolverClick;
                //audioSource.Play();


            }
        }
        else
        {
            if (!dead)
            {
                navMesh.SetDestination(Player.position);

                anim.Play("Run");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FlashLight"))
        {
            navMesh.SetDestination(Player.position);

            anim.Play("Run");
            Debug.Log("HITTING!!!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FlashLight"))
        {
            navMesh.SetDestination(Player.position);

            anim.Play("Run");
            Debug.Log("HITTING COLLISION!!!");
        }
    }


}

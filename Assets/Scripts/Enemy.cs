using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform Player;
    Animation anim;

    NavMeshAgent NavMesh;
    Collider colliders;

    bool dead = false;

    float health = 15;

    void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animation>();
    }

    void Start()
    {
        NavMesh = GetComponent<NavMeshAgent>();
        colliders = GetComponent<Collider>();

    }

    void Update()
    {

        if (Vector3.Distance(Player.transform.position, transform.position) < 15f && Vector3.Distance(Player.transform.position, transform.position) > 0 && !dead)
        {

            NavMesh.SetDestination(Player.position);
            //transform.LookAt(Player);

            //anim.Play("Run");

        }

    }

    public void TakeDamage(float Amount)
    {
        health -= Amount;
        if (health <= 0 && !dead)
        {
            dead = true;
            //anim["Death"].wrapMode = WrapMode.Once;
            //anim.Play("Death");
            NavMesh.enabled = false;
            colliders.enabled = false;
        }
    }

}

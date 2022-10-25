using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Unity CharacterController
    public CharacterController CharacterController;
    float Speed = 7f;
    float JumpSpeed = 7f;

    //Gravity
    float Gravity = 20;
    float VerticalSpeed = 0;

    //Rotate with mouse
    public Transform CameraHolder;
    float MouseSensitivity = 1f;
    float UpLimit = -80;
    float DownLimit = 80;


    //Shoot
    float Damage = 1f;
    float Range = 20f;
    public Camera PlayerCamera;
    public ParticleSystem MuzzleFlash;

    //public ParticleSystem ImpactEffect;

    float time;


    void Start()
    {
        
    }

    void Update()
    {
        Move();
        Rotate();
        Shoot();
    }

    private void Move()
    {
        float HorizontalMove = Input.GetAxis("Horizontal");
        float VerticalMove = Input.GetAxis("Vertical");

        if (CharacterController.isGrounded)
        {
            VerticalSpeed = -2f;
        }
        else
        {
            VerticalSpeed -= Gravity * Time.deltaTime;
        }

        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 12;
        }
        else
        {
            Speed = 7;
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && CharacterController.isGrounded)
        {
            VerticalSpeed = JumpSpeed;
        }
        
        //Adds gravity
        Vector3 GravityMove = new Vector3(0, VerticalSpeed, 0);

        Vector3 Move = transform.forward * VerticalMove + transform.right * HorizontalMove;
        CharacterController.Move(Speed * Time.deltaTime * Move + GravityMove * Time.deltaTime);
    }

    public void Rotate()
    {
        float HorizontalRotation = Input.GetAxis("Mouse X");
        float VerticalRotation = Input.GetAxis("Mouse Y");

        transform.Rotate(0, HorizontalRotation * MouseSensitivity, 0);
        CameraHolder.Rotate(-VerticalRotation * MouseSensitivity, 0, 0);

        Vector3 CurrentRotation = CameraHolder.localEulerAngles;

        if (CurrentRotation.x > 180)
        {
            CurrentRotation.x -= 360;
        }

        CurrentRotation.x = Mathf.Clamp(CurrentRotation.x, UpLimit, DownLimit);
        CameraHolder.localRotation = Quaternion.Euler(CurrentRotation);
    }


    public void Shoot()
    {
        time += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && time >= 0.5f)
        {
            time = 0;

            MuzzleFlash.Play();

            //Play Gunshot audio
            /*AudioSource audio = GetComponent<AudioSource>();
            audio.clip = GunShot;
            audio.Play();*/

            //Using raycast to shoot
            RaycastHit Hit;
            if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out Hit, Range))
            {

                if (Hit.transform.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemy = Hit.transform.GetComponent<Enemy>();
                    enemy.TakeDamage(Damage);
                }

                if (Hit.transform.gameObject.CompareTag("Enemy2"))
                {
                    Enemy2 enemy2 = Hit.transform.GetComponent<Enemy2>();
                    enemy2.TakeDamage(Damage);
                }
                /*
                if (Hit.transform.gameObject.CompareTag("Floor"))
                {
                    GameObject ImpactGameObject = Instantiate(ImpactEffect, Hit.point, Quaternion.LookRotation(Vector3.up));
                    ImpactGameObject.transform.position += Vector3.up * 0.001f;
                }*/
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
        }
    }

}

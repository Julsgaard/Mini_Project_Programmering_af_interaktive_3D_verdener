using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    float Range = 40f;
    public Camera PlayerCamera;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem ImpactEffect;
    public GameObject mainCamera;


    //Reload
    int currentAmmo;
    int maxAmmo = 6;
    bool isReloading = false;
    public GameObject Gun;
    //int ammo = 12;

    //FlashLight
    public GameObject flashLight;
    bool flashLightIsOn = false;


    public GameObject dollsCollectedUI;
    public TMP_Text dollText;

    float time;


    void Start()
    {
        currentAmmo = maxAmmo;

    }

    void Update()
    {
        Move();
        Rotate();
        Shoot();
        ReloadGun();
        //AimGun();
        Flashlight();

        //Debug.Log("ammo: " + ammo);


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

        if (Input.GetMouseButtonDown(0) && time >= 0.25f && !isReloading)
        {
            if (currentAmmo > 0)
            {
                time = 0;

                MuzzleFlash.Play();

                currentAmmo--;

                //Debug.Log("currentAmmo: " + currentAmmo);


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

                    
                    if (Hit.transform.gameObject.CompareTag("Terrain"))
                    {
                        ParticleSystem ImpactParticle = Instantiate(ImpactEffect, Hit.point, Quaternion.LookRotation(Vector3.up));
                        //ImpactParticle.transform.position += Vector3.up * 0.001f;
                    }

                }


                //Recoil after the raycast so the shot hits the target
                mainCamera.transform.Rotate(-7, 0, 0, Space.Self);


            }
            else
            {
                //Play no bullets sound (click sound)
            }
        }
    }

    void ReloadGun()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < 6 && !isReloading)
        {
            StartCoroutine(ReloadWaitForSeconds());

        }

        if (isReloading)
        {
            //Gun.transform.rotation = Quaternion.Euler(0, 0, 90);

            Gun.transform.Rotate(1f, 0, 0, Space.Self);
        }
        else
        {
            Gun.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    /*void AimGun()
    {
        if (Input.GetButton("Fire2"))
        {
            //Debug.Log("Test");


        }
            

    }*/


    //If T is pressed then the flashlight will turn on and off if it is pressed again
    void Flashlight()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!flashLightIsOn)
            {
                flashLight.SetActive(true);
                flashLightIsOn = true;
            }
            else
            {
                flashLight.SetActive(false);
                flashLightIsOn = false;
            }
        }
    }




    //Stops the game if colliding with enemy
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Time.timeScale = 0;

            //GameManager DEAD!!!
        }

    }


    //Picking up Dolls
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Doll") && Input.GetKey(KeyCode.E))
        {
            GameManager.dolls++;
            Destroy(other.gameObject);
            Debug.Log(GameManager.dolls);
            
            //Shows the dolls collected on the UI for 5 seconds
            StartCoroutine(ShowDollsCollected());
        }
    }


    //Reload timer Wait for 5 seconds
    IEnumerator ReloadWaitForSeconds()
    {
        //Debug.Log("Reloading!");

        isReloading = true;

        //Animation spin gun
        //Gun.transform.localEulerAngles

        yield return new WaitForSeconds(5);

        currentAmmo = maxAmmo;

        isReloading = false;

        //Debug.Log("currentAmmo: " + currentAmmo);

    }


    IEnumerator ShowDollsCollected()
    {
        dollText.text = $"Dolls {GameManager.dolls}/4";

        dollsCollectedUI.SetActive(true);

        yield return new WaitForSeconds(5);

        dollsCollectedUI.SetActive(false);

    }

}

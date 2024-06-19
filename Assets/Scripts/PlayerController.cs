using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Unity CharacterController
    public CharacterController CharacterController;
    float speed = 6f;
    float JumpSpeed = 7f;

    //Gravity
    float gravity = 20;
    float verticalSpeed = 0;

    //Rotate with mouse
    public Transform cameraholder;
    float mouseSensitivity = 1f;
    float upLimit = -80;
    float downLimit = 80;

    //Shoot
    float damage = 1f;
    float range = 40f;
    public Camera PlayerCamera;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem impactEffectTerrain;
    public ParticleSystem impactEffectConcrete;
    public GameObject mainCamera;
    float time;

    //Reload
    int currentAmmo;
    int maxAmmo = 6;
    bool isReloading = false;
    public GameObject Gun;

    //FlashLight
    public GameObject flashLight;
    bool flashLightIsOn = false;

    //UI
    public GameObject dollsCollectedUI;
    public TMP_Text dollText;
    public GameObject victoryPanel;

    //Sounds
    public AudioSource audioSource;
    public AudioClip revolverShot;
    public AudioClip revolverClick;
    public AudioClip revolverReload;

    //Exit 
    float exitTime;


    // Start is called before the first frame update
    void Start()
    {
        //Sets the current ammo to max ammo which is 6
        currentAmmo = maxAmmo;

        //Gets the AudioSoruce from the GameObject
        audioSource = GetComponent<AudioSource>();

        //Move the player to the bed in the hospital 
        //transform.position = new Vector3(-2.6f, 2.4f, -146.4f);

        //Turns off the flashlight when the game is started
        flashLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        Shoot();
        ReloadGun();
        Flashlight();
    }

    //Move the player with the keyboard
    private void Move()
    {
        float HorizontalMove = Input.GetAxis("Horizontal");
        float VerticalMove = Input.GetAxis("Vertical");

        if (CharacterController.isGrounded)
        {
            verticalSpeed = -2f;
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }

        //Sprinting
        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 12;
        }
        else
        {
            speed = 7;
        }*/

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && CharacterController.isGrounded)
        {
            verticalSpeed = JumpSpeed;
        }

        //Adds gravity
        Vector3 GravityMove = new Vector3(0, verticalSpeed, 0);

        //The final vector3
        Vector3 Move = transform.forward * VerticalMove + transform.right * HorizontalMove;
        
        //Moveing the character
        CharacterController.Move(speed * Time.deltaTime * Move + GravityMove * Time.deltaTime);
    }

    //Move around the camera with the mouse
    public void Rotate()
    {
        // Mouse X and Y axis
        float HorizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        float VerticalRotation = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player horizontally
        transform.Rotate(0, HorizontalRotation, 0);

        // Rotate the camera holder vertically and clamp the rotation
        cameraholder.Rotate(-VerticalRotation, 0, 0);

        // Get the current rotation of the camera holder
        Vector3 CurrentRotation = cameraholder.localEulerAngles;

        // Adjust the x rotation to allow proper clamping
        if (CurrentRotation.x > 180)
        {
            CurrentRotation.x -= 360;
        }

        // Clamp the x rotation
        CurrentRotation.x = Mathf.Clamp(CurrentRotation.x, upLimit, downLimit);

        // Apply the clamped rotation
        cameraholder.localRotation = Quaternion.Euler(CurrentRotation.x, 0, 0);
    }

    //Shoot gun
    public void Shoot()
    {
        time += Time.deltaTime;

        //Shoot gun if the left mouse button is clicked, the time is more than 0.3 and the gun is not reloading 
        if (Input.GetMouseButtonDown(0) && time >= 0.3f && !isReloading)
        {
            //Checks if the current gun ammo is more than 0
            if (currentAmmo > 0)
            {
                //So the player can only shoot a few times every second - Resets the time
                time = 0;

                //Plays the unity particle system
                MuzzleFlash.Play();

                //One less bullet in the chamber
                currentAmmo--;

                //Debug.Log("currentAmmo: " + currentAmmo);

                //Play Gunshot audio
                audioSource.clip = revolverShot;
                audioSource.Play();

                //Using raycast to shoot
                RayCast();

                //Gun recoil, it is after the raycast so the shot hits the target before the recoil is applied
                mainCamera.transform.Rotate(-7, 0, 0, Space.Self);
            }
            else
            {
                //So the player can only shoot a few times every second - Resets the time
                time = 0;

                //Play no bullets sound
                audioSource.clip = revolverClick;
                audioSource.Play();
            }
        }
    }

    //Using raycast to shoot
    void RayCast()
    {
        RaycastHit Hit;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out Hit, range))
        {
            //Checking if the object hit by the raycast has the Enemy tag
            if (Hit.transform.gameObject.CompareTag("Enemy"))
            {
                //Gets the enemy script from the GameObject hit by the raycast
                Enemy enemy = Hit.transform.GetComponent<Enemy>();
                //Runs the TakeDamage function with the damage argument
                enemy.TakeDamage(damage);
            }

            //Instantiate bullet impact on terrain if the tag is "Terrain"
            if (Hit.transform.gameObject.CompareTag("Terrain"))
            {
                ParticleSystem ImpactParticle = Instantiate(impactEffectTerrain, Hit.point, Quaternion.LookRotation(Vector3.up));
            }

            //Instantiate bullet impact on building if the tag is "Concrete"
            if (Hit.transform.gameObject.CompareTag("Concrete"))
            {
                ParticleSystem ImpactParticle = Instantiate(impactEffectConcrete, Hit.point, Quaternion.LookRotation(Vector3.up));
            }
        }
    }

    //Reload the gun
    void ReloadGun()
    {
        //If the "R" key is pressed, the currentAmmo is less than 6 and isReloading is false then the Coroutine ReloadWaitForSeconds will run 
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < 6 && !isReloading)
        {
            StartCoroutine(ReloadWaitForSeconds());
        }

        //While the gun is reloading, the gun will rotate around its x-axis. Else the gun will reset to its original position
        if (isReloading)
        {
            Gun.transform.Rotate(220f * Time.deltaTime, 0, 0, Space.Self);
        }
        else
        {
            Gun.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    //Turn on and off Flashlight
    void Flashlight()
    {
        //If T is pressed then the flashlight will turn on and off if it is pressed again
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

    //Triggered when on trigger collider enters the players collider
    void OnTriggerEnter(Collider other)
    {
        //Loads "DeathCutscene" if the player is colliding with the enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Time.timeScale = 0;

            //GameManager.PlayerDead = true;
            SceneManager.LoadScene("DeathCutscene");
        }

        //Resets the time if the player walk out of the exit collider
        exitTime = 0;
    }

    //Updates when players collider is inside trigger collider
    void OnTriggerStay(Collider other)
    {
        //Picking up Dolls with the key "E"
        if (other.gameObject.CompareTag("Doll") && Input.GetKey(KeyCode.E))
        {
            //Adds one to the dolls int from GameManager and destroys the doll
            GameManager.dolls++;
            Destroy(other.gameObject);
            Debug.Log($"Dolls Collected: {GameManager.dolls}");
            
            //Shows the dolls collected on the UI for 5 seconds
            StartCoroutine(ShowDollsCollected());
        }

        //Player has to stay in the exit collider for 3 seonds to win the game
        exitTime += Time.deltaTime;
        //Loads "Victory" scene when the player is inside the Exit gate collider, 4 or more dolls are collected and the exitTime is more than 3
        if (other.gameObject.CompareTag("Exit") && GameManager.dolls >= 4 && exitTime > 3)
        {
            //Debug.Log(exitTime);
            SceneManager.LoadScene("Victory");
        }
    }


    //Reload timer Wait for 5 seconds
    IEnumerator ReloadWaitForSeconds()
    {
        //Sets isReloading to true, so the player can't shoot while reloading
        isReloading = true;

        //Reload gun sound
        audioSource.clip = revolverReload;
        audioSource.Play();

        //Waits for 5 seconds then continues the code
        yield return new WaitForSeconds(5);

        //The ammo is set to maxAmmo which is 6
        currentAmmo = maxAmmo;

        //isReloading is set back to false
        isReloading = false;
    }

    //Shows how many dolls the player has collected on the UI for 5 seconds
    IEnumerator ShowDollsCollected()
    {
        //Sets the UI text
        dollText.text = $"Dolls {GameManager.dolls}/4";

        //Displays the UI GameObject
        dollsCollectedUI.SetActive(true);

        //Waits for 5 seconds then continues the code
        yield return new WaitForSeconds(5);

        //Sets the GameObject SetActive state back to false
        dollsCollectedUI.SetActive(false);
    }
}

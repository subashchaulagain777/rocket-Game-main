using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RocketShip : MonoBehaviour
{

    [Header("Script")]

    HealthBar healthBar;
    gameController gc;


    [Header("Value")]

    public float speed;
    public float rotatingSpeed;


    public int MaxHealth = 100;
    public int currentHealth;



    [Space]
     AudioSource audioS;
    Rigidbody rb;
    
    bool isAlive = true;


    [Header("Audio")]
    public AudioClip mainEngine;
    public AudioClip DeathEngine;
    public AudioClip SucessEngine;

    [Space]


    [Header("particle and GameObejct")]
    public ParticleSystem flames;
    public GameObject explosion;


   

   

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<gameController>();
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
        healthBar = FindAnyObjectByType<HealthBar>();

        healthBar.MaxHealth(MaxHealth);
        currentHealth = MaxHealth;

      
    }

    // Update is called once per frame
    void Update()
    {

        if(isAlive)
        {
            Movement();
            explosion.SetActive(false);
        }
       
    }


    public void Movement()
    {
        AddPower();

        Rotation();

    }

    private void AddPower()
    {
        if (Input.GetKey(KeyCode.Space))
        {


            if (audioS.isPlaying == false)
            {
                audioS.PlayOneShot(mainEngine);
            }
            flames.Play();
            rb.AddRelativeForce(speed * Time.deltaTime * Vector3.up, ForceMode.Impulse);

        
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            FlamesStop();
           // Debug.Log(flames);
            audioS.Stop();
            
        }
    }

    void FlamesStop()
    {
       // flames.Clear();
        flames.Stop();
    }

    private void Rotation()
    {
        rb.freezeRotation = true;
        if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow)))
        {

            transform.Rotate(rotatingSpeed * Time.deltaTime * Vector3.forward);

        }

        if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow)))
        {

            transform.Rotate(rotatingSpeed * Time.deltaTime * Vector3.back);

        }

        rb.freezeRotation = false;
    }

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }


    private void OnCollisionEnter(Collision collision)
    {
         if(!isAlive)
        {
            return;
        }

        if (collision.gameObject.CompareTag("landing"))
        {
           
            gc.NextLevel();
            AudioSource.PlayClipAtPoint(SucessEngine, Camera.main.transform.position);
            FlamesStop();
            
            audioS.Stop();

            isAlive = false;

        }



        if (collision.gameObject.CompareTag("obstacle"))
        {

            TakeDamage(15);

            if(currentHealth<=0)
            {
                //PlayerHealth = 0;
                gc.ResetGame();
                AudioSource.PlayClipAtPoint(DeathEngine, Camera.main.transform.position);

                FlamesStop();
                explosion.SetActive(true);
                audioS.Stop();
                isAlive = false;
            }
           
           
            


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("fuel"))
        {
            Debug.Log("fuel");
            currentHealth += 40;
            if(currentHealth>=100)
            {
                currentHealth = 100;
            }
            healthBar.SetHealth(currentHealth);
        }
    }


    private void TakeDamage( int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
       
       
        
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketShip : MonoBehaviour
{

    public float speed;
    public float rotatingSpeed;
     AudioSource audioS;
    Rigidbody rb;
    gameController gc;
    bool isAlive = true;

    public AudioClip mainEngine;
    public AudioClip DeathEngine;
    public AudioClip SucessEngine;


    public ParticleSystem flames;
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<gameController>();
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if(isAlive)
        {
            Movement();
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
            flames.Clear();
            flames.Stop();
            Debug.Log(flames);
            audioS.Stop();
            
        }
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
            audioS.Stop();

            isAlive = false;

        }



        if (collision.gameObject.CompareTag("obstacle"))
        {

            gc.ResetGame();
            AudioSource.PlayClipAtPoint(DeathEngine, Camera.main.transform.position);
            audioS.Stop();
            isAlive=false;
           
            


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("fuel"))
        {
            Debug.Log("this is fuel");
        }
    }


    
}

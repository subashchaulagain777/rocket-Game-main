using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketShip : MonoBehaviour
{

    public float speed;
    public float rotatingSpeed;
    public AudioSource audioS;
    Rigidbody rb;
    gameController gc;
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<gameController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        Movement();
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
                audioS.Play();
            }
            rb.AddRelativeForce(speed * Time.deltaTime * Vector3.up, ForceMode.Impulse);

        }
        else
        {
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
        

        if (collision.gameObject.CompareTag("landing"))
        {
            Debug.Log("This is landing");
            gc.NextLevel();

        }



        if (collision.gameObject.CompareTag("obstacle"))
        {

            gc.ResetGame();

            this.enabled = false;
            audioS.Stop();
           
            


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

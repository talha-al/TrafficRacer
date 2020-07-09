using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArabaKontrol : MonoBehaviour
{
    Rigidbody arabaFizik;
    private float minX = -3.92f, maxX = 3.92f;
    OyunKontrol okScript;
    Trafik trafikScript;
    Quaternion sonNoktaA, sonNoktaB;

    void Start()
    {
        arabaFizik = GetComponent<Rigidbody>();
        okScript = GameObject.FindObjectOfType<OyunKontrol>();
        trafikScript = GameObject.FindObjectOfType<Trafik>();

        sonNoktaA = Quaternion.Euler(0, -10, 7.5f);
        sonNoktaB = Quaternion.Euler(0, 10, -7.5f);
    }

    void FixedUpdate()
    {
        Hareket();
    }

    void Hareket()
    {

        //transform.Translate(Input.acceleration.x, 0, 0);
        //if(Input.acceleration.x<0)
        //{
        //    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, sonNoktaA, 40 * Time.deltaTime);

        //}

        //else if(Input.acceleration.x>)
        //{
        //    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, sonNoktaB, 40 * Time.deltaTime);

        //}


        float yatay = Input.GetAxis("Horizontal");
        arabaFizik.velocity = new Vector3(yatay * 10, 0);

        arabaFizik.position = new Vector3(
                Mathf.Clamp(arabaFizik.position.x, minX, maxX),
                0.0f,
                0);

        if (yatay == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, sonNoktaA, 40 * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, sonNoktaB, 40 * Time.deltaTime);

        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "trafik")
        {
            if (okScript.hiz >= 80)
            {
                transform.rotation = Quaternion.Euler(0, 25, 0);
                Invoke("OyunBitti", 0.3f);

            }
            else if (okScript.hiz < 80)
            {
                okScript.yolIvme -= 2f;

                if (okScript.yolIvme <= 1)
                {
                    okScript.yolIvme = 1;
                }

                collision.gameObject.transform.rotation = Quaternion.Euler(0, 10, 0);

            }

        }

        //if (collision.gameObject.tag == "bariyer")
        //{
        //    //okScript.yolIvme -= 0.5f;
        //}

    }

    public void OyunBitti()
    {
        Time.timeScale = 0;
        okScript.oyunBitti = true;
        okScript.trafikOlusturma = false;

        PlayerPrefs.SetFloat("Skor", okScript.skor);

        if (okScript.skor > okScript.rekor)
        {
            okScript.rekor = okScript.skor;
            PlayerPrefs.SetFloat("Rekor", okScript.rekor);

        }


        SceneManager.LoadScene("AnaMenü");
    }



}

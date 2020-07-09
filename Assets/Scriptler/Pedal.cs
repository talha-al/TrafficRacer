using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    OyunKontrol okScript;
    public GameObject araba;
    ArabaKontrol arbScript;
    Trafik trafikScrp;
    public AudioSource frenSes;
  
    public void Start()
    {
        okScript = GameObject.FindObjectOfType<OyunKontrol>();
        arbScript = GameObject.FindObjectOfType<ArabaKontrol>();
        trafikScrp = GameObject.FindObjectOfType<Trafik>();
        frenSes = GetComponent<AudioSource>();

    }



    public void OnMouseDrag()
    {
        okScript.ivmeKontrol = false;
        okScript.yolIvme -= Time.deltaTime * 2.1f;
        okScript.trafikIvme -= Time.deltaTime * 2f;

        araba.transform.rotation = Quaternion.Euler(7, 0, 0);


        if (okScript.yolIvme <= 2)
        {
            okScript.yolIvme = 2;
        }

       
    }

    public void OnMouseUpAsButton()
    {
        okScript.ivmeKontrol = true;
    }



}

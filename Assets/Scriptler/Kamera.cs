using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    public GameObject araba;
    public Vector3 uzaklik;

    void Awake()
    {
        uzaklik = transform.position - araba.transform.position;
        this.gameObject.SetActive(false);
    }

    
    void FixedUpdate()
    {
        //transform.position = araba.transform.position + uzaklik;
        transform.position = Vector3.Lerp(transform.position, araba.transform.position + uzaklik, 1f);

    }
    
}

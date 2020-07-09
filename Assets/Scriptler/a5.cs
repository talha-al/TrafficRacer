using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a5 : MonoBehaviour
{
    OyunKontrol okScript;
    Collider coll;

    public GameObject araba;
    private bool makasKontrol = true;

    void Awake()
    {
        coll = GetComponent<Collider>();
        okScript = GameObject.FindObjectOfType<OyunKontrol>();

        transform.rotation = Quaternion.Euler(270, 0, 0);

    }


    void Update()
    {
        if (this.transform.position.z <= -13.4)
        {
            this.gameObject.SetActive(false);
        }

        MakasAtma();
    }

    public void MakasAtma()
    {
        float _xDist = Mathf.Abs(this.gameObject.transform.position.x - okScript.arabaPosX);
        float _zDist = Mathf.Abs(this.gameObject.transform.position.z - okScript.arabaPosZ);

        if (_xDist <= 2 && _zDist <= 3 && makasKontrol)
        {
            okScript.skor += 100;
            okScript.makasSayisi++;
            makasKontrol = false;

        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("araba"))
        {
            coll.isTrigger = true;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -4);

        }

        if (collision.gameObject.tag == "trafik")
        {
            this.gameObject.SetActive(false);
        }
    }

}

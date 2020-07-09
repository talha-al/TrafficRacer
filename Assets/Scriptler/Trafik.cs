using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trafik : MonoBehaviour
{
    OyunKontrol okScript;
    public bool ivmeKontrol, makasKontrol = true;
    public GameObject araba;
    Collider coll;


    void Start()
    {

        coll = GetComponent<Collider>();
        okScript = GameObject.FindObjectOfType<OyunKontrol>();
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

        //Debug.Log("x: " + _xDist);
        //Debug.Log("z: " + _zDist);

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

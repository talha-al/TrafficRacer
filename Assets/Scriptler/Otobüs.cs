using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otobüs : MonoBehaviour
{
    [SerializeField] float Ivme;
    public GameObject araba;
    Collider coll;
    Rigidbody otobüsFizik;
    OyunKontrol okScript;
    private bool makasKontrol = true;
    void Start()
    {
        coll = GetComponent<Collider>();
        transform.rotation = Quaternion.Euler(0, 180, 0);

    }

    void Update()
    {
        MakasAtma();

        if (this.transform.position.z<=-13.4)
        {
            this.gameObject.SetActive(false);
        }
        
        
    }

    public void MakasAtma()
    {
        float _xDist = Mathf.Abs(this.gameObject.transform.position.x - araba.transform.position.x);
        float _zDist = Mathf.Abs(this.gameObject.transform.position.z - araba.transform.position.z);

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

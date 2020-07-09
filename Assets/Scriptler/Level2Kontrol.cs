using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Kontrol : MonoBehaviour
{
    ArabaKontrol arbKontrol;

    Rigidbody fizik1, fizik2, fizik3, çölFizik1, çölFizik2;

    public GameObject yol1, yol2, yol3, çöl1, çöl2;
    public GameObject[] trafiktekiArabalar;
    public int arabaNumarasi;

    private float[] xEksenler;
    private float[] zEksenler;

    public float zEksen, xDiziNumarasi;
    public int xEksen;

    public int arabaDiziRndm;
    public float xRndm;
    
    private float arabaOlusturmaZaman;

    Ayarlar _ayarlarScrpt;
    Trafik _trafikScript;
    Light anaIsik, far;

    public bool oyunBitti, trafikOlusturma, ivmeKontrol = true;

    public GameObject araba;
    public GameObject _anaIsikObje, _farObje;
    public GameObject[] kameralar;
    private List<GameObject> _arabaList;

    public float trafikIvme = 4f;
    private int kameraNumarasi;

    public float arabaOlusturmaZamanBuyuk, arabaOlusturmaZamanKucuk;

    public float arabaPosX, arabaPosZ;

    public int makasSayisi = 0;
    public float hiz = 0, skor = 0, rekor = 0;
    public float yolIvme = 0, yolIvmeOrtalamasi = 0;
    private int _ayarlarSayac;

    [Header("Texts")]
    public Text hizText;
    public Text skorText, makasText;

    

    void Start()
    {
        _ayarlarSayac = PlayerPrefs.GetInt("Yoğunluk");

        oyunBitti = false;
        trafikOlusturma = true;

        kameralar[0].SetActive(true);

        anaIsik = _anaIsikObje.GetComponent<Light>();
        far = _farObje.GetComponent<Light>();

        far.range = 0;

        fizik1 = yol1.GetComponent<Rigidbody>();
        fizik2 = yol2.GetComponent<Rigidbody>();
        fizik3 = yol3.GetComponent<Rigidbody>();

        çölFizik1 = çöl1.GetComponent<Rigidbody>();
        çölFizik2 = çöl2.GetComponent<Rigidbody>();


        xEksenler = new float[4];
        xEksenler[0] = -3.4f;
        xEksenler[1] = -1.1f;
        xEksenler[2] = 1.23f;
        xEksenler[3] = 3.52f;

        arabaDiziRndm = 0;
        xDiziNumarasi = 0;
        xRndm = 0;

        kameraNumarasi = 0;

        arabaDiziRndm = 0;
        xDiziNumarasi = 0;
        xEksen = 0;
        xRndm = 0;

        arbKontrol = GameObject.FindObjectOfType<ArabaKontrol>();
        _trafikScript = GameObject.FindObjectOfType<Trafik>();
        _ayarlarScrpt = GameObject.FindObjectOfType<Ayarlar>();
        StartCoroutine(TrafikOlustur());
        arbKontrol = GameObject.FindObjectOfType<ArabaKontrol>();

        StartCoroutine(TrafikOlustur());
    }


    void Update()
    {
        YolHareket();
        TrafikHizKontrol();
        arabaPosX = araba.transform.position.x;
        arabaPosZ = araba.transform.position.z;
        //mesafeText.text = "Mesafe: " + sonMesafe;

    }

    void YolHareket()
    {
        if (trafikOlusturma && !oyunBitti)
        {
            yolIvme += Time.deltaTime * 0.4f; // Zamana bağlı olarak ivme  artar. Yol - yöne daha hızlı gelir.

            fizik1.velocity = new Vector3(0, 0, -5 * yolIvme);
            fizik2.velocity = new Vector3(0, 0, -5 * yolIvme);
            fizik3.velocity = new Vector3(0, 0, -5 * yolIvme);

            çölFizik1.velocity = new Vector3(0, 0, -5 * yolIvme);
            çölFizik2.velocity = new Vector3(0, 0, -5 * yolIvme);


            if (fizik1.transform.position.z <= -28.78)
            {
                fizik1.transform.position += new Vector3(0, -0.1471748f, 86.34f);

            }
            if (fizik2.transform.position.z <= -28.78)
            {
                fizik2.transform.position += new Vector3(0, -0.1471748f, 86.34f);
            }

            if (fizik3.transform.position.z <= -28.78)
            {
                fizik3.transform.position += new Vector3(0, -0.1471748f, 86.34f);
            }

            if (çölFizik1.transform.position.z <= -41f)
            {
                çölFizik1.transform.position += new Vector3(0, -0.1471748f, 120.34f);

            }
            if (çölFizik2.transform.position.z <= -41f)
            {
                çölFizik2.transform.position += new Vector3(0, -0.1471748f, 120.34f);

            }

            if (yolIvme >= 6.5f)
            {
                yolIvme = 6.5f;
            }
        }

    }



    void TrafikHizKontrol()
    {
        if (trafikOlusturma)
        {
            if (ivmeKontrol)
            {
                trafikIvme += Time.deltaTime * 2f;

            }
            else
            {
                trafikIvme -= Time.deltaTime * 2;

            }

            if (trafikIvme >= 7)
            {
                trafikIvme = 7;
            }

            else if (trafikIvme <= -1)
            {
                trafikIvme = -1;
            }

            hiz = 20 * yolIvme;
            hizText.text = "Hız: " + (int)hiz;
            skor += +hiz * 0.005f;
            skorText.text = "Skor: " + skor.ToString("0");
            makasText.text = "Makas: " + makasSayisi;
            //string sonSkor = skor.ToString("0.##");
        }


    }

    IEnumerator TrafikOlustur()
    {
        while (trafikOlusturma && !oyunBitti)
        {
            if (_ayarlarSayac == 0)
            {
                arabaOlusturmaZamanKucuk = Random.Range(3.8f, 5.5f);
                arabaOlusturmaZamanBuyuk = Random.Range(3.5f, 4.5f);
            }

            else if (_ayarlarSayac == 1)
            {
                arabaOlusturmaZamanKucuk = Random.Range(3f, 4);
                arabaOlusturmaZamanBuyuk = Random.Range(2.5f, 3.5f);
            }

            else if (_ayarlarSayac == 2)
            {
                arabaOlusturmaZamanKucuk = Random.Range(3f, 4);
                arabaOlusturmaZamanBuyuk = Random.Range(1.7f, 2.8f);
            }

            else if (_ayarlarSayac == 3)
            {
                arabaOlusturmaZamanKucuk = Random.Range(3f, 4);
                arabaOlusturmaZamanBuyuk = Random.Range(1, 2);
            }

            else if (_ayarlarSayac == 4)
            {
                arabaOlusturmaZamanKucuk = Random.Range(0.8f, 1.9f);
                arabaOlusturmaZamanBuyuk = Random.Range(0.5f, 1.5f);
            }

            arabaNumarasi = Random.Range(0, trafiktekiArabalar.Length);

            if (arabaDiziRndm == arabaNumarasi)
            {
                arabaNumarasi = Random.Range(0, trafiktekiArabalar.Length);
            }

            else
            {
                xEksen = Random.Range(0, xEksenler.Length);

                if (xRndm == xEksen)
                {
                    xEksen = Random.Range(0, xEksenler.Length);

                }
                else
                {
                    xDiziNumarasi = xEksenler[xEksen];
                    Vector3 olusturulacakKonum = new Vector3(xDiziNumarasi, -0.65f, 28);
                    GameObject obj = Instantiate(trafiktekiArabalar[arabaNumarasi], olusturulacakKonum, Quaternion.identity);
                    _arabaList.Add(obj);

                    foreach (GameObject arabalar in _arabaList)
                    {

                        arabalar.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -trafikIvme);
                    }

                    arabaDiziRndm = arabaNumarasi;
                    xRndm = xEksen;

                    if (yolIvme <= 1.5f)
                    {
                        yield return new WaitForSeconds(arabaOlusturmaZamanKucuk);

                    }

                    else
                    {
                        yield return new WaitForSeconds(arabaOlusturmaZamanBuyuk);
                    }

                }

            }
            yield return new WaitForSeconds(0.05f);//null

        }

    }

    public void GeceModu()
    {
        anaIsik.intensity = 0.1f;
        far.range = 99.3f;
    }

    public void GündüzModu()
    {
        anaIsik.intensity = 0.69f;
        far.range = 0;

    }

    public void FarKontrol()
    {
        if (far.range == 99.3f)
        {
            far.range = 0;
        }

        else if (far.range == 0)
        {
            far.range = 99.3f;
        }
    }

    public void KameraAyar()
    {
        kameralar[kameraNumarasi].SetActive(false);
        kameralar[kameraNumarasi++].SetActive(true);

        if (kameraNumarasi >= kameralar.Length)
        {
            kameraNumarasi = 0;
        }
    }
}

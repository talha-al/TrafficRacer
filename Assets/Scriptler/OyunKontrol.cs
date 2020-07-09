using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour
{
    ArabaKontrol arbKontrol;
    Ayarlar _ayarlarScrpt;
    Trafik _trafikScript;
    Light anaIsik, far;

    Rigidbody fizik1, fizik2, fizik3, dağFizik1, dağFizik2, dağFizik3, dağFizik4;

    public bool oyunBitti, trafikOlusturma, ivmeKontrol = true;

    public GameObject araba;
    public GameObject yol1, yol2, yol3;
    public GameObject dağ1, dağ2, dağ3, dağ4;
    public GameObject _anaIsikObje, _farObje;
    public GameObject[] kameralar;
    public List<GameObject> _arabaList;
    public GameObject[] trafiktekiArabalar;

    [Header("Texts")]
    public Text hizText;
    public Text skorText, makasText;

    public float trafikIvme = 4f;
    private int kameraNumarasi;
    private float[] xEksenler;

    private float xDiziNumarasi;
    private int xEksen;
    private int arabaNumarasi;
    public float arabaOlusturmaZamanBuyuk, arabaOlusturmaZamanKucuk;

    public float arabaPosX, arabaPosZ;

    public int arabaDiziRndm;
    public float xRndm;

    public int makasSayisi = 0;
    public float hiz = 0, skor, rekor;
    public float yolIvme = 0, yolIvmeOrtalamasi = 0;
    private int _ayarlarSayac;

    void Start()
    {
        _ayarlarSayac = PlayerPrefs.GetInt("Yoğunluk");

        Time.timeScale = 1;
        oyunBitti = false;
        trafikOlusturma = true;

        kameralar[0].SetActive(true);



        fizik1 = yol1.GetComponent<Rigidbody>();
        fizik2 = yol2.GetComponent<Rigidbody>();
        fizik3 = yol3.GetComponent<Rigidbody>();

        dağFizik1 = dağ1.GetComponent<Rigidbody>();
        dağFizik2 = dağ2.GetComponent<Rigidbody>();
        dağFizik3 = dağ3.GetComponent<Rigidbody>();
        dağFizik4 = dağ4.GetComponent<Rigidbody>();

        anaIsik = _anaIsikObje.GetComponent<Light>();
        far = _farObje.GetComponent<Light>();

        far.range = 0;

        rekor = PlayerPrefs.GetFloat("Rekor");

        xEksenler = new float[4];
        xEksenler[0] = -3.4f;
        xEksenler[1] = -1.1f;
        xEksenler[2] = 1.23f;
        xEksenler[3] = 3.52f;

        kameraNumarasi = 0;

        arabaDiziRndm = 0;
        xDiziNumarasi = 0;
        xEksen = 0;
        xRndm = 0;

        arbKontrol = GameObject.FindObjectOfType<ArabaKontrol>();
        _trafikScript = GameObject.FindObjectOfType<Trafik>();
        _ayarlarScrpt = GameObject.FindObjectOfType<Ayarlar>();
        StartCoroutine("TrafikOlustur");
        
    }

    void Update()
    {
        YolHareket();
        TrafikHizKontrol();
        arabaPosX = araba.transform.position.x;
        arabaPosZ = araba.transform.position.z;
        
    }

    void YolHareket()
    {
        if (trafikOlusturma && !oyunBitti)
        {
            yolIvme += Time.deltaTime * 0.4f; // Zamana bağlı olarak ivme  artar. Yol - yöne daha hızlı gelir.

            fizik1.velocity = new Vector3(0, 0, -5 * yolIvme);
            fizik2.velocity = new Vector3(0, 0, -5 * yolIvme);
            fizik3.velocity = new Vector3(0, 0, -5 * yolIvme);

            dağFizik1.velocity = new Vector3(0, 0, -5 * yolIvme);
            dağFizik2.velocity = new Vector3(0, 0, -5 * yolIvme);
            dağFizik3.velocity = new Vector3(0, 0, -5 * yolIvme);
            dağFizik4.velocity = new Vector3(0, 0, -5 * yolIvme);

            if (fizik1.transform.position.z <= -28.78)
            {
                fizik1.transform.position += new Vector3(0, 0, 86.34f);

            }
            if (fizik2.transform.position.z <= -28.78)
            {
                fizik2.transform.position += new Vector3(0, 0, 86.34f);
            }

            if (fizik3.transform.position.z <= -28.78)
            {
                fizik3.transform.position += new Vector3(0, 0, 86.34f);
            }


            if (dağFizik1.transform.position.z <= -41f)
            {
                dağFizik1.transform.position += new Vector3(0, 0, 110);
                //-0.1471748f
            }

            if (dağFizik2.transform.position.z <= -41f)
            {
                dağFizik2.transform.position += new Vector3(0, 0, 110);

            }
            if (dağFizik3.transform.position.z <= -41f)
            {
                dağFizik3.transform.position += new Vector3(0, -0, 110);

            }
            if (dağFizik4.transform.position.z <= -41f)
            {
                dağFizik4.transform.position += new Vector3(0, -0, 110);

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
                    Vector3 olusturulacakKonum = new Vector3(xDiziNumarasi, -0.5f, 28);
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


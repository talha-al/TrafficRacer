using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ayarlar : MonoBehaviour
{
    OyunKontrol _okScript;
    public Text kontrolText, trafikYogunlukText;
    public string[] trafikYogunluk;
    private int sayac;

    void Start()
    {
        _okScript = GameObject.FindObjectOfType<OyunKontrol>();
        kontrolText.text = "Tuş";
        trafikYogunluk = new string[5];
        trafikYogunluk[0] = "Çok Az";
        trafikYogunluk[1] = "Az";
        trafikYogunluk[2] = "Orta";
        trafikYogunluk[3] = "Fazla";
        trafikYogunluk[4] = "Çok Fazla";
        sayac = 2;
    }


    void Update()
    {
        Debug.Log(sayac);
    }

    public void KontrolDeğistir()
    {
        if (kontrolText.text == "Tuş")
        {
            kontrolText.text = "Sensör";
        }

        else if (kontrolText.text == "Sensör")
        {
            kontrolText.text = "Tuş";
        }

    }

    public void TrafikYogunlukAzalt()
    {
        if (sayac <= 1)
        {
            sayac = 1;
        }

        sayac -= 1;
        trafikYogunlukText.text = trafikYogunluk[sayac];
        

    }

    public void TrafikYogunlukArttır()
    {

        if (sayac >= trafikYogunluk.Length-1)
        {
            sayac = 3;
        }

        sayac += 1;
        trafikYogunlukText.text = trafikYogunluk[sayac];
        
    }


    public void AnaMenüyeDön()
    {
        PlayerPrefs.SetInt("Yoğunluk", sayac);
        SceneManager.LoadScene("AnaMenü");
    }
}

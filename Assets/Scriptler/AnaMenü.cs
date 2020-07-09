using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenü : MonoBehaviour
{
    public Text rekorText,skorText;
    private float rekor;
    private float skor;
    void Start()
    {
        skor = PlayerPrefs.GetFloat("Skor");
        skorText.text = "Skor: " + (int)skor;

        rekor= PlayerPrefs.GetFloat("Rekor");
        rekorText.text = "Rekor: " + (int)rekor;

    }


    void Update()
    {
        
    }

    public void OyunaBasla()
    {
        SceneManager.LoadScene("Dağ");
    }

   public void AyarlaraGit()
    {
        SceneManager.LoadScene("Ayarlar");
    }
}

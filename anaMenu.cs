using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class anaMenu : MonoBehaviour
{
    public Text yuksekscoreTxt;
    public Text puanTxt;

    private void Start()
    {
        int enYuksekSkor = PlayerPrefs.GetInt("yuksekscorekayit");
        int gelenpuan = PlayerPrefs.GetInt("scorekayit");

        yuksekscoreTxt.text = "En Yüksek Puan: "+enYuksekSkor;
        puanTxt.text = "Puan: " + gelenpuan;
    }
    public void OyunaBasla()
    {
        SceneManager.LoadScene("level1");
    }
    public void OyundanCik()
    {
        Application.Quit();
    }
}

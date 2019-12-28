using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerControl : MonoBehaviour
{
    public Sprite[] birdSprite;
    SpriteRenderer birdRenderer;
    Rigidbody2D fizik;
    AudioSource[] sesler;

    bool ileriGeriKontrol = true;
    bool gameOver = true;

    int birdSayac = 0;
    float birdAnimTime = 0;

    int score = 0;
    public Text scoreTxt;

    int HighScore = 0;

    gameControl oyunkontrol;
    private void Start()
    {
        birdRenderer = GetComponent<SpriteRenderer>(); //degiskenimize spriterenderer componentini yukle
        fizik = GetComponent<Rigidbody2D>(); //degiskenimize rigidbody componentini yuklse
        oyunkontrol = GameObject.FindGameObjectWithTag("oyunkontrol").GetComponent<gameControl>(); //gameControl scriptine erismek icin tagiyla arayip atiyoruz
        sesler = GetComponents<AudioSource>(); //sesler dizisine components atiyoruz
        HighScore=PlayerPrefs.GetInt("kayitscore");
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameOver) //mouse sol butonu tiklandiginda
        {
            fizik.velocity = new Vector2(0,0); //yer cekimi arttigi icin velocity'i sifirliyoruz yani hizini sifirladik
            fizik.AddForce(new Vector2(0,200)); //kusun ziplamasi icin fizigine kuvvet uyguluyoruz
            sesler[0].Play();
            
        }
        if (fizik.velocity.y>0) //fizigin y degeri buyuk ise 0 dan
        {
            transform.eulerAngles = new Vector3(0,0,45); //kusun z acisini 45 yap
        }
        else
        {
            transform.eulerAngles = new Vector3(0,0,-45);//kusun z acisini -45 yap
        }

        Animasyon(); //animasyon metodunu cagir
    }

    void Animasyon()
    {
        birdAnimTime += Time.deltaTime; //degiskenimize zaman kadar deger atıyoruz
        if (birdAnimTime > 0.2f) //0.2f den buyuk oldugu zaman
        {
            birdAnimTime = 0; //zamani sifirla

            if (ileriGeriKontrol) //true ise
            {
                birdRenderer.sprite = birdSprite[birdSayac]; //kusun sprite'ini olusturdugumuz dizinin sayac degerini ata
                birdSayac++; //sayaci arttir ki spritelar bir sonra ki dongude degissin
                if (birdSayac == birdSprite.Length) //sayacin uzunlugu dizinin uzunluguna esitlendiginde
                {
                    birdSayac--; //sayaci azalt cünkü sayacın degeri suan 3
                    ileriGeriKontrol = false; //kontrolu false yap
                }
            }
            else
            {
                birdSayac--; //sayaci azalt cunku önce ki dongu 2 sayisini oynatmisti
                birdRenderer.sprite = birdSprite[birdSayac]; //kusun sprite'ini olusturdugumuz dizinin sayac degerini ata
                if (birdSayac == 0) //sayac sifira esitlendiginde
                {
                    birdSayac++; //sayaci 1 arttir
                    ileriGeriKontrol = true; //kontrolu true yap ki diger donguye girsin
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="puan") //degdigimiz objenin tag i puan ise
        {
            score++; //score degiskenini arttir
            scoreTxt.text = "Score: "+score; //score textine yazdir
            sesler[1].Play(); //score sesini cal

        }
        if (collision.gameObject.tag=="engel") //degdigimiz objenin tag i engel ise
        {
            
            gameOver = false; //bool degiskenini false yap
            sesler[2].Play(); //engel sesini cal
            oyunkontrol.GameOver(); //gameControl scriptinden GameOver metodunu cagir
            Invoke("anaMenuyeDon", 2);
            GetComponent<CircleCollider2D>().enabled = false; //baska ses calmamasi icin kusun colliderini kapat
        }
        if (score>HighScore) //alinan puan en yuksek puandan buyuk ise
        {
            HighScore = score; //puanin degerini en yuksek puana ata
            PlayerPrefs.SetInt("yuksekscorekayit",HighScore); //kayit et
        }
        
    }

    void anaMenuyeDon()
    {
        PlayerPrefs.SetInt("scorekayit",score);
        SceneManager.LoadScene("menu");

    }

}

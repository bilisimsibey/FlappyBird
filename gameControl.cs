using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControl : MonoBehaviour
{
    public GameObject gokyuzuBir; //1. gokyuzu
    public GameObject gokyuzuİki; //2. gokyuzu

    Rigidbody2D fizikBir; //1. fizik
    Rigidbody2D fizikİki; //2. fizik

    float uzunluk = 0;
    public float gokyuzuHiz = -1.5f;

    public GameObject engel;
    public int kacAdetEngel=5; // kac adet engel olusmasini istiyor isek
    GameObject[] engeller;

    float degisimZaman;
    int sayac;
    bool oyunbitti = true;

    private void Start()
    {
        fizikBir = gokyuzuBir.GetComponent<Rigidbody2D>(); //1. gokyuzu fizigini atadik
        fizikİki = gokyuzuİki.GetComponent<Rigidbody2D>(); //2. gokyuzu fizigini atadik

        fizikBir.velocity = new Vector2(gokyuzuHiz,0); //1. gokyuzu hizi
        fizikİki.velocity = new Vector2(gokyuzuHiz,0); //2. gokyuzu hizi

        uzunluk = gokyuzuBir.GetComponent<BoxCollider2D>().size.x; //uzunluk degiskenimize 1. gokyuzunun box collider x ekseni uzunlugunu aldik

        engeller = new GameObject[kacAdetEngel]; //olusturulan diziye kac eleman oldugunu veriyoruz

        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i] = Instantiate(engel,new Vector2(-20,-20),Quaternion.identity); //engeller dizisinin i elemanini olustur.
            Rigidbody2D fizikEngel= engeller[i].AddComponent<Rigidbody2D>(); //kod ile objelere rigidbody componenti ekliyoruz
            fizikEngel.gravityScale = 0; //objelerin yercekimini kapatiyoruz
            fizikEngel.velocity = new Vector2(gokyuzuHiz,0); //engelleri, gokyuzu hizi kadar hizlandiriyoruz
        }
    }

    private void Update()
    {
        if (oyunbitti)
        {

            if (gokyuzuBir.transform.position.x <= -uzunluk) //1. gokyuzunun x pozisyonu uzunluk degiskeninin eksi degerine kucuk esit ise
            {
                gokyuzuBir.transform.position += new Vector3(uzunluk * 2, 0, 0); //1. gokyuzunun x pozisyonuna uzunluk degiskeninin 2 kati kadar deger ekle
            }
            if (gokyuzuİki.transform.position.x <= -uzunluk) //2. gokyuzunun x pozisyonu uzunluk degiskeninin eksi degerine kucuk esit ise
            {
                gokyuzuİki.transform.position += new Vector3(uzunluk * 2, 0, 0); //2. gokyuzunun x pozisyonuna uzunluk degiskeninin 2 kati kadar deger ekle
            }
        }




        //----------------------------------------------------------------------------

        degisimZaman += Time.deltaTime; //degiskenimize reel zaman degeri veriyoruz

        if (degisimZaman>2f) //2 saniyeden buyuk ise
        {
            degisimZaman = 0; //saniyeyi sifirla ki her 2 saniyede if e girebilsin
            float Yeksenim = Random.Range(-0.50f,1.10f); //random olarak engellerin yukseklik-alcaklik degerlerini belirliyoruz
            engeller[sayac].transform.position = new Vector3(10,Yeksenim); //engeller dizisinde ki objelerin pozisyonunu 18 den baslat y eksenini random al
            sayac++; //sayaci arttir ki engeller olusmaya devam etsin
            if (sayac>=engeller.Length) //sayac bir sure sonra hata verecektir bu yuzden dizinin uzunlugundan buyuk esit ise
                sayac = 0; //sayaci sifirla ki dongu bozulmasin
        }
    }

    public void GameOver()
    {
        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero; //engellerin velocitysini sifirliyoruz
            fizikBir.velocity = Vector2.zero; //fizikbir sifirliyoruz
            fizikİki.velocity = Vector2.zero; //fizikiki sifirliyoruz
        }
        oyunbitti = false; //bool degiskeni false yapiyoruz
    }
}

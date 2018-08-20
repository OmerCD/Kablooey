using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class AraMenüFonksiyonları : MonoBehaviour {
    public Text highScore;
    public Text menuScore;
    public Text score;
    public Text coin;
    public static bool oyunDurdu = false;
    public static bool oyunBitti = false;
    public static bool geriSayım = false;
    void Awake()
    {
        oyunDurdu = oyunBitti = false;
    }
    public void AraMenüGöster()
    {
        
        if (!oyunBitti)
        {
            Canvas AraMenü = GetComponent<Canvas>();
            if (AraMenü.enabled)
            {
                oyunDurdu = false;
                AraMenü.enabled = false;
            }
            else {
                highScore.text = PlayerPrefs.GetInt("High Score").ToString();
                menuScore.text = score.text;
                AraMenü.enabled = true;
                oyunDurdu = true;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AraMenüGöster();
        }
    }
	public void AnaMenüyeDön()
    {
        oyunDurdu = false;
        SceneManager.LoadScene("Ana Menü");
    }
    public void TekrarOyna()
    {
        int can = PlayerPrefs.GetInt("Can Sayısı");       
        if (can!=0)
        {
            coin.text = Convert.ToString(can - 1);
            PlayerPrefs.SetInt("Can Sayısı", can - 1);
            SceneManager.LoadScene("1");
        }
        oyunDurdu = true;
    }
    public void Cikis()
    {
        Application.Quit();
    }
}

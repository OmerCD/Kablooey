using UnityEngine;
using System.Collections;

public class Kutu : MonoBehaviour
{
    public KutuÖzelliği KutuCinsi;
    public enum KutuÖzelliği
    {
        Normal,
        Parlak,
        Siyah,
        Patlak,
        Süre
    }
    public virtual Color Renk
    {
        get { return GetComponent<Renderer>().material.color; }
    }

    public bool Süre
    {
        get
        {
            return KutuCinsi == KutuÖzelliği.Süre;
        }
    }
    public bool Normal
    {
        get
        {
            return KutuCinsi == KutuÖzelliği.Normal;
        }
    }
    public bool Siyah
    {
        get
        {
            return KutuCinsi == KutuÖzelliği.Siyah;
        }
    }
    public bool Parlak
    {
        get
        {
            return KutuCinsi == KutuÖzelliği.Parlak;
        }
    }
    public bool Patlak
    {
        get
        {
            return KutuCinsi==KutuÖzelliği.Patlak;
        }

        set
        {
            KutuCinsi = value ? KutuÖzelliği.Patlak:KutuCinsi;
        }
    }

    public int X { get; private set; }

    public int Y { get; private set; }

    bool patlamaDurumu = false;
    float kaybolmaSayacı;
    void Awake()
    {
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        kaybolmaSayacı = KutuKontrol.patlamaEfektiSüresi;
    }

    public void SetUpPosition()
    {
        X = (int)transform.position.x;
        Y = (int)transform.position.y;
    }
    void OnMouseUpAsButton()
    {

        if (!KutuKontrol.patlamaVar && !AraMenüFonksiyonları.oyunDurdu)
        {
            int kutuÖzelliği = -1;
            if (KutuCinsi==KutuÖzelliği.Parlak)
            {
                kutuÖzelliği = 0;
            }
            else if (KutuCinsi == KutuÖzelliği.Siyah)
            {
                kutuÖzelliği = 1;
            }
            else if (KutuCinsi==KutuÖzelliği.Süre)
            {
                kutuÖzelliği = 2;
            }
            PuanGöster.yeniKutuyaTıklanıldı = true;
            KutuKontrol.tıklananKutu = new Vector3(X, Y, kutuÖzelliği);
        }
    }
    public void Patlat(GameObject PatlamaEfekti)
    {
        KutuCinsi = KutuÖzelliği.Patlak;
        Instantiate(PatlamaEfekti, new Vector3(X, Y, transform.position.z - 0.5f), Quaternion.identity);
        KutuKontrol.patlamaVar = patlamaDurumu = true;
    }
    public bool AynıRenk(Kutu DiğerKutu)
    {
        if (DiğerKutu == null)
            return false;
        return Renk == DiğerKutu.Renk;
    }

    private void Pool()
    {
        Destroy(KutuCinsi == KutuÖzelliği.Parlak ? GetComponent<KutuParlak>() : GetComponent<Kutu>());
        KutuKontrol.BoxPool.Push(gameObject);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    protected void Update()
    {
        if (patlamaDurumu)
        {
            kaybolmaSayacı -= Time.deltaTime;
            if (kaybolmaSayacı <= 0)
            {
                //Destroy(gameObject);
                Pool();
                patlamaDurumu = KutuKontrol.patlamaVar = false;
                return;
            }
        }
        float x = this.X;
        float y = this.Y;
        if (y > 0 && !KutuKontrol.KutuVarmı(x, y - 1))
        {
            while (y > 0 && !KutuKontrol.KutuVarmı(x, y - 1))
            {
                y--;
            }
            this.Y = (int)y;
        }
        if (this.Y != transform.position.y)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(x, y), KutuKontrol.KutuDüşmeHızı * Time.deltaTime);
        }
    }
}

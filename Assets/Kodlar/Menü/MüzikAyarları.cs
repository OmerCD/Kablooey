using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MüzikAyarları : MonoBehaviour
{
    public Sprite mute, muteHover, mutePressed, unmute, unmuteHover, unmutePressed;
    AudioSource ses;
    Button btn;
    public GameObject müzikObjesi;
    GameObject müzik;


    void Start()
    {
        //Button b = GetComponent<Button>();
        //b.image.sprite = açık;
        GameObject geçiciMüzikObjesi = GameObject.Find("Müzik(Clone)");
        if (!geçiciMüzikObjesi)
        {
            müzik = Instantiate(müzikObjesi);
        }
        else
        {
            müzik = geçiciMüzikObjesi;
        }
        ses = müzik.GetComponent<AudioSource>();

        DontDestroyOnLoad(müzik);
        btn = GetComponent<Button>();
        try
        {
            if (PlayerPrefs.GetInt("Ses") == 0)
            {
                ses.mute = true;
                SpriteChange(btn, false);
            }
            else
            {
                ses.mute = false;
                SpriteChange(btn, true);
            }
        }
        catch
        {

        }
    }
    public void MüzikAyarla()
    {
        ses = müzik.GetComponent<AudioSource>();
        btn = GetComponent<Button>();
        if (ses.mute)
        {
            ses.mute = false;
            SpriteChange(btn, true);
            PlayerPrefs.SetInt("Ses", 1);

        }
        else
        {
            ses.mute = true;
            SpriteChange(btn, false);
            PlayerPrefs.SetInt("Ses", 0);
        }
    }

    private void SpriteChange(Button btn, bool state)
    {
        SpriteState spriteState = new SpriteState();
        spriteState = btn.spriteState;
        if (state)
        {
            btn.GetComponent<Image>().sprite = unmute;
            spriteState.highlightedSprite = unmuteHover;
            spriteState.pressedSprite = unmutePressed;
        }
        else
        {
            btn.GetComponent<Image>().sprite = mute;
            spriteState.highlightedSprite = muteHover;
            spriteState.pressedSprite = mutePressed;
        }
        btn.spriteState = spriteState;
    }
}

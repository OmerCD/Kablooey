using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SesAyarları : MonoBehaviour {
    public Sprite mute, muteHover, mutePressed, unmute, unmuteHover, unmutePressed;
    public static bool sesDurumu;
    void Awake()
    {
        Button btn=GetComponent<Button>();
        SpriteChange(btn, true);
        sesDurumu = PlayerPrefs.GetInt("SesDurumu") == 1 ? true : false;
    }
    void Start()
    {
        Button btn = GetComponent<Button>();
        SpriteChange(btn, sesDurumu);
        foreach (AudioSource item in FindObjectsOfType<AudioSource>())
        {
            if (item.name == "Müzik(Clone)")
            {
                continue;
            }
            item.GetComponent<AudioSource>().mute = !sesDurumu;
        }
    }
    public void SesleriAyarla()
    {
        Button btn = GetComponent<Button>();
        sesDurumu = btn.image.sprite.name == "unmute" ? false : true;
        SpriteChange(btn, sesDurumu);

        PlayerPrefs.SetInt("SesDurumu", sesDurumu ? 1 : 0);

        foreach (AudioSource item in FindObjectsOfType<AudioSource>())
        {
            if (item.name== "Müzik(Clone)")
            {
                continue;
            }
            item.GetComponent<AudioSource>().mute = !sesDurumu;
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

using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer_Behavior : MonoBehaviour
{
    public Toggle musicToggle;
    public Slider musicSlider;
    public Image musicImage;
    public Toggle sfxToggle;
    public Slider sfxSlider;
    public Image sfxImage;
    public AudioSource musicPlayer;
    public AudioSource sfxPlayer;
    public Sprite musicOnIcon;
    public Sprite musicOffIcon;
    public Sprite sfxOnIcon;
    public Sprite sfxOffIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateMusic()
    {


    }

    public void toggleMusic()
    {
        print("run");
        bool b = musicToggle.isOn;

        musicPlayer.mute = b;

        if (b == true)
        {
            musicImage.overrideSprite = musicOnIcon;
        }

        if (b == false)
        {
            musicImage.overrideSprite = musicOffIcon;
        }

    }

    public void updateSFX()
    {

    }

    public void toggleSFX()
    {

    }
}

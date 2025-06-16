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
    public float baseMusicVolume;
    public float baseSFXVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSlider.value = baseMusicVolume;
        sfxSlider.value = baseSFXVolume;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hide(bool b)
    {
        musicSlider.gameObject.SetActive(b);
        sfxSlider.gameObject.SetActive(b);
        musicToggle.gameObject.SetActive(b);
        sfxToggle.gameObject.SetActive(b);

    }

    public void restartMusic()
    {
        musicPlayer.Play();
    }

    public void updateMusic()
    {
        float v = musicSlider.value;
        musicPlayer.volume = v * baseMusicVolume;
    }

    public void toggleMusic()
    {
        print("run");
        bool b = musicToggle.isOn;

        musicPlayer.mute = !b;

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
        float v = sfxSlider.value;
        sfxPlayer.volume = v * baseSFXVolume;
    }

    public void toggleSFX()
    {
        print("run");
        bool b = sfxToggle.isOn;

        sfxPlayer.mute = !b;

        if (b == true)
        {
            sfxImage.overrideSprite = sfxOnIcon;
        }

        if (b == false)
        {
            sfxImage.overrideSprite = sfxOffIcon;
        }

    }
}

using UnityEngine;
using UnityEngine.UI;

public class Camera_Behavior : MonoBehaviour
{

    public float zoomSliderValue;
    public Slider zoomSlider;
    public float baseZoom;
    public float currentZoom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zoomSlider = GameObject.Find("Zoom Slider").GetComponent<Slider>();
        baseZoom = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateZoom()
    {
        zoomSliderValue = zoomSlider.value;
        Camera.main.orthographicSize = zoomSliderValue * baseZoom;

    }

    public void hide(bool b)
    {
        zoomSlider.gameObject.SetActive(b);
    }
}

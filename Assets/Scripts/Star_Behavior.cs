using UnityEngine;
using System.Collections;

public class Star_Behavior : MonoBehaviour
{
    public SpriteRenderer sR;
    public float solarMass;
    public float solarRadius;
    public float rotationSpeed;
    public float axisRotation;
    public string classLetter;
    public float solarMassMin = 0.08f;
    public float solarMassMax = 16f;
    public float solarRadiusMin = 0.7f;
    public float solarRadiusMax = 6.6f;
    public float rotationSpeedMin = 0;
    public float rotationSpeedMax = 10;
    public float axisRotationMin = 0;
    public float axisRotationMax = 360;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();

        solarMass = (Random.Range(0.45f, 3f));

        CalculateStar();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateStar();
    }

    void CalculateStar()
    {
        solarMass = Mathf.Clamp(solarMass, 0.01f, 20f);

        //solarRadius = ((solarRadiusMax - solarRadiusMin) / (solarMassMax - solarMassMin)) * (solarMass - solarMassMin) + solarRadiusMin;

        solarRadius = (Mathf.Pow(solarMass, 0.78f) + 0.25f);

        transform.localScale = new Vector3(solarRadius, solarRadius, solarRadius);

        //add later: lerp the colors of star so it is smooth instead of one or the other.

        if (solarMass < 0.08f)
        {
            classLetter = "L";
            sR.color = Color.Lerp(Color.red, Color.grey, 0.6f);
        }
        
        if (0.08f <= solarMass && solarMass < 0.45f)
        {
            classLetter = "M";
            sR.color = Color.red;
        }

        else if (0.45f <= solarMass && solarMass < 0.8f)
        {
            classLetter = "K";
            sR.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
        }

        else if (0.8f <= solarMass && solarMass < 1.04)
        {
            classLetter = "G";
            sR.color = Color.yellow;
        }

        else if (1.04f <= solarMass && solarMass < 1.4f)
        {
            classLetter = "F";
            sR.color = Color.white;
        }

        else if (1.4f <= solarMass && solarMass < 2.1f)
        {
            classLetter = "A";
            sR.color = Color.Lerp(Color.white, Color.blue, 0.25f);
        }

        else if (2.1f <= solarMass && solarMass < 16f)
        {
            classLetter = "B";
            sR.color = Color.Lerp(Color.white, Color.blue, 0.75f);
        }

        else if (16f <= solarMass)
        {
            classLetter = "O";
            sR.color = Color.blue;
        }
    }

}

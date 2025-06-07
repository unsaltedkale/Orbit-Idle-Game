using UnityEngine;
using System.Collections;

public class Star_Behavior : MonoBehaviour
{
    public GameManager gm;
    public SpriteRenderer sR;
    public float solarMass;
    public float solarRadius;
    public string classLetter;
    public float solarMassMin = 0.08f;
    public float solarMassMax = 16f;
    public float solarRadiusMin = 0.7f;
    public float solarRadiusMax = 6.6f;
    public GameManager.starState currentState;
    public float lerpVal;
    public Vector3 oldSize;
    public Vector3 targetSize;
    public Color oldColor;
    public Color targetColor;
    public bool firstTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();

        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameManager.starState.protostar)
        {
            ProtostarUpdate();
        }

        else if (currentState == GameManager.starState.mainSequence)
        {
            MainSequenceUpdate();
        }

        else if (currentState == GameManager.starState.giant)
        {
            GiantUpdate();
        }

        else if (currentState == GameManager.starState.whiteDwarf)
        {
            WhiteDwarfUpdate();
        }

        else if (currentState == GameManager.starState.neutronStar)
        {
            NeutronStarUpdate();
        }
        
        else if (currentState == GameManager.starState.blackHole)
        {
            BlackHoleUpdate();
        }
    }

    public void ProtostarUpdate()
    {
        if (firstTime)
        {
            lerpVal = 0;

            firstTime = false;
        }

        int tempValue = new Mathf.Lerp(oldSize.x, targetSize.x, lerpVal);
        transform.localScale = new Vector3(tempValue, tempValue, 0f);

        sR.color = Color.Lerp(oldColor, targetColor, lerpVal);

    }

    public void MainSequenceUpdate()
    {
        
    }

    public void GiantUpdate()
    {
        
    }

    public void WhiteDwarfUpdate()
    {
        
    }

    public void NeutronStarUpdate()
    {
        
    }

    public void BlackHoleUpdate()
    {
        
    }

    void CalculateStar()
    {
        solarMass = Mathf.Clamp(solarMass, 0.08f, 20f);

        //solarRadius = ((solarRadiusMax - solarRadiusMin) / (solarMassMax - solarMassMin)) * (solarMass - solarMassMin) + solarRadiusMin;

        solarRadius = Mathf.Pow(solarMass, 0.78f) + 0.25f;

        transform.localScale = new Vector3(solarRadius, solarRadius, solarRadius);

        //add later: lerp the colors of star so it is smooth instead of one or the other.
        
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

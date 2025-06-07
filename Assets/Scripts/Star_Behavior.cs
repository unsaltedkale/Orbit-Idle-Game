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
    public float timeOfEachStageSeconds;
    public float progressOfTheStageSeconds;
    public Color redGiant = new Color32(204, 65, 0, 255);
    public Color blueGiant = new Color32(76, 188, 255, 255);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeOfEachStageSeconds = 6;

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
            progressOfTheStageSeconds = 0;

            progressOfTheStageSeconds = 0;

            oldSize = transform.localScale;
            float f = CalculateSolarRadiusOfMainSequence();
            targetSize = new Vector3(f, f, f);
            oldColor = sR.color;
            targetColor = CalculateColorOfMainSequence();

            firstTime = false;
        }

        progressOfTheStageSeconds += Time.deltaTime;

        lerpVal = progressOfTheStageSeconds / timeOfEachStageSeconds;

        transform.localScale = Vector3.Lerp(oldSize, targetSize, lerpVal);

        sR.color = Color.Lerp(oldColor, targetColor, lerpVal);

        if (progressOfTheStageSeconds >= timeOfEachStageSeconds)
        {
            print("Protostar phase ended");

            currentState = GameManager.starState.mainSequence;
            firstTime = true;
            
        }

    }

    public void MainSequenceUpdate()
    {
        if (firstTime)
        {
            
            lerpVal = 0;
            progressOfTheStageSeconds = 0;

            oldSize = transform.localScale;
            float f = CalculateSolarRadiusOfGiant();
            targetSize = new Vector3(f, f, f);

            oldColor = sR.color;
            targetColor = CalculateColorOfGiant();

            firstTime = false;
        }

        progressOfTheStageSeconds += Time.deltaTime;

        lerpVal = progressOfTheStageSeconds / timeOfEachStageSeconds;

        transform.localScale = Vector3.Lerp(oldSize, targetSize, lerpVal);

        sR.color = Color.Lerp(oldColor, targetColor, lerpVal);

        if (progressOfTheStageSeconds >= timeOfEachStageSeconds)
        {
            print("Main Sequence phase ended");

            currentState = GameManager.starState.giant;
            firstTime = true;
            
        }


        
    }

    public void GiantUpdate()
    {
        if (firstTime)
        {
            
            lerpVal = 0;
            progressOfTheStageSeconds = 0;

            oldSize = transform.localScale;
            float f = oldSize.x + 0.5f;
            targetSize = new Vector3(f, f, f);

            oldColor = sR.color;
            targetColor = Color.Lerp(sR.color, Color.black, 0.3f);

            firstTime = false;
        }

        progressOfTheStageSeconds += Time.deltaTime;

        lerpVal = progressOfTheStageSeconds / timeOfEachStageSeconds;

        transform.localScale = Vector3.Lerp(oldSize, targetSize, lerpVal);

        sR.color = Color.Lerp(oldColor, targetColor, lerpVal);

        if (progressOfTheStageSeconds >= timeOfEachStageSeconds)
        {
            print("Giant phase ended");

            currentState = DetermineEndOfLifeStarState();
            firstTime = true;
            
        }

    }

    public void WhiteDwarfUpdate()
    {
        if (firstTime)
        {

            lerpVal = 0;
            progressOfTheStageSeconds = 0;

            oldSize = transform.localScale;
            float f = 0.2f;
            targetSize = new Vector3(f, f, f);

            oldColor = sR.color;
            targetColor = Color.Lerp(Color.white, Color.grey, 0.1f);

            firstTime = false;

            gm.SuperNovaExplosion();
        }

        progressOfTheStageSeconds += Time.deltaTime;

        lerpVal = progressOfTheStageSeconds / 3f;

        transform.localScale = Vector3.Lerp(oldSize, targetSize, lerpVal);

        sR.color = Color.Lerp(oldColor, targetColor, lerpVal);

        if (progressOfTheStageSeconds >= timeOfEachStageSeconds)
        {
            print("Waiting For Recycle after WhiteDwarf");

            currentState = GameManager.starState.waitingForRecycle;
            firstTime = true;
            
        }

    }

    public void NeutronStarUpdate()
    {
        if (firstTime)
        {
            lerpVal = 0;
            progressOfTheStageSeconds = 0;

            oldSize = transform.localScale;
            float f = 0.1f;
            targetSize = new Vector3(f, f, f);

            oldColor = sR.color;
            targetColor = Color.Lerp(Color.white, Color.blue, 0.1f);

            firstTime = false;

            gm.SuperNovaExplosion();
        }

        progressOfTheStageSeconds += Time.deltaTime;

        lerpVal = progressOfTheStageSeconds / 3f;

        transform.localScale = Vector3.Lerp(oldSize, targetSize, lerpVal);

        sR.color = Color.Lerp(oldColor, targetColor, lerpVal);

        if (progressOfTheStageSeconds >= timeOfEachStageSeconds)
        {
            print("Waiting For Recycle after Neutron Star");

            currentState = GameManager.starState.waitingForRecycle;
            firstTime = true;
            
        }
        
    }

    public void BlackHoleUpdate()
    {
        if (firstTime)
        {
            lerpVal = 0;
            progressOfTheStageSeconds = 0;

            oldSize = transform.localScale;
            float f = 0.2f;
            targetSize = new Vector3(f, f, f);

            oldColor = sR.color;
            targetColor = Color.black;

            firstTime = false;

            gm.SuperNovaExplosion();
        }

        progressOfTheStageSeconds += Time.deltaTime;

        lerpVal = progressOfTheStageSeconds / 3f;

        transform.localScale = Vector3.Lerp(oldSize, targetSize, lerpVal);

        sR.color = Color.Lerp(oldColor, targetColor, lerpVal);

        if (progressOfTheStageSeconds >= timeOfEachStageSeconds)
        {
            print("Waiting For Recycle after BlackHole");

            currentState = GameManager.starState.waitingForRecycle;
            firstTime = true;
            
        }
        
    }

    public UnityEngine.Color CalculateColorOfMainSequence()
    {
        if (0.08f <= solarMass && solarMass < 0.45f)
        {
            classLetter = "M";
            return Color.red;
        }

        else if (0.45f <= solarMass && solarMass < 0.8f)
        {
            classLetter = "K";
            return Color.Lerp(Color.red, Color.yellow, 0.5f);
        }

        else if (0.8f <= solarMass && solarMass < 1.04)
        {
            classLetter = "G";
            return Color.yellow;
        }

        else if (1.04f <= solarMass && solarMass < 1.4f)
        {
            classLetter = "F";
            return Color.white;
        }

        else if (1.4f <= solarMass && solarMass < 2.1f)
        {
            classLetter = "A";
            return Color.Lerp(Color.white, Color.blue, 0.25f);
        }

        else if (2.1f <= solarMass && solarMass < 16f)
        {
            classLetter = "B";
            return Color.Lerp(Color.white, Color.blue, 0.75f);
        }

        else if (16f <= solarMass)
        {
            classLetter = "O";
            return Color.blue;
        }

        return new Color(0,0,0,1);
    }

    public UnityEngine.Color CalculateColorOfGiant()
    {
        if (solarMass <= 8f)
        {
            return redGiant;
        }
        

        else if (8f < solarMass)
        {
            return blueGiant;
        }

        return new Color(0,0,0,1);
    }

    public float CalculateSolarRadiusOfMainSequence()
    {
        return Mathf.Pow(solarMass, 0.78f) + 0.25f;
    }

    public float CalculateSolarRadiusOfGiant()
    {
        return (Mathf.Pow(solarMass, 0.78f) + 0.25f)*3f;
    }

    public GameManager.starState DetermineEndOfLifeStarState()
    {
        if (solarMass <= 8f)
        {
            return GameManager.starState.whiteDwarf;
        }


        else if (8f < solarMass && solarMass < 16f)
        {
            return GameManager.starState.neutronStar;
        }

        else if (16f < solarMass)
        {
            return GameManager.starState.blackHole;
        }

        else
        {
            print("Error calculating end of star life. Returning White Dwarf Automatically.");

            return GameManager.starState.whiteDwarf;
        }

    }
}

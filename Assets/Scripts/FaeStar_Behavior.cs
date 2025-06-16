using UnityEngine;

public class FaeStar_Behavior : MonoBehaviour
{
    public Sprite faeSprite1;
    public Sprite faeSprite2;
    public Sprite faeSprite3;
    public SpriteRenderer sR;
    public enum faeState
    {
        wait,
        shine,
        decay
    }
    public faeState currentFaeState;
    public bool firstTime;
    public float totalSecondsUntil;
    public float secondsUntil;
    public Color32 whiteTransparent = new Color32(255,255,255,0);
    public Color32 targetColor;
    public float brightness;
    //(54,31)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentFaeState = faeState.wait;
        firstTime = true;

        sR.color = whiteTransparent;

        brightness = 25;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFaeState == faeState.wait)
        {
            Wait();
        }

        else if (currentFaeState == faeState.shine)
        {
            Shine();
        }

        else if (currentFaeState == faeState.decay)
        {
            Decay();
        }

    }

    public void Wait()
    {
        if (firstTime)
        {
            totalSecondsUntil = Random.Range(3f, 7f);
            secondsUntil = 0;

            firstTime = false;
        }

        secondsUntil += Time.deltaTime;

        if (secondsUntil >= totalSecondsUntil)
        {
            currentFaeState = faeState.shine;
            firstTime = true;
        }

    }

    public Vector3 ChoosePlace()
    {
        //float a = Random.Range(-54f, 54f);

        //float b = Random.Range(-31f, 31f);

        float w = Camera.main.scaledPixelWidth;

        float h = Camera.main.scaledPixelHeight;

        float a = Random.Range(Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x, Camera.main.ScreenToWorldPoint(new Vector3(w,h,0)).x);

        float b = Random.Range(Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).y, Camera.main.ScreenToWorldPoint(new Vector3(w,h,0)).y);

        return new Vector3(a, b, 0);
        
    }

    public void Shine()
    {
        if (firstTime)
        {
            totalSecondsUntil = Random.Range(3f, 5f);
            secondsUntil = 0;

            transform.position = ChoosePlace();

            byte f = (byte)Random.Range(0, brightness);

            sR.color = whiteTransparent;

            targetColor = new Color32(255, 255, 255, f);

            firstTime = false;
        }

        secondsUntil += Time.deltaTime;

        sR.color = Color.Lerp(whiteTransparent, targetColor, (secondsUntil / totalSecondsUntil));

        if (secondsUntil >= totalSecondsUntil)
        {
            currentFaeState = faeState.decay;
            firstTime = true;
        }

    }

    public void Decay()
    {
        if (firstTime)
        {
            secondsUntil = 0;

            firstTime = false;
        }

        secondsUntil += Time.deltaTime;

        sR.color = Color.Lerp(targetColor, whiteTransparent, (secondsUntil / totalSecondsUntil));

        if (secondsUntil >= totalSecondsUntil)
        {
            currentFaeState = faeState.wait;
            firstTime = true;
        }

    }
}

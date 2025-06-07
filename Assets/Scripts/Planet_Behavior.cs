using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Planet_Behavior : MonoBehaviour
{
    public GameManager gm;
    public GameObject star;
    public float orbitSpeed;
    public float rotationSpeed;
    public Vector3 mousePos;
    public Vector3 h; //firstMousePos
    public Vector3 j; //secondMousePos
    public float r; 
    public float o; //clickDistance
    public float d; //radiusVar
    public float a; //horizontalstretch
    public float b; //vertical stretch
    public float theta; //c
    public Vector3 tangentIntersect;
    public float v1;
    public float v2;
    public LineRenderer lR;
    public TrailRenderer tR;
    public bool startingPlanet;
    public Vector3 vectorHold;
    public bool hasHitIntersect;
    public float solarMass;
    public int key;
    public TextMeshProUGUI namePlate;
    public Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();

        lR.enabled = false;

        tR.enabled = false;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        Color c = gm.requestColor();

        Color t = c - new Color(0, 0, 0, 1);

        spriteRenderer.color = c;

        lR.startColor = t;
        lR.endColor = c;

        tR.startColor = c;
        tR.endColor = t;

        orbitSpeed = 1;

        hasHitIntersect = false;

        StartCoroutine(startPlanet());

        namePlate.text = "Planet " + key.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 roll = Camera.main.WorldToScreenPoint(transform.position + offset);
        roll.z = 0f;
        namePlate.transform.position = roll;

        if (startingPlanet == false && Input.GetKeyDown("" + key.ToString()))
        {
            startingPlanet = true;
            hasHitIntersect = false;
            lR.enabled = false;
            tR.enabled = false;
            StartCoroutine(startPlanet());
        }

        Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        temp.z = 0f;
        mousePos = temp;


        if (lR.enabled == true)
        {
            lR.SetPosition(1, new Vector3(mousePos.x, h.y, 0));
        }

        if (tR.enabled == true && hasHitIntersect == false)
        {
            if (h.x < j.x)
            {
                transform.position = new Vector3(transform.position.x + (orbitSpeed * Time.deltaTime), transform.position.y, transform.position.z);

                if (transform.position.x >= 0)
                {
                    hasHitIntersect = true;
                }
            }

            else if (h.x > j.x)
            {
                transform.position = new Vector3(transform.position.x - (orbitSpeed * Time.deltaTime), transform.position.y, transform.position.z);

                if (transform.position.x <= 0)
                {
                    hasHitIntersect = true;
                }
            }

            if (Mathf.Abs(transform.position.x) < 0.02f && hasHitIntersect == false)
            {
                hasHitIntersect = true;
            }

            if (hasHitIntersect == true)
            {
                print((Mathf.Abs(4 / (Mathf.Abs(r) - 1))));

                orbitSpeed = (Mathf.Abs(1 / (Mathf.Abs(r) - 1))) + ((orbitSpeed) / (Mathf.PI * ((3 * (a + b)) - Mathf.Sqrt(((3 * a) + b) * (a + (3 * b))))));
                float tempR = Mathf.Abs(r)/6;
                if (tempR < 1)
                {
                    tempR = 1;
                }
                //orbitSpeed = Mathf.Clamp(orbitSpeed, 0, 0.8f/Mathf.Abs(r));
            }
        }


        if (tR.enabled == true && hasHitIntersect == true)
            {
                if (h.x < j.x)
                {
                    theta += (h.y / Mathf.Abs(h.y)) * (orbitSpeed * Time.deltaTime);
                }

                else if (h.x > j.x)
                {
                    theta -= (h.y / Mathf.Abs(h.y)) * (orbitSpeed * Time.deltaTime);   
                }

                theta = theta % (2 * Mathf.PI);

                v1 = d * a * Mathf.Sin(theta);

                v2 = d * b * Mathf.Cos(theta);

                transform.position = new Vector3(v1, v2, 0f);
            }

    }

    public IEnumerator startPlanet()
    {
        yield return  null;

        solarMass = GameObject.Find("Star").GetComponent<Star_Behavior>().solarMass;

        startingPlanet = true;
        //first click

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) == true);

        h = mousePos;

        transform.position = h;

        yield return null;

        //second click for velocity

        lR.enabled = true;

        lR.positionCount = 2;
        lR.SetPosition(0, transform.position);

        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Mouse0) == true);

        j = mousePos;

        lR.enabled = false;

        //calculate orbit

        yield return StartCoroutine(calculateOrbit());

        yield break;
    }

    public IEnumerator calculateOrbit()
    {
        j = new Vector3 (j.x, 0f, 0f);

        r = Mathf.Sqrt((Mathf.Pow(h.x, 2)) + (Mathf.Pow(h.y, 2)));

        d = h.y;

        o = Mathf.Sqrt((Mathf.Pow(h.x - j.x, 2)) + (Mathf.Pow(h.y - j.y, 2)));

        a = Mathf.Abs(o * (1/(d * d)));

        if (a < 1)
        {
            a = 1/a;
        }

        b = 1;

        tangentIntersect = new Vector3(h.y, 0f, 0f);

        theta = 0;

        if (h.y < 0)
        {
            theta = 2 * Mathf.PI;
        }

        v1 = d * a * Mathf.Sin(theta);

        v2 = d * b * Mathf.Cos(theta);

        orbitSpeed = Mathf.Pow(o,3/2);

        tR.enabled = true;
        startingPlanet = false;

        yield break;
    }
    
}

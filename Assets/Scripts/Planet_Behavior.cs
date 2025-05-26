using UnityEngine;
using System.Collections;

public class Planet_Behavior : MonoBehaviour
{

    public GameObject star;
    public float orbitSpeed;
    public float rotationSpeed;
    public Vector3 mousePos;
    public Vector3 h; //firstMousePos
    public Vector3 j; //secondMousePos
    public float o; //clickDistance
    public float d; //radiusVar
    public float l1; //xintercept
    public float z1; //yintercept
    public float a; //horizontalstretch
    public float b; //vertical stretch
    public float g1; //finalDilation
    public float theta; //c
    public Vector3 tangentIntersect;
    public float v1;
    public float v2;
    public float v3;
    public float v4;
    public float v5;
    public float v6;
    public LineRenderer lR;
    public TrailRenderer tR;
    public bool startingPlanet;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lR = gameObject.GetComponent<LineRenderer>();

        lR.enabled = false;

        tR = gameObject.GetComponent<TrailRenderer>();

        tR.enabled = false;

        orbitSpeed = 1;

        StartCoroutine(startPlanet());
    }

    // Update is called once per frame
    void Update()
    {
        if (startingPlanet == false && Input.GetKeyDown(KeyCode.R))
        {
            startingPlanet = true;
            tR.enabled = false;
            StartCoroutine(startPlanet());
        }

        mousePos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        
        if (lR.enabled == true)
        {
            lR.SetPosition(1, mousePos / 152);
        }

        if (tR.enabled == true)
        {
            v1 = d * a * Mathf.Sin(theta);

            v2 = d * b * Mathf.Cos(theta);

            v3 = ((l1 * Mathf.Sin(theta)) / g1);

            v4 = ((d / g1) * (o / l1) * Mathf.Cos(theta));

            v5 = (v1 + v3) / 2;

            v6 = (v2 + v4) / 2;

            theta += (orbitSpeed * Time.deltaTime);

            theta = theta % (2 * Mathf.PI);

            transform.position = new Vector3(v5, v6, 0f);
        }
        
    }

    public IEnumerator startPlanet()
    {
        startingPlanet = true;
        //first click

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) == true);

        h = mousePos / 152;

        transform.position = h;

        yield return null;

        //second click for velocity

        lR.enabled = true;

        lR.positionCount = 2;
        lR.SetPosition(0, transform.position);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) == true);

        j = mousePos / 152;

        lR.enabled = false;

        //calculate orbit

        yield return StartCoroutine(calculateOrbit());

        //another coroutine to send it on a line and then transition to orbit*/
        yield break;
    }

    public IEnumerator calculateOrbit()
    {
        d = Mathf.Sqrt((Mathf.Pow(h.x, 2)) + (Mathf.Pow(h.y, 2)));

        o = Mathf.Sqrt((Mathf.Pow(h.x - j.x, 2)) + (Mathf.Pow(h.y - j.y, 2)));

        l1 = ((-j.y) * ((j.x - h.x) / (j.y - h.y))) + j.x;

        a = o / l1;

        b = l1 / d;

        g1 = 1;


        theta = Vector2.Angle(Vector2.right, transform.position);

        v1 = d * a * Mathf.Sin(theta);

        v2 = d * b * Mathf.Cos(theta);

        v3 = ((l1 * Mathf.Sin(theta)) / g1);

        v4 = ((d / g1) * (o / l1) * Mathf.Cos(theta));

        v5 = (v1 + v3) / 2;

        v6 = (v2 + v4) / 2;

        transform.position = new Vector3 (v5, v6, 0);

        tR.enabled = true;
        startingPlanet = false; 


        //go along line
        //yield return new WaitUntil(() => distance between is less than 0.01f);

        //go along curve starting from intersection point

        yield break;
    }
    
}

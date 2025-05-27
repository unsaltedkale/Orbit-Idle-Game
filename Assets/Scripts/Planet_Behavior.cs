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
    public float r; 
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
    public Vector3 vectorHold;
    public bool hasHitIntersect;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lR = gameObject.GetComponent<LineRenderer>();

        lR.enabled = false;

        tR = gameObject.GetComponent<TrailRenderer>();

        tR.enabled = false;

        orbitSpeed = 1;

        hasHitIntersect = false;

        StartCoroutine(startPlanet());
    }

    // Update is called once per frame
    void Update()
    {
        if (startingPlanet == false && Input.GetKeyDown(KeyCode.R))
        {
            startingPlanet = true;
            hasHitIntersect = false;
            lR.enabled = false;
            tR.enabled = false;
            StartCoroutine(startPlanet());
        }

        mousePos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);

        if (lR.enabled == true)
        {
            lR.SetPosition(1, new Vector3 (mousePos.x / 152, h.y, 0));
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
                 
            
                orbitSpeed = (Mathf.Abs(1/(Mathf.Abs(r)-1))) + ((orbitSpeed) / (Mathf.PI * ((3 * (a + b)) - Mathf.Sqrt(((3 * a) + b) * (a + (3 * b))))));
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

                v3 = ((l1 * Mathf.Sin(theta)) / g1);

                v4 = ((d / g1) * (o / l1) * Mathf.Cos(theta));

                v5 = (v1 + v3) / 2;

                v6 = (v2 + v4) / 2;

                transform.position = new Vector3(v1, v2, 0f);
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
        j = new Vector3 (j.x, 0f, 0f);

        r = Mathf.Sqrt((Mathf.Pow(h.x, 2)) + (Mathf.Pow(h.y, 2)));

        d = h.y;

        o = Mathf.Sqrt((Mathf.Pow(h.x - j.x, 2)) + (Mathf.Pow(h.y - j.y, 2)));

        l1 = ((-j.y) * ((j.x - h.x) / (j.y - h.y))) + j.x;

        //a = o / l1;

        a = Mathf.Abs(o * (1/(d * d)));

        if (a < 1)
        {
            a = 1/a;
        }

        //b = l1 / d;

            b = 1;

        g1 = 1;

        tangentIntersect = new Vector3(h.y, 0f, 0f);

        theta = 0;

        if (h.y < 0)
        {
            theta = 2 * Mathf.PI;
        }

        v1 = d * a * Mathf.Sin(theta);

        v2 = d * b * Mathf.Cos(theta);

        v3 = ((l1 * Mathf.Sin(theta)) / g1);

        v4 = ((d / g1) * (o / l1) * Mathf.Cos(theta));

        v5 = (v1 + v3) / 2;

        v6 = (v2 + v4) / 2;

        orbitSpeed = o;

        tR.enabled = true;
        startingPlanet = false;


        //go along line
        //yield return new WaitUntil(() => distance between is less than 0.01f);

        //go along curve starting from intersection point

        yield break;
    }

    public IEnumerator ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
    {
        //distance between point and start point
        Vector3 vVector1 = vPoint - vA;

        Vector3 vVector2 = (vB - vA).normalized;

        float d = Vector3.Distance(vA, vB);
        float t = Vector3.Dot(vVector2, vVector1);

        if (t <= 0)
        {
            print("yielded vA");
            vectorHold = vA;
            yield return vA;
            yield break;
        }


        if (t >= d)
        {
            print("yielded vB");
            vectorHold = vB;
            yield return vB;
            yield break;
        }

        Vector3 vVector3 = vVector2 * t;

        Vector3 vClosestPoint = vA + vVector3;

        vectorHold = vClosestPoint;

        yield return vClosestPoint;

        yield break;
    }
    
}

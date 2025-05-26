using UnityEngine;
using System.Collections;

public class Planet_Behavior : MonoBehaviour
{

    public GameObject star;
    public float orbitSpeed;
    public float rotationSpeed;
    public Vector3 mousePos;
    public Vector3 firstMousePos; //h
    public Vector3 secondMousePos; //j
    public float clickDistance; //o
    public float radiusVar; //r or d
    public float xIntercept; //l1
    public float yIntercept; //z1
    public float stretchHorizontal; //a
    public float stretchVertical; //b
    public float finalDilation; //g1
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
        if (lR.enabled == true)
        {
            lR.SetPosition(1, mousePos / 152);
        }

        mousePos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);

        /*theta += (orbitSpeed * Time.deltaTime);

        theta = theta % (2 * Mathf.PI);

        v1 = radiusVar * stretchVertical * Mathf.Sin(theta);
        v2 = radiusVar * stretchHorizontal * Mathf.Cos(theta);

        transform.position = new Vector3(v1, v2, 0f);*/
    }

    public IEnumerator startPlanet()
    {
        //first click

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) == true);

        firstMousePos = mousePos / 152;

        transform.position = firstMousePos;

        yield return null;

        //second click for velocity

        lR.enabled = true;

        lR.positionCount = 2;
        lR.SetPosition(0, transform.position);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) == true);

        secondMousePos = mousePos / 152;

        lR.enabled = false;

        //calculate orbit

        yield return StartCoroutine(calculateOrbit());

        //another coroutine to send it on a line and then transition to orbit*/
        yield break;
    }

    public IEnumerator calculateOrbit()
    {
        radiusVar = Mathf.Sqrt((Mathf.Pow(firstMousePos.x, 2)) + (Mathf.Pow(firstMousePos.y, 2)));

        clickDistance = Mathf.Sqrt((Mathf.Pow(firstMousePos.x - secondMousePos.x, 2)) + (Mathf.Pow(firstMousePos.y - secondMousePos.y, 2)));

        


        //go along line
        //yield return new WaitUntil(() => distance between is less than 0.01f);

        //go along curve starting from intersection point

        yield break;
    }
    
}

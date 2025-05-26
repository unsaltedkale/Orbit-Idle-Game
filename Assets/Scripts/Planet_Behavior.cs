using UnityEngine;
using System.Collections;

public class Planet_Behavior : MonoBehaviour
{

    public GameObject star;
    public float orbitSpeed;
    public float rotationSpeed;
    public Vector3 mousePos;
    public Vector3 mousePosHold;
    public bool clicked;
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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        orbitSpeed = 1;

        StartCoroutine(startPlanet());
    }

    // Update is called once per frame
    void Update()
    {
        
        mousePos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print(mousePos);
        }

        theta += (orbitSpeed * Time.deltaTime);

        theta = theta % (2 * Mathf.PI);

        v1 = radiusVar * stretchVertical * Mathf.Sin(theta);
        v2 = radiusVar * stretchHorizontal * Mathf.Cos(theta);

        transform.position = new Vector3(v1, v2, 0f);
    }

    public IEnumerator startPlanet()
    {
        //first click

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) == true);

        mousePosHold = firstMousePos;

        transform.position = firstMousePos;

        /*mousePosHold = new Vector3(0, 0, 0);
        clicked = false;

        //second click for velocity

        StartCoroutine(drawVelocityRay());

        yield return StartCoroutine(waitforMouseClick());

        mousePosHold = secondMousePos;

        mousePosHold = new Vector3(0, 0, 0);
        clicked = false;

        //calculate orbit

        yield return StartCoroutine(calculateOrbit());

        //another coroutine to send it on a line and then transition to orbit*/
        yield break;
    }

    public IEnumerator waitforMouseClick()
    {
        while (clicked == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                clicked = true;
                mousePos = mousePosHold;
                yield break;
            }
        }
    }

    public IEnumerator drawVelocityRay()
    {
        while (clicked == false)
        {
            Debug.DrawLine(firstMousePos, mousePos, Color.red);

            if (clicked == true)
            {
                yield break;
            }
        }


    }

    public IEnumerator calculateOrbit()
    {
        radiusVar = Mathf.Sqrt((Mathf.Pow(firstMousePos.x, 2)) + (Mathf.Pow(firstMousePos.y, 2)));

        clickDistance = Mathf.Sqrt((Mathf.Pow(firstMousePos.x - secondMousePos.x, 2)) + (Mathf.Pow(firstMousePos.y - secondMousePos.y, 2)));

        yield break;
    }
}

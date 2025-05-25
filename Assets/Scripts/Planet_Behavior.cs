using UnityEngine;
using System.Collections;

public class Planet_Behavior : MonoBehaviour
{

    public GameObject star;
    public Vector3 mousePos;
    public Vector3 mousePosHold;
    public bool clicked;
    public Vector3 firstMousePos; //h
    public Vector3 secondMousePos; //j
    public Vector3 clickDistance; //o
    public Vector3 radiusVar; //r or d
    public Vector3 xIntercept; //l
    public Vector3 yIntercept; //z
    public float stretchHorizontal; //a
    public float stretchVertical; //b
    public float finalDilation; //g1
    public float theta; //c



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(startPlanet());
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    public IEnumerator startPlanet()
    {
        //first click

        yield return StartCoroutine(waitforMouseClick());

        mousePosHold = firstMousePos;

        mousePosHold = new Vector3(0, 0, 0);
        clicked = false;

        //second click for velocity

        StartCoroutine(drawVelocityRay());

        yield return StartCoroutine(waitforMouseClick());

        mousePosHold = secondMousePos;

        mousePosHold = new Vector3(0, 0, 0);
        clicked = false;

        //calculate orbit

        yield return StartCoroutine(calculateOrbit());

        //another coroutine to send it on a line and then transition to orbit
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
            Debug.DrawRay(firstMousePos, mousePos, Color.red);

            if (clicked == true)
            {
                yield break;
            }
        }


    }

    public IEnumerator calculateOrbit()
    {


        yield break;
    }
}

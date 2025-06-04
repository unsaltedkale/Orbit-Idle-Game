using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Color> colorList;
    public List<Color> colorAvailableList;
    public List<GameObject> planetNumbersAvailable;
    public GameObject planet;
    public bool abort;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    // Update is called once per frame
        void Update()
    {

        if (Input.anyKeyDown)
        {
            int k = 0;

            for (int i = 1; i < 9; i++)
            {
                if (Input.GetKeyDown("" + i.ToString()))
                {
                    k = i;
                    break;
                }
            }

            abort = false;

            foreach (GameObject b in planetNumbersAvailable)
            {
                if (b != null)
                {
                    if (b.GetComponent<Planet_Behavior>().a == 0)
                    {
                        abort = true;
                        break;
                    }
                }
            }

            if (k != 0 && !abort)
            {
                if (planetNumbersAvailable[k - 1] == null)
                {
                    GameObject p = Instantiate(planet);

                    Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
                    temp.z = 0f;
                    Vector3 mP = temp;

                    p.transform.position = mP;

                    planetNumbersAvailable[k - 1] = p;

                    p.GetComponent<Planet_Behavior>().key = k;
                }
            }
        }
    }

    public Color requestColor()
    {
        if (colorAvailableList.Count == 0)
        {
            foreach (Color c in colorList)
            {
                colorAvailableList.Add(c);
            }
        }

        int i = Random.Range(0, colorAvailableList.Count);

        Color d = colorAvailableList[i];

        colorAvailableList.RemoveAt(i);

        return d;
    }
}

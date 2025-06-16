using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<PlanetData> planetDataList;
    public List<GameObject> planetNumbersAvailable;
    public List<Color> colorProtoStars;
    public GameObject planet;
    public GameObject starPrefab;
    public GameObject star;
    public GameObject superNova;
    public bool abort;
    public float baseStarMass;
    public enum starState
    {
        protostar,
        mainSequence,
        giant,
        whiteDwarf,
        neutronStar,
        blackHole,
        waitingForRecycle,
        recycle
    }
    public starState currentState;
    public bool enabledStart;
    public Camera_Behavior camera_Behavior;
    public AudioPlayer_Behavior audioPlayer_Behavior;
    public bool isUIOpen;
    public TextMeshProUGUI uiHelpTipText;

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

        baseStarMass = 0.08f;

        currentState = starState.protostar;

        isUIOpen = true;

        StartCoroutine(StartStar());

    }

    public IEnumerator StartStar()
    {
        float newStarMass = Mathf.Clamp(baseStarMass + Random.Range(-0.3f, 0.3f), 0.08f, 30f);

        Color newProtoStarColor = colorProtoStars[Random.Range(0, colorProtoStars.Count)];

        star = Instantiate(starPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        yield return null;

        Star_Behavior starScript = star.GetComponent<Star_Behavior>();

        star.SetActive(false);

        starScript.firstTime = true;

        starScript.currentState = starState.protostar;

        starScript.sR.color = newProtoStarColor;
        starScript.solarMass = newStarMass;
        star.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        star.SetActive(true);

        yield break;
    }

    public IEnumerator RecycleStarProcess()
    {
        print("Waiting for spacebar");

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        foreach (GameObject p in planetNumbersAvailable)
        {
            if (p != null)
            {
                Planet_Behavior pS = p.GetComponent<Planet_Behavior>();

                pS.decayOrbit = true;
            }
        }
            

        yield return new WaitUntil(() => GameObject.FindWithTag("Planet") == false);

        Destroy(star);

        baseStarMass += 0.5f;

        SuperNovaExplosion(true);

        StartCoroutine(StartStar());

        audioPlayer_Behavior.restartMusic();

        yield break;

    }

    public void addToStarRadius(float i)
    {
        star.transform.localScale += new Vector3(i, i, i);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            enabledStart = !enabledStart;

            foreach (GameObject b in planetNumbersAvailable)
            {
                if (b != null)
                {
                    b.GetComponent<Planet_Behavior>().namePlate.enabled = enabledStart;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            isUIOpen = !isUIOpen;

            camera_Behavior.hide(isUIOpen);

            audioPlayer_Behavior.hide(isUIOpen);

            uiHelpTipText.enabled = isUIOpen;
            
        }

        if (Input.anyKeyDown)
            {
                int k = 0;

                for (int i = 1; i < 10; i++)
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

                        p.GetComponent<Planet_Behavior>().key = k;

                        Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
                        temp.z = 0f;
                        Vector3 mP = temp;

                        p.transform.position = mP;

                        planetNumbersAvailable[k - 1] = p;

                        p.GetComponent<Planet_Behavior>().namePlate.enabled = enabledStart;
                    }
                }
            }
    }

    public Color requestColor(int i)
    {
        return planetDataList[i - 1].color;
        /*if (colorAvailableList.Count == 0)
        {
            foreach (Color c in colorList)
            {
                colorAvailableList.Add(c);
            }
        }

        int i = Random.Range(0, colorAvailableList.Count);

        Color d = colorAvailableList[i];

        colorAvailableList.RemoveAt(i);*/
    }
    public Sprite requestSprite(int i)
    {
        return planetDataList[i - 1].sprite;
    }

    public void SuperNovaExplosion(bool b)
    {
        GameObject s = Instantiate(superNova);
        s.transform.position = new Vector3(0, 0, 0);

        if (b)
        {
            s.GetComponent<SuperNova_Behavior>().color1 = new Color32(253, 255, 153, 255);
            s.GetComponent<SuperNova_Behavior>().color2 = new Color32(170, 104, 224, 0);
        }
    }
}

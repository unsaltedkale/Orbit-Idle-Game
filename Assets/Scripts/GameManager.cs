using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Color> colorList;
    public List<Color> colorAvailableList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(gm != null && gm !=this)
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

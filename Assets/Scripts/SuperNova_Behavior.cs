using UnityEngine;

public class SuperNova_Behavior : MonoBehaviour
{
    public SpriteRenderer sR;
    public float progress;
    public float lerpVal;
    public Vector3 oldSize;
    public Vector3 targetSize;
    public Color color1;
    public Color color2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float n = 0.05f;

        transform.localScale = new Vector3(n,n,n);

        oldSize = transform.localScale;

        float i = 30f;

        targetSize = new Vector3(i, i, i);

        sR.color = color1;
    }

    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime;

        lerpVal = progress / 10f;

        transform.localScale = Vector3.Lerp(oldSize, targetSize, lerpVal);

        sR.color = Color.Lerp(color1, color2, lerpVal);

        if (lerpVal >= 1f)
        {
            Destroy(gameObject);
        }

    }
}

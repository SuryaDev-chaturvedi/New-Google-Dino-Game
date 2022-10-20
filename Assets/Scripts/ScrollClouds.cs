using UnityEngine;

public class ScrollClouds : MonoBehaviour
{
    public Material material;
    public float xVel = 0.1f;
    Vector2 offset;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    void Update()
    {
        offset = new Vector2(xVel, 0);

        material.mainTextureOffset += offset * Time.deltaTime;
    }
}



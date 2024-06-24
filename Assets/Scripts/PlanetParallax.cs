using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetParallax : MonoBehaviour
{

    private Vector3 startPos;
    private Vector3 currentPos;
    public GameObject camera;
    public float parallaxEffect;

    public float offsetX = -20f;
    public float offsetY = 15f;

    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        // length = GetComponent<SpriteRenderer>().bounds.size.x;

        // mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update() {
        // currentPos = transform.position;
    }

    void FixedUpdate()
    {
        float tempX = (camera.transform.position.x * (1 - parallaxEffect));
        float tempY = (camera.transform.position.y * (1 - parallaxEffect));
        float distX = (camera.transform.position.x * parallaxEffect);
        float distY = (camera.transform.position.y * parallaxEffect);
        // The Y position below can be set to "DistY" to only apply only horizontal following, "camera.transform.position.y" for both.
        transform.position = new Vector3(tempX + offsetX + (distX * (parallaxEffect / 5)), tempY + offsetY + (distY * (parallaxEffect / 5)), 70f);

        // The Y position below can be set to "0" to only apply only horizontal parallax, "distY * (parallaxEffect / 5)" for both.
        // mat.SetTextureOffset("_MainTex", new Vector3(distX * (parallaxEffect / 5), distY * (parallaxEffect / 5), 0f));

        // if (temp > startpos + length) {
        //     startpos += length;
        //     Debug.Log("Right");
        // } else if (temp < startpos - length) {
        //      startpos -= length;
        //      Debug.Log("Left");
        // }
    }
}

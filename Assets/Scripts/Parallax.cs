using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private Vector3 startpos;
    [SerializeField] private GameObject targetCamera;
    public float parallaxEffect;

    private float offset;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        // length = GetComponent<SpriteRenderer>().bounds.size.x;

        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (targetCamera.transform.position.x * (1 - parallaxEffect));
        float distX = (targetCamera.transform.position.x * parallaxEffect);
        float distY = (targetCamera.transform.position.y * parallaxEffect);
        // The Y position below can be set to "DistY" to only apply only horizontal following, "targetCamera.transform.position.y" for both.
        transform.position = new Vector3(targetCamera.transform.position.x, targetCamera.transform.position.y, transform.position.z);

        // The Y position below can be set to "0" to only apply only horizontal parallax, "distY * (parallaxEffect / 5)" for both.
        mat.SetTextureOffset("_MainTex", new Vector2(distX * (parallaxEffect / 5), distY * (parallaxEffect / 5)));

        // if (temp > startpos + length) {
        //     startpos += length;
        //     Debug.Log("Right");
        // } else if (temp < startpos - length) {
        //      startpos -= length;
        //      Debug.Log("Left");
        // }
    }
}

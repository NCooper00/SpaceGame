using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWormBody1 : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 1.5f;

    private Transform nextSegment;

    // Start is called before the first frame update
    void Awake()
    {
        nextSegment = GameObject.Find("BottomConnection1").transform;
    }

    // Update is called once per frame
    void Update()
    {

        var direction = nextSegment.position - transform.position;

        transform.up = Vector3.MoveTowards(transform.up, direction, rotationSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, speed * Time.deltaTime);
    }
}

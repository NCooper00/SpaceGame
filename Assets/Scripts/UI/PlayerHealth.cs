using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Player player;

    // public GameObject Ui;

    public Text text;

    private int health;

    // Start is called before the first frame update
    void Start()
    {
        player = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        health = player.ReturnHealth();

        text.text = "Health: " + health.ToString();
    }
}

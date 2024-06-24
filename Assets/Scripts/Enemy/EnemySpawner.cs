using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // [SerializeField]
    private GameObject player;
    private Transform playerPos;

    [SerializeField]
    private GameObject steelStingrayPrefab;
    [SerializeField]
    private float steelStingrayInterval = 5f;

    void Awake() {
        player = GameObject.Find("PLAYER");
        playerPos = player.GetComponent<Transform>();
    }

    void Start()
    {
        StartCoroutine(spawnEnemy(steelStingrayInterval, steelStingrayPrefab));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-50f, 50f) + (playerPos.position.x + Random.Range(-50f, 50f)), Random.Range(-60f, 60f) + (playerPos.position.y + Random.Range(-50f, 50f)), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }

}

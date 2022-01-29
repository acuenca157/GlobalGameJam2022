using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController player;

    public GameObject candelabroPrefab;
    public int minLamps = 5;
    public int maxLamps = 9;

    private int numLamps;
    private int countLamps = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        LightGenerator lg = FindObjectOfType<LightGenerator>();
        Random.seed = ( (int) Mathf.Floor(System.DateTime.Now.Second) );
        Transform[] points = lg.getPoints(Random.Range(minLamps + 1, maxLamps));
        numLamps = points.Length;
        player.setCandels(points.Length + 1);
        foreach (Transform t in points) {
            Instantiate(candelabroPrefab, t.position, Quaternion.identity);
        }
    }

    public void addCandle() {
        player.addCandle();
    }

    public bool removeCandle() {
        return player.removeCandle();
    }

    public Transform getPlayerTransform() {
        return player.gameObject.transform;
    }

    public void addLamp() {
        countLamps++;
        if (countLamps == numLamps) {
            Debug.Log("WIN");
        }
    }
}

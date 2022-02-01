using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private PlayerController player;
    private PlayerMovement pMove;

    private SongsManager sm;
    private UIGameController uiGame;
    public GameObject candelabroPrefab;
    public int minLamps = 5;
    public int maxLamps = 9;

    private int numLamps;
    private int countLamps = 0;

    // Start is called before the first frame update
    void Start()
    {
        sm = FindObjectOfType<SongsManager>();
        sm.setPlay();
        uiGame = FindObjectOfType<UIGameController>();
        player = FindObjectOfType<PlayerController>();
        pMove = FindObjectOfType<PlayerMovement>();
        LightGenerator lg = FindObjectOfType<LightGenerator>();
        Random.seed = ( (int) Mathf.Floor(System.DateTime.Now.Second) );
        Transform[] points = lg.getPoints(Random.Range(minLamps + 1, maxLamps));
        numLamps = points.Length;
        player.setCandels(points.Length + 3);
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
            win();
        }
    }

    public void win() {
        Debug.Log("Has ganado pinche putito");
        player.dead = true;
        pMove.dead = true;
        sm.play.SetActive(false);
        sm.dead.SetActive(false);
        uiGame.win();
    }

    public void lose() {
        Debug.Log("Has muerto pinche putito");
        player.dead = true;
        pMove.dead = true;
        uiGame.loose();
        sm.setDead();
    }
}

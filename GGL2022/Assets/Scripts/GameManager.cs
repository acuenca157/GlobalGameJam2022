using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private PlayerController player;
    private PlayerMovement pMove;
    private IA ia;
    private LanternController lanternController;
    private RayoAlfonso rayoAlfonso;

    private SongsManager sm;
    private UIGameController uiGame;

    [SerializeField] private int numLamps;
    private int countLamps = 0;

    // Start is called before the first frame update
    void Start()
    {
        sm = FindObjectOfType<SongsManager>();
        sm.setPlay();
        uiGame = FindObjectOfType<UIGameController>();
        player = FindObjectOfType<PlayerController>();
        pMove = FindObjectOfType<PlayerMovement>();
        ia = FindObjectOfType<IA>();
        lanternController = FindObjectOfType<LanternController>();
        rayoAlfonso = FindObjectOfType<RayoAlfonso>();

        GameObject[] candelabros = GameObject.FindGameObjectsWithTag("lamp");

        numLamps = candelabros.Length;
        
        player.setCandels(numLamps + 3);
    }

    public void doPause(bool status)
    {
        pMove.dead = status;
        player.dead = status;
        ia.inPause = status;
        lanternController.freeze = status;
        rayoAlfonso.freeze = status;
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
        Debug.Log("Player Won");
        player.dead = true;
        pMove.dead = true;
        sm.play.SetActive(false);
        sm.dead.SetActive(false);
        uiGame.win();
    }

    public void lose() {
        Debug.Log("Player Losed");
        player.dead = true;
        pMove.dead = true;
        uiGame.loose();
        sm.setDead();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using aburron.Input;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameController : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private GameObject panelPause, firstSelectedPause, firstSelectedLoose;
    private EventSystem eventSystem;
    private AbuInput input = new AbuInput();
    private bool inPause, dead;
    
    public string textDead = "I... Don't feel well...";

    public Image panelMuerte;
    public TMP_Text textTitle, btExit, btRetry;


    private void Awake()
    {
        input.Enable();
        input.onStart += doPause;
        eventSystem = FindObjectOfType<EventSystem>();

        gm = FindObjectOfType<GameManager>();
    }
    
    private void OnDestroy()
    {
        input.Disable();
    }
    
    public void doPause()
    {
        if (!dead)
        {
            panelPause.SetActive(!inPause);
            eventSystem.SetSelectedGameObject(firstSelectedPause);
            gm.doPause(!inPause);
            inPause = !inPause;
        }
    }

    public void restartScene() {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void salir() {
        Application.Quit();
    }

    public void loose()
    {
        dead = true;
        StartCoroutine(looseAnim());
    }

    public void win()
    {
        dead = true;
        StartCoroutine(winAnim());
    }

    IEnumerator looseAnim() {
        while (panelMuerte.color.a < 1f) {
            yield return new WaitForEndOfFrame();
            panelMuerte.color = new Color(0, 0, 0, (panelMuerte.color.a + 0.01f));
        }
        textTitle.gameObject.SetActive(true);

        foreach (char c in textDead) {
            yield return new WaitForSeconds(0.1f);
            textTitle.text += c;
        }

        btExit.gameObject.SetActive(true);
        btRetry.gameObject.SetActive(true);

        while (btExit.color.a < 1f)
        {
            yield return new WaitForSeconds(0.1f);
            btExit.color = new Color(255, 255, 255, (panelMuerte.color.a + 0.01f));
            btRetry.color = new Color(255, 255, 255, (panelMuerte.color.a + 0.01f));
        }

        //btExit.gameObject.GetComponent<Button>().interactable = true;
        //btRetry.gameObject.GetComponent<Button>().interactable = true;
        
        eventSystem.SetSelectedGameObject(firstSelectedLoose);

        yield return new WaitForEndOfFrame();
    }

    IEnumerator winAnim()
    {
        while (panelMuerte.color.a < 1f)
        {
            yield return new WaitForEndOfFrame();
            panelMuerte.color = new Color(0, 0, 0, (panelMuerte.color.a + 0.01f));
        }
        SceneManager.LoadScene("Cinematic");
        yield return new WaitForEndOfFrame();
    }


}

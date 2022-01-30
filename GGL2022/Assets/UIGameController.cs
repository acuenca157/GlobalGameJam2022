using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGameController : MonoBehaviour
{
    public string textDead = "I... Don't feel well...";

    public Image panelMuerte;
    public TMP_Text textTitle, btExit, btRetry;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loose() {
        StartCoroutine(looseAnim());
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

        btExit.gameObject.GetComponent<Button>().interactable = true;
        btRetry.gameObject.GetComponent<Button>().interactable = true;

        yield return new WaitForEndOfFrame();
    }


}

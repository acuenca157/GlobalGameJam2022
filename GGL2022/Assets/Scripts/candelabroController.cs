using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

using FMODUnity;

public class candelabroController : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private EventReference soundFarolillo;
    public Sprite encendido;

    [SerializeField]
    [Range(0f, 10f)]
    private float range;

    private SpriteRenderer sr;
    private Light2D luz;

    private Transform ia;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        luz = GetComponentInChildren<Light2D>();

        ia = FindObjectOfType<IA>().transform;
    }

    private void Update()
    {
        if (Vector2.Distance(this.transform.position, ia.transform.position) < range)
        {
            Debug.Log("Vete nen");
            ia.GetComponent<IA>().goAway(this.transform.position);
        }
    }

    public void encender() {
        this.tag = "Untagged";
        sr.sprite = encendido;
        StartCoroutine(encenderLuz(0.8f));
        luz.pointLightOuterRadius = 5f;
        range = 5.0f;
        RuntimeManager.PlayOneShot(soundFarolillo);
        gm.addLamp();
    }

    IEnumerator encenderLuz(float f) {
        while (luz.intensity < f) {
            luz.intensity += 0.01f;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }
}

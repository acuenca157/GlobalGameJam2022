using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using FMODUnity;

using aburron.Input;
using UnityEngine.tvOS;

public class PlayerController : MonoBehaviour
{
    private AbuInput input = new AbuInput();
    
    RayoAlfonso ra;
    public GameObject candle;
    public float maxLinerna, maxRadio, minLinterna, minRadio;
    private float nvlLinterna, nvlRadio;
    private int candels;
    public Light2D luzCono, luzRadio;
    public bool dead = false;
    [Range(0, 10)]
    public float range;

    [SerializeField] private EventReference step;
    [SerializeField] private EventReference putVelaSound;
    [SerializeField] private EventReference notVelaSound;
    [SerializeField] private EventReference pickVelaSound;

    private void Awake()
    {
        input.Enable();
        input.onActionBottomRow1 += doAction;
    }

    private void doAction(float value)
    {
        if (value == 1f && !dead)
        {
            GameObject[] elements = GameObject.FindGameObjectsWithTag("lamp");
            GameObject[] velas = GameObject.FindGameObjectsWithTag("candle");
            GameObject[] velasApagadas = GameObject.FindGameObjectsWithTag("velaApagada");
            elements = elements.Concat(velas).ToArray();
            elements = elements.Concat(velasApagadas).ToArray();

            GameObject nearest = elements[0];
            
            foreach (GameObject element in elements)
            {
                if (Vector2.Distance(this.transform.position, element.transform.position) < 
                    Vector2.Distance(this.transform.position, nearest.transform.position))
                {
                    nearest = element;
                }
            }

            float distance = Vector2.Distance(this.transform.position, nearest.transform.position);
            Debug.Log(distance + "");
            
            if (distance < range)
            {
                switch (nearest.tag)
                {
                    case "velaApagada":
                        quitVela(nearest);
                        break;
                    case "candle":
                        quitVela(nearest);
                        break;
                    case "lamp":
                        if (candels > 0)
                        {
                            candelabroController cd = nearest.GetComponent<candelabroController>();
                            cd.encender();
                        }

                        break;
                }
            } else {
                putVela();
            }
        }
    }
    
    private void putVela()
    {
        if (candels > 0)
        {
            removeCandle();
            Instantiate(candle, transform.position, Quaternion.identity);
            RuntimeManager.PlayOneShot(putVelaSound);
        }
        else
        {
            RuntimeManager.PlayOneShot(notVelaSound);
        }
    }

    private void quitVela(GameObject candle)
    {
        addCandle();
        RuntimeManager.PlayOneShot(pickVelaSound);
        Destroy(candle.gameObject);
    }

    private void Start()
    {
        ra = FindObjectOfType<RayoAlfonso>();
        
        luzCono.pointLightOuterRadius = candels * nvlLinterna;
        luzRadio.pointLightOuterRadius = candels * nvlRadio;
    }
    private void Update()
    {
        if (!dead) {
            ra.distancia = nvlLinterna * candels;
        }
    }

    public void stepSound() {
        if(!dead)
            RuntimeManager.PlayOneShot(step);
    }

    public void setCandels(int n) {
        candels = n;

        //Calculo de las variables de nivel de Luminosidad
        //Averiguar niveles de diferencias entre maximos y minimos y a partir de ahi
        //Calcular el nivel de que sube o baja cada vela. Con el aura, sería igual
        
        nvlLinterna = (maxLinerna) / candels;
        nvlRadio = (maxRadio) / candels;

        luzCono.pointLightOuterRadius = candels * nvlLinterna;
        luzRadio.pointLightOuterRadius = candels * nvlRadio;

    }

    public void addCandle() {
        candels++;
        luzCono.pointLightOuterRadius = candels * nvlLinterna;
        luzRadio.pointLightOuterRadius = candels * nvlRadio;
    }

    public bool removeCandle() {
        if (candels > 0)
        {
            candels--;
            luzCono.pointLightOuterRadius = candels + 1 * nvlLinterna;
            luzRadio.pointLightOuterRadius = candels + 1 * nvlRadio;
            return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnDestroy()
    {
        input.Disable();
        input.onActionBottomRow1 -= doAction;
    }
}

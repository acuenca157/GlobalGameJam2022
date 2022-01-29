using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public GameObject candle;
    public float maxLinerna, maxRadio, minLinterna, minRadio;
    private float nvlLinterna, nvlRadio;
    private int candels;
    public Light2D luzCono, luzRadio;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (candels > 0)
            {
                putVela();
            }
        }
    }

    private void putVela()
    {
        bool canPutVela = true;
        GameObject[] candles = GameObject.FindGameObjectsWithTag("candle");
        GameObject[] lamps = GameObject.FindGameObjectsWithTag("lamp");

        foreach (GameObject candle in candles) {
            if ( Vector2.Distance(candle.transform.position, this.transform.position) <= 3f ) {
                canPutVela = false;
            }
        }

        if (canPutVela) {
            foreach (GameObject candle in lamps)
            {
                if (Vector2.Distance(candle.transform.position, this.transform.position) <= 3f)
                {
                    canPutVela = false;
                }
            }
        }

        if (canPutVela) {
            removeCandle();
            Instantiate(candle, transform.position, Quaternion.identity);
        }

        
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
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}

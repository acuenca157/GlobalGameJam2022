using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class candelabroController : MonoBehaviour
{
    private GameManager gm;
    private Transform Player;

    [Range(0, 5)]
    public float range = 1.32f;
    public Sprite encendido;
    bool won = false;

    private SpriteRenderer sr;
    private Light2D luz;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        Player = gm.getPlayerTransform();
        sr = GetComponent<SpriteRenderer>();
        luz = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update() 
    {
        if (Vector2.Distance(this.transform.position, Player.position) <= range)
        {
            if (!won)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (gm.removeCandle()) {
                        encender();
                    }
                    
                }
            }
            else {
                Debug.Log("Recargar Bateria");
            }
        }
    }

    public void encender() {
        sr.sprite = encendido;
        StartCoroutine(encenderLuz(0.8f));
        luz.pointLightOuterRadius = 5f;

        gm.addLamp();
        won = true;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    IEnumerator encenderLuz(float f) {
        while (luz.intensity < f) {
            luz.intensity += 0.01f;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
    }
}

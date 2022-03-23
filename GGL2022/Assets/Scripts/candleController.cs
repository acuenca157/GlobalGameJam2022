using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class candleController : MonoBehaviour
{
    Animator anim;
    
    [SerializeField] private EventReference blowVelaSound;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void apagarVela() {
        this.tag = "velaApagada";
        RuntimeManager.PlayOneShot(blowVelaSound);
        anim.SetTrigger("unlit");
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

}

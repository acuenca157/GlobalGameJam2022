using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candleController : MonoBehaviour
{
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) < 1f) {
            if (Input.GetKeyDown(KeyCode.E)) {
                player.addCandle();
                Destroy(this.gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

}

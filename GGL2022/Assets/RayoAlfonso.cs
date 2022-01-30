using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoAlfonso : MonoBehaviour
{
	public float distancia = 0f;

	Color color;

	IA ia;

    private void Start()
    {
		ia = FindObjectOfType<IA>();
    }
    void FixedUpdate()
	{
		Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

		//Get the first object hit by the ray
		RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distancia);

		color = Color.red;

		ia.freeze = false;
		//If the collider of the object hit is not NUll
		if (hit.collider != null)
		{
			//Hit something, print the tag of the object
			Debug.Log("Hitting: " + hit.collider.tag);
			ia.freeze = false;
			if (hit.collider.tag == "IA") {
				color = Color.green;
				ia.freeze = true;
			}
		}

		//Method to draw the ray in scene for debug purpose
		Debug.DrawRay(transform.position, dir * distancia, color);
	}
}

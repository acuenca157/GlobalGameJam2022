using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aburron.Input;

public class RayoAlfonso : MonoBehaviour
{
	public float threshold;
	public float distancia = 0f;
	Color color;
	IA ia;
	private Vector2 dir = new Vector2();
	[NonSerialized] public bool freeze;

	private bool usingController;

	private AbuInput input = new AbuInput();

	[SerializeField] private GameObject pos;

	private void Awake()
	{
		usingController = getUsingJoystick();
		
		input.Enable();
		input.onRightStick += onRightStick;
	}
	
	private void onRightStick(Vector2 vector2)
	{
		if(usingController)
			dir = vector2;
	}
	
	private bool getUsingJoystick()
	{
		bool val = PlayerPrefs.GetInt("usingJoyStick") == 1 ? true : false;
		return val;
	}

	private void Start()
    {
		ia = FindObjectOfType<IA>();
    }
    void FixedUpdate()
	{
		if (!freeze)
		{
			color = Color.red;
			ia.freeze = false;

			if (!usingController)
			{
				dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			}

			//Get the first object hit by the ray
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distancia);

			//If the collider of the object hit is not NUll
			if (hit.collider != null)
			{
				//Hit something, print the tag of the object
				//Debug.Log("Hitting: " + hit.collider.tag);
				ia.freeze = false;
				if (hit.collider.tag == "IA")
				{
					color = Color.green;
					ia.freeze = true;
				}
			}

			//Method to draw the ray in scene for debug purpose
			Debug.DrawRay(transform.position, dir * distancia, color);
		}
	}
    
    private void OnDestroy()
    {
	    input.Disable();
    }
}

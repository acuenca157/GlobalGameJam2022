using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using aburron.Input;
using UnityEngine.tvOS;

public class LanternController : MonoBehaviour
{
    private AbuInput input = new AbuInput();
    private Vector2 dir;
    [NonSerialized] public bool freeze;

    private bool usingController;

    private void Awake()
    {
        input.Enable();
        input.onRightStick += onRightStick;

        usingController = getUsingJoystick();
    }
    
    private bool getUsingJoystick()
    {
        bool val = PlayerPrefs.GetInt("usingJoyStick") == 1 ? true : false;
        return val;
    }

    private void onRightStick(Vector2 vector2)
    {
        if(usingController)
         dir = vector2;
    }

    // Update is called once per frame
    void Update()
    {

        if (!freeze)
        {
            if(!usingController)
                dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = rotation;
        }

        
    }

    private void OnDestroy()
    {
        input.Disable();
    }
}

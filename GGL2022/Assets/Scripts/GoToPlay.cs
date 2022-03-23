using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GoToPlay : MonoBehaviour
{
    public void goToPlay(bool joystick)
    {
        savePref(joystick);
        SceneManager.LoadScene("ControlsScene");
    }

    private void savePref(bool val)
    {
        PlayerPrefs.SetInt("usingJoyStick", val ? 1 : 0);
        PlayerPrefs.Save();
    }
}

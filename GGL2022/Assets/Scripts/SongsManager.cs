using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SongsManager : MonoBehaviour
{
    [SerializeField] private EventReference atack;
    public GameObject play, dead;


    public void setPlay() {
        play.SetActive(true);
        dead.SetActive(false);
    }
    public void setDead()
    {
        StartCoroutine(mata());
    }

    IEnumerator mata() {
        play.SetActive(false);
        yield return new WaitForEndOfFrame();
        RuntimeManager.PlayOneShot(atack);
        yield return new WaitForSeconds(1.5f);
        dead.SetActive(true);
        yield return new WaitForEndOfFrame();
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using FMODUnity;

public class ReproducirVideo : MonoBehaviour {
    [SerializeField] private EventReference audio;

    public RawImage image;
    public VideoClip clip;
    public RawImage imagenNegra;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    //private AudioSource audioSource;
 // Use this for initialization
 void Start () {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
   }

    IEnumerator playVideo()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        //audioSource = gameObject.AddComponent<AudioSource>();
        videoPlayer.playOnAwake = false;
        //audioSource.playOnAwake = false;
        //audioSource.Pause();
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = clip;
        //videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        //videoPlayer.EnableAudioTrack(0, true);
        //videoPlayer.SetTargetAudioSource(0, audioSource);
        videoPlayer.Prepare();

        WaitForSeconds waitTime = new WaitForSeconds(1);
        while(!videoPlayer.isPrepared)
        {
            yield return waitTime;
            break;
        }
       
        image.texture = videoPlayer.texture;
        videoPlayer.Play();
        //audioSource.Play();
        RuntimeManager.PlayOneShot(audio);
        imagenNegra.enabled = false;
        videoPlayer.loopPointReached += EndReached;
    }
 
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("SceneMenu");
    }

   // Update is called once per frame
    void Update () {

    }
}


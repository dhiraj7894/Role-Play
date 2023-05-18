using UnityEngine;
using System;
using RP;

public class VideoPlayer : Singleton<VideoPlayer>
{
    //public static VideoPlayer Instance{get;private set;}
    [SerializeField] private UnityEngine.Video.VideoPlayer videoPlayer; 
    [SerializeField] private UnityEngine.Video.VideoClip videoClip;
    [SerializeField] private RectTransform videoPlayerRawImage;
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private bool videoComplete;
    [SerializeField] private int videoSpeed = 10;

    // Start is called before the first frame update

    public static event EventHandler OnVideoCompletePlay;
    public static event EventHandler OnVideoPlay;
    private void Awake() {
        //Instance=this;
    }
    void Start()
    {
        RenderTextureRelease();
        ScaleManagementOfVideoPlayerRawImage(new Vector2(1920, 1080), new Vector3(0, 0, 0));
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            videoPlayer.frame = (long)videoPlayer.frameCount;
        }
    }

    public void VideoPlay(){
        OnVideoPlay?.Invoke(this, EventArgs.Empty);
        RenderTextureRelease();
        PlayVideo(videoClip, videoSpeed, SetVideoComplete);    
        
        //Below code is to skip starting long video
        //videoPlayer.frame = (long)videoPlayer.frameCount;
    }




    private void SetVideoComplete(UnityEngine.Video.VideoPlayer vp){
        RenderTextureRelease();        
        videoPlayer.clip = null;        
        OnVideoCompletePlay?.Invoke(this, EventArgs.Empty);
        videoSpeed = 1;
        ScaleManagementOfVideoPlayerRawImage(new Vector2(1164, 645), new Vector3(-325f, 27f, 0));        
        videoPlayer.loopPointReached -= SetVideoComplete;
    }


    private void OnApplicationQuit()
    {
        RenderTextureRelease();
    }
    public void PlayVideo(UnityEngine.Video.VideoClip clip, float clipSpeed, UnityEngine.Video.VideoPlayer.EventHandler VP, bool mute = false)
    {       
        videoPlayer.clip = clip;
        videoPlayer.Play();
        videoPlayer.playbackSpeed = videoPlayer.playbackSpeed * clipSpeed;
        videoPlayer.loopPointReached += VP;
        videoPlayer.SetDirectAudioMute(0, mute);
    }
    public void PauseVideo()
    {
        int devideFrameCountBy = 5;
        videoPlayer.frame = (long)videoPlayer.frameCount / devideFrameCountBy;
        videoPlayer.Pause();
    }

    public void ContinuePlaying()
    {
        videoPlayer.Play();
    }
    public void RemoveEvent(UnityEngine.Video.VideoPlayer.EventHandler vp)
    {
        videoPlayer.loopPointReached -= vp;
    }
    public void ScaleManagementOfVideoPlayerRawImage(Vector2 UISize, Vector3 position)
    {
        videoPlayerRawImage.sizeDelta = UISize;
        videoPlayerRawImage.localPosition = position;
    }

    public void RenderTextureRelease()
    {
        renderTexture.Release();
    }

}

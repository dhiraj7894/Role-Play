using UnityEngine;
using System;

public enum Role
{
    character1,
    character2
}
public class VideoPlayerInParts : MonoBehaviour
{
    

    [SerializeField] Role role;
    [SerializeField] VideoManager manager;
    [SerializeField] private int RecordingCount;
    [SerializeField] private float currentVideoLength;

    private int _videoID;


    public static event EventHandler OnTargetSetToCH1;
    public static event EventHandler OnTargetSetToCH2;
    private void Start()
    {
        VideoManager.onCharacter1Choosen += OnCharacter1Choosen;
        VideoManager.onCharacter2Choosen += OnCharacter2Choosen;        
    }

    private void OnCharacter1Choosen(object sender, System.EventArgs e)
    {
        
        if (manager.videoClipsParts[0].name.Contains("C"))
        {
            Debug.Log(" First Character is Customer so start recording");
            ShowRecordingOption(0);
            OnTargetSetToCH1?.Invoke(this, EventArgs.Empty);
            role = Role.character1;
        }
        else
        {
            Debug.Log(" First Character is Seller");
            VideoPlayer.Insternce.PlayVideo(VideoManager.Instance.videoClipsParts[0], 1, OnVideoComplete);
            RecordTimer.Instance.GetRecordingStatus = true;
            manager.EnableRecordingButton(false);
            OnTargetSetToCH2?.Invoke(this, EventArgs.Empty);
            role = Role.character2;
        }
        
    }

    private void OnCharacter2Choosen(object sender, System.EventArgs e)
    {
        
        if (manager.videoClipsParts[0].name.Contains("S"))
        {
            Debug.Log(" First Character is seller so start recording");            
            ShowRecordingOption(0);
            OnTargetSetToCH2?.Invoke(this, EventArgs.Empty);
            role = Role.character2;
        }
        else
        {
            Debug.Log(" First Character is Customer");
            VideoPlayer.Insternce.PlayVideo(VideoManager.Instance.videoClipsParts[0], 1, OnVideoComplete);
            RecordTimer.Instance.GetRecordingStatus = true;
            manager.EnableRecordingButton(false);
            OnTargetSetToCH1?.Invoke(this, EventArgs.Empty);
            role = Role.character1;
        }
    }

   

    public void PlayVideoOnLoop()
    {

        switch (role)
        {
            case Role.character1:
                if (VideoManager.Instance.isCharacter1)
                {
                    // wait for recording
                    Debug.Log("Waiting for Recording from Customer Side");
                    ShowRecordingOption(_videoID);
                }
                else
                {
                    // play next video
                    OtherCharacterVideoPlay();
                }
                OnTargetSetToCH1?.Invoke(this, EventArgs.Empty);
                break;

            case Role.character2:
                if (VideoManager.Instance.isCharacter2)
                {
                    // wait for recording
                    Debug.Log("Waiting for Recording from Seller Side");
                    ShowRecordingOption(_videoID);
  
                }
                else
                {
                    // play next video
                    OtherCharacterVideoPlay();
                }
                OnTargetSetToCH2?.Invoke(this, EventArgs.Empty);
                break;
        }
    }

    private void OnVideoComplete(UnityEngine.Video.VideoPlayer vp)
    {
        VideoPlayer.Insternce.RemoveEvent(OnVideoComplete);
        _videoID++;
        role = role == Role.character1 ? role = Role.character2 : role = Role.character1;
        if (manager.videoClipsParts.Count > _videoID) { PlayVideoOnLoop(); } else { GameManager.Instance.EnableRestartOption(); }
    }


    private void OnRecordingVideoComplete(UnityEngine.Video.VideoPlayer vp)
    {
        //Debug.Log("Waiting for player Input");
        VideoPlayer.Insternce.RemoveEvent(OnRecordingVideoComplete);
    }
    
    public void NextButton()
    {
        RecordingCount++;
        _videoID++;
        role = role == Role.character1 ? role = Role.character2 : role = Role.character1;
      
        VideoManager.Instance.AudioFileNumber = RecordingCount;
        if (manager.videoClipsParts.Count > _videoID) { PlayVideoOnLoop(); } else { GameManager.Instance.EnableRestartOption(); }
    }

    public void ShowRecordingOption(int ID)
    {
        manager.EnableRecordingButton(true);
        RecordTimer.Instance.GetRecordingStatus = false;
        VideoPlayer.Insternce.PlayVideo(VideoManager.Instance.videoClipsParts[ID], 1, OnRecordingVideoComplete, true);
        currentVideoLength = (float)VideoManager.Instance.videoClipsParts[ID].length;
        RecordTimer.Instance.GetRecordingLength(currentVideoLength);
        VideoPlayer.Insternce.PauseVideo();
    }

    public void OtherCharacterVideoPlay()
    {
        manager.EnableRecordingButton(false);
        VideoPlayer.Insternce.PlayVideo(VideoManager.Instance.videoClipsParts[_videoID], 1, OnVideoComplete);
    }

    public void DeRegisterEvents()
    {
        VideoManager.onCharacter1Choosen -= OnCharacter1Choosen;
        VideoManager.onCharacter2Choosen -= OnCharacter2Choosen;
    }
    
}

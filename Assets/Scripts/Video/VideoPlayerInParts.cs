using UnityEngine;

public class VideoPlayerInParts : MonoBehaviour
{
    enum Role
    {
        character1,
        character2
    }
    [SerializeField] Role role;
    [SerializeField] VideoManager manager;
    [SerializeField] private int RecordingCount;
    [SerializeField] private float currentVideoLength;

    private int videoID;

    

    private void Start()
    {
        VideoManager.onCustomerChoosen += OnCustomerChoosen;
        VideoManager.onSellerChoosen += OnSellerChoosen;        
    }


    private void OnSellerChoosen(object sender, System.EventArgs e)
    {
        
        if(manager.videoClipsParts[0].name.Contains("S"))
        {
            Debug.Log(" First Character is seller so start recording");
            //VideoPlayer.insternce.PlayVideo(VideoManager.Instance.videoClipsParts[videoID], 1, OnRecordingVideoComplete, true);
            ShowRecordingOption(0);
            role = Role.character2;
        }
        else
        {
            Debug.Log(" First Character is Customer");
            VideoPlayer.insternce.PlayVideo(VideoManager.Instance.videoClipsParts[0], 1, OnVideoComplete);
            RecordTimer.Instance.GetRecordingStatus = true;
            manager.EnableRecordingButton(false);
            role = Role.character1;
        }

        
    }

    private void OnCustomerChoosen(object sender, System.EventArgs e)
    {
        
        if (manager.videoClipsParts[0].name.Contains("C"))
        {
            Debug.Log(" First Character is Customer so start recording");
            //VideoPlayer.insternce.PlayVideo(VideoManager.Instance.videoClipsParts[videoID], 1, OnRecordingVideoComplete, true);
            ShowRecordingOption(0);
            role = Role.character1;
        }
        else
        {
            Debug.Log(" First Character is Seller");
            VideoPlayer.insternce.PlayVideo(VideoManager.Instance.videoClipsParts[0], 1, OnVideoComplete);
            RecordTimer.Instance.GetRecordingStatus = true;
            manager.EnableRecordingButton(false);
            role = Role.character2;
        }
    }

    public void PlayVideoOnLoop()
    {

        switch (role)
        {
            case Role.character1:
                if (VideoManager.Instance.isCustomer)
                {
                    // wait for recording
                    Debug.Log("Waiting for Recording from Customer Side");
                    ShowRecordingOption(videoID);
                }
                else
                {
                    // play next video
                    OtherCharacterVideoPlay();
                }
                break;

            case Role.character2:
                if (VideoManager.Instance.isSeller)
                {
                    // wait for recording
                    Debug.Log("Waiting for Recording from Seller Side");
                    ShowRecordingOption(videoID);
  
                }
                else
                {
                    // play next video
                    OtherCharacterVideoPlay();
                }
                break;
        }
    }

    private void OnVideoComplete(UnityEngine.Video.VideoPlayer vp)
    {
        VideoPlayer.insternce.RemoveEvent(OnVideoComplete);
        videoID++;
        role = role == Role.character1 ? role = Role.character2 : role = Role.character1;
        if (manager.videoClipsParts.Count > videoID) { PlayVideoOnLoop(); } else { GameManager.Instance.EnableRestartOption(); }
    }


    private void OnRecordingVideoComplete(UnityEngine.Video.VideoPlayer vp)
    {
        //Debug.Log("Waiting for player Input");
        VideoPlayer.insternce.RemoveEvent(OnRecordingVideoComplete);
    }
    
    public void NextButton()
    {
        RecordingCount++;
        videoID++;
        role = role == Role.character1 ? role = Role.character2 : role = Role.character1;
      
        VideoManager.Instance.AudioFileNumber = RecordingCount;
        if (manager.videoClipsParts.Count > videoID) { PlayVideoOnLoop(); } else { GameManager.Instance.EnableRestartOption(); }
    }

    public void ShowRecordingOption(int ID)
    {
        manager.EnableRecordingButton(true);
        RecordTimer.Instance.GetRecordingStatus = false;
        VideoPlayer.insternce.PlayVideo(VideoManager.Instance.videoClipsParts[ID], 1, OnRecordingVideoComplete, true);
        currentVideoLength = (float)VideoManager.Instance.videoClipsParts[ID].length;
        RecordTimer.Instance.GetRecordingLength(currentVideoLength);
        VideoPlayer.insternce.PauseVideo();
    }

    public void OtherCharacterVideoPlay()
    {
        manager.EnableRecordingButton(false);
        VideoPlayer.insternce.PlayVideo(VideoManager.Instance.videoClipsParts[videoID], 1, OnVideoComplete);
    }

    public void DeRegisterEvents()
    {
        VideoManager.onCustomerChoosen -= OnCustomerChoosen;
        VideoManager.onSellerChoosen -= OnSellerChoosen;
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using RP;

public class VideoManager : Singleton<VideoManager>
{
    //public static VideoManager instance { get; private set; }

    [SerializeField] private Transform selectCharacterToPlayTheRole;
    [SerializeField] private Transform startVideoButton;
    [SerializeField] private Transform recordingPanal;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button recordingButton;

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoPlayerInParts VideoPlayerInParts;
    public bool isCustomer;
    public bool isSeller;

    public static event EventHandler onCustomerChoosen;
    public static event EventHandler onSellerChoosen;    

    public List<UnityEngine.Video.VideoClip> videoClipsParts = new List<UnityEngine.Video.VideoClip>();

    public string AudioName;
    public int AudioFileNumber;

    private void Awake()
    {
        //instance = this;
    }

    private void Start() {
        //recordingPanal.gameObject.SetActive(false);

        VideoPlayer.OnVideoCompletePlay += OnVideoCompletePlay;
        VideoPlayer.OnVideoPlay += OnVideoPlay;        

    }

    private void OnVideoPlay(object sender, System.EventArgs e)
    {
        startVideoButton.gameObject.SetActive(false);
        
    }

    float uiMoveSpeed = 0.25f;
    float resetSelectCharacterUIPosition = 1000f;
    private void OnVideoCompletePlay(object sender, System.EventArgs e)
    {
        selectCharacterToPlayTheRole.gameObject.SetActive(true);        
        LeanTween.move(selectCharacterToPlayTheRole.GetComponent<RectTransform>(), new Vector3(0, 0, 0), uiMoveSpeed).setEaseLinear();
    }



    public void IsPlayerChooseCustomer()
    {
        isCustomer = true;
        isSeller = false;
        AudioName = "Customer";        
        onCustomerChoosen?.Invoke(this, EventArgs.Empty);
        EnableRecordingPanal();
        LeanTween.move(selectCharacterToPlayTheRole.GetComponent<RectTransform>(), new Vector3(0, resetSelectCharacterUIPosition, 0), uiMoveSpeed).setEaseLinear();
    }
    public void IsPlayerChooseSeller()
    {
        isCustomer = false;
        isSeller = true;
        AudioName = "Seller";
        //Recorder.Instance.GetAudioName("Seller");
        EnableRecordingPanal();
        onSellerChoosen?.Invoke(this, EventArgs.Empty);
        LeanTween.move(selectCharacterToPlayTheRole.GetComponent<RectTransform>(), new Vector3(0, resetSelectCharacterUIPosition, 0), uiMoveSpeed).setEaseLinear();
    }

    public void EnableRecordingPanal()
    {
        if (recordingPanal.gameObject.activeSelf == false) recordingPanal.gameObject.SetActive(true);
    }
    public void ContinueVideoPlayer()
    {        
        VideoPlayerInParts.NextButton();        
    }

    public void EnableRecordingButton(bool isTrue)
    {
        recordingButton.interactable = isTrue;
    }

    public void DeRegisterEvents()
    {
        VideoPlayer.OnVideoCompletePlay -= OnVideoCompletePlay;
        VideoPlayer.OnVideoPlay -= OnVideoPlay;
        VideoPlayerInParts.DeRegisterEvents();
    }
}

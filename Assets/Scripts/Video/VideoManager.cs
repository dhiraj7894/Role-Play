using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using RP;

public class VideoManager : Singleton<VideoManager>
{


    [SerializeField] private Transform selectCharacterToPlayTheRole;
    [SerializeField] private Transform startVideoButton;
    [SerializeField] private Transform recordingPanal;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI _nextButtonText;
    [SerializeField] private Button recordingButton;
    [SerializeField] private Button _character1; // Customer Side
    [SerializeField] private Button _character2; // Seller Side
    [SerializeField] private string _character1Name;
    [SerializeField] private string _character2Name;



    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoPlayerInParts VideoPlayerInParts;
    public bool isCharacter1;
    public bool isCharacter2;

    public static event EventHandler onCharacter1Choosen;
    public static event EventHandler onCharacter2Choosen;    

    public List<UnityEngine.Video.VideoClip> videoClipsParts = new List<UnityEngine.Video.VideoClip>();

    public string AudioName;
    public int AudioFileNumber;


    private void Start() {

        _nextButtonText.text = "";
        nextButton.interactable = false;

        VideoPlayer.OnVideoCompletePlay += OnVideoCompletePlay;
        VideoPlayer.OnVideoPlay += OnVideoPlay;


        _character1.onClick.AddListener(()=> {
            IsPlayerChooseCharacter1(_character1Name);
        });
        _character2.onClick.AddListener(() => {
            IsPlayerChooseCharacter2(_character2Name);
        });
        nextButton.onClick.AddListener(() => {
            _nextButtonText.text = "";
            nextButton.interactable = false;
            VideoPlayerInParts.NextButton();
        });
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



    public void IsPlayerChooseCharacter1(string _char1)
    {
        isCharacter1 = true;
        isCharacter2 = false;
        //"Character_1_"
        AudioName = _char1;        
        onCharacter1Choosen?.Invoke(this, EventArgs.Empty);
        EnableRecordingPanal();
        LeanTween.move(selectCharacterToPlayTheRole.GetComponent<RectTransform>(), new Vector3(0, resetSelectCharacterUIPosition, 0), uiMoveSpeed).setEaseLinear();
    }
    public void IsPlayerChooseCharacter2(string _char2)
    {
        isCharacter1 = false;
        isCharacter2 = true;
        //"Character_2_"
        AudioName = _char2;
        EnableRecordingPanal();
        onCharacter2Choosen?.Invoke(this, EventArgs.Empty);
        LeanTween.move(selectCharacterToPlayTheRole.GetComponent<RectTransform>(), new Vector3(0, resetSelectCharacterUIPosition, 0), uiMoveSpeed).setEaseLinear();
    }

    public void EnableRecordingPanal()
    {
        if (recordingPanal.gameObject.activeSelf == false) recordingPanal.gameObject.SetActive(true);
    }
    public void ContinueVideoPlayer()
    {
        nextButton.interactable = true;
        _nextButtonText.text = "Click here to Resume...";
    }

    public void EnableRecordingButton(bool isTrue)
    {
        recordingButton.interactable = isTrue;
        if (isTrue) {
            recordingButton.GetComponent<Image>().color = Color.green;
            RecordTimer.Instance.GetRecordingTimerText.text = "Press here record";
        } else {
            recordingButton.GetComponent<Image>().color = Color.red;            
        }
    }

    public void DeRegisterEvents()
    {
        VideoPlayer.OnVideoCompletePlay -= OnVideoCompletePlay;
        VideoPlayer.OnVideoPlay -= OnVideoPlay;
        VideoPlayerInParts.DeRegisterEvents();
    }
    public string GetCharatcer1Value
    {
        get
        {
            return _character1Name;
        }
        private set { }
    }
    public string GetCharatcer2Value
    {
        get
        {
            return _character2Name;
        }
        private set { }
    }
}

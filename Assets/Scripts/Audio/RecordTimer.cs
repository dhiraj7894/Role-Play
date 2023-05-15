using System.Collections;
using UnityEngine;
using TMPro;
using RP;

public class RecordTimer : Singleton<RecordTimer> {

    [SerializeField] private TextMeshProUGUI recordingTimerText;    
    [SerializeField] private Recorder Recorder;    
    [SerializeField] private float recordingLength;
    public bool GetRecordingStatus{get; set;}
    private int duration;
    private void OnEnable()
    {
        recordingTimerText.text = "";
    }
    public void StartTimer()
    {
        VideoPlayer.Insternce.ContinuePlaying();         
        Recorder.timeToRecord =duration = (int)recordingLength;        
        StartCoroutine(Timer());
    }
    public void StopTimer()
    {
        recordingTimerText.text = "";
        //desable Recording Button
        VideoManager.Instance.EnableRecordingButton(false);
        VideoManager.Instance.ContinueVideoPlayer();
        //Debug.LogWarning("Video and Recording Complete");
        StopCoroutine(Timer());

    }

    IEnumerator Timer()
    {
        while (duration > 0)
        {
            //Debug.Log(duration);
            recordingTimerText.text = $"{duration / 60:00}:{duration % 60:00}";
            duration--;
            
            yield return new WaitForSeconds(1);
        }
    }

    public float GetRecordingLength(float time)
    {
        return recordingLength = time;
    }

    public TextMeshProUGUI GetRecordingTimerText
    {
        get
        {
            return recordingTimerText;
        }
        set
        {
            recordingTimerText = value;
        }
    }
   

}

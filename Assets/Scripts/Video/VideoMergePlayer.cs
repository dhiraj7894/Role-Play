using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoMergePlayer : MonoBehaviour
{
    [SerializeField] Role _currentRole;
    [SerializeField] private VideoPlayerInParts _videoPlayerInParts;
    [SerializeField] private Transform _videoMergeUI;
    [SerializeField] private Button _playButton;
    [SerializeField] private int _audioId;

    private void Start()
    {
        _playButton.onClick.AddListener(() =>
        {
            VideoManager.Instance.EnableRecordingPanal();
            VideoPlayer.Instance.RenderTextureRelease();
            onMergerEnable(false);
            VideoPlayer.Instance.ScaleManagementOfVideoPlayerRawImage(new Vector2(1920, 1080), new Vector3(0, 0, 0));
            OnStartOfVideo();
        });
    }

    public void onMergerEnable(bool isTrue)
    {
        _videoMergeUI.gameObject.SetActive(isTrue);
    }

    public void OnStartOfVideo()
    {
        _videoPlayerInParts.VideoID = 0;
        _audioId = 0;

        if (VideoManager.Instance.isCharacter1)
        {
            if (VideoManager.Instance.videoClipsParts[0].name.Contains("C"))
            {
                AudioManager.Instance.PlayAudio(_audioId);
                VideoPlayer.Instance.PlayVideo(VideoManager.Instance.videoClipsParts[0], 1, OnMutedVideoComplete, true);
                _currentRole = Role.character1;
            }
            else
            {
                _currentRole = Role.character2;
                AudioVideoPlayer();
            }
        }

        if (VideoManager.Instance.isCharacter2)
        {
            if (VideoManager.Instance.videoClipsParts[0].name.Contains("S"))
            {
                Debug.Log("Found Letter and Playing same video");
                AudioManager.Instance.PlayAudio(_audioId);
                VideoPlayer.Instance.PlayVideo(VideoManager.Instance.videoClipsParts[0], 1, OnMutedVideoComplete, true);
                _currentRole = Role.character2;
            }
            else
            {
                _currentRole = Role.character1;
                AudioVideoPlayer();
            }
        }
    }

    public void AudioVideoPlayer()
    {

        switch (_currentRole)
        {
            case Role.character1:
                /*
                    If video manager's boolen value is same as role value then playe muted video
                    else play same video with un muted
                    */
                if (VideoManager.Instance.isCharacter1)
                {
                    //Debug.Log("Matching Role and Boolen same video");
                    VideoPlayer.Instance.PlayVideo(VideoManager.Instance.videoClipsParts[_videoPlayerInParts.VideoID], 1, OnMutedVideoComplete, true);
                }
                else
                {
                    //Debug.Log(" Not Matching Role and Boolen same video");
                    VideoPlayer.Instance.PlayVideo(VideoManager.Instance.videoClipsParts[_videoPlayerInParts.VideoID], 1, OnVideoComplete);
                }
                break;

            case Role.character2:
                /*
                  If video manager's boolen value is same as role value then playe muted video
                  else play same video with un muted
                  */
                if (VideoManager.Instance.isCharacter2)
                {
                    //Debug.Log("Matching Role and Boolen same video 1");                   
                    VideoPlayer.Instance.PlayVideo(VideoManager.Instance.videoClipsParts[_videoPlayerInParts.VideoID], 1, OnMutedVideoComplete, true);
                }
                else
                {
                    //Debug.Log(" Not Matching Role and Boolen same video 1");
                    VideoPlayer.Instance.PlayVideo(VideoManager.Instance.videoClipsParts[_videoPlayerInParts.VideoID], 1, OnVideoComplete);
                }

                break;
        }
    }
    private void OnVideoComplete(UnityEngine.Video.VideoPlayer vp)
    {
        VideoPlayer.Instance.RemoveEvent(OnVideoComplete);
        _currentRole = _currentRole == Role.character1 ? _currentRole = Role.character2 : _currentRole = Role.character1;
        _videoPlayerInParts.VideoID++;
        Debug.Log("+");
        if (VideoManager.Instance.videoClipsParts.Count > _videoPlayerInParts.VideoID)
        {
            AudioManager.Instance.PlayAudio(_audioId);
            AudioVideoPlayer();
        }else{
            GameManager.Instance.EnableRestartOption();
        }
    }
    private void OnMutedVideoComplete(UnityEngine.Video.VideoPlayer vp)
    {
        VideoPlayer.Instance.RemoveEvent(OnMutedVideoComplete);
        _currentRole = _currentRole == Role.character1 ? _currentRole = Role.character2 : _currentRole = Role.character1;
        _audioId++;
        _videoPlayerInParts.VideoID++;
        Debug.Log("++");
        if (VideoManager.Instance.videoClipsParts.Count > _videoPlayerInParts.VideoID)
        {
            AudioVideoPlayer();
        }else{
            GameManager.Instance.EnableRestartOption();
        }
    }
}
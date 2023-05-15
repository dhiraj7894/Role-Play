using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrowCharacters : MonoBehaviour {

    [SerializeField] private Transform _arrowUI;
    [SerializeField] private Vector2 _character1RectPOsition;
    [SerializeField] private Vector2 _character2RectPOsition;
    [SerializeField] private float _uiMoveSpeed = 0.5f;

    private void Start()
    {
        VideoPlayerInParts.OnTargetSetToCH1 += VideoPlayerInParts_OnTargetSetToCH1;
        VideoPlayerInParts.OnTargetSetToCH2 += VideoPlayerInParts_OnTargetSetToCH2;
    }

    private void VideoPlayerInParts_OnTargetSetToCH2(object sender, System.EventArgs e)
    {
        Debug.Log("CH2");
        LeanTween.move(_arrowUI.GetComponent<RectTransform>(), _character2RectPOsition, _uiMoveSpeed).setEaseLinear();
    }

    private void VideoPlayerInParts_OnTargetSetToCH1(object sender, System.EventArgs e)
    {
        Debug.Log("CH1");
        LeanTween.move(_arrowUI.GetComponent<RectTransform>(), _character1RectPOsition, _uiMoveSpeed).setEaseLinear();
    }

    public void DeRegisterEvent()
    {
        VideoPlayerInParts.OnTargetSetToCH1 -= VideoPlayerInParts_OnTargetSetToCH1;
        VideoPlayerInParts.OnTargetSetToCH2 -= VideoPlayerInParts_OnTargetSetToCH2;
    }
}

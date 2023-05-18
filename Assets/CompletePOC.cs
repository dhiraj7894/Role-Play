using UnityEngine;

public class CompletePOC : MonoBehaviour
{
    [SerializeField]private Transform _restartButton;
    [SerializeField]private Transform _resetText;
    private void OnEnable() {
        VideoPlayer.Instance.RenderTextureRelease();
        float uiSpeed = 0.25f;
        LeanTween.move(_restartButton.GetComponent<RectTransform>(),new Vector2(0,0),uiSpeed);
        LeanTween.move(_resetText.GetComponent<RectTransform>(),new Vector2(0,190f),uiSpeed);
    }
}

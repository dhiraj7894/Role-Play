using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioPlayerButton : MonoBehaviour
{
    [SerializeField] private Button clickButton;
    [SerializeField] private TextMeshProUGUI audioTamplateName;

    private void Start()
    {
        //audioTamplateName = 
        clickButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayAudio(audioTamplateName.text);
        });
    }
    public void SetAudioTamplateName(string text)
    {
        audioTamplateName.text = text;
    }

    /*public void OnClickButton()
    {
        AudioManager.Instance.PlayAudio(audioTamplateName.text);
    }*/
}

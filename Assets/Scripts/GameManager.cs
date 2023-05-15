using UnityEngine;
using UnityEngine.SceneManagement;
using RP;
public class GameManager : Singleton<GameManager> {

    [SerializeField] private TargetArrowCharacters _targetArrowCharacters;
    [SerializeField] private Transform restartButton;

    private void Start()
    {
        Application.targetFrameRate = 60;
        restartButton.gameObject.SetActive(false);
    }
    public void RestartScene()
    {
        VideoPlayer.Insternce.RenderTextureRelease();
        VideoManager.Instance.DeRegisterEvents();
        _targetArrowCharacters.DeRegisterEvent();
        SceneManager.LoadScene(0);
    }

    public void EnableRestartOption()
    {
        restartButton.gameObject.SetActive(true);
    }
}

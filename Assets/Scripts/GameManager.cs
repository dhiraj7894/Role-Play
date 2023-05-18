using UnityEngine;
using UnityEngine.SceneManagement;
using RP;
public class GameManager : Singleton<GameManager> {

    [SerializeField] private TargetArrowCharacters _targetArrowCharacters;
    [SerializeField] private VideoMergePlayer _videoMerger;
    [SerializeField] private Transform _restartPanal;

    private void Start()
    {
        Application.targetFrameRate = 60;
        _restartPanal.gameObject.SetActive(false);
    }
    public void RestartScene()
    {
        VideoPlayer.Instance.RenderTextureRelease();
        VideoManager.Instance.DeRegisterEvents();
        _targetArrowCharacters.DeRegisterEvent();
        SceneManager.LoadScene(0);
    }

    public void EnableRestartOption()
    {
        _restartPanal.gameObject.SetActive(true);        
    }
    public void EnableCombinedVideoPlayerUI(){
        _targetArrowCharacters.OnEnableArrowUI(false);
        _videoMerger.onMergerEnable(true);
    }

}

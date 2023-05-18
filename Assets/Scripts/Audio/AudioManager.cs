using System.Collections.Generic;
using UnityEngine;
using RP;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _audioTamplate;

    public List<AudioClip> audioClips = new List<AudioClip>();

    private void Awake()
    {
        _audioTamplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        foreach (Transform item in _container)
        {
            if (item == _audioTamplate) continue;
            Destroy(item.gameObject);
        }

        foreach (AudioClip aClip in audioClips)
        {
            Transform audioTransform = Instantiate(_audioTamplate, _container);
            audioTransform.gameObject.SetActive(true);
            audioTransform.GetComponent<AudioPlayerButton>().SetAudioTamplateName(aClip.name);
        }
    }



    public void PlayAudio(string audio)
    {

        foreach (AudioClip item in audioClips)
        {
            if(item.name == audio)
            {
                _audioSource.clip = item;
                _audioSource.Play();
                
            }
        }
    }

    public void PlayAudio(int ID){
        if(audioClips.Count > 0){
            _audioSource.clip = audioClips[ID];
            _audioSource.Play(); 
        }else{
            Debug.LogError("Audio list is empty.");
        }
               
    }
}

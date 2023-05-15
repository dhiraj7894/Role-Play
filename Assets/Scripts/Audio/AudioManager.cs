using System.Collections.Generic;
using UnityEngine;
using RP;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform container;
    [SerializeField] private Transform audioTamplate;

    public List<AudioClip> audioClips = new List<AudioClip>();

    private void Awake()
    {
        audioTamplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        foreach (Transform item in container)
        {
            if (item == audioTamplate) continue;
            Destroy(item.gameObject);
        }

        foreach (AudioClip aClip in audioClips)
        {
            Transform audioTransform = Instantiate(audioTamplate, container);
            audioTransform.gameObject.SetActive(true);
            audioTransform.GetComponent<AudioPlayerButton>().SetAudioTamplateName(aClip.name);
        }
    }

/*    public void UpdateName(string audio)
    {
        //audioTransform.name = audio;
    }*/

    public void PlayAudio(string audio)
    {

        foreach (AudioClip item in audioClips)
        {
            if(item.name == audio)
            {
                audioSource.clip = item;
                audioSource.Play();
                
            }
        }
        

        /*
        if (AudioClips.Count > 0)
        {
            AudioSource.clip = AudioClips[0];
            AudioSource.Play();
        }*/
    }
}

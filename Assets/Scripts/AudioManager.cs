
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource UISource;

    public List<_Audio> MusicList;
    public List<_Audio> SFXList;
    public List<_Audio> SFXListUI;

    [System.Serializable]
    public class _Audio {

        public string audioName;
        public AudioClip audioClip;

    }

    private void Start()
    {
        _Audio startMusic = MusicList.FirstOrDefault(audio => audio.audioName == "bg-1");
        musicSource.clip = startMusic.audioClip;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void ChangeBackGroundMusic(string name)
    {
        _Audio changeMusic = MusicList.First(audio => audio.audioName == name);
        musicSource.clip = changeMusic.audioClip;
        musicSource.PlayDelayed(1.5f);

    }

    public void PlaySoundFx(string name)
    {
        _Audio changeSound = SFXList.FirstOrDefault(audio => audio.audioName == name);
        SFXSource.clip = changeSound.audioClip;
        SFXSource.Play();

    }

    public void PlaySoundUI(string name)
    {
        _Audio changeSound = SFXListUI.FirstOrDefault(audio => audio.audioName == name);
        UISource.clip = changeSound.audioClip;
        UISource.Play();

    }


}


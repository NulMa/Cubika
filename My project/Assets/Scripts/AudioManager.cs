using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static AudioManager;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Drop, Merged, Land, Move}


    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        Init();
        PlayBgm(true);
    }

    private void Update() {

        volCtrl();
    }


    public void volCtrl() {

        bgmPlayer.volume = GameManager.instance.bgmVol.value;

        for (int index = 0; index < sfxPlayers.Length; index++) {
            sfxPlayers[index].volume = GameManager.instance.sfxVol.value;
        }
    }



    public void Init() {
        //BGM player
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = true;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        //SFX Player
        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for (int index = 0; index < sfxPlayers.Length; index++) {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
        }
    }
    public void PlayBgm(bool isPlay) {
        if (isPlay) {
            bgmPlayer.Play();
        }
        else {
            bgmPlayer.Stop();
        }
    }


    public void PlaySfx(Sfx sfx, int num) {

        for (int index = 0; index < sfxPlayers.Length; index++) {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;


            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + num];
            sfxPlayers[loopIndex].Play();

            break;

        }
    }


}

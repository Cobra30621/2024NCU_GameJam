using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Audio;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    private static BGMManager _instance;
    public static BGMManager Instance => _instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float defaultVolume = 0.5f;
    [SerializeField] private float fadeSpeed = 1f;
    
    // 用於存儲背景音樂片段的字典
    [SerializeField] private Dictionary<string, AudioClip> bgmClips = new Dictionary<string, AudioClip>();

    private string currentBGM = "";
    private bool isFading = false;

    [SerializeField] private BGMConfigSO bgmConfig;

    private void Awake()
    {
        // 單例模式設置
        if (_instance == null)
        {
            _instance = this;
        }
        
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        
        // 初始化音訊字典
        InitializeAudioDictionary();
    }
    
    private void OnDestroy()
    {
        _instance = null;
    }

    private void InitializeAudioDictionary()
    {
        bgmClips.Clear();
        foreach (var audioData in bgmConfig.audioList)
        {
            bgmClips[audioData.id] = audioData.clip;
        }
    }

    // 播放背景音樂
    [Button]
    public void PlayBGM(string bgmName, bool fadeIn = true)
    {
        AudioData audioData = bgmConfig.GetAudioData(bgmName);
        if (audioData == null)
        {
            Debug.LogWarning($"背景音樂 {bgmName} 未找到！");
            return;
        }

        if (currentBGM == bgmName) return;

        currentBGM = bgmName;
        defaultVolume = audioData.volume;
        
        if (fadeIn)
        {
            StartCoroutine(FadeInBGM(bgmName));
        }
        else
        {
            audioSource.clip = audioData.clip;
            audioSource.volume = defaultVolume;
            audioSource.Play();
        }
    }

    // 停止背景音樂
    public void StopBGM(bool fadeOut = true)
    {
        if (fadeOut)
        {
            StartCoroutine(FadeOutBGM());
        }
        else
        {
            audioSource.Stop();
            currentBGM = "";
        }
    }

    // 設置音量
    public void SetVolume(float volume)
    {
        defaultVolume = Mathf.Clamp01(volume);
        if (!isFading)
        {
            audioSource.volume = defaultVolume;
        }
    }

    // 添加背景音樂到字典
    public void AddBGM(string bgmName, AudioClip clip)
    {
        if (!bgmClips.ContainsKey(bgmName))
        {
            bgmClips.Add(bgmName, clip);
        }
    }

    // 淡入效果
    private IEnumerator FadeInBGM(string bgmName)
    {
        isFading = true;
        AudioData audioData = bgmConfig.GetAudioData(bgmName);
        audioSource.clip = audioData.clip;
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < defaultVolume)
        {
            audioSource.volume += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        audioSource.volume = defaultVolume;
        isFading = false;
    }

    // 淡出效果
    private IEnumerator FadeOutBGM()
    {
        isFading = true;
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        audioSource.Stop();
        currentBGM = "";
        isFading = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance => GameManager.Instance.SfxManager;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float defaultVolume = 1f;
    
    // 用於存儲音效片段的字典
    [SerializeField] private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    [SerializeField] private SFXConfigSO sfxConfig;

    private void Awake()
    {
        // 單例模式設置
        // 獲取AudioSource組件
        audioSource = GetComponent<AudioSource>();
        
        // 初始化音訊字典
        InitializeAudioDictionary();
    }

    private void InitializeAudioDictionary()
    {
        soundEffects.Clear();
        foreach (var audioData in sfxConfig.audioList)
        {
            soundEffects[audioData.id] = audioData.clip;
        }
    }

    // 播放音效的方法
    [Button]
    public void PlaySound(string soundName)
    {
        AudioData audioData = sfxConfig.GetAudioData(soundName);
        if (audioData == null)
        {
            Debug.LogWarning($"音效 {soundName} 未找到！");
            return;
        }

        audioSource.PlayOneShot(audioData.clip, audioData.volume);
    }

    // 播放音效並指定音量
    public void PlaySound(string soundName, float volumeMultiplier)
    {
        AudioData audioData = sfxConfig.GetAudioData(soundName);
        if (audioData == null)
        {
            Debug.LogWarning($"音效 {soundName} 未找到！");
            return;
        }

        audioSource.PlayOneShot(audioData.clip, audioData.volume * volumeMultiplier);
    }

    // 添加音效到字典中
    public void AddSound(string soundName, AudioClip clip)
    {
        if (!soundEffects.ContainsKey(soundName))
        {
            soundEffects.Add(soundName, clip);
        }
    }
}
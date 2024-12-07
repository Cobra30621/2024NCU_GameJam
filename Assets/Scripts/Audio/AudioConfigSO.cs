using UnityEngine;
using System.Collections.Generic;

public abstract class AudioConfigSO : ScriptableObject
{
    public List<AudioData> audioList = new List<AudioData>();

    public AudioData GetAudioData(string id)
    {
        return audioList.Find(audio => audio.id == id);
    }
}

[CreateAssetMenu(fileName = "BGMConfig", menuName = "Audio/BGM Config")]
public class BGMConfigSO : AudioConfigSO { }

[CreateAssetMenu(fileName = "SFXConfig", menuName = "Audio/SFX Config")]
public class SFXConfigSO : AudioConfigSO { }

[System.Serializable]
public class AudioData
{
    public string id;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
}
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FmodAudioManager : MonoBehaviour
{
  public static FmodAudioManager Instance { get; private set; }
  public EventInstance ambienceEventInstance;

  public EventInstance areaEventInstance;


    private void Awake()
    {
        if (Instance != null)
        {
           Debug.LogError("Multiple instances of FmodAudioManager detected!"); 
        }
        Instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 position)
    {
        var instance = RuntimeManager.CreateInstance(sound);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(position));
        instance.start();
        instance.release();
    }

    public void StopOneShot(EventReference sound)
    {
        
        var instance = RuntimeManager.CreateInstance(sound);
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.release();
    }

    public void SetMusicArea(Area area)
    {
        areaEventInstance.setParameterByName("Area", (float) area);
    }

    public void SetAmbienceParameter(string paramName, float value)
    {
        ambienceEventInstance.setParameterByName(paramName, value);
    }
}

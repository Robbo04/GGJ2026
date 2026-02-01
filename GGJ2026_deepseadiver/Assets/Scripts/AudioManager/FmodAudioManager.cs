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
        RuntimeManager.PlayOneShot(sound, position);
    }

    public void SetMusicArea(Area area)
    {
        areaEventInstance.setParameterByName("Area", (float) area);
    }

    public void SetAmbienceParameter(string paramName, float value)
    {
        ambienceEventInstance.setParameterByName(paramName, value);
        Debug.Log("parameter triggered");
    }

    public void stopPlaying()
    {
        areaEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ambienceEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}

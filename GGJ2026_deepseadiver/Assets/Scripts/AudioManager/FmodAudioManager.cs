using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

public class FmodAudioManager : MonoBehaviour
{
  public static FmodAudioManager Instance { get; private set; }
  public FMOD.Studio.EventInstance EventInstance;

  public FMOD.Studio.EventInstance areaEventInstance;

  [SerializeField] private GameObject QTEManager;

  public EventReference example;


    private void Awake()
    {
        if (Instance != null)
        {
           Debug.LogError("Multiple instances of FmodAudioManager detected!");
        }
        Instance = this;
    }

    public void PlayOneShot(EventInstance sound, Vector3 position)
    {
        EventInstance instance = sound;
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

    public void SetWaterArea(string paramName, Area area)
    {
        areaEventInstance.setParameterByName(paramName, (float) area);
    }

    public void SetAmbienceParameter(string paramName, Looping loop)
    {
        EventInstance.setParameterByName(paramName, (float) loop, false);
    }
}

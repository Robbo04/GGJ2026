using FMODUnity;
using UnityEngine;

public class AreaChangeTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header ("parameter change")]

    [SerializeField] private Area area;
    [SerializeField] private EventReference ambienceEvent;
    private FMOD.Studio.EventInstance instance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            instance = RuntimeManager.CreateInstance("event:/CageDownFixed");
                instance.start();
                FmodAudioManager.Instance.SetAmbienceParameter("SetLooping", 1.0f);
                instance.getParameterByName("Area", out float currentArea);
                Debug.Log(currentArea);
                instance.release();
                
        }
    }
 
}

using FMODUnity;
using UnityEngine;

public class AreaChangeTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header ("parameter change")]

    [SerializeField] private Looping loop;
    [SerializeField] public GameObject AudioManager;
    private FMOD.Studio.EventInstance instance;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            AudioManager.GetComponent<FmodAudioManager>().EventInstance = AudioManager.GetComponent<FmodAudioManager>().moveCrainSound;
            instance = AudioManager.GetComponent<FmodAudioManager>().EventInstance;
            FmodAudioManager.Instance.SetAmbienceParameter("SetLooping", loop);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FmodAudioManager.Instance.SetAmbienceParameter("SetLooping", loop - 1);
        }
    }
 
}

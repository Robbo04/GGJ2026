using FMODUnity;
using UnityEngine;

public class AreaChangeTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header ("parameter change")]

    [SerializeField] private Looping loop;
    [SerializeField] public GameObject QTEManager;
    [SerializeField] public GameObject AudioManager;
    private FMOD.Studio.EventInstance instance;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            AudioManager.GetComponent<FmodAudioManager>().EventInstance = QTEManager.GetComponent<KeySelector>().moveCrainSound;
            instance = AudioManager.GetComponent<FmodAudioManager>().EventInstance;
            FmodAudioManager.Instance.SetAmbienceParameter("SetLooping", loop);
        }
    }
 
}

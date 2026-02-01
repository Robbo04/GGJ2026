using UnityEngine;

public class AreaChangeTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header ("parameter change")]

    [SerializeField] private Area area;

    private void OnTriggerEnter(Collider collision)
    {
       if(collision.CompareTag("Player"))
       {
            FmodAudioManager.Instance.SetMusicArea(area);
            FmodAudioManager.Instance.stopPlaying();
            Debug.Log("Area changed to: " + area.ToString());
           
       }
    }
}

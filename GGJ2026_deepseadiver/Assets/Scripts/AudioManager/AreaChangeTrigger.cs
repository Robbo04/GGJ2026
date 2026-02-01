using UnityEngine;

public class AreaChangeTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header ("parameter change")]

    [SerializeField] private Area area;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag.Equals("Player"))
       {
           FmodAudioManager.Instance.SetMusicArea(area);
       }
    }
}

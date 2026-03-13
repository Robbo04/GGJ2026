using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header ("parameter change")]

    [SerializeField] private string parameterName;
    [SerializeField] private Area area;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       FmodAudioManager.Instance.SetWaterArea(parameterName, area);
    }
}

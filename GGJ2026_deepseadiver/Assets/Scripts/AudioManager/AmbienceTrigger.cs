using UnityEngine;

public class AmbienceTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header ("parameter change")]

    [SerializeField] private string parameterName;
    [SerializeField] private float parameterValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       FmodAudioManager.Instance.SetAmbienceParameter(parameterName, parameterValue);
    }
}

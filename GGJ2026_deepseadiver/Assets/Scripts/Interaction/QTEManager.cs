using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] public GameObject QTE;
    public void StartMinigame()
    {
        QTE.GetComponent<KeySelector>().enabled = true;
        Debug.Log("Minigame Started");
    }
}

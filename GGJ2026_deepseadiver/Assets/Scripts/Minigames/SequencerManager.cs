using FMODUnity;
using UnityEngine;

public class SequencerManager : MonoBehaviour
{
    [Header("Activation Settings")]
    public GameObject lever; // Scripts to enable when lever is pulled down
    

    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject AudioManager;
    public GameObject leverCover;
    public int correctButtons;

    void Start()
    {
        correctButtons = 0;
        //print(buttons.Length);
    }

    // Update is called once per frame
    public void UpdateButtons()
    {
        correctButtons = 0;

        foreach (GameObject button in buttons)
        {
            if (button.GetComponent<ButtonToggle>().isOn)
            {
                correctButtons++;
                print("button pressed");
                AudioManager.GetComponent<FmodAudioManager>().PlayOneShot(AudioManager.GetComponent<FmodAudioManager>().buttonSound, transform.position);
            }
        }
        print(correctButtons);
        if (correctButtons == buttons.Length)
        {
            Debug.Log("Minigame Complete");
            Destroy(leverCover);
            // Enable all assigned scripts
            lever.GetComponent<Outline>().enabled = true;
            lever.GetComponent<Interactable>().enabled = true;
            FmodAudioManager.Instance.PlayOneShot(AudioManager.GetComponent<FmodAudioManager>().completeSound, transform.position);

        }

    }
}

using UnityEngine;

public class SequencerManager : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    public GameObject leverCover;
    public int correctButtons;

    void Start()
    {
        correctButtons = 0;
        print(buttons.Length);
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
            }
        }
        print(correctButtons);
        if (correctButtons == buttons.Length)
        {
            Debug.Log("Minigame Complete");
            Destroy(leverCover);
        }

    }
}

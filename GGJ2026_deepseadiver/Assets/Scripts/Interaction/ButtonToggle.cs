using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
    [SerializeField] Material materialOn;
    [SerializeField] Material materialOff;

    [SerializeField] GameObject[] influencedObjects;

    public bool isOn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void ButtonToggleState()
    {
        foreach (GameObject influenced in influencedObjects)
        {
            influenced.GetComponent<ButtonToggle>().toggleLight();
        }
    }

    public void toggleLight()
    {
        isOn = !isOn;

        if (isOn)
        {
            this.GetComponent<MeshRenderer>().material = materialOn;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = materialOff;
        }

        FindObjectOfType<SequencerManager>().UpdateButtons();
    }
}

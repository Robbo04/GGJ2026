using UnityEngine;
using FMODUnity;
using UnityEditor.UI;


public class ButtonToggle : MonoBehaviour
{
    [SerializeField] Material materialOn;
    [SerializeField] Material materialOff;

    [SerializeField] GameObject[] influencedObjects;

    [SerializeField] private EventReference buttonSound; 

    [SerializeField] public GameObject gameObject;



    public bool isOn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void ButtonToggleState()
    {
        foreach (GameObject influenced in influencedObjects)
        {
            influenced.GetComponent<ButtonToggle>().toggleLight();
            gameObject.GetComponent<StudioEventEmitter>().Play();
        }
        FindObjectOfType<SequencerManager>().UpdateButtons();
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

        
    }
}

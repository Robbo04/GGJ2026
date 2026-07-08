using Unity.VisualScripting;
using UnityEngine;

public class ScreenButtonLogicScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Material[] buttonColors;
    
    int currentColorIndex = 0;
    
    enum ButtonState
    {
        tap,
        toggle,
        hold
    }

    [SerializeField] private ButtonState state;



    public void OnInteract()
    {
        print("Button Interacted");
        if (buttonColors != null && buttonColors.Length > 0)
        {
            if (currentColorIndex < buttonColors.Length - 1)
            {
                currentColorIndex++;
            }
            else
            {
                currentColorIndex = 0;
            }
            print("Current Color Index: " + currentColorIndex);
            this.GetComponent<Renderer>().material = buttonColors[currentColorIndex];    
        }
    }
}

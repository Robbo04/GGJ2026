using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Splines;
using TMPro;

public class KeySelector : MonoBehaviour
{
    [SerializeField] public GameObject imageRef;
    string c;
    int score = 0;
    int maxScore = 3;

    public float targetTime = 2.0f;
    private bool isListenerRegistered = false;
    string pressedString = "";
    string sequenceToRepeat = "";
    bool waitingForInput = false;

    [SerializeField] public GameObject player;
    public TextMeshProUGUI textref;

    public Sprite spriteA, spriteB, spriteC, spriteD, spriteE, spriteF, spriteG, spriteH, spriteI, spriteJ, 
    spriteK, spriteL, spriteM, spriteN, spriteO, spriteP, spriteQ, spriteR, spriteS, 
    spriteT, spriteU, spriteV, spriteW, spriteX, spriteY, spriteZ;    

    
    public static Dictionary<char, Sprite> charToSprite = new Dictionary<char, Sprite> ();

    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charToSprite.Add('A', spriteA);
        charToSprite.Add('B', spriteB);
        charToSprite.Add('C', spriteC);
        charToSprite.Add('D', spriteD);
        charToSprite.Add('E', spriteE); 
        charToSprite.Add('F', spriteF);
        charToSprite.Add('G', spriteG);
        charToSprite.Add('H', spriteH);
        charToSprite.Add('I', spriteI);
        charToSprite.Add('J', spriteJ);
        charToSprite.Add('K', spriteK);
        charToSprite.Add('L', spriteL);
        charToSprite.Add('M', spriteM);
        charToSprite.Add('N', spriteN);
        charToSprite.Add('O', spriteO);
        charToSprite.Add('P', spriteP);
        charToSprite.Add('Q', spriteQ);
        charToSprite.Add('R', spriteR);
        charToSprite.Add('S', spriteS);
        charToSprite.Add('T', spriteT);
        charToSprite.Add('U', spriteU);
        charToSprite.Add('V', spriteV);
        charToSprite.Add('W', spriteW);
        charToSprite.Add('X', spriteX);
        charToSprite.Add('Y', spriteY);
        charToSprite.Add('Z', spriteZ);
        
        // Hide the image initially
        imageRef.GetComponent<Image>().enabled = false;
  
        StartCoroutine(ShowSequence());
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingForInput)
        {
            HandlePlayerInput();
        }
    }

    char GetCharFromKey(KeyControl key)
    {
        // Convert KeyCode to character
        string keyName = key.name.ToUpper();
        
        // For letter keys (a, b, c, etc.)
        if (keyName.Length == 1 && char.IsLetter(keyName[0]))
        {
            return keyName[0];
        }
        return '\0';
    }

    IEnumerator ShowSequence()
    {
        sequenceToRepeat = "";
        // Show image during sequence
        imageRef.GetComponent<Image>().enabled = true;
        
        for (int round = 0; round < maxScore; round++)
        {
            c = GenerateRandomLetter().ToString();
            sequenceToRepeat += c;
            Debug.Log($"Round {round + 1}: Show '{c}' | Full Sequence: {sequenceToRepeat}");
            
            if (charToSprite.ContainsKey(c[0]))
            {
                
                imageRef.GetComponent<Image>().sprite = charToSprite[c[0]];
                textref.text = "Count: " + (round + 1).ToString();
            }
            
            yield return new WaitForSeconds(targetTime);
        }
        
        Debug.Log($"Sequence complete! Repeat this: {sequenceToRepeat}");
        pressedString = "";
        waitingForInput = true;
    }
    
    void HandlePlayerInput()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            foreach (var key in keyboard.allKeys)
            {
                if (key.wasPressedThisFrame)
                {
                    char pressedChar = GetCharFromKey(key);

                    if (pressedChar != '\0')
                    {
                        pressedString += pressedChar;
                        Debug.Log($"Player pressed: {pressedChar} | Input so far: {pressedString}");
                        
                        // Display the pressed image
                        if (charToSprite.ContainsKey(pressedChar))
                        {
                            imageRef.GetComponent<Image>().sprite = charToSprite[pressedChar];
                        }

                        // Check if input matches sequence so far
                        if (pressedString.Length <= sequenceToRepeat.Length && 
                            sequenceToRepeat.Substring(0, pressedString.Length) == pressedString)
                        {
                            // Correct so far
                            if (pressedString == sequenceToRepeat)
                            {
                                // Complete correct sequence!
                                Debug.Log("Perfect! You completed the sequence!");
                                score++;
                                waitingForInput = false;
                                targetTime = targetTime / 2;

                                
                                if (score < maxScore)
                                {
                                    Debug.Log($"Score: {score}/{maxScore}. Next round starting...");
                                    StartCoroutine(ShowSequence());
                                }
                                else
                                {
                                    Debug.Log("All rounds complete! You won!");
                                    // Hide image when game is completed
                                    imageRef.GetComponent<Image>().enabled = false;
                                    player.GetComponent<SplineAnimate>().enabled = true;                              }
                            }
                        }
                        else
                        {
                            // Wrong input!
                            Debug.Log($"Wrong! Expected '{sequenceToRepeat}' but got '{pressedString}'");
                            Debug.Log("Game Over! Restarting...");
                            score = 0;
                            waitingForInput = false;
                            StartCoroutine(ShowSequence());
                        }
                    }
                }
            }
        }
    }
    
    char GenerateRandomLetter()
    {
        int randomIndex = Random.Range(0, 26);
        return (char)('A' + randomIndex);
    }                   
                
}



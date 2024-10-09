using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class DialogueTest : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameComponent;
    public float textSpeed;
    public GameObject player;

    private int index;
    private string[] lines;
    private string[] names;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        lines = script();
        names = namesB();

        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (textComponent.text == lines[index]) {
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    void StartDialogue() {
        player.GetComponent<FirstPersonController>().enabled = false;
        Time.timeScale = 0;
         Debug.Log("Starting dialogue. Line: " + lines[index]);
        index = 0;

nameComponent.text = names[index];
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine() {
        Debug.Log("Typing line...");
        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine() {
        index++;
        if (index < lines.Length && !string.IsNullOrEmpty(lines[index])) {
            textComponent.text = string.Empty;
            nameComponent.text = string.Empty;
            nameComponent.text = names[index];
            StartCoroutine(TypeLine());
        }
        else {
            gameObject.SetActive(false);
            player.GetComponent<FirstPersonController>().enabled = true;
            Time.timeScale = 1;
        }
    }
    string[] script() {
        string[] theLines = new string[30];
        //tier 0 script when the player starts the game
        theLines[0] = "I see you are the latest competitor";
        theLines[1] = "I do not remember meeting you before, so I assume you are new.";
        theLines[2] = "In that case, I will go easy on you. Not too sure if my assistants will however";
        theLines[3] = "What do you mean by assistants!? We’re all on the same team with the same power!";
        theLines[4] = "Yeah, come on Jav, you cannot have everything to yourself. Do I need to remind you what happened the last time we ordered pizza?";
        theLines[5] = "Ok fine. So, do one of you two want to explain the game rules to our new challenger?";
        theLines[6] = "Sure, I’ll do it. Your goal in this game is to make it to the end of this dungeon. Do it by whatever means necessary.";
        theLines[7] = "If you die, it is game over. I believe that it is enough information. It is pretty simple, right?";
        theLines[8] = "Agreed.";
        theLines[9] = "Ok, good luck out there. Well, at least as much as you can have.";
        //tier 1
        return theLines;
    }
    string[] namesB() {
        //tier 0
        string[] theNames = new string[30];
        theNames[0] = "Jav Ascript";
        theNames[1] = "Jav Ascript";
        theNames[2] = "Jav Ascript";
        theNames[3] = "C. Plusplus";
        theNames[4] = "C. Sharp";
        theNames[5] = "Jav Ascript";
        theNames[6] = "C. Sharp";
        theNames[7] = "C. Plusplus";
        theNames[8] = "Jav Ascript & C. Sharp";
        theNames[9] = "Jav Ascript";
        //tier 1
        // theNames[10] = "Jav Ascript";
        // theNames[11] = "Jav Ascript";
        // theNames[12] = "Jav Ascript";
        // theNames[13] = "Jav Ascript";
        // theNames[14] = "Jav Ascript";
        // theNames[15] = "Jav Ascript";
        // theNames[16] = "Jav Ascript";
        // theNames[17] = "Jav Ascript";
        // theNames[18] = "Jav Ascript";
        // theNames[19] = "Jav Ascript";
        // theNames[20] = "Jav Ascript";
        // theNames[21] = "Jav Ascript";
        // theNames[22] = "Jav Ascript";
        // theNames[23] = "Jav Ascript";
        // theNames[24] = "Jav Ascript";
        // theNames[25] = "Jav Ascript";
        // theNames[26] = "Jav Ascript";
        // theNames[27] = "Jav Ascript";
        // theNames[28] = "Jav Ascript";
        // theNames[29] = "Jav Ascript";
        return theNames;
    }
}

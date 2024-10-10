using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameComponent;
    public float textSpeed;
    public GameObject player;

    private int index;
    private string[] lines;
    private string[] names;
    private IEnumerator TypeLineCoroutine; // Correct placement for coroutine reference
    public int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        //zeroTierScript();
    }

    // Update is called once per frame
    void Update() {
    if (Input.GetMouseButtonDown(0)) {
        if (TypeLineCoroutine != null) {
            // Stop the coroutine and show the full text only if it is currently typing
            StopCoroutine(TypeLineCoroutine);
            textComponent.text = lines[index]; // Show the full line
            TypeLineCoroutine = null; // Reset coroutine reference
            Debug.Log("Coroutine stopped, full text displayed: " + textComponent.text);
        } else if (textComponent.text == lines[index]) {
            // Only proceed to the next line if the current line is fully displayed
            NextLine();
        }
    }
}
    
    void StartDialogue() {
        textComponent.text = string.Empty;
        player.GetComponent<FirstPersonController>().enabled = false;
        Time.timeScale = 0;
        index = 0;
         Debug.Log("Starting dialogue. Line: " + lines[index]);
nameComponent.text = names[index];
 TypeLineCoroutine = TypeLine(); // Start typing coroutine
        StartCoroutine(TypeLineCoroutine);
    }
    IEnumerator TypeLine() {
          textComponent.text = "";
        Debug.Log("Typing line...");
        // Type each character 1 by 1

        // Append the first character to text
        if (count == 0) {
        if (lines[index].Length > 0) {
            textComponent.text += lines[index][0]; // Start with the first character
            yield return new WaitForSecondsRealtime(textSpeed);
            count++;
        }
        }
        
      foreach (char c in lines[index].ToCharArray()) {
              textComponent.text += c;
             // Debug.Log("Typed character: " + c); 
              yield return new WaitForSecondsRealtime(textSpeed);
          }
      
        Debug.Log("Coroutine ended");
        TypeLineCoroutine = null; // Set coroutine to null after it's done

    }
    void NextLine() {
        index++;
        if (index < lines.Length && !string.IsNullOrEmpty(lines[index])) {
            textComponent.text = string.Empty;
            nameComponent.text = names[index];
            TypeLineCoroutine = TypeLine(); // Start next line coroutine
            StartCoroutine(TypeLineCoroutine);
        }
        else {
            gameObject.SetActive(false);
            player.GetComponent<FirstPersonController>().enabled = true;
            Time.timeScale = 1;
        }
    }
    //tier 0 script when the player starts the game
   public void zeroTierScript() {
        lines = new string[10];
        lines[0] = "I see you are the latest competitor";
        lines[1] = "I do not remember meeting you before, so I assume you are new.";
        lines[2] = "In that case, I will go easy on you. Not too sure if my assistants will however";
        lines[3] = "What do you mean by assistants!? We're all on the same team with the same power!";
        lines[4] = "Yeah, come on Jav, you cannot have everything to yourself. Do I need to remind you what happened the last time we ordered pizza?";
        lines[5] = "Ok fine. So, do one of you two want to explain the game rules to our new challenger?";
        lines[6] = "Sure, I'll do it. Your goal in this game is to make it to the end of this dungeon. Do it by whatever means necessary.";
        lines[7] = "If you die, it is game over. I believe that it is enough information. It is pretty simple, right?";
        lines[8] = "Agreed.";
        lines[9] = "Ok, good luck out there. Well, at least as much as you can have.";
//tier 0 names that correspond to their lines
        names = new string[10];
        names[0] = "Jav Ascript";
        names[1] = "Jav Ascript";
        names[2] = "Jav Ascript";
        names[3] = "C. Plusplus";
        names[4] = "C. Sharp";
        names[5] = "Jav Ascript";
        names[6] = "C. Sharp";
        names[7] = "C. Plusplus";
        names[8] = "Jav Ascript & C. Sharp";
        names[9] = "Jav Ascript";
        StartDialogue();
    }
    //tier one lines after the player complete the dungeon the first time
    public void firstTierScript() {
        lines = new string[6];
        lines[0] = "I'm sure you're confused why you're back in this room.";
        lines[1] = "It turns out we lied a little bit. OOPS!";
        lines[2] = "Well Jav always lies and changes his mind. Yeah, so you will have to clear through this dungeon again. Except this time, you will make it more difficult for yourself.";
        lines[3] = "It is not to be mean; it is just how the game works. But don't worry: it's not all bad news!";
        lines[4] = "You are going to choose to add a new enemy, trap, or room to this dungeon. But this will give you more points to your high score. Everyone likes an even higher high score, right? It's cool!";
        lines[5] = "Ok, add your new obstacle, and clear the dungeon again! Wishing you a merry dungeon run and a happy new high score!";
        
        names = new string[6];
        names[0] = "C. Sharp";
        names[1] = "Jav Ascript";
        names[2] = "C. Plusplus";
        names[3] = "Jav Ascript";
        names[4] = "C. Sharp";
        names[5] = "Jav Ascript";
        StartDialogue();
    }
//tier 2 lines
    public void secondTierScripts() {
        lines = new string[5];
        lines[0] = "How did I get into this mess?";
        lines[1] = "That's what I wonder too. I feel tricked!";
        lines[2] = "I heard there is a way for each player to escape their dungeon.";
        lines[3] = "Do you know what it is?";
        lines[4] = "Shhhh.. I heard one of the emperors coming towards us!";
        
        names = new string[5];
        names[0] = "Competitor 1";
        names[1] = "Competitor 2";
        names[2] = "Competitor 1";
        names[3] = "Competitor 2";
        names[4] = "Competitor 1";
        StartDialogue();
    }
    //tier three lines
    public void thirdTierScript() {
        lines = new string[10];
        lines[0] = "Ah yes, we meet again.";
        lines[1] = "That was supposed to be my line!";
        lines[2] = "C said you can't have everything to yourself, Jav.";
        lines[3] = "Which one of you two said that!?";
        lines[4] = "Anyways challenger, We came here to tell you just how great of a job you are doing, even though you are stuck here basically forever.";
        lines[5] = "Well, there may be a way out for you. Not that I would want you to find that out, because I enjoy watching you play this game.";
        lines[6] = "Only Gary has ever escaped successfully. Most players struggle to escape.";
        lines[7] = "Does the player even know who Gary is?";
        lines[8] = "I have said too much. I won't be in trouble, will I?";
        lines[9] = "Don't be upset. I'm just happy the player hasn't bombed the place yet. Wait I wasn't supposed to say the yet part. STOP TALKING!";
       
       names = new string[10];
       names[0] = "C. Plusplus";
        names[1] = "Jav Ascript";
        names[2] = "C. Plusplus";
        names[3] = "Jav Ascript";
        names[4] = "C. Sharp";
        names[5] = "Jav Ascript";
        names[6] = "C. PlusPlus";
        names[7] = "Jav Ascript";
        names[8] = "C. PlusPlus";
        names[9] = "Jav Ascript";
        StartDialogue();
    }
}

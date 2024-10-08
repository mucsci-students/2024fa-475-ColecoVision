using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    //Dialogue/text will be stored as an array of strings (was going to do vector/list).
    //Will use a for loop to go through the appropriate indexes.
    //Print the text/string out like how we did in the book's games (amazing racer, chaos ball)
    //Will need to add some lines or a method or something to space out the prints.


    // Start is called before the first frame update
    void Start()
    {
        //for testing purposes.
        print(Script());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //List acts as vector would in c++ or arraylist in java ??
    string[] Script()
    {
        //List<string> theLines = new List<string>();
        string[] theLines = new string[50];
        
        //***********************************
        //this loop is a test.
        //for (int i = 0; i <= 10; i++)
        //{
            //theLines.Add(i.ToString());
            //print(theLines[i]);
        //}
        //***********************************

        //TheLines for Tier 0.
        theLines[0] = "I see you are the latest competitor";
        theLines[1] = "I do not remember meeting you before, so I assume you are new.";
        theLines[2] = "In that case, I will go easy on you. Not too sure if my assistants will however";
        theLines[3] = "What do you mean by “assistants!?” We’re all on the same team with the same power!";
        theLines[4] = "Yeah, come on Jav, you cannot have everything to yourself. Do I need to remind you what happened the last time we ordered pizza? You started shouting that everyone needed to give their slices over to you!";
        theLines[5] = "Ok fine. So, do one of you two want to explain the game rules to our new challenger?";
        theLines[6] = "Sure, I’ll do it. Your goal in this game is to make it to the end of this dungeon. Do it by whatever means necessary. As long as you do not die, you will get a high score.";
        theLines[7] = "If you die, it is game over. I believe that it is enough information. It is pretty simple, right?";
        theLines[8] = "Agreed.";
        theLines[9] = "Ok, good luck out there. Well, at least as much as you can have.";

        //will have to add an extra line in the loop or something to not make this all spam out at once, and also to see it on-screen.
        for (int tZero = 0; tZero <= 9; tZero++)
        {
            //theLines.Add(theLines[tZero]);
            print(theLines[tZero]);
        }

        //TheLines for Tier 1.
        theLines[10] = "I’m sure you’re confused why you’re back in this room.";
        theLines[11] = "It turns out we lied a little bit. OOPS!";
        theLines[12] = "Well Jav always lies and changes his mind. Yeah, so you will have to clear through this dungeon again. Except this time, you will make it more difficult for yourself.";
        theLines[13] = "It is not to be mean; it is just how the game works. But don’t worry: it’s not all bad news!";
        theLines[14] = "You are going to choose to add a new enemy, trap, or room to this dungeon. But this will give you more points to your high score. Everyone likes an even higher high score, right? It's cool!";
        theLines[15] = "Ok, add your new obstacle, and clear the dungeon again! Wishing you a merry dungeon run and a happy new high score!";

        //see tZero loop.
        for (int tOne = 10; tOne <= 15; tOne++)
        {
            print(theLines[tOne]);
        }

        //TheLines for Tier 2.
        theLines[16] = "How did I get into this mess?";
        theLines[17] = "That’s what I wonder too. I feel tricked!";
        theLines[18] = "I heard there is a way for each player to escape their dungeon.";
        theLines[19] = "Do you know what it is?";
        theLines[20] = "Shhhh.. I heard one of the emperors coming towards us!";

        //see tZero loop.
        for (int tTwo = 16; tTwo <= 20; tTwo++)
        {
            print(theLines[tTwo]);
        }

        //TheLines for Tier 3.
        theLines[21] = "Ah yes, we meet again.";
        theLines[22] = "That was supposed to be my line!";
        theLines[23] = "C said you can’t have everything to yourself, Tyler.";
        theLines[24] = "Which one of you two said that!?";
        theLines[25] = "Anyways challenger, We came here to tell you just how great of a job you are doing, even though you are stuck here basically forever.";
        theLines[26] = "Well, there may be a way out for you. Not that I would want you to find that out, because I enjoy watching you play this game.";
        theLines[27] = "Only Gary has ever escaped successfully. Most players struggle to escape.";
        theLines[28] = "Does the player even know who Gary is?";
        theLines[29] = "I have said too much. I won’t be in trouble, will I?";
        theLines[30] = "Don’t be upset. I’m just happy the player hasn’t bombed the place yet. Wait I wasn’t supposed to say the yet part. STOP TALKING!";

        //see tZero loop.
        for (int tThree = 21; tThree <= 30; tThree++)
        {
            print(theLines[tThree]);
        }

        //**********************************
        return theLines;
    }

}

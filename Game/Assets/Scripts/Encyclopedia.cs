using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Encyclopedia : MonoBehaviour
{
    private string[] contents;
    private string[] descriptions;

    // Start is called before the first frame update
    void Start()
    {
        contents = Contents();
        descriptions = Descriptions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string[] Contents()
    {
        //0 - 2 = enemies, 3 - 6 = traps, 7 - 9 = collectibles.
        contents = new string[10];

        //enemies
        contents[0] = "goblin";
        contents[1] = "skeleton";
        contents[2] = "spider";

        //traps
        contents[3] = "spikes";
        contents[4] = "lava";
        contents[5] = "webs";
        contents[6] = "trapdoor";

        //collectibles
        contents[7] = "coins";
        contents[8] = "book";
        contents[9] = "LEGEND"; //secret bomb item

        return contents;
    }

    public string[] Descriptions()
    {
        descriptions = new string[10];

        //enemies
        descriptions[0] = "Little ugly green things that follow you. May spawn in groups.";
        descriptions[1] = "Legend says that the skeletons are the remains of past players' bodies and souls. They've been given dark magic powers.";
        descriptions[2] = "EWW! AHH! GET IT AWAY!!! Yeah nobody likes spiders. Well some people do but spiders are disgusting. Who am I to judge? Uh anyways, these will follow you through several rooms and may climb walls. May leave webs behind.";

        //traps
        descriptions[3] = "They prick you really hard and you die.";
        descriptions[4] = "Some people pretend the floor is made of this. Here it really is. Its hot and it burns you and you die.";
        descriptions[5] = "Spiders leave these behind. I don't want to say anymore. Spiders creep me out. THERES ONE ON MY DESK!!";
        descriptions[6] = "Step on it and you fall through!";

        //collectibles
        descriptions[7] = "Looks like everyone is copying mario, huh? These give you 50 points.";
        descriptions[8] = "The thing you're holding. Get your eyes checked! JK, this book tells you all about the dungeon. Well I have to be sort of nice, it may affect my grade. You may earn 500 points, but I'm hoping to earn 100! At least 99?";
        descriptions[9] = "Legend says a player may be able to escape finding this item. Nobody knows for sure what it is, and I'm certainly not telling you.";

        return contents;
    }

    public string SearchAndRead(int contentsIndex)
    {
        //if player clicks on the item from contents[contentsIndex]
        //  then you would call this method

        //return contents[contentsIndex];
        return descriptions[contentsIndex];
    }
}

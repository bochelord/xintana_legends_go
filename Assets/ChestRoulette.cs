using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRoulette : MonoBehaviour {


    public Sprite[] available_chest_contents;
    public GameObject[] chests;

    public int numChests;


	// Use this for initialization
	void Start () {

        chests = new GameObject[numChests];

        GeneratePrizes();

	}
	
	// Update is called once per frame
	void Update () {
		
	}



    /// <summary>
    /// Function that generate the Prizes for all the chests that are included on the list (in principle three)
    /// Possible contents:
    ///     - gold (x amount)
    ///     - herbs (+1 life? + healthbar?)
    ///     - gems (+ another token to play so the gems are the tokens)
    ///     - shells (+ Score? + )
    ///     - nothing (sadness)
    /// Percentage separation is as follows:
    /// - 1 chest empty
    /// - 2 chests prize
    /// </summary>
    private void GeneratePrizes()
    {
        float diceroll = 0;

        foreach (GameObject chest in chests)
        {

            diceroll = RadUtils.d100();

            if (diceroll < 20) // 20% gems
            {

            }
            else if(diceroll >= 20 && diceroll < 40) // 20% gold
            {

            }
            else if (diceroll >=40 && diceroll < 60) // 20% shells
            {

            }
            else if (diceroll >= 60) // empty
            {

            }
        }
    }
}

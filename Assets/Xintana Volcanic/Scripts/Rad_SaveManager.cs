using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rad_SaveManager : MonoBehaviour {

    public static bool debugmode = false;

    public static string xintanaProfileFilename = "xintanaprofile.rad";
    public static string pokedexProfileFilename = "xintanapokedex.rad";
    public static XintanaProfile profile = new XintanaProfile().GenerateXintanaProfile(0);
    public static PokedexProfile pokedex = new PokedexProfile().GeneratePokedex();

    public static void SaveData()
    {
        ES2.Save(profile, xintanaProfileFilename + "?tag=profile-" + profile.profileID); //TODO 
        ES2.Save(pokedex, pokedexProfileFilename);
        Debug.Log("====SAVING PROFILE====");
    }

    public static void LoadData()
    {
        //TODO TODO TODO -----!!!!!!
        // This is commented cause we made the versions not compatible and we store the version number inside the file... (we should put it on another file)
        // Also these things need to be remove prior to release and set the saving system to save the version on a separate file or create the new stuff on a new savefile...
        // Or maybe we can add a version number on the name of the savefile and then make the check before hand...

        if (ES2.Exists(xintanaProfileFilename + "?tag=profile-" + profile.profileID))
        {

            profile = ES2.Load<XintanaProfile>(xintanaProfileFilename + "?tag=profile-" + profile.profileID);
            Debug.Log("Profile " + profile.profileID + " loaded");
        }
        else
        {
            profile = new XintanaProfile().GenerateXintanaProfile(0);
        }

        
        if (ES2.Exists(pokedexProfileFilename))
        {

            pokedex = ES2.Load<PokedexProfile>(pokedexProfileFilename);
            Debug.Log("Pokedex " + pokedex + " loaded");
        }
        else
        {
            pokedex = new PokedexProfile().GeneratePokedex();
            Debug.Log("Pokedex " + pokedex + " created");
        }
        SaveData();
    }

    public static void Button_DebugModeOn()
    {
        debugmode = true;
        Debug.Log("DEBUGMODE " + debugmode);
    }


    public static void OnApplicationQuit()
    {
        SaveData();
    }
}

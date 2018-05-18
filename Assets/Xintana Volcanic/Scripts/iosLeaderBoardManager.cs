using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class iosLeaderBoardManager : MonoBehaviour {


	public string leaderboardID;

	#region GAME_CENTER    
    public void  AuthenticateToGameCenter()
    {
        #if UNITY_IPHONE
        Social.localUser.Authenticate(success =>{
            if (success)
            {
                Debug.Log("Authentication successful");
            }
            else
            {
                Debug.Log("Authentication failed");
            }
        });
        #endif
    }


	public void ReportScore(long score, string leaderboardID)
    {
        #if UNITY_IPHONE
        //Debug.Log("Reporting score " + score + " on leaderboard " + leaderboardID);
        Social.ReportScore(score, leaderboardID, success =>
           {
            if (success)
            {
                Debug.Log("Reported score successfully");
            }
            else
            {
                Debug.Log("Failed to report score");
            }
 
            Debug.Log(success ? "Reported score successfully" : "Failed to report score"); Debug.Log("New Score:"+score);  
        });
        #endif
    }


	public void ShowLeaderboard()
    {
        #if UNITY_IPHONE
            Social.ShowLeaderboardUI();
        #endif
    }
    #endregion


	public void LoginAddScoreLeaderBoard(){
		#if UNITY_IPHONE
		if (Social.localUser.authenticated){
			ReportScore(Rad_SaveManager.profile.highscore,leaderboardID);
		}
		else 
		{
			AuthenticateToGameCenter();
			ReportScore(Rad_SaveManager.profile.highscore,leaderboardID);
		}
		#endif
	}
}


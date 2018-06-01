using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadAmazonLeaderboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(AGSClient.IsServiceReady()) {

            AGSLeaderboardsClient.SubmitScoreSucceededEvent += submitScoreSucceeded;
            AGSLeaderboardsClient.SubmitScoreFailedEvent += submitScoreFailed;

        }
    }

    public void ShowAmazonLeaderboard()
    {
        AGSLeaderboardsClient.ShowLeaderboardsOverlay();
    }

    public void SubmitScoreToAmazon(int score)
    {
        AGSLeaderboardsClient.SubmitScore("leaderboard_high_score", score);
    }

    private void submitScoreSucceeded(string leaderboardId)
    {
        Debug.Log("Score has been submitted successfully to " + leaderboardId);
    }

    private void submitScoreFailed(string leaderboardId, string error)
    {
        Debug.Log("Score has not been submitted to " + leaderboardId);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey =
        "0db418d5eb6cf8b98dd70263163ebdda714f0781bf2c040ebc1b00c2d9599d06";

    private GameManager gameManager;                //Reference GameManager Script.
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();  // Get reference to GameManager
        GetLeaderboard();
        
    }
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => 
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }    
        }));
    }
    public void SetLeaderboardEntry(string username)
    {
        int score = gameManager.GetScore();  // Get score from GameManager
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            GetLeaderboard();  // Refresh the leaderboard after uploading new entry
        }));
        LeaderboardCreator.ResetPlayer();
    }
    /*
    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            username.Substring(0, 2);
            GetLeaderboard();
        }));
    }
    */

}

using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inputScore;
    [SerializeField]
    private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;

    private Leaderboard leaderboard;  // Reference to Leaderboard script
    private void Start()
    {
        leaderboard = FindObjectOfType<Leaderboard>();  // Get reference to Leaderboard script
    }
    public void SubmitScore()
    {
        string playerName = inputName.text;  // Get the player's name from the input field
        string scoreText = inputScore.text;  // Get the score from the input text

        // Attempt to parse the score as an integer
        if (int.TryParse(scoreText, out int playerScore))
        {
            // If successful, pass the playerName and playerScore to the Leaderboard's method
            leaderboard.SetLeaderboardEntry(playerName);
        }
        else
        {
            // Handle invalid input (score is not a valid number)
            Debug.LogError("Invalid score format. Please enter a valid number.");
            // Optionally, you can show a UI error message or highlight the input field
        }
    }
}

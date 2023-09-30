using UnityEngine;

namespace MyGame {
    public class HighScoreManager : MonoBehaviour {
        public TMPro.TextMeshProUGUI scoresList;
        // manages highscore list
        void displayScore() {
            string scoreLines = "";
            int idx = 1;
            foreach (var score in ScoreDisplay.scores) {
                if (idx > 5) { break; }
                if (score == 0) { continue; }
                scoreLines += $"{idx}.{score}\n";
                idx++;
            }
            scoresList.text = scoreLines;
        }

        void Start() {
            ScoreDisplay.GetScores();
            displayScore();
        }
    }
}

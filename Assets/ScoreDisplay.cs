using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public class ScoreDisplay : MonoBehaviour { // displays score to player at end of game
        public TMPro.TextMeshProUGUI score;
        //sorted descending
        public static SortedSet<int> scores = new(Comparer<int>.Create((a,b) => b.CompareTo(a)));
        public static bool gameStart = false;

        void SaveScores() {
            int idx = 1;
            foreach(var score in scores) {
                if (idx > 5) { break; }
                PlayerPrefs.SetInt($"Score{idx}", score);
                idx++;
            }
            PlayerPrefs.Save();
            Debug.Log("Saving scores!");
        }

        public static void GetScores() {
            if (!gameStart) {
                for (int i = 1; i <= 5; i++) {
                    int score = PlayerPrefs.GetInt($"Score{i}");
                    scores.Add(score);
                    Debug.Log(score);
                }
                gameStart = true;
                Debug.Log("Retrieving player scores...");
            }
        }

        void Start() {

            GetScores();

            scores.Add(PlayerPrefs.GetInt("Score"));
            score.text = $"SCORE: {PlayerPrefs.GetInt("Score")}";
            SaveScores();
        }
    }
}

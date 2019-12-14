using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    // Highscore entry that holds score and method type
    [System.Serializable]
    struct HighScoreEntry
    {
        public int score;
    }

    // highscores that holds a list of HighScoreEntries
    struct Highscores
    {
        public List<HighScoreEntry> highscoreEntryList;
    }

    // Highscore UI Elements
    public Transform m_highscoreContainer;
    public Transform m_highscoreTemplate;
    // Highscore entry UI transform
    List<Transform> hsEntryTransforms;
    // List of highScoreEntries
    List<HighScoreEntry> hsEntryList;
    int highestScore;

    public int HighestScore { get { return highestScore; } }

    // Start is called before the first frame update
    void Awake()
    {
        // Check if the player has a highscore and generate their scores
        // max of 5 highscores
        if (PlayerPrefs.HasKey("highscoreTable"))
        {
            string jsonString = PlayerPrefs.GetString("highscoreTable");
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            highscores.highscoreEntryList.Sort((entry1, entry2) => entry2.score.CompareTo(entry1.score));

            hsEntryTransforms = new List<Transform>();

            highestScore = highscores.highscoreEntryList[0].score;

            for (int i = 0; i < 5; i++)
            {
                CreateHighscoreTransform(highscores.highscoreEntryList[i], m_highscoreContainer, hsEntryTransforms);
            }
        }
        else
        {
            // If no highscores exist full table will empty score data
            hsEntryList = new List<HighScoreEntry>()
            {
                new HighScoreEntry { score = 0 },
                new HighScoreEntry { score = 0 },
                new HighScoreEntry { score = 0 },
                new HighScoreEntry { score = 0 },
                new HighScoreEntry { score = 0 }
            };

            hsEntryTransforms = new List<Transform>();

            for (int i = 0; i < 5; i++)
            {
                CreateHighscoreTransform(hsEntryList[i], m_highscoreContainer, hsEntryTransforms);
            }

            Highscores highscores = new Highscores { highscoreEntryList = hsEntryList };
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }
    }

    public void AddHighScoreEntry(int score)
    {
        //Create Highscore Entry
        HighScoreEntry hsEntry = new HighScoreEntry { score = score };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Add new Highscore to Highscores list
        highscores.highscoreEntryList.Add(hsEntry);

        // Save Updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    void CreateHighscoreTransform(HighScoreEntry hsEntry, Transform container, List<Transform> transforms)
    {
        float templateHeight = 100f;

        Transform highscoreTransform = Instantiate(m_highscoreTemplate, container);
        RectTransform highscoreRectTransform = highscoreTransform.GetComponent<RectTransform>();
        highscoreRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transforms.Count);
        highscoreTransform.gameObject.SetActive(true);

        int rank = transforms.Count + 1;
        string rankString;

        switch (rank)
        {
            default: rankString = rank + "th"; break;
            case 1: rankString = "1st"; break;
            case 2: rankString = "2nd"; break;
            case 3: rankString = "3rd"; break;
        }

        highscoreTransform.Find("Position Text").GetComponent<Text>().text = rankString;
        highscoreTransform.Find("Score Text").GetComponent<Text>().text = hsEntry.score.ToString();

        transforms.Add(highscoreTransform);
    }
}

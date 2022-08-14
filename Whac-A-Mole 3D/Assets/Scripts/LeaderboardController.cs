using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class LeaderboardController : MonoBehaviour
{
    // Inspector Properties.
    public GameObject HighscoresPanel;
    public GameObject LoadingPanel;

    // Life Cycle.
    void Start()
    {
        Time.timeScale = 1;
        GetHighscoreList();
    }
    void Update()
    {
        
    }

    // Private Methods.
    private void GetHighscoreList()
    {
        var scoreClient = new PlayerScoreClient();
        var scores = scoreClient.GetHighscores();

        var scoreRows = HighscoresPanel.GetComponentsInChildren<RectTransform>()
            .Where(x => x.name.Contains("Panel"))
            .ToArray();

        for (int i = 0; i < scores.Count; i++)
        {
            var textsComponents = scoreRows[i].GetComponentsInChildren<TextMeshProUGUI>();
            textsComponents[0].text = scores[i].Nome;
            textsComponents[1].text = scores[i].Ponto.ToString();
        }
    }

    // Events.
    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    // Inspector Properties.
    public float CountdownTimer = 60f;
    public float Interval = 50f;
    public float IntervalDecrement = 0.5f;
    public float MinInterval = 15f;
    public GameObject Hammer;
    public GameObject PauseMenuPanel;
    public GameObject HighscoreMenuPanel;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI CountdownText;
    public GameObject Lamp;
    public TextMeshProUGUI FinalScoreText;
    public TMP_InputField PlayerNameInputField;
    public List<GameObject> Moles;

    // Members.
    private int _score;
    private float _timerMole = 50;
    private float _initialCountDownTimer;

    private float _emissionFadeOutTime = 0.5f;
    private float _emissionFadeOutTimeCounter = 0;
    private bool _emissionEnabled = false;

    // Life Cycle.
    void Start()
    {
        Time.timeScale = 1;
        _initialCountDownTimer = CountdownTimer;
        PauseMenuPanel.SetActive(false);
        Moles.ForEach(x =>
        {
            x.GetComponentInChildren<CapsuleCollider>().enabled = false;
        });
    }
    void Update()
    {
        ControlLampFeedback();
    }
    private void FixedUpdate()
    {
        ShowMole();
        UpdateCountdown();
    }

    // Private Methods.
    private void ShowMole()
    {
        if (_timerMole > 0)
        {
            _timerMole -= 1;
            return;
        }

        _timerMole = Interval;
        if (Interval > MinInterval)
            Interval -= IntervalDecrement;
        else
            Interval = MinInterval;


        var moleToActivate = UnityEngine.Random.Range(0, Moles.Count);

        var selectedMole = Moles[moleToActivate];
        var selectedMoleAnimator = selectedMole.GetComponent<Animator>();

        if (!selectedMoleAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return;

        selectedMoleAnimator.SetTrigger("Show");

        var speedIncrement = MinInterval / Interval;
        selectedMoleAnimator.speed = 1 + speedIncrement;
    }
    private void UpdateCountdown()
    {
        if (CountdownTimer > 0f)
        {
            CountdownTimer -= Time.deltaTime;
            if(CountdownTimer <= 0)
            {
                CountdownTimer = 0;
                CountdownText.text = Mathf.Floor(CountdownTimer).ToString();
                GameOver();
            }
        }

        if (CountdownTimer <= _initialCountDownTimer / 2)
        {
            CountdownText.color = Color.yellow;
        }

        if (CountdownTimer < 10f)
        {
            CountdownText.color = Color.red;
        }

        CountdownText.text = Mathf.Floor(CountdownTimer).ToString();
    }
    private void GameOver()
    {
        Time.timeScale = 0;

        FinalScoreText.text = _score.ToString();

        PauseMenuPanel.SetActive(false);
        HighscoreMenuPanel.SetActive(true);
    }
    private void ControlLampFeedback()
    {
        if (_emissionEnabled && _emissionFadeOutTimeCounter < _emissionFadeOutTime)
            _emissionFadeOutTimeCounter += Time.deltaTime;
        else if(_emissionEnabled)
        {
            Lamp.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            Lamp.GetComponentInChildren<Light>().enabled = false;
            _emissionEnabled = false;
            _emissionFadeOutTimeCounter = 0f;
        }
    }

    // Public Methods.
    public void IncrementScore()
    {
        _score += 1;
        ScoreText.text = _score.ToString();

        Lamp.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        Lamp.GetComponentInChildren<Light>().enabled = true;
        _emissionEnabled = true;
    }
    public void OnPauseButtonClick()
    {
        Time.timeScale = 0;
        PauseMenuPanel.SetActive(true);
    }
    public void OnResumeButtonClick()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void OnSubmitButtonClick()
    {
        if(String.IsNullOrEmpty(PlayerNameInputField.text))
            return;

        var scoreClient = new PlayerScoreClient();
        scoreClient.PostPlayerScore(PlayerNameInputField.text, _score);

        SceneManager.LoadScene("LeaderboardScene");
    }
}

using Assets.Scripts.Interfaces;
using Assets.Scripts.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    #region Fields

    private Image scoreBarImage;
    private IScoreService scoreService;

    #endregion

    #region Properties

    public int GoalScore = 100;

    #endregion

    #region MonoBehaviour

    void Start()
    {
        scoreBarImage = GetComponent<Image>();
        scoreService = GameObject.FindFirstObjectByType<ScoreService>().GetComponent<IScoreService>() ?? throw new Exception("IScoreService not found");
    }

    
    void Update()
    {
        UpdateScoreBar(scoreService.GetScore());
    }

    #endregion

    #region Private methods

    private void UpdateScoreBar(int score)
    {
        scoreBarImage.fillAmount = (float)score/(float)GoalScore;
    }

    #endregion
}

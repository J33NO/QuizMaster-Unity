using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteAnswer = 20f;
    [SerializeField] float timeToShowAnswer = 5f;
    public bool isAnsweringQuestion = false;
    public bool loadNextQuestion = false;
    public float fillFraction;
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    private void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        //If the user is currently answering the question
        if(isAnsweringQuestion)
        {
            //Does the user still have time left on the timer?
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteAnswer;
            }
            else //Timer has run out, user is out of time, show answer and begin new countdown for dispalying answer
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowAnswer;
            }
        }
        else //User is no longer answering question
        {
            //user is currently viewing the answer
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToShowAnswer;
            }
            //User is ready to move onto next question.
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteAnswer;
                loadNextQuestion = true;
            }
        }
    }
}
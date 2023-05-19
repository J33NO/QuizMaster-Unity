using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO questionScriptableObject;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        DisplayQuestion();
    }

    private void Update() {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = questionScriptableObject.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = questionScriptableObject.GetAnswer(i);
        }
    }

    private void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void GetNextQuestion()
    {
        SetButtonState(true);
        DisplayQuestion();
        SetDefaultButtonSprite();
    }

    private void SetDefaultButtonSprite()
    {
        foreach(var button in answerButtons)
        {
            var buttonImage = button.GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    public void OnAnswerClicked(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
    }

    void DisplayAnswer(int index)
    {
        var correctAnswerIndex = questionScriptableObject.GetCorrectAnswerIndex();
        Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

        if(index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
        }
        else
        {
            questionText.text = $"Incorrect! The correct answer is: {questionScriptableObject.GetAnswer(correctAnswerIndex)}";
        }
        buttonImage.sprite = correctAnswerSprite;
    }
}

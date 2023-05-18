using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO questionScriptableObject;
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    void Start()
    {
        DisplayQuestion();
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
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    public void OnAnswerClicked(int index)
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

        SetButtonState(false);
    }
}

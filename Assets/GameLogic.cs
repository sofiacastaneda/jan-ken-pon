using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    public int playerChoice = -1;
    private bool gameStarted = false;

    public GameObject[] AIOpponentHands;

    public AudioSource gameaudio;
    public AudioClip prestart;
    public AudioClip start;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip tie;
    public AudioClip nochoice;

    private int opponentChoice;

    public void StartGame()
    {
        if (gameStarted)
        {
            Debug.Log("The game has already started.");
            return;
        }

        StartCoroutine(PreStart());
    }

    private IEnumerator PreStart()
    {
        gameaudio.PlayOneShot(prestart);
        yield return new WaitWhile(() => gameaudio.isPlaying);
        gameStarted = true;
        StartCoroutine(PerformOpponentChoice());
    }

    private IEnumerator PerformOpponentChoice()
    {
        //Animation handAnimation = opponentHandObject.GetComponent<Animation>();
        //handAnimation.Play("HandMovement");

        gameaudio.PlayOneShot(start);

        opponentChoice = UnityEngine.Random.Range(0, 3);

        yield return new WaitWhile(() => gameaudio.isPlaying);

        AIOpponentHands[0].SetActive(false);
        AIOpponentHands[opponentChoice].SetActive(true);

        yield return new WaitForSeconds(0.5f);

        if (playerChoice == -1)
        {
            Debug.Log("You didn't make a choice.");
            gameaudio.PlayOneShot(nochoice);
        }
        else
        {
            StartCoroutine(DetermineWinner());
        }

        AIOpponentHands[opponentChoice].SetActive(false);
        AIOpponentHands[0].SetActive(true);
        playerChoice = -1;
        gameStarted = false;
}

    private IEnumerator DetermineWinner()
    {
        if (playerChoice == opponentChoice)
        {
            Debug.Log("Tie!");
            gameaudio.PlayOneShot(tie);
        }
        else if ((playerChoice == 0 && opponentChoice == 2) ||
                 (playerChoice == 1 && opponentChoice == 0) ||
                 (playerChoice == 2 && opponentChoice == 1))
        {
            Debug.Log("Player wins!");
            gameaudio.PlayOneShot(win);
        }
        else
        {
            Debug.Log("AIOpponent wins!");
            gameaudio.PlayOneShot(lose);
        }

        yield return new WaitForSeconds(5f);
    }

    public void SetPlayerChoice(int choice)
    {
        playerChoice = choice;
    }
}
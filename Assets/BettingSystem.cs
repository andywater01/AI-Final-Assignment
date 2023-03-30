using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BettingSystem : MonoBehaviour
{
    public CollisionChecker colcheck;

    public TextMeshProUGUI playerBalanceText;
    public int PlayerBalance = 50;
    public int PlayerBet = 0;
    public int PlayerWinOrLose = 0; //0 = Not selected, 1 = Win, 2 = Lose
    public TextMeshProUGUI playerBetText;


    public TextMeshProUGUI AIBalanceText;
    public int AIBalance = 50;
    public int AIBet = 0;
    public int AIWinOrLose = 0; //0 = Not selected, 1 = Win, 2 = Lose
    public TextMeshProUGUI AIBetText;

    public bool gameStarted = false;

    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted == true)
        {
            if (colcheck.GetWinOrLose() == 1)
            {
                Debug.Log("The Crab Won!");
                if (PlayerWinOrLose == 1)
                {
                    PlayerBalance += PlayerBet * 2;
                    playerBalanceText.text = "Balance " + PlayerBalance;
                }
                else if (PlayerWinOrLose == 2)
                {
                    AIBalance += AIBet * 2;
                    AIBalanceText.text = "Balance " + AIBalance;
                }
                Restart();
            }
            else if (colcheck.GetWinOrLose() == 2)
            {
                Debug.Log("The Crab Lost!");
                if (PlayerWinOrLose == 2)
                {
                    PlayerBalance += PlayerBet * 2;
                    playerBalanceText.text = "Balance " + PlayerBalance;
                }
                else if (PlayerWinOrLose == 1)
                {
                    AIBalance += AIBet * 2;
                    AIBalanceText.text = "Balance " + AIBalance;
                }
                Restart();
            }

            
            
        }
    }


    public void Restart()
    {
        colcheck.SetWinOrLose(0);
        Time.timeScale = 0;
        gameStarted = false;
        PlayerBet = 0;
        AIBet = 0;
        playerBetText.text = playerBetText.text = ("Win/Lose " + "\n" + PlayerBet + " Coins");
        AIBetText.text = AIBetText.text = ("Win/Lose " + "\n" + AIBet + " Coins");
    }

    public void PlayGame()
    {
        AIChoice();
        AIBetText.text = AIBetText.text = ("Win/Lose " + "\n" + AIBet + " Coins");
        Time.timeScale = 1;
        gameStarted = true;

    }

    public void Minus5()
    {
        if (PlayerBet >= 5 && gameStarted == false)
        {
            PlayerBalance += 5;
            PlayerBet -= 5;
            playerBalanceText.text = "Balance " + PlayerBalance;
        }
    }

    public void Add1()
    {
        if (PlayerBalance >= 1 && gameStarted == false)
        {
            PlayerBalance -= 1;
            PlayerBet += 1;
            playerBalanceText.text = "Balance " + PlayerBalance;
        }

        if (PlayerWinOrLose == 1)
        {
            playerBetText.text = ("Win " + "\n" + PlayerBet + " Coins");
        }
        if (PlayerWinOrLose == 2)
        {
            playerBetText.text = ("Lose " + "\n" + PlayerBet + " Coins");
        }
        else
        {
            playerBetText.text = ("Win/Lose " + "\n" + PlayerBet + " Coins");
        }

    }

    public void Add5()
    {
        if (PlayerBalance >= 5 && gameStarted == false)
        {
            PlayerBalance -= 5;
            PlayerBet += 5;
            playerBalanceText.text = "Balance " + PlayerBalance;
        }

        if (PlayerWinOrLose == 1)
        {
            playerBetText.text = ("Win " + "\n" + PlayerBet + " Coins");
        }
        if (PlayerWinOrLose == 2)
        {
            playerBetText.text = ("Lose " + "\n" + PlayerBet + " Coins");
        }
        else
        {
            playerBetText.text = ("Win/Lose " + "\n" + PlayerBet + " Coins");
        }
    }

    public void Add10()
    {
        if (PlayerBalance >= 10 && gameStarted == false)
        {
            PlayerBalance -= 10;
            PlayerBet += 10;
            playerBalanceText.text = "Balance " + PlayerBalance;

            if (PlayerWinOrLose == 1)
            {
                playerBetText.text = ("Win " + "\n" + PlayerBet + " Coins");
            }
            if (PlayerWinOrLose == 2)
            {
                playerBetText.text = ("Lose " + "\n" + PlayerBet + " Coins");
            }
            else
            {
                playerBetText.text = ("Win/Lose " + "\n" + PlayerBet + " Coins");
            }
        }
    }

    public void VoteToWin()
    {
        if (gameStarted == false)
        {
            PlayerWinOrLose = 1;
            playerBalanceText.text = "Balance " + PlayerBalance;
            playerBetText.text = ("Win " + "\n" + PlayerBet + " Coins");
        }
        
    }

    public void VoteToLose()
    {
        if (gameStarted == false)
        {
            PlayerWinOrLose = 2;
            playerBalanceText.text = "Balance " + PlayerBalance;
            playerBetText.text = ("Lose " + "\n" + PlayerBet + " Coins");
        }
            
    }

    public void AIChoice()
    {
        if (PlayerWinOrLose == 1) //Player bets the AI will beat the course
        {
            AIWinOrLose = 2;
            AIBet = Random.Range(1, AIBalance);
            AIBalance -= AIBet;
            AIBalanceText.text = "Balance " + AIBalance;
            AIBetText.text = ("Lose " + "\n" + AIBet + " Coins");
        }

        else if (PlayerWinOrLose == 2) //Player bets the AI will NOT beat the course
        {
            AIWinOrLose = 1;
            AIBet = Random.Range(1, AIBalance);
            AIBalance -= AIBet;
            AIBalanceText.text = "Balance " + AIBalance;
            AIBetText.text = ("Win " + "\n" + AIBet + " Coins");
        }
    }
}

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

    public GameObject YouWin;
    public GameObject AIWins;

    

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
                if (PlayerWinOrLose == 1) //Crab Gets to End and Player Guesses it will (Player Wins)
                {
                    if (AIBet > PlayerBet)
                    {
                        PlayerBalance += (PlayerBet) + (AIBet - (AIBet - PlayerBet));

                        AIBalance += (AIBet - PlayerBet);
                    }
                    else if (PlayerBet > AIBet)
                    {
                        PlayerBalance += (PlayerBet) + AIBet;
                    }
                    AIBalanceText.text = "Balance " + AIBalance;
                    playerBalanceText.text = "Balance " + PlayerBalance;
                    YouWin.SetActive(true);
                }
                else if (PlayerWinOrLose == 2) //Crab Gets to End and Player Guesses it won't (AI Wins)
                {
                    if (PlayerBet > AIBet)
                    {
                        AIBalance += (PlayerBet) + (PlayerBet - (PlayerBet - AIBet));

                        PlayerBalance += (PlayerBet - AIBet);
                    }
                    else if (AIBet > PlayerBet)
                    {
                        AIBalance += (AIBet) + PlayerBet;
                    }
                    playerBalanceText.text = "Balance " + PlayerBalance;
                    AIBalanceText.text = "Balance " + AIBalance;
                    AIWins.SetActive(true);
                }
                Restart();
            }
            else if (colcheck.GetWinOrLose() == 2) //Crab Doesn't Get To The End And Player Guesses It Won't (Player Wins)
            {
                Debug.Log("The Crab Lost!");
                if (PlayerWinOrLose == 2)
                {
                    if (AIBet > PlayerBet)
                    {
                        PlayerBalance += (PlayerBet) + (AIBet - (AIBet - PlayerBet));

                        AIBalance += (AIBet - PlayerBet);
                    }
                    else if (PlayerBet > AIBet)
                    {
                        PlayerBalance += (PlayerBet) + AIBet;
                    }
                    AIBalanceText.text = "Balance " + AIBalance;
                    playerBalanceText.text = "Balance " + PlayerBalance;
                    YouWin.SetActive(true);
                }
                else if (PlayerWinOrLose == 1) //Crab Doesn't Get To The End And Player Guesses It Will (AI Wins)
                {
                    if (PlayerBet > AIBet)
                    {
                        AIBalance += (PlayerBet) + (PlayerBet - (PlayerBet - AIBet));

                        PlayerBalance += (PlayerBet - AIBet);
                    }
                    else if (AIBet > PlayerBet)
                    {
                        AIBalance += (AIBet) + PlayerBet;
                    }
                    playerBalanceText.text = "Balance " + PlayerBalance;
                    AIBalanceText.text = "Balance " + AIBalance;
                    AIWins.SetActive(true);
                }
                Restart();
            }

            
            
        }
    }


    public void Restart() //Resets Values
    {
        colcheck.SetWinOrLose(0);
        Time.timeScale = 0;
        gameStarted = false;
        PlayerBet = 0;
        AIBet = 0;
        playerBetText.text = playerBetText.text = ("Win/Lose " + "\n" + PlayerBet + " Coins");
        AIBetText.text = AIBetText.text = ("Win/Lose " + "\n" + AIBet + " Coins");

        if ((AIBet == 0 && AIBalance == 0) || (PlayerBet == 0 && PlayerBalance == 0))
        {
            if (AIBet == 0 && AIBalance == 0)
            {
                Debug.Log("Player Wins! AI is out of money");
            }
            else if (PlayerBet == 0 && PlayerBalance == 0)
            {
                Debug.Log("AI Wins! Player is out of money");
            }
        }
    }

    public void PlayGame() //Starts the Race
    {
        YouWin.SetActive(false);
        AIWins.SetActive(false);
        AIChoice();
        if (AIWinOrLose == 1)
            AIBetText.text = AIBetText.text = ("Win" + "\n" + AIBet + " Coins");
        else if (AIWinOrLose == 2)
            AIBetText.text = AIBetText.text = ("Lose" + "\n" + AIBet + " Coins");
        Time.timeScale = 1;
        gameStarted = true;

    }

    public void Minus5() // Subtracts 5 from your bet
    {
        if (PlayerBet >= 5 && gameStarted == false)
        {
            PlayerBalance += 5;
            PlayerBet -= 5;
            playerBalanceText.text = "Balance " + PlayerBalance;
        }
    }

    public void Add1() //Adds 1 coin to your bet
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

    public void Add5() //Adds 5 coins to your bet
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

    public void Add10() //Adds 10 coins to your bet
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

    public void VoteToWin() // Button to check if you think the crab will cross the finish line
    {
        if (gameStarted == false)
        {
            PlayerWinOrLose = 1;
            playerBalanceText.text = "Balance " + PlayerBalance;
            playerBetText.text = ("Win " + "\n" + PlayerBet + " Coins");
        }
        
    }

    public void VoteToLose() //Button to check if you want to bet on the crab not crossing the finish line
    {
        if (gameStarted == false)
        {
            PlayerWinOrLose = 2;
            playerBalanceText.text = "Balance " + PlayerBalance;
            playerBetText.text = ("Lose " + "\n" + PlayerBet + " Coins");
        }
            
    }

    public void AIChoice() //Randomly select a value of coins and bets it on the opposite choice of the player. (Crab wins or not)
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

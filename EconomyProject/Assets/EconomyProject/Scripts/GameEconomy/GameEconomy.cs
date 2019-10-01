using UnityEngine;

public enum GameState { ChooseAction, AuctionOrQuest }
public class GameEconomy : MonoBehaviour
{
    public GameState State = GameState.ChooseAction;

    public GameAuction GameAuction => FindObjectOfType<GameAuction>();

    public GameQuests GameQuest => FindObjectOfType<GameQuests>();

    public EconomyAgent [] CurrentPlayers => FindObjectsOfType<EconomyAgent>();
    public bool EveryPlayerReady
    {
        get
        {
            bool ready = true;
            foreach(var agent in CurrentPlayers)
            {
                if (!agent.Ready)
                {
                    ready = false;
                }
            }
            return ready;
        }
    }

    private void Update()
    {
        switch (State)
        {
            case GameState.ChooseAction:
                if (EveryPlayerReady)
                {
                    State = GameState.AuctionOrQuest;
                    ResetPlayers();
                }
                break;
            case GameState.AuctionOrQuest:
                bool questOver = GameQuest.RunQuests();
                bool auctionOver = GameAuction.RunAuction();
                if(questOver && auctionOver)
                {
                    ResetPlayers();
                }
                break;
            default:
                break;
        }
    }

    public void ResetPlayers()
    {
        foreach (var player in CurrentPlayers)
        {
            player.Ready = false;
        }
    }

    public void ResetTurn()
    {
        State = GameState.ChooseAction;

        GameQuest.Reset();
    }
}

using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public enum GameState { ChooseAction, AuctionOrQuest }
    public class GameEconomy : MonoBehaviour
    {
        public GameState state = GameState.ChooseAction;

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
                    if (!agent.ready)
                    {
                        ready = false;
                    }
                }
                return ready;
            }
        }

        private void Update()
        {
            switch (state)
            {
                case GameState.ChooseAction:
                    if (EveryPlayerReady)
                    {
                        state = GameState.AuctionOrQuest;
                        GameAuction.SetupAuction();
                        SetUi();
                    }
                    break;
                case GameState.AuctionOrQuest:
                    bool questOver = GameQuest.RunQuests();
                    bool auctionOver = GameAuction.RunAuction(Time.deltaTime);
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
                player.ready = false;
            }
        }

        public void SetUi()
        {
            foreach (var player in CurrentPlayers)
            {
                player.SetUi();
            }
        }

        public void ResetTurn()
        {
            state = GameState.ChooseAction;

            GameQuest.Reset();
        }
    }
}
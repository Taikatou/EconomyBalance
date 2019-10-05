﻿using System;
using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class EconomySystem : MonoBehaviour
    {
        // Start is called before the first frame update
        protected AgentActionChoice actionChoice;


        public EconomyAgent[] CurrentPlayers
        {
            get
            {
                EconomyAgent[] playerAgents = FindObjectsOfType<EconomyAgent>();
                return Array.FindAll(playerAgents, element => element.chosenChoice == actionChoice);
            }
        }
    }
}

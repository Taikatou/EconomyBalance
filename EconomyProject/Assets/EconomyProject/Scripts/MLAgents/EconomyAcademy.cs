using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public class EconomyAcademy : Academy
    {
        public override void InitializeAcademy()
        {
            Monitor.SetActive(true);
        }
    }
}

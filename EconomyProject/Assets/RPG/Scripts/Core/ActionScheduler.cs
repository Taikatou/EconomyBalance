using UnityEngine;

namespace Assets.RPG.Scripts.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction _currentAction;

        public void StartAction(IAction action)
        {
            if (_currentAction != action)
            {
                _currentAction?.Cancel();
                _currentAction = action;
            }
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
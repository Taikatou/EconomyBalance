using UnityEngine;

namespace Assets.RPG.Scripts.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private readonly Health _healthComponent = null;
        [SerializeField]
        private readonly RectTransform _foreground = null;
        [SerializeField]
        private readonly Canvas _rootCanvas = null;


        void Update()
        {
            if (Mathf.Approximately(_healthComponent.GetFraction(), 0)
            || Mathf.Approximately(_healthComponent.GetFraction(), 1))
            {
                _rootCanvas.enabled = false;
                return;
            }

            _rootCanvas.enabled = true;
            _foreground.localScale = new Vector3(_healthComponent.GetFraction(), 1, 1);
        }
    }
}
using DataModels;
using UnityEngine;

namespace DefaultNamespace
{
    public class Person : MonoBehaviour
    {
        [SerializeField] private PersonUI UI;
        private PersonData _initialPersonData;

        void OnEnable()
        {
            BallEventManager.Instance.OnBallPossessionChanged += HandleBallPossessionChange;
        }

        public void Initialize(PersonData personData)
        {
            // todo
            UpdateState(personData);
            UI.Initialize("defaulname");
        }

        public void UpdateState(PersonData personData)
        {
            transform.position = personData.targetPosition;
            transform.rotation = personData.TargetRotation;

            if (personData.PersonContext is { HasBallPossession: true })
            {
                BallEventManager.Instance.ChangeBallPossession(personData);
            }
        }

        private void HandleBallPossessionChange(int playerId, Possession possession) //999
        {
            if (playerId == _initialPersonData.Id)
            {
                bool hasPossession = (possession == Possession.HomeTeam && _initialPersonData.TeamSide == 0) ||
                                     (possession == Possession.AwayTeam && _initialPersonData.TeamSide == 1);

                UI.UpdateView(hasPossession);
            }
        }

        void OnDisable()
        {
            if (BallEventManager.Instance != null)
            {
                BallEventManager.Instance.OnBallPossessionChanged -= HandleBallPossessionChange;
            }
        }
    }
}
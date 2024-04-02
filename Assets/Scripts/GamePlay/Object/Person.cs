using DataModels;
using UnityEngine;
using Utilities;

namespace DefaultNamespace
{
    public class Person : BaseInterpolatedObject
    {
        [SerializeField] private PersonUI UI;
        private PersonData _initialPersonData;

        void OnEnable()
        {
            BallEventManager.Instance.OnBallPossessionChanged += HandleBallPossessionChange;
        }

        public void Initialize(PersonData personData)
        {
            if (personData == null || personData.Position == null || personData.Position.Length < 3)
            {
                Debug.LogError("Invalid PersonData provided.");
                return;
            }

            _initialPersonData = personData;
            UpdateState(personData);

            
            UI.Initialize("defaultname");
        }

        public override void UpdateState(IInterpolatedStateData interpolatedStateData)
        {
            base.UpdateState(interpolatedStateData);

            var personData = interpolatedStateData as PersonData;

            if (personData != null && personData.PersonContext.HasBallPossession)
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
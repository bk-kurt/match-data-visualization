using System;
using DataModels;
using Utilities;

namespace Managers
{
    public class BallEventManager : MonoSingleton<BallEventManager>
    {
        public event Action<int, Possession> OnBallPossessionChanged;

        public void ChangeBallPossession(PersonData personData)
        {
            OnBallPossessionChanged?.Invoke(personData.Id,
                personData.PersonContext.HasBallPossession
                    ? InterpretPossession(personData.TeamSide)
                    : Possession.None);
        }

        private Possession InterpretPossession(int teamSide)
        {
            // todo
            return teamSide == 0 ? Possession.HomeTeam : Possession.AwayTeam;
        }
    }
}
using Sirenix.OdinInspector;
using UnityEngine;

namespace PunchCars.Leagues
{
    [CreateAssetMenu(fileName = "New League", menuName = "PunchCars/Leagues/League", order = 0)]
    public class LeagueSO : ScriptableObject, ILeagueProvider
    {
        [SerializeField] private LeagueID _leagueID;
        [Space]
        [Min(0)] [SerializeField] private int _cupsForAvailable;
        [Space]
        [SerializeField] private string _leagueName;
        [SerializeField, PreviewField(75)] private Sprite _shieldIcon;
        [SerializeField, PreviewField(75)] private Sprite _levelIcon;

        public CustomLeague GetLeague()
        {
            return new CustomLeague(_leagueID, _leagueName, _shieldIcon, _levelIcon, _cupsForAvailable);
        }
    }
}

using Sirenix.OdinInspector;
using UnityEngine;

namespace PunchCars.Leagues
{
    [CreateAssetMenu(fileName = "New League", menuName = "PunchCars/Leagues/League", order = 0)]
    public class LeagueSO : ScriptableObject, ILeagueProvider
    {
        [SerializeField] private LeagueID _leagueID;
        [Space]
        [SerializeField] private string _name;
        [Min(0)] [SerializeField] private int _cupsForAvailable;
        [SerializeField, PreviewField(75)] private Sprite _icon;

        public CustomLeague GetLeague()
        {
            return new CustomLeague(_leagueID, _name, _icon, _cupsForAvailable);
        }
    }
}

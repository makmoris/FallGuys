using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PunchCars.Leagues
{
    [CreateAssetMenu(fileName = "New Leagues Container", menuName = "PunchCars/Leagues/LeaguesContainer", order = 0)]
    public class LeaguesContainer : ScriptableObject, ILeaguesProvider
    {
        [InfoBox("Duplicate identifiers were found! This could cause errors!", InfoMessageType.Error,
            "HasDuplicatesIdentifiers")]

        [SerializeField] private LeagueSO[] _leaguesPack;

        private CustomLeague[] _leagues;

        public CustomLeague[] LeaguesPack
        {
            get
            {
                return _leagues ??= _leaguesPack.Select(p => p.GetLeague()).ToArray();
            }
        }

        private bool HasDuplicatesIdentifiers()
        {
            var products = new List<CustomLeague>(_leaguesPack.Length);
            products.AddRange(_leaguesPack.Select(p => p.GetLeague()));
            var setOfUniqueIds = new HashSet<string>(products.Select(p => p.LeagueID.ToString()));

            return products.Count != setOfUniqueIds.Count;
        }
    }
}

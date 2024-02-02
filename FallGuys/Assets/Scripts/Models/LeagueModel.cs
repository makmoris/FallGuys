using PunchCars.Leagues;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PunchCars.Models
{
    public class LeagueModel
    {
        private ILeaguesProvider _leaguesProvider;

        [Inject]
        public LeagueModel(ILeaguesProvider leaguesProvider)
        {
            _leaguesProvider = leaguesProvider;

            
        }
    }
}

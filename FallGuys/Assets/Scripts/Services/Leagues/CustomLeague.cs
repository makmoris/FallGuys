using UnityEngine;

namespace PunchCars.Leagues
{
    public class CustomLeague
    {
        public LeagueID LeagueID { get; private set; }
        public string Name { get; private set; }
        public Sprite Icon { get; private set; }
        public int CupsForAvailable { get; private set; }

        public CustomLeague(LeagueID leagueID, string name, Sprite icon, int cupsForAvailable)
        {
            this.LeagueID = leagueID;
            Name = name;
            Icon = icon;
            CupsForAvailable = cupsForAvailable;
        }
    }
}

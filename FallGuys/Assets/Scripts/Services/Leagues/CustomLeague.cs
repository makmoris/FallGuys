using UnityEngine;

namespace PunchCars.Leagues
{
    public class CustomLeague
    {
        public LeagueID LeagueID { get; private set; }
        public string LeagueName { get; private set; }
        public Sprite ShieldIcon { get; private set; }
        public Sprite LevelIcon { get; private set; }
        public int CupsForAvailable { get; private set; }

        public CustomLeague(LeagueID leagueID, string leagueName, Sprite shieldIcon, Sprite levelIcon, int cupsForAvailable)
        {
            LeagueID = leagueID;
            LeagueName = leagueName;
            ShieldIcon = shieldIcon;
            LevelIcon = levelIcon;
            CupsForAvailable = cupsForAvailable;
        }
    }
}

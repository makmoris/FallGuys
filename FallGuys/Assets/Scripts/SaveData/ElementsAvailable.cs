using System;
using System.Collections.Generic;

namespace PunchCars.SaveData
{
    [Serializable]
    public class ElementsAvailable
    {
        public Dictionary<string, bool> allAvailableElementsList; // в этот же словарик закинуть и Vehicle и Weapon Lobby Available
    }
}

using System;
using System.Collections.Generic;

namespace SaveData
{
    [Serializable]
    public class ElementsSelect
    {
        public Dictionary<string, int> allSelectedElementsList; // в последующем кидать сюда индексы других элементов наподобие материала, колес и т.п.
    }
}

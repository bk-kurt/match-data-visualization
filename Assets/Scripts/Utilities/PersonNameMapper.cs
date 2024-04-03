using System.Collections.Generic;

namespace Utilities
{
    public static class PersonNameMapper
    {
        private static readonly Dictionary<int, string> PlayerNameMap = new Dictionary<int, string>();
        
        public static void PopulateMap(FrameData data)
        {
            PlayerNameMap.Clear();
            int i=0;
            
            foreach (var person in data.Persons)
            {
                PlayerNameMap.Add(person.Id,$"Sneijer {i++}");  // we can GetPersonNameFrom football team roster DB.
            }
        }

  
        public static string GetMappedNameById(int id)
        {
            string playerName = "Unknown";

      
            if (PlayerNameMap.TryGetValue(id, out var value))
            {
                playerName = value;
            }
            else
            {
                // Debug.LogWarning("Player ID " + id + " not found in the mapping.");
            }

            return playerName;
        }
    }
}
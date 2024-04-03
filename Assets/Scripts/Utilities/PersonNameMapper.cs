using System.Collections.Generic;

namespace Utilities
{
    public static class PersonNameMapper
    {
        private static Dictionary<int, string> playerNameMap = new Dictionary<int, string>();
        
        public static void PopulateMap(FrameData data)
        {
            playerNameMap.Clear();
            int i=0;
            
            foreach (var person in data.Persons)
            {
                playerNameMap.Add(person.Id,$"Sneijer {i++}");  // we can GetPersonNameFrom football team roster DB.
            }
        }

  
        public static string GetMappedNameById(int id)
        {
            string playerName = "Unknown";

      
            if (playerNameMap.ContainsKey(id))
            {
                playerName = playerNameMap[id];
            }
            else
            {
                // Debug.LogWarning("Player ID " + id + " not found in the mapping.");
            }

            return playerName;
        }
    }
}
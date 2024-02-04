using System.Collections.Concurrent;

namespace AppSecAssignment2RE.Models
{
    public class SessionManager
    {
        // Concurrent dictionary to store session IDs
        private static ConcurrentDictionary<string, string> userSessions = new ConcurrentDictionary<string, string>();

        // Method to add session ID
        public static void AddSession(string userId, string sessionId)
        {
            userSessions.AddOrUpdate(userId, sessionId, (key, oldValue) => sessionId);
        }

        // Method to remove session ID
        public static void RemoveSession(string userId)
        {
            userSessions.TryRemove(userId, out _);
        }

        // Method to check if session ID is valid
        public static bool IsSessionValid(string userId, string sessionId)
        {
            return userSessions.TryGetValue(userId, out string storedSessionId) && storedSessionId == sessionId;
        }
    }
}

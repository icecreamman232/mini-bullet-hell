
namespace JustGame.Scripts.Managers
{
    #region Game Event
    
    public enum GameEvents
    {
        PlAYER_SPAWNED,
        PAUSE,
        UNPAUSE,
        PLAYER_DEAD,
    }
    public struct GameEvent
    {
        public GameEvents EventType;
        private static GameEvent e;
        public static void Trigger(GameEvents events)
        {
            e.EventType = events;
            EventManager.TriggerEvent(e);
        }
    }
    
    #endregion
}

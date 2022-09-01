using System.Collections.Generic;
using TestEcs.Api;

namespace TestEcs.Data
{
    public class CachedData
    {
        private readonly Dictionary<int, IMovable> _players = new Dictionary<int, IMovable>();
        private readonly Dictionary<int, IDoor> _doors = new Dictionary<int, IDoor>();
        private readonly Dictionary<int, IDoorButton> _doorButtons = new Dictionary<int, IDoorButton>();

        public IReadOnlyDictionary<int, IMovable> Players => _players;
        public IReadOnlyDictionary<int, IDoor> Doors => _doors;
        public IReadOnlyDictionary<int, IDoorButton> DoorButtons => _doorButtons;

        public void AddMovableEntity(int entity, IMovable player)
        {
            if (_players.ContainsKey(entity)) return;
            _players.Add(entity, player);
        }
        
        public void AddDoorEntity(int entity, IDoor door)
        {
            if (_doors.ContainsKey(entity)) return;
            _doors.Add(entity, door);
        }

        public void AddButtonEntity(int entity, IDoorButton button)
        {
            if (_doorButtons.ContainsKey(entity)) return;
            _doorButtons.Add(entity, button);
        }
    }
}
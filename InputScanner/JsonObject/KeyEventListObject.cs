using System.Collections.Generic;

namespace InputScanner.JsonObject
{
    public class KeyEventListObject : BaseObject
    {
        public string Kind => "#keyEventList";

        public KeyEventListObject()
        {
            KeyEvents = new List<KeyEventObject>();
        }

        public List<KeyEventObject> KeyEvents { get; set; }
    }
}

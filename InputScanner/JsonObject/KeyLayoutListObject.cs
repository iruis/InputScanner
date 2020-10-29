using System.Collections.Generic;

namespace InputScanner.JsonObject
{
    public class KeyLayoutListObject : BaseObject
    {
        public string Kind => "#keyLayoutList";

        public KeyLayoutListObject()
        {
            KeyLayouts = new List<KeyLayoutObject>();
        }

        public List<KeyLayoutObject> KeyLayouts { get; set; }
    }
}

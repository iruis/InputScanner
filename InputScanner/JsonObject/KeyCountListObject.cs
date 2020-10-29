using System.Collections.Generic;

namespace InputScanner.JsonObject
{
    public class KeyCountListObject : BaseObject
    {
        public string Kind => "#keyCountList";

        public KeyCountListObject()
        {
            KeyCounts = new List<KeyCountObject>();
        }

        public List<KeyCountObject> KeyCounts { get; set; }
    }
}

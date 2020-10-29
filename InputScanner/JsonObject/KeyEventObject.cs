namespace InputScanner.JsonObject
{
    public class KeyEventObject : BaseObject
    {
        public string Kind => "#keyEvent";

        public long KeyCode { get; set; }
        public bool Pressed { get; set; }
        public long Count { get; set; }
        public double? Percent { get; set; }
    }
}

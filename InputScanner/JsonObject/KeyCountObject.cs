namespace InputScanner.JsonObject
{
    public class KeyCountObject : BaseObject
    {
        public string Kind => "#keyCount";
        public long KeyCode { get; set; }
        public long Count { get; set; }
        public double? Percent { get; set; }
    }
}

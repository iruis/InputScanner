namespace InputScanner.JsonObject
{
    public class KeyLayoutObject : BaseObject
    {
        public string Kind => "#keyLayout";

        public long KeyCode { get; set; }

        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public string Label { get; set; }
    }
}

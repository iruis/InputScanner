namespace InputScanner.JsonObject
{
    public class ButtonLayoutObject : BaseObject
    {
        public string Kind => "#buttonLayout";

        public string Shape { get; set; }
        public string Label { get; set; }
        public string ButtonName { get; set; }

        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

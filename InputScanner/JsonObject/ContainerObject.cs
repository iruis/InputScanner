namespace InputScanner.JsonObject
{
    public class ContainerObject : BaseObject
    {
        public string Kind => "#container";

        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

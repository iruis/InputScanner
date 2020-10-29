namespace InputScanner.JsonObject
{
    public class ColorSetObject : BaseObject
    {
        public string Kind => "#colorSet";

        public string Text { get; set; }
        public string Border { get; set; }
        public string Shadow { get; set; }
        public string BackgroundReleased { get; set; }
        public string BackgroundPressed { get; set; }
    }
}

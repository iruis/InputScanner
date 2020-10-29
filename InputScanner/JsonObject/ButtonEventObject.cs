namespace InputScanner.JsonObject
{
    public class ButtonEventObject : BaseObject
    {
        public string Kind => "#buttonEvent";

        public string ButtonName { get; set; }
        public object Value { get; set; }
    }
}

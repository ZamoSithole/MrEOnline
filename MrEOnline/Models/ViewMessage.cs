namespace MrEOnline.Models{
    public enum ViewMessageType {
        Unknown, Error, Success, Warning
    }

    public enum ViewMode {
        Create, Edit, Delete, ReadOnly
    }

    public class ViewMessage {
        public string Message { get; set; }
        public ViewMessageType Type { get; set; }
        public ViewMessage() {; }
        public ViewMessage(string message, ViewMessageType viewMessageType)
        {
            Message = message;
            Type = viewMessageType;
        }
    }
}
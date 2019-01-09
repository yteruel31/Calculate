namespace Calculate.Model
{
    public class OperationModel
    {
        private OperationModel(string value)
        {
            Value = value;
        }
        public string Value { get; set; }

        public static OperationModel Addition => new OperationModel("+");
        public static OperationModel Substract => new OperationModel("-");
        public static OperationModel Multiply => new OperationModel("*");
        public static OperationModel Divide => new OperationModel("/");
    }
}
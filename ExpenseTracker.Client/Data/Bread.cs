namespace ExpenseTracker.Client.Data
{
    public class Bread
    {
        public string Message { get; set; }
        public string Banner { get; set; }
        public enum Hawtness
        {
            Success, Error
        }

        public Hawtness Level;

        public static Bread Success(string banner, string message = null)
        {
            return new Bread
            {
                Banner = banner,
                Message = message,
                Level = Hawtness.Success
            };
        }

        public static Bread Error(string banner, string message = null)
        {
            return new Bread
            {
                Banner = banner,
                Message = message,
                Level = Hawtness.Error
            };
        }
    }
}

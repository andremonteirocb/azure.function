namespace Fundamentos.Azure.Function
{
    public class EmailSettings
    {
        public string Smtp { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
    }
}

namespace ClientPersonData
{
    //public class Person
    //{
    //    public string UserID { get; set; }
    //    public Info info {get; set;}
    //    public Interes[] intereses { get; set; }
    //   // public Aplication applowner { get; set; }
    //}
    //public class Info
    //{
    //    public string publicAdress { get; set; }
    //    public string country { get; set; }
    //}
    public class Interes
    {
        public string nameInteres { get; set; }
        public int rate { get; set; }
    }
    public class Result
    {
        public string nameOrg { get; set; }
        public int count { get; set; }
    }
    public class data
    {
        public string from { get; set; }
        public string to { get; set; }
        public double quantity { get; set; }
    }
    //public class Aplication
    //{
    //    public string publAdres { get; set; }
    //    public string nameApplication { get; set; }
    //}
}

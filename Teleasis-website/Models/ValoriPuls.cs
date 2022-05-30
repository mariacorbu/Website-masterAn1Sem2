namespace Teleasis_website.Models
{
    public class ValoriPuls
    {
        public ValoriPuls(string data, string value)
        {
            this.data = data;
            this.value = value;
        }
        public ValoriPuls()
        {

        }
        public string data { get; set; }
        public string value { get; set; }
    }
}

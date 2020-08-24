namespace MindMapAPI.Models
{
    public class TestUser
    {
        private string name;
        private string password;

        public TestUser() 
        {
        }

        public TestUser(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
    }
}

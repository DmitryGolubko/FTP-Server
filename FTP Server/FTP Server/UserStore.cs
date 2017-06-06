using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace FTP_Server
{
    public static class UserStore
    {
        private static List<User> users;

        static UserStore()
        {
            users = new List<User>();

            XmlSerializer serializer = new XmlSerializer(users.GetType(), new XmlRootAttribute("Users"));

            if (File.Exists("users.xml"))
            {
                using (StreamReader reader = new StreamReader("users.xml"))
                {
                    users = serializer.Deserialize(reader) as List<User>;
                }
                
            }
            else
            {
                users.Add(new User
                {
                    Username = "root",
                    Password = "root",
                });
            }

            using (StreamWriter w = new StreamWriter("users.xml"))
            {
                serializer.Serialize(w, users);
            }
        }

        public static User Validate(string username, string password)
        {
            User user = (from u in users where u.Username == username && u.Password == password select u).SingleOrDefault();

            return user;
        }
    }
}

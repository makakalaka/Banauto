using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Banauto.Backend
{
    public class User
    {
        private string username;
        private string password;
        private string name;
        private string surname;
        private string phone;
        private string type;

        SQLiteConnection Connection = Backend.DatabaseUtility.Connection;


        public void Login(string username, string password)//iesko username ir password atitikmens duomenu bazeje
        {
            Connection.Open();
            string sql = "select * from Users";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (username == Convert.ToString(reader[0]) && password == Convert.ToString(reader[1]))
                {
                    this.password = Convert.ToString(reader[0]);
                    this.username = Convert.ToString(reader[1]);
                    this.name = Convert.ToString(reader[2]);
                    this.surname = Convert.ToString(reader[3]);
                    this.phone = Convert.ToString(reader[4]);
                    this.type = Convert.ToString(reader[5]);
                    Connection.Close();
                    break;
                }
            }
            command.Dispose();
            Connection.Close();
        }

        public void Register(User user)//patalpina nauja user i database
        {
            Connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("insert into Users values(@username, @password, @name, @surname, @phone, @type)", Connection);
            cmd.Parameters.AddWithValue("@username", user.username);
            cmd.Parameters.AddWithValue("@password", user.password);
            cmd.Parameters.AddWithValue("@name", user.name);
            cmd.Parameters.AddWithValue("@surname", user.surname);
            cmd.Parameters.AddWithValue("@phone", user.phone);
            cmd.Parameters.AddWithValue("@type", user.type);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            Connection.Close();
        }

        private bool UsernameValidation(string username)//tikrina, ar nera tokio paties username duomenu bazeje, koki norima sukurti
        {
            Connection.Open();
            string sql = "select username from Users";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string uname = Convert.ToString(reader[0]);
                if (uname == username)
                    return false;
            }
            command.Dispose();
            Connection.Close();
            return true;
        }


        public string Type
        {
            get { return type; }
            set
            {
                if (value != "")
                    type = value;
                else throw new Exception("Neįvestas tipas!");

            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                if (value != "")//ar tuscias laukelis
                {
                    if (Regex.IsMatch(value, @"^[0-9]+$"))//ar sudaryta tik is skaiciu
                    {
                        if (value.Length == 9)//ar tinkamas numerio formatas (9 skaiciai)
                            phone = value;
                        else throw new Exception("Netinkamas telefono numeris (telefono numeris turi susidaryti iš 9 skaičių, pradedant 86..)!");
                    }
                    else throw new Exception("Telefono numeris turi susidaryti iš skaičių!");
                }
                else throw new Exception("Neįvestas telefono numeris!");

            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                if (value != "")//ar tuscias laukelis
                {
                    if (Regex.IsMatch(value, @"^[\p{L}]+$")) //ar sudaryta tik is raidziu
                        surname = value;
                    else throw new Exception("Pavardė turi būti sudaryta tik iš raidžių!");
                }
                else throw new Exception("Neįvesta pavardė!");

            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value != "")//ar tuscias laukelis
                {
                    if (Regex.IsMatch(value, @"^[\p{L}]+$"))//ar sudarytas tik is raidziu
                        name = value;
                    else throw new Exception("Vardas turi būti sudarytas tik iš raidžių!");
                }
                else throw new Exception("Neįvestas vardas!");

            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))//ar sudarytas tik is skaiciu ir raidziu
                    password = value;
                else throw new Exception("Slaptažodis turi būti sudarytas tik iš raidžių ir skaičių!");
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                if (value != "")//ar tuscias laukelis
                {
                    if (Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))//ar sudarytas tik is skaiciu ir raidziu
                    {
                        if (UsernameValidation(value))//ar autentiskas username
                            username = value.ToLower();
                        else throw new Exception("Toks vartotojo vardas jau egzistuoja!");
                    }
                    else throw new Exception("Prisijungimo vardas turi būti sudarytas tik iš raidžių ir skaičių!");
                }
                else throw new Exception("Neįvestas prisijungimo vardas!");
            }
        }
    }
}


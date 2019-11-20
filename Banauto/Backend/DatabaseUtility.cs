using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banauto.Backend
{
    public static class DatabaseUtility
    {

        //main connection 
        public static SQLiteConnection Connection = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "\\DataBases\\Database.db");



        public static int TotalAutos()//skaiciuoja kiek eiluciu lenteleje 
        {
            Connection.Open();
            string sql = "SELECT COUNT (*) vehicleMake FROM Vehicles";
            var command = new SQLiteCommand(sql, Connection);
            int count = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            Connection.Close();
            return count;
        }

        public static void UploadImage(string vehicleID, string filePath)
        {
            string[] tokens = filePath.Split('\\');
            System.IO.File.Copy(filePath, Environment.CurrentDirectory + "\\Images\\" + vehicleID + tokens[tokens.Length - 1], true);//prie imageName pridedamas vehicleID (naudojant same image for multiple autos)
            Connection.Open();
            SQLiteCommand cmd = new SQLiteCommand($"insert into '{vehicleID}' values (@name)", Connection);
            cmd.Parameters.AddWithValue("@name", vehicleID + tokens[tokens.Length - 1]);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            Connection.Close();
        }

        public static void SubmitOrder(Backend.Order order)
        {
            Connection.Open();
            var sql ="insert into Orders(ID, username, name, surname, phone, vehicleID, status) VALUES (@ID, @username, @name, @surname, @phone, @vehicleID, @status)";
            using (var cmd = new SQLiteCommand(sql, Connection))
            {
                cmd.Parameters.AddWithValue("@ID", order.OrderID);
                cmd.Parameters.AddWithValue("@username", order.NewUsername);
                cmd.Parameters.AddWithValue("@name", order.Name);
                cmd.Parameters.AddWithValue("@surname", order.Surname);
                cmd.Parameters.AddWithValue("@phone", order.Phone);
                cmd.Parameters.AddWithValue("@vehicleID", order.AutoID);
                cmd.Parameters.AddWithValue("@status", "0");
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            Connection.Close();
        }

        public static string LastID()
        {
            Connection.Open();
            string sql = $"select * from Vehicles";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            int id=0;
            while (reader.Read())
            {
                id = Convert.ToInt32(reader[5]);
            }
            command.Dispose();
            Connection.Close();
            id++;
            return Convert.ToString(id);
        }

        public static void ChangePhone(Backend.User user)
        {
            SQLiteConnection Connection = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "\\DataBases\\Database.db");
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
            string sql = "UPDATE Users SET phone=@phone Where username = @username";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, Connection))
            {
                cmd.Parameters.AddWithValue("@phone", user.Phone);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            Connection.Close();
        }
    }
}
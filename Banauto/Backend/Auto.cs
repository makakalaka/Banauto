using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Banauto.Backend
{
    public class Auto
    {
        private string vehicleMake;
        private string vehicleModel;
        private string vehicleDescription;
        private string vehicleColour;
        private string vehicleKw;
        public string vehicleID;
        private string vehiclePrice;


        public string VehiclePrice
        {
            get { return vehiclePrice; }
            set
            {
                if (Regex.IsMatch(value, @"^[0-9]+$"))//ar sudarytas tik is skaiciu ir raidziu
                    vehiclePrice = value;
                else throw new Exception("Kaina turi būti sudaryta tik iš skaičių!");
            }
        }

        public string VehicleKw
        {
            get { return vehicleKw; }
            set
            {
                if (Regex.IsMatch(value, @"^[0-9]+$"))//ar sudarytas tik is skaiciu ir raidziu
                    vehicleKw = value;
                else throw new Exception("Galia turi būti sudaryta tik iš skaičių!");
            }
        }

        public string VehicleColour
        {
            get { return vehicleColour; }
            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z]+$"))//ar sudarytas tik is skaiciu ir raidziu
                    vehicleColour = value.ToUpper();
                else throw new Exception("Spalva turi būti sudaryta tik iš raidžių!");
            }
        }

        public string VehicleDescription
        {
            get { return vehicleDescription; }
            set { vehicleDescription = value; }
        }

        public string VehicleModel
        {
            get { return vehicleModel; }
            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))//ar sudarytas tik is skaiciu ir raidziu
                    vehicleModel = value.ToUpper();
                else throw new Exception("Modelis turi būti sudarytas tik iš raidžių ir skaičių!");
            }
        }

        public string VehicleMake
        {
            get { return vehicleMake; }
            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))//ar sudarytas tik is skaiciu ir raidziu
                    vehicleMake = value.ToUpper();
                else throw new Exception("Gamintojas turi būti sudarytas tik iš raidžių ir skaičių!");
            }
        }

        SQLiteConnection Connection = Backend.DatabaseUtility.Connection;

        public void AddAuto()
        {
            this.vehicleID =Backend.DatabaseUtility.LastID();
            Connection.Open();
            string sql = $"create table '{this.vehicleID}' (name text)";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            command.ExecuteNonQuery();

            SQLiteCommand cmd = new SQLiteCommand("insert into Vehicles values(@vehicleMake, @vehicleModel, @vehicleDescription, @vehicleColour, @vehicleKw, @vehicleID, @vehiclePrice)", Connection);
            cmd.Parameters.AddWithValue("@vehicleMake", this.vehicleMake);
            cmd.Parameters.AddWithValue("@vehicleModel", this.vehicleModel);
            cmd.Parameters.AddWithValue("@vehicleDescription", this.vehicleDescription);
            cmd.Parameters.AddWithValue("@vehicleColour", this.vehicleColour);
            cmd.Parameters.AddWithValue("@vehicleKw", this.vehicleKw);
            cmd.Parameters.AddWithValue("@vehicleID", this.vehicleID);
            cmd.Parameters.AddWithValue("@vehiclePrice", this.vehiclePrice);
            cmd.ExecuteNonQuery();
            command.Dispose();
            Connection.Close();
        }

        public void UpdateAuto()
        {
            Connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Vehicles SET vehicleMake=@vehicleMake,vehicleModel=@vehicleModel,vehicleDescription=@vehicleDescription,vehicleColour=@vehicleColour,vehicleKw=@vehicleKw,vehiclePrice=@vehiclePrice Where vehicleID = @vehicleID", Connection);
            cmd.Parameters.AddWithValue("@vehicleMake", this.vehicleMake);
            cmd.Parameters.AddWithValue("@vehicleModel", this.vehicleModel);
            cmd.Parameters.AddWithValue("@vehicleDescription", this.vehicleDescription);
            cmd.Parameters.AddWithValue("@vehicleColour", this.vehicleColour);
            cmd.Parameters.AddWithValue("@vehicleKw", this.vehicleKw);
            cmd.Parameters.AddWithValue("@vehicleID", this.vehicleID);
            cmd.Parameters.AddWithValue("@vehiclePrice", this.vehiclePrice);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            Connection.Close();
        }
    }
}

using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppConsole.Cod
{
    public class TesteServerPostGres
    {
        public static void TestePostGre()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=192.168.1.120;Port=5432;User Id=postgres;Password=12345678;Database=bsTeste01;");
            conn.Open();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM current_catalog;", conn);

                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        Console.Write("{0} \t", dr[i]);
                    }
                    Console.WriteLine();
                }

            }
            finally
            {
                conn.Close();
            }
        }
    }
}

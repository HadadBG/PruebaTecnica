// See https://aka.ms/new-console-template for more information
using MySql.Data.MySqlClient;
using Prueba_Tecnica;

MySql.Data.MySqlClient.MySqlConnection conn;
string connectionString;

connectionString = "server=127.0.0.1;uid=root;" +
    "pwd=root;database=prueba1";


try
{
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        MySqlCommand command = new MySqlCommand(Querys.get_topUsers, connection);
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        Console.WriteLine("Top 10 Usuarios \n");
        try
        {
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0}, {1}",
                reader["nombre"], reader["sueldo"]));
            }
        }
        finally
        {
            reader.Close();
        }

        command = new MySqlCommand(Querys.get_info, connection);
        reader = command.ExecuteReader();
        Console.WriteLine("Convirtiendo a csv .... \n");
        List<Info> arrInfo = new List<Info>();
        try
        {
            while (reader.Read())
            {
                arrInfo.Add(new Info()
                {
                    fechaIngreso = Convert.ToString(
                        DBNull.Value.Equals(reader["fechaIngreso"])?
                        "NULL":reader["fechaIngreso"]
                        ),
                    sueldo = (Convert.ToDouble(
                        DBNull.Value.Equals(reader["sueldo"]) ?
                        "0":reader["sueldo"]                          
                    )),
                    nombreCompleto = Convert.ToString(reader["nombreCompleto"]),
                    username = Convert.ToString(reader["username"])
                });
            }
        }
        finally
        {
            reader.Close();
        }
        string[][] arrInfoStringed;
        arrInfoStringed= arrInfo.Select(i =>
       {
           List<String> arrString = new List<String>();
           System.Reflection.PropertyInfo[] properties = i.GetType().GetProperties();
           foreach (System.Reflection.PropertyInfo property in properties)
           {
              arrString.Add(Convert.ToString(property.GetValue(i))??"NULL");
           }
           return arrString.ToArray();
       }).ToArray<string[]>();
        using (System.IO.StreamWriter file = new System.IO.StreamWriter("./result.csv"))
        {
            foreach(string[] arrString in arrInfoStringed)
            {
                file.Write(string.Join(",", arrString ));
                file.Write("\n");
            }
            
            
        }
        Console.WriteLine("Archivo generado : result.csv");

        command = new MySqlCommand(Querys.update_user, connection);
        command.Parameters.AddWithValue("@pSalario", 600);
        command.Parameters.AddWithValue("@pUser", "user01");
        command.ExecuteNonQuery();
        Console.WriteLine("Usuario user01 se ajusto su sueldo a 600");

        command = new MySqlCommand(Querys.add_user, connection);
        command.Parameters.AddWithValue("@puserName", "HadadBG");
        command.Parameters.AddWithValue("@pnombre", "Hadad");
        command.Parameters.AddWithValue("@ppaterno", "Bautista");
        command.Parameters.AddWithValue("@pmaterno", "García");
        command.Parameters.AddWithValue("@psueldo", 15000);
        command.ExecuteNonQuery();
        Console.WriteLine("Usuario añadido , se añdio a HadadBG");
        connection.Close();
    }

}
catch (MySql.Data.MySqlClient.MySqlException ex)
{
    Console.WriteLine(ex.StackTrace);
}
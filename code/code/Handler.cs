using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace code
{
    internal abstract class Handler : ISpecificHandler
    {
               

        protected string _connectionString = "Data Source=LAPTOP-VQVR3Q8R;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // SQL Operations
        public ArrayList SelectAll()
        {
            object obj = GetTable();
            ArrayList objects = new ArrayList();
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand comando = new SqlCommand($"SELECT * FROM {GetTable()}", connection);
                using (SqlDataReader dataReader = comando.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            switch (GetTable())
                            {
                                case "Producto":
                                    Product prod = new Product();
                                    prod.SetValues(
                                        dataReader.GetInt64(0),
                                        dataReader.GetString(1),
                                        dataReader.GetDecimal(2),
                                        dataReader.GetDecimal(3),
                                        dataReader.GetInt32(4));
                                    objects.Add(prod);
                                    break;
                                case "Usuario":
                                    User usr = new User();
                                    usr.SetValues(
                                        dataReader.GetInt64(0),
                                        dataReader.GetString(1),
                                        dataReader.GetString(2),
                                        dataReader.GetString(3),
                                        dataReader.GetString(4),
                                        dataReader.GetString(5));
                                    objects.Add(usr);
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                }
            }

            return objects;
        }

        

        public void Update()
        {
            string updatedValues = "";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                for (int i = 0; i < GetValues().Count; i++)
                {
                    if (i == 0)
                    {
                        if (GetValues()[i] is string)
                            updatedValues += $" {GetFields()[i]} =  '{GetValues()[i]}'";
                        else
                            updatedValues += $" {GetFields()[i]} =  {GetValues()[i]}";
                    }
                    else
                    {
                        if (GetValues()[i] is string)
                            updatedValues += $", {GetFields()[i]} =  '{GetValues()[i]}'";
                        else
                            updatedValues += $", {GetFields()[i]} =  {GetValues()[i]}";
                    }
                    
                }

                SqlCommand comando = new SqlCommand($"UPDATE {GetTable()} SET" + updatedValues, 
                    connection);

                comando.ExecuteNonQuery();
            }
            
        }

        public void Insert(ArrayList objects)
        {
            
            string insertedValues = "";
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {

                for(int i = 0; i < objects.Count; i++)
                {
                    insertedValues += i == 0 ? "(" : ",(";
                    for (int y = 0; y < GetFields().Length; y++)
                    {
                        insertedValues += y == 0 ? $"{GetValues()[y]}" 
                            : $", {GetValues()[y]}"; 
                    }
                    insertedValues += ")";
                }

                SqlCommand comando = new SqlCommand($"INSERT INTO {GetTable()}" +
                    $" VALUES {insertedValues}", connection);

                comando.ExecuteNonQuery();
            }
        }

        ArrayList ISpecificHandler.SelectSpecific()
        {
            ArrayList objectsRetrieved = new ArrayList();
            int option;
            Console.WriteLine("\n\n===== INTERFAZ ESPECIFICA DE ACCIONES ======\n" +
                "Accion que deseas hacer:" +
                "\n1) Traer simple usuario" +
                "\n2) Traer Productos asociados a un usuario" +
                "\n3) Traer Porductos vendidos asociados a un usuario" +
                "\n4) Traer lista de ventas asociadas a un usuario");

            while(!(int.TryParse(Console.ReadLine(), out option)) || (option < 0 || option >4))
            {
                Console.WriteLine("\n==== valor no valido vuelve a intentar =====\n");
            }
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                
                switch (option)
                {
                    case 1:
                        int id;
                        // practicando con parameters
                        Console.Write("\nIngresa el usuario que deseas recuperar:");
                        while (!(int.TryParse(Console.ReadLine(), out id)))
                        {
                            Console.WriteLine("\n==== valor no valido vuelve a intentar =====\n");
                        }

                        SqlCommand command = new SqlCommand($"SELECT * FROM Usuario" +
                            $" WHERE Id = @parameter", connection);

                        //parametro
                        SqlParameter parameter = new SqlParameter();
                        parameter.ParameterName = "parameter";
                        parameter.Value = id;
                        parameter.SqlDbType = SqlDbType.BigInt;

                        command.Parameters.Add(parameter);

                        using(SqlDataReader dataReader = command.ExecuteReader())
                        {
                            User user1 = new User();
                            user1.SetValues(dataReader.GetInt64(0),
                                        dataReader.GetString(1),
                                        dataReader.GetString(2),
                                        dataReader.GetString(3),
                                        dataReader.GetString(4),
                                        dataReader.GetString(5));
                            objectsRetrieved.Add(user1);
                        }
                        return objectsRetrieved;
                        break;

                    case 2:
                        Console.Write("\nIngresa el usuario que deseas recuperar" +
                            " para regresar los productos que ha registrado:");
                        while (!(int.TryParse(Console.ReadLine(), out id)))
                        {
                            Console.WriteLine("\n==== valor no valido vuelve a intentar =====\n");
                        }

                        SqlCommand command2 = new SqlCommand("SELECT prod.Id, prod.Descripciones, " +
                            "prod.Costo, prod.PrecioVenta, prod.Stock, prod.IdUsuario " +
                            "FROM Producto prod " +
                            "INNER JOIN " +
                            "Usuario usr " +
                            $"ON {id} = prod.IdUsuario;", connection);

                        using(SqlDataReader dataReader = command2.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                Console.WriteLine($"\n\nProductos asociados a usuario " +
                                    $"{id} son: ");
                                while (dataReader.Read())
                                {
                                    Product prod = new Product();
                                    prod.SetValues(
                                        dataReader.GetInt64(0),
                                        dataReader.GetString(1),
                                        dataReader.GetDecimal(2),
                                        dataReader.GetDecimal(3),
                                        dataReader.GetInt32(4));
                                    objectsRetrieved.Add(prod);
                                }
                            }
                        }

                        return objectsRetrieved;
                        break;

                    case 3:
                        Console.Write("\nIngresa el usuario que deseas recuperar" +
                            " para regresar los productos que ha vendido:");
                        while (!(int.TryParse(Console.ReadLine(), out id)))
                        {
                            Console.WriteLine("\n==== valor no valido vuelve a intentar =====\n");
                        }

                        SqlCommand command3 = new SqlCommand("SELECT prod.Id, " +
                            "prod.Descripciones AS ProductoVendido, " +
                            "" +
                            "FROM Producto prod " +
                            "INNER JOIN " +
                            "ProductoVendido prodv " +
                            $"ON {id} = prodv.IdProducto;", connection);

                        using(SqlDataReader dataReader = command3.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                Console.WriteLine("\nProductos que ha vendido el usuario" +
                                    $"{id} son: ");
                                while (dataReader.Read())
                                {
                                    Product prod = new Product();
                                    prod.SetValues(
                                        dataReader.GetInt64(0),
                                        dataReader.GetString(1));
                                    objectsRetrieved.Add(prod);
                                }
                            }
                        }

                        return objectsRetrieved;
                        break;
                    case 4:

                        //temporal mientras se arregla la base de datos
                        return null;
                        break;
                    default:
                        return null;
                        break;
                }
            }
            

        }


        protected abstract void SetValues(params object[] values);
        //protected abstract object GetType();
        protected abstract ArrayList GetValues();
        protected abstract string[] GetFields(params object[] fields);
        protected abstract string GetTable();
    }
}

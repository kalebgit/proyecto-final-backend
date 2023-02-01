using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    internal abstract class Handler
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
                                    this.SetValues(
                                        dataReader.GetInt64(0),
                                        dataReader.GetString(1),
                                        dataReader.GetDecimal(2),
                                        dataReader.GetDecimal(3),
                                        dataReader.GetInt32(4));
                                    break;
                                case "Usuario":
                                    this.SetValues(
                                        dataReader.GetInt64(0),
                                        dataReader.GetString(1),
                                        dataReader.GetString(2),
                                        dataReader.GetString(3),
                                        dataReader.GetString(4),
                                        dataReader.GetString(5));
                                    break;
                                default:
                                    break;
                            }
                            objects.Add(this);
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
                for(int i = 0; i < GetValues().Count; i++)
                {
                    if(GetValues()[i] is string)
                    {
                        updatedValues += $" {GetFields()[i]} =  '{GetValues()[i]}',";
                    }
                    else
                        updatedValues += $" {GetFields()[i]} =  {GetValues()[i]},";
                }

                SqlCommand comando = new SqlCommand($"UPDATE {GetTable()} SET" + updatedValues, 
                    connection);

                comando.ExecuteNonQuery();
            }
            
        }


        protected abstract void SetValues(params object[] values);
        protected abstract object GetType();
        protected abstract ArrayList GetValues();
        protected abstract string[] GetFields();
        protected abstract string GetTable();
    }
}

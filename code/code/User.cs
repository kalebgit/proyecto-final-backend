using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    internal class User : Handler
    {
        


        // instance variables
        private long _id;
        private string _name;
        private string _lastName;
        private string _userName;
        private string _password;
        private string _mail;

        // properties
        public long Id
        {
            get
            {
                return _id;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    Console.Write("\n---------- No ingresaste valor ----------\n" +
                        "\nPon un nombre: ");
                    value = Console.ReadLine();
                }
                _name = value;
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    Console.Write("\n---------- No ingresaste valor ----------\n" +
                        "\nPon un apellido: ");
                    value = Console.ReadLine();
                }
                _lastName = value;
            }
        }
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    Console.Write("\n---------- No ingresaste valor ----------\n" +
                        "\nPon un usuario: ");
                    value = Console.ReadLine();
                }
                _userName = value;
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    Console.Write("\n---------- No ingresaste valor ----------\n" +
                        "\nPon una contrasenia: ");
                    value = Console.ReadLine();
                }
                _password = value;
            }
        }
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    Console.Write("\n---------- No ingresaste valor ----------\n" +
                        "\nPon una descripcion: ");
                    value = Console.ReadLine();
                }
                _mail = value;
            }
        }


        // inherited method
        protected override ArrayList GetValues()
        {
            return new ArrayList()
            {
                _name,
                _lastName,
                _userName,
                _password,
                _mail
            };
        }

        protected override string[] GetFields(params object[] fields)
        {
            if(fields.Length == 0)
            {
                return new string[]
                {
                "Nombre",
                "Apellido",
                "NombreUsuario",
                "Contraseña",
                "Mail"
                };
            }

            else
                return (string[])fields;

        }

        protected override string GetTable()
        {
            return "Usuario";
        }

        //protected override User GetType()
        //{
        //    return this;
        //}
        protected override void SetValues(params object[] values)
        {
            this._id = (long)values[0];
            this._name = values[1].ToString();
            this._lastName = values[2].ToString();
            this._userName = values[3].ToString();
            this._password = values[4].ToString();
            this._mail = values[5].ToString();
        }

        // metodo para iniciar sesion
        public void SignIn()
        {
            string userName;
            string password;
            Console.WriteLine("\n\n========= INICIO DE SESION ============\n\n");
            Console.Write("Ingresa tu usuario: ");
            userName = Console.ReadLine();
            Console.Write("Ingresa tu contrasenia: ");
            //do
            //{
            //    Console.Read
            //}while()
        }

        
    }
}

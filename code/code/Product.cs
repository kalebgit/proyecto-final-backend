using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    internal class Product : Handler
    {
        

        // instance variables
        private long _id;
        private string _description;
        private decimal _cost;
        private decimal _sellingprice;
        private int _stock;

        // properties
        public long Id
        {
            get
            {
                return _id;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    Console.Write("\n---------- No ingresaste valor ----------\n" +
                        "\nPon una descripcion: ");
                    value = Console.ReadLine();
                }
                _description = value;
            }
        }
        public decimal Cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
            }
        }
        public decimal SellingPrice
        {
            get
            {
                return _sellingprice;
            }

            set
            {
                if(value <= 0)
                {
                    bool boolean;
                    do
                    {

                        Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                        "\nPon un precio de venta: ");
                        boolean = decimal.TryParse(Console.ReadLine(), out value);
                    } while (!boolean);
                    
                }
                _sellingprice = value;
            }
        }
        public int Stock
        {
            get
            {
                return _stock;
            }
            set
            {
                if (value <= 0)
                {
                    bool boolean;
                    do
                    {

                        Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                        "\nPon un precio de venta: ");
                        boolean = int.TryParse(Console.ReadLine(), out value);
                    } while (!boolean);

                }
                _stock = value;
            }
        }


        // inherited method
        protected override ArrayList GetValues()
        {
            return new ArrayList()
            {
                _description,
                _cost,
                _sellingprice,
                _stock
            };
        }

        protected override string[] GetFields()
        {
            return new string[] 
            {
            "Descripciones",
            "Costo",
            "PrecioVenta",
            "Stock",
            };
        }

        protected override string GetTable()
        {
            return "Prodcuto";
        }

        protected override Product GetType()
        {
            return new Product();
        }

        protected override void SetValues(params object[] values)
        {
            this._id = (long)values[0];
            this._description = values[1].ToString();
            this._cost = (decimal)values[2];
            this._sellingprice = (decimal)values[3];
            this._stock = (int)values[4];
        }
    }
}

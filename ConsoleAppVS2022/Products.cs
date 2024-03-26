using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    public class Product // by default internal
    {
        #region Fields
        private int _productId = 0; // this is private by default
        private string _productName = string.Empty; // use _ for fields
        private decimal _unitPrice = 0m;
        private short _unitInStock = 0;
        private bool _isDiscontinued = false;
        #endregion

        #region Methods
        public void Show()
        {
            Console.WriteLine($"Id: {_productId}, Name: {_productName}, Price:{_unitPrice}, Stock: {_unitInStock}, Discontinued:{_isDiscontinued}"); ;
        }
        
        #region Properties
        public int ProductId 
        { 
            get { return _productId; }
            set { _productId = value; }
        }
        public string ProductName
        { 
            get { return _productName; } 
            set { _productName = value; }
        }
        public decimal UnitPrice
        { 
            get { return _unitPrice; }
            set { _unitPrice = value; } 
        }  
        public short UnitInStock
        { 
            get { return _unitInStock; } 
            set { _unitInStock = value; }
        }
        public bool IsDiscontinued
        { 
            get { return _isDiscontinued;}
            set { _isDiscontinued = value; }
        }

        //public string Description { get; set; } ==> don't use in Production code
        //complier creates a field called d_backingField...
        //complier also generates get and set code
        #endregion
        #endregion

        #region Constructors
        public Product(): this(0)
        {
            /*_productName = string.Empty;
            _productId= 0;
            _isDiscontinued= false;
            _unitInStock= 0;
            _unitPrice= 0;*/
        }

        public Product(int productId)
            :this(productId,string.Empty,0,0)
        {
            /*_productId = productId;
            _productName = string.Empty;
            _isDiscontinued = false;
            _unitInStock = 0;
            _unitPrice = 0;*/
            Console.WriteLine("Id called");
        }
        public Product(int productId, string name)
            :this(productId,name,0)
        {
            /*_productId = productId;
            _productName = name;
            _isDiscontinued = false;
            _unitInStock = 0;
            _unitPrice = 0;*/
            Console.WriteLine("Id and name called");
        }
        public Product(int productId, string name, decimal price)
            :this(productId,name,price,0)
        {
            /*_productId = productId;
            _productName = name;
            _isDiscontinued = false;
            _unitInStock = 0;
            _unitPrice = price;*/
            Console.WriteLine("Id, name and price called");
        }
        public Product(int productId, string name, decimal price, short stock) 
        {
            _productId = productId;
            _productName = name;
            _isDiscontinued = false;
            _unitInStock = stock;
            _unitPrice = price;
            Console.WriteLine("Id,name,price,stock called");
        }
    #endregion
}
}

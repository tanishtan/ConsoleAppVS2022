using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    public class ProductManager
    {
        ProductCollection collection = new ProductCollection();
        public void CreateNew(Product model)
        {
            if(model is null)
                throw new ArgumentNullException("model","Argument missing");
            var item = GetProduct(model.ProductId);
            if(item is null)
            {
                var productId = collection.Count + 1;
                model.ProductId = productId;
                collection.Add(model);

            }
        }
        public void UpdateProduct(Product model)
        {
            var item = GetProduct(model.ProductId); 
            if(item != null)
            {
                collection.Remove(item);
                collection.Add(model);
            }
        }
        public Product GetProduct(int id)
        {
            Product item = null;
            /*foreach(var obj in collection.GetAll())
            {
                if(obj.ProductId == id)
                {
                    return obj;
                }
            }*/
            //var items = collection.GetAll();
            for(var i = 0; i < collection.Count; i++)
            {
                /*if (items[i].ProductId == id)
                {
                    return items[i];
                }*/
                if (collection[i].ProductId == id) return collection[i];
            }

            return null;
        }
        public Product[] GetProduct() => collection.GetAll();
    }
    public class ProductCollection : System.Collections.CollectionBase
    {
        //Method expressions initializer => uses lambda expression, works well with single line stmt
        /*public void Add(Product product) => List.Add(product);*/
        public void Add(Product product) { List.Add(product); }
        public Product GetAt(int position)
        {
            return (Product)List[position]!;
        }

        public Product[] GetAll()
        {
            Product[] products = new Product[List.Count];
            List.CopyTo(products, 0);
            return products;
        }
        public void Remove(Product product) { List.Remove(product); }

        new public int Count { get { return List.Count; } }

        //getter/setter
        public Product this[int index]
        {
            
            get {return List[index] as Product; }
            set {List[index] = value; }
        }
       
        //Property expression
        //public int Length { get => List.Count; }

        //Constructor expression
        /*int _capacity;
        public ProductCollection(int initialCapacity) => _capacity = initialCapacity;*/
    }
}

using System;
using System.Collections.Generic;

namespace project01
{
    #region class product
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int Quantity;
    } 
    #endregion

    internal class Program
    {
        #region list
        static List<Product> products = new List<Product>();
        static List<Product> cart = new List<Product>();

        #endregion

        #region stack for orders
        static Stack<Product> orders = new Stack<Product>();

        static double discount = 0.1; // 10% discount 
        #endregion

        #region add product
        static void AddProduct()
        {
            try
            {
                Product p = new Product();

                Console.Write("Enter Id: ");
                p.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Name: ");
                p.Name = Console.ReadLine();

                Console.Write("Enter Price: ");
                p.Price = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter Quantity: ");
                p.Quantity = Convert.ToInt32(Console.ReadLine());

                products.Add(p);

                Console.WriteLine("Product Added Successfully");
            }
            catch
            {
                Console.WriteLine("Invalid Input!");
            }
        }
        #endregion

        
        #region view products
        static void ViewProducts()
        {
            foreach (Product p in products)
            {
                Console.WriteLine("===================");
                Console.WriteLine($"Id: {p.Id}");
                Console.WriteLine($"Name: {p.Name}");
                Console.WriteLine($"Price: {p.Price}");
                Console.WriteLine($"Quantity: {p.Quantity}");
            }

            Console.WriteLine("Count: " + products.Count);
        } 
        #endregion

        #region Overloading Search by Id
        static void SearchProduct(int id)
        {
            Product found = products.Find(p => p.Id == id);

            if (found != null)
            {
                Console.WriteLine("Product Found");
                Console.WriteLine(found.Name);
            }
            else
            {
                Console.WriteLine("Product Not Found");
            }
        }
        #endregion

        #region Overloading Search by Name
        static void SearchProduct(string name)
        {
            Product found = products.Find(p => p.Name == name);

            if (found != null)
            {
                Console.WriteLine("Product Found");
                Console.WriteLine(found.Name);
            }
            else
            {
                Console.WriteLine("Product Not Found");
            } 
        #endregion
        }

       
        #region out
        static bool FindProduct(int id, out Product found)
        {
            found = products.Find(p => p.Id == id);
            return found != null;
        }
        #endregion


        #region ref
        


        static void UpdateQuantity(ref int x, int y)
        {
            x = x - y;
        }
        #endregion

        
       #region add to cart
        static void AddToCart(int productId, int quantity)
        {
            Product p;

            if (FindProduct(productId, out p))
            {
                if (p.Quantity >= quantity)
                {
                    UpdateQuantity(ref p.Quantity, quantity);

                    Product item = new Product();
                    item.Id = p.Id;
                    item.Name = p.Name;
                    item.Price = p.Price;
                    item.Quantity = quantity;

                    cart.Add(item);
        #endregion

      #region save order in stack
                    orders.Push(item);

                    Console.WriteLine("Added To Cart");
                }
                else
                {
                    Console.WriteLine("Insufficient Quantity");
                }
            }
            else
            {
                Console.WriteLine("Product Not Found");
            }
        } 
                    #endregion

       #region Rrcursion
        static void ShowCart(int index)
        {
            if (index >= cart.Count)
                return;

            Console.WriteLine("===================");
            Console.WriteLine($"Name: {cart[index].Name}");
            Console.WriteLine($"Price: {cart[index].Price}");
            Console.WriteLine($"Quantity: {cart[index].Quantity}");

            ShowCart(index + 1);
        }

        static void ViewCart()
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Cart Is Empty");
                return;
            }

            ShowCart(0);
        } 
        #endregion

       #region checkout with discount

        static void Checkout()
        {
            double total = 0;

            foreach (Product p in cart)
            {
                total += p.Price * p.Quantity;
            }

            double finalTotal = total - (total * discount);

            Console.WriteLine($"Total Before Discount = {total}");
            Console.WriteLine("Discount = 10%");
            Console.WriteLine($"Final Price = {finalTotal}");

            cart.Clear();

            Console.WriteLine("Checkout Done!");
        }

        #endregion



        #region Last order

        static void LastOrder()
        {
            if (orders.Count > 0)
            {
                Product p = orders.Peek();
                Console.WriteLine($"Last Order: {p.Name} | Qty: {p.Quantity}");
            }
            else
            {
                Console.WriteLine("No Orders Yet");
            }
        }

        static void Main(string[] args)
        {
            bool run = true;

            while (run)
            {
                try
                {
                    Console.WriteLine("\n1. Add Product");
                    Console.WriteLine("2. View Products");
                    Console.WriteLine("3. Search Product");
                    Console.WriteLine("4. Add To Cart");
                    Console.WriteLine("5. View Cart");
                    Console.WriteLine("6. Checkout");
                    Console.WriteLine("7. Last Order");
                    Console.WriteLine("8. Exit");

                    Console.Write("Choose: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            AddProduct();
                            break;

                        case 2:
                            ViewProducts();
                            break;

                        case 3:
                            Console.WriteLine("1. Search By Id");
                            Console.WriteLine("2. Search By Name");

                            int searchChoice = Convert.ToInt32(Console.ReadLine());

                            if (searchChoice == 1)
                            {
                                Console.Write("Enter Id: ");
                                SearchProduct(Convert.ToInt32(Console.ReadLine()));
                            }
                            else
                            {
                                Console.Write("Enter Name: ");
                                SearchProduct(Console.ReadLine());
                            }
                            break;

                        case 4:
                            Console.Write("Enter Product Id: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Quantity: ");
                            int qty = Convert.ToInt32(Console.ReadLine());

                            AddToCart(id, qty);
                            break;

                        case 5:
                            ViewCart();
                            break;

                        case 6:
                            Checkout();
                            break;

                        case 7:
                            LastOrder();
                            break;

                        case 8:
                            run = false;
                            break;

                        default:
                            Console.WriteLine("Invalid Choice");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Input!");
                }
            } 
        #endregion


        }
    }
}
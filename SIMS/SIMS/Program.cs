using System;

namespace SIMS
{
    class Program
    {
        static void Main(string[] args)
        {
            var inventory = new Inventory();

            while (true)
            {
                DisplayMenu();
                int choice = GetMenuChoice();
                ProcessMenuChoice(choice, inventory);
                Console.WriteLine();
            }
        } // End of Main method

        private static void DisplayMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. Search Product");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
        } // End of DisplayMenu method

        private static int GetMenuChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                Console.Write("Enter your choice: ");
            }

            return choice;
        } // End of GetMenuChoice method

        private static void ProcessMenuChoice(int choice, Inventory inventory)
        {
            switch (choice)
            {
                case 1:
                    AddProduct(inventory);
                    break;
                case 2:
                    ViewAllProducts(inventory);
                    break;
                case 3:
                    UpdateProduct(inventory);
                    break;
                case 4:
                    DeleteProduct(inventory);
                    break;
                case 5:
                    SearchProduct(inventory);
                    break;
                case 6:
                    Console.WriteLine("Exiting program...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        } // End of ProcessMenuChoice method

        private static void AddProduct(Inventory inventory)
        {
            Console.Write("Enter product name: ");
            string name = ReadNonEmptyString();

            Console.Write("Enter product price: ");
            decimal price = ReadDecimal();

            Console.Write("Enter product quantity: ");
            int quantity = ReadInteger();

            Product product = new Product(name, price, quantity);
            if (inventory.AddProduct(product))
            {
                Console.WriteLine("Product added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add product. Product may already exist.");
            }
        } // End of AddProduct method

        private static void ViewAllProducts(Inventory inventory)
        {
            inventory.ViewAllProducts();
        } // End of ViewAllProducts method

        private static void UpdateProduct(Inventory inventory)
        {
            Console.Write("Enter current product name: ");
            string currentProductName = ReadNonEmptyString();

            Console.Write("Enter new product name (leave empty to keep current name): ");
            string newProductNameInput = Console.ReadLine()?.Trim();
            string newProductName =
                string.IsNullOrEmpty(newProductNameInput) ? currentProductName : newProductNameInput;

            Console.Write("Enter new product price (leave empty to keep current price): ");
            string newProductPriceInput = Console.ReadLine()?.Trim();
            decimal? newProductPrice = string.IsNullOrEmpty(newProductPriceInput)
                ? null
                : decimal.Parse(newProductPriceInput);

            Console.Write("Enter new product quantity (leave empty to keep current quantity): ");
            string newProductQuantityInput = Console.ReadLine()?.Trim();
            int? newProductQuantity = string.IsNullOrEmpty(newProductQuantityInput)
                ? null
                : int.Parse(newProductQuantityInput);

            if (inventory.UpdateProduct(currentProductName, newProductName, newProductPrice, newProductQuantity))
            {
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update product. Product not found.");
            }
        } // End of UpdateProduct method

        private static void DeleteProduct(Inventory inventory)
        {
            Console.Write("Enter product name to delete: ");
            string deleteProductName = ReadNonEmptyString();
            if (inventory.DeleteProduct(deleteProductName))
            {
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete product. Product not found.");
            }
        } // End of DeleteProduct method

        private static void SearchProduct(Inventory inventory)
        {
            Console.Write("Enter product name to search: ");
            string searchProductName = ReadNonEmptyString();
            inventory.SearchProduct(searchProductName);
        } // End of SearchProduct method

        //Methods to handle user input
        private static string ReadNonEmptyString()
        {
            string input;
            do
            {
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid input. Please enter a non-empty string.");
                }
            } while (string.IsNullOrEmpty(input));

            return input;
        } // End of ReadNonEmptyString method

        private static decimal ReadDecimal()
        {
            decimal result;
            while (!decimal.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal number.");
            }

            return result;
        } // End of ReadDecimal method

        private static int ReadInteger()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }

            return result;
        } // End of ReadInteger method
    } // End of Program class
} // End of SIMS namespace
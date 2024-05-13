# Simple Inventory Management System

A basic inventory management system implemented as a C# console-based application, now enhanced with SQL Server for robust data management. This system allows a user to manage a list of products, where each product has a name, price, and quantity in stock.

## Main Functionalities

- **Add a product**: The user is prompted for the product name, price, and quantity. The product will be added to the SQL Server database, ensuring no duplicates exist based on the product name.
- **View all products**: Display a list of all products in the inventory, fetched from SQL Server, along with their prices and quantities.
- **Edit a product**: The user is asked for a product name. If the product exists in the SQL Server database, the user can update its name, price, or quantity.
- **Delete a product**: The user is asked for a product name. If the product is found in the SQL Server database, it will be removed.
- **Search for a product**: The user is asked for a product name. The system checks the SQL Server database and displays the product's details (name, price, and quantity) if found. If not, the user will be informed that the product was not found.

## Database Integration

The application utilizes SQL Server, a relational database management system, for storing and managing product data. This integration ensures data integrity and supports complex queries for advanced data management.

## Getting Started

Ensure you have SQL Server installed and running on your machine. Update the SQL Server connection string in the application settings to connect to your database instance.

### Setup

1. Create a new database named `InventoryManagement`.
2. Run the provided SQL script to create the `Products` table within your new database.
3. Update the application's configuration to point to your SQL Server instance.


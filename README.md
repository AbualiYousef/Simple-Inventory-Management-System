# Simple Inventory Management System

A basic inventory management system implemented as a C# console-based application, now enhanced with MongoDB for data storage. This system allows a user to manage a list of products, where each product has a name, price, and quantity in stock.

## Main Functionalities

- **Add a product**: The user is prompted for the product name, price, and quantity. The product will be added to the MongoDB database, ensuring no duplicates exist based on the product name.
- **View all products**: Display a list of all products in the inventory, fetched from MongoDB, along with their prices and quantities.
- **Edit a product**: The user is asked for a product name. If the product exists in the MongoDB database, the user can update its name, price, or quantity.
- **Delete a product**: The user is asked for a product name. If the product is found in the MongoDB database, it will be removed.
- **Search for a product**: The user is asked for a product name. The system checks the MongoDB database and displays the product's details (name, price, and quantity) if found. If not, the user will be informed that the product was not found.

## Database Integration

The application utilizes MongoDB, a NoSQL database, for storing and managing product data. This integration provides flexibility and scalability for handling large volumes of data efficiently.

## Getting Started

Ensure you have MongoDB installed and running on your machine. Update the MongoDB connection string in the application settings to connect to your database instance.


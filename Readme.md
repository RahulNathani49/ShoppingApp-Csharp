# Shopping App Project

This project is a Shopping App that allows users to browse and purchase products from an inventory. It provides functionalities such as user login, product selection, cart management, and order placement.

## Functionalities

1. User Authentication:
   - Users can log in using their username and password credentials.
   - User passwords are stored in an encrypted format in the users database.

2. Product View:
   - After logging in, users can view the available products fetched from the Inventory Database.

3. Cart Management:
   - Users can select items from the product list, specify the quantity, and add them to their cart.
   - The cart keeps track of the selected items and their quantities.
   - Users can see the total price of the items in the cart before proceeding to checkout.

4. Checkout and Order Placement:
   - Users can proceed to checkout from the cart.
   - When the user clicks on the checkout button, the items in the cart are placed as an order.
   - The cart becomes empty after the order is placed.
   - Users can view their order history from the order history tab.

## Database

The project uses the following database tables:

- User1: This table stores user data, including usernames and encrypted passwords.
- Inventory: Products are stored in the inventory table.
- Cart: The cart table stores the selected items and their quantities.
- Orders: The orders table stores all the placed orders along with the order IDs for each user.

To set up the database, run the SQL queries provided in the `sqlquery/databasequery.sql` file. These queries will create the necessary tables and populate them with sample product values.

## Running the Project

To run the project, follow these steps:

1. Clone the repository.
2. Set up the database using the provided SQL queries.
3. Configure the database connection in the project files.
4. Install any necessary dependencies.
5. Run the project.

## Contributing

Contributions to the project are welcome. If you have any suggestions, improvements, or new features to add, please open an issue or submit a pull request.


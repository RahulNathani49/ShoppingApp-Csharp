create database ShoppingCart;
use ShoppingCart;


create Table Users1(
UserId int Identity(1001,1) Primary Key,
UserName nvarchar(50) NOT NULL,
HashPassword varbinary(50) NOT NULL,
Salt varbinary(50) NOT NULL,
IsLoggedIn bit, 
CreatedOn DateTime2 NOT NULL,
) 

select * from Users1;

create Table Inventory(
ProductID int Identity(10001,1) Primary Key,
ProductName nvarchar(50) NOT NULL,
ProductCategory nvarchar(50) NOT NULL,
ProductDescription nvarchar(50) NOT NULL,
ProductPrice decimal not null,
Discount decimal
)
drop table inventory;
insert into dbo.inventory values ('Nike Shoes','Shoes','Formal Footwear',99.80,11.90);
insert into dbo.inventory values ('Polo Shirt','Shirt','Formal white Shirt',55.80,10.00);

select * from dbo.Inventory


create table Cart(
CartId int Identity,
UserId int NOT NULL,
ProductID int NOT NULL,
Quantity int NOT NULL,
Primary Key(UserId,ProductId)
)


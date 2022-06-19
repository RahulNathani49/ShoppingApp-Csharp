create database ShoppingCart;
use ShoppingCart;


create Table Users1(
UserId int Identity(1001,1) Primary Key,
UserName nvarchar(50) NOT NULL,
HashPassword varbinary(50) NOT NULL,
Salt varbinary(50) NOT NULL,
IsLoggedIn bit, 
CreatedOn DateTime2 NOT NULL,
);



create Table Inventory(
ProductID int Identity(10001,1) Primary Key,
ProductName nvarchar(50) NOT NULL,
ProductCategory nvarchar(50) NOT NULL,
ProductDescription nvarchar(50) NOT NULL,
ProductPrice decimal not null,
Discount decimal
);

insert into dbo.inventory values ('Nike Shoes','Shoes','Formal Footwear',99.80,11.90);
insert into dbo.inventory values ('Polo Shirt','Shirt','Formal white Shirt',55.80,10.00);
insert into dbo.inventory values ('adidas jacket','jacket','Formal white jacket',25.80,3.00);
insert into dbo.inventory values ('Polo jeans','jeans','black jeans',99.80,10.00);

create table Cart(
CartId int Identity,
UserId int NOT NULL,
ProductID int NOT NULL,
Quantity int NOT NULL,
Primary Key(UserId,ProductId)
);

ALTER TABLE Cart
ADD FOREIGN KEY (ProductID) REFERENCES Inventory(ProductID);

create Table UserToOrderMap(
UserId int Foreign Key references Users1(UserId) NOT NULL,
OrderId int Primary kEY Identity(2000,1)
);

create Table Orders(
OrderId int Foreign Key references UserToOrderMap(OrderId),
ProductId int Not NUll Foreign Key References Inventory(ProductID),
Quantity int not null
);

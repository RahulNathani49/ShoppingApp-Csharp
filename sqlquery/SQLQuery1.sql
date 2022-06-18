create database ShoppingCart;
use ShoppingCart;

create Table Users(
UserId int Identity(1001,1) Primary Key,
UserName nvarchar(50) NOT NULL,
HashPassword binary(64) NOT NULL,
Salt binary(64) NOT NULL,
CreatedOn DateTime2 NOT NULL
) 




insert into users values('rahul','pass@123')
insert int users values('Omkar','value@123');

select * from Users

truncate table Users



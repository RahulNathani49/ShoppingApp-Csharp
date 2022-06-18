create database ShoppingCart;
use ShoppingCart;

create Table Users(
UserId int Identity(1001,1) Primary Key,
UserName nvarchar(50) NOT NULL,
HashPassword varbinary(64) NOT NULL,
Salt binary(64) NOT NULL,
IsLoggedIn bit, 
CreatedOn DateTime2 NOT NULL,

) 

create Table Users1(
UserId int Identity(1001,1) Primary Key,
UserName nvarchar(50) NOT NULL,
HashPassword varbinary(50) NOT NULL,
Salt varbinary(50) NOT NULL,
IsLoggedIn bit, 
CreatedOn DateTime2 NOT NULL,
) 

select * from Users1;
drop table Users
insert into users values('rahul','pass@123')
insert int users values('Omkar','value@123');
insert int users values('Harmeet','key@123');

select * from Users

delete from Users where UserId in(1002,1003);
select * from Users;
truncate table Users

update users set IsLoggedIn=0 where UserName='Harmeet'


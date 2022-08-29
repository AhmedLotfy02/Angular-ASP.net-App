
CREATE DATABASE taskDB; 

create table taskDB.dbo.Users( 
 firstName  varchar(100)    not null, 
 fatherName  varchar(100)    not null, 
 familyName  varchar(100)    not null,  
    birthdate varchar(200)    not null, 
 occupation varchar(200)    not null,  
    address  varchar(200)    not null,  
    password  varchar(200)    not null,  
    username varchar(100)    not null, 
     
     primary key(username) 
);
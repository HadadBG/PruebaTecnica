create database prueba1;
use prueba1;
create table usuarios(
    userId int PRIMARY KEY AUTO_INCREMENT,
    Login VARCHAR(100),
    Nombre VARCHAR(100),
    Paterno  VARCHAR(100),
    Materno VARCHAR(100)
);

create table empleados(
    userId int PRIMARY KEY,
    Sueldo double,
    FechaIngreso date,
    FOREIGN KEY (userId) REFERENCES usuarios(userId)
);

LOAD DATA INFILE 'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/DatosPracticaSQL1.csv' 
INTO TABLE usuarios 
FIELDS TERMINATED BY ','
LINES TERMINATED BY '\r\n'(Login,Nombre,Paterno,Materno);


DELIMITER $$
CREATE FUNCTION transform ( starting_value VARCHAR(100) )
RETURNS double
BEGIN

   DECLARE aux VARCHAR(100);
   DECLARE strlen int;
   SET aux = TRIM(starting_value);
   SET strLen = LENGTH(aux);
   RETURN REPLACE(MID(aux,2,strLen),",","")+0.0 ;

END; $$
DELIMITER ;

LOAD DATA INFILE 'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/DatosPracticaSQL2.csv' 
INTO TABLE empleados 
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\r\n'(userId,@var1,FechaIngreso)
SET Sueldo = transform(@var1);

select userId from usuarios where  userId<6 or userId>11 or userId=8;

Select u.nombre,e.sueldo from empleados e LEFT JOIN usuarios u on e.userId = u.userId 
where e.FechaIngreso BETWEEN '2000-01-01' AND '2001-01-01';

update empleados e LEFT JOIN usuarios u on e.userId = u.userId SET e.sueldo=e.sueldo*1.10 
where e.FechaIngreso BETWEEN '2000-01-01' AND '2001-01-01';

Select u.nombre,e.sueldo from empleados e LEFT JOIN usuarios u on e.userId = u.userId 
where e.FechaIngreso BETWEEN '2000-01-01' AND '2001-01-01';

Select GROUP_CONCAT(u.nombre) as "empleados menor a 1200" , e.sueldo as "sueldo" from empleados e LEFT JOIN usuarios u on e.userId = u.userId 
 group by sueldo
having sueldo < 1200  ;


Select GROUP_CONCAT(u.nombre) as "empleados mayor o igual a 1200" , e.sueldo as "sueldo" from empleados e LEFT JOIN usuarios u on e.userId = u.userId 
 group by sueldo
having sueldo >= 1200 ;

       
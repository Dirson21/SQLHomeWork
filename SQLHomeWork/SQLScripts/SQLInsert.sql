--INSERT
INSERT INTO CustomerAccount(Login, Password, Balance)
VALUES ('log1', 'pas1',  100),
	   ('log2', 'pas2',  100),
	   ('log3', 'pas3',  100),
	   ('log4', 'pas4',  100),
	   ('log5', 'pas5',  100),
       ('log6', 'pas6',  100),
	   ('log7', 'pas7',  100)

INSERT INTO Product(Name, Price)
VALUES  ('Пеперони', 20),
		('Сырная', 24),
		('Маргарита', 10),
		('Милано', 25),
		('Песто', 20),
		('Карбонара', 20)

INSERT INTO "Order"(CustomerId,TimeOrder)
VALUES (1, '2022-07-11 16:37:23'),
	   (6, '2022-06-11 14:37:23')

INSERT INTO OrderProduct(OrderId,ProductId)
VALUES	(1, 1),
		(2, 1),
		(1, 3),
		(1, 4),
		(2, 2)
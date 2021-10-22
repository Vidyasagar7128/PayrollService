ALTER PROCEDURE sp_PayRoll(
	@BasicPay varchar(50),
	@Deduction varchar(50),
	@TaxablePay varchar(50),
	@IncomeTax varchar(50),
	@NetPay varchar(50),
	@EmployeeId int
)
AS
BEGIN
SET NOCOUNT ON;
INSERT into payRoll values(@BasicPay,@Deduction,@TaxablePay,@IncomeTax,@NetPay,@EmployeeId);
END
GO

ALTER PROCEDURE sp_createEmp(
	@Name varchar(50),
	@Address varchar(50),
	@Gender varchar(50),
	@StartDate varchar(50)
)
AS
BEGIN
SET NOCOUNT ON;
INSERT into employee values(@Name,@Address,@Gender,@StartDate);
END
GO

Create Table employee
(
	Id int IDENTITY PRIMARY KEY,
	Name varchar(255) NOT NULL,
	Address varchar(100) DEFAULT 'Delhi',
	Gender varchar(5),
	StartDate DATE,
	UNIQUE (Name)
);

SELECT * from employee
CREATE Table Department
(
	DepartmentId int IDENTITY PRIMARY KEY,
	DepartmentName varchar(255) NOT NULL,
	EmployeeId int,
	FOREIGN KEY (EmployeeId) REFERENCES employee(Id)
);
Create Table PayRoll
(
	PayRollId int IDENTITY PRIMARY KEY,
	BasicPay varchar(50),
	Deduction varchar(50),
	TaxablePay varchar(50),
	IncomeTax varchar(50),
	NetPay varchar(50),
	EmployeeId int
);
SELECT * from payRoll;
Insert into payRoll(BasicPay,Deduction,TaxablePay,IncomeTax,NetPay,EmployeeId) values(28000,2000,800,100,25900,8);
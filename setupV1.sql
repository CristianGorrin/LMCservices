create database LMCdatabase
go
use LMCdatabase
go

-- Tables OK
create table tblDepartment
(
	department int IDENTITY(1,1) primary key NOT NULL, 
	companyName nvarchar(100) NOT NULL,
	cvrNo int NOT NULL,
	phoneNo nvarchar(25) NOT NULL UNIQUE,
	altPhoneNo nvarchar(25),
	_address nvarchar(100) NOT NULL,
	postNo int NOT NULL, --FK tblPostNo
	email nvarchar(50),
	active bit,
	departmentHeadNo int NOT NULL --FK tblWorkers
);

create table tblWorkers
(
	workNo int IDENTITY(1,1) primary key NOT NULL,
	name nvarchar(50) NOT NULL,
	surname nvarchar(50) NOT NULL,
	workerStatus int NOT NULL, --FK tblWorkerStatus
	phoneNo nvarchar(25) NOT NULL,
	altPhoneNo nvarchar(25),
	homeAddress nvarchar(100) NOT NULL,
	postNo int NOT NULL, --FK statusNo
	email nvarchar(50),
	active bit
);

create table tblBankAccounts
(
	Id int IDENTITY(1,1) primary key NOT NULL,
	bank nvarchar(100) NOT NULL,
	accountName nvarchar(50),
	regNo int NOT NULL,
	accountNo int NOT NULL,
	balance decimal(8,4)
);

create table tblWorkerStatus
(
	statusNo int IDENTITY(1,1) primary key NOT NULL,
	status nvarchar(40) NOT NULL
);

create table tblPostNo
(
	ID int IDENTITY(1,1) primary key NOT NULL,
	postNo int NOT NULL UNIQUE,
	city nvarchar(100) NOT NULL UNIQUE
);

create table tblPrivateCustomers
(
	privateCustomersNo int IDENTITY(1,1) primary key NOT NULL,
	name nvarchar(50) NOT NULL,
	surname nvarchar(50) NOT NULL,
	phoneNo nvarchar(25) NOT NULL,
	altPhoneNo nvarchar(25),
	homeAddress nvarchar(100) NOT NULL,
	postNo int NOT NULL, --FK tblPostNo
	email nvarchar(50),
	active bit,
);

create table tblCompanyCustomers
(
	companyCustomersNo int IDENTITY(1,1) primary key NOT NULL,
	companyName nvarchar(100) NOT NULL,
	companyContactPerson nvarchar(100),
	cvrNo int NOT NULL,
	phoneNo nvarchar(25) NOT NULL,
	altPhoneNo nvarchar(25),
	_address nvarchar(100) NOT NULL,
	postNo int NOT NULL, --FK tblPostNo
	email nvarchar(50),
	active bit
);

create table tblCompanyOrders
(
	invoiceNo int IDENTITY(1,1) primary key NOT NULL,
	createdDate smalldatetime,
	taskDate smalldatetime NOT NULL,
	descriptionTask nvarchar(200) NOT NULL,
	dateSendBill smalldatetime,
	daysToPaid int,
	hoursUse decimal(3,1),
	paidHour decimal(3,1),
	createBy int NOT NULL, --FK tblWorkers
	paidToACC int NOT NULL, --FK tblBankAccounts
	customer int NOT NULL, --FK tblCompanyCustomers
	paid bit
);

create table tblPrivetOrders
(
	invoiceNo int IDENTITY(1,1) primary key NOT NULL,
	createdDate smalldatetime,
	taskDate smalldatetime NOT NULL,
	descriptionTask nvarchar(200) NOT NULL,
	dateSendBill smalldatetime,
	daysToPaid int,
	hoursUse decimal(3,1),
	paidHour decimal(3,1),
	createBy int NOT NULL, --FK tblWorkers
	paidToACC int NOT NULL, --FK tblBankAccounts
	customers int NOT NULL, --FK tblPrivateCustomers
	paid bit
);

create table tblDeleteItems
(
	ItemNo int IDENTITY(1,1) primary key NOT NULL,
	itemInfo nvarchar(MAX), -- has the information to restore an item from a table in DB in string
	deleteDate smalldatetime,
	restored bit
);

go
-- FK Constraints OK
Alter Table tblDepartment Add Constraint FK_tblDepartment_tblPostNo_postNo
foreign Key (postNo) references tblPostNo (ID);

Alter Table tblDepartment Add Constraint FK_tblDepartment_tblWorkers_workNo
foreign Key (departmentHeadNo) references tblWorkers (workNo);

Alter Table tblWorkers Add Constraint FK_tblWorkers_tblWorkerStatus_workerStatus
foreign Key (workerStatus) references tblWorkerStatus (statusNo);

Alter Table tblWorkers Add Constraint FK_tblWorker_statusNo_postNo
foreign Key (postNo) references tblPostNo (ID);

Alter Table tblPrivateCustomers Add Constraint FK_tblPrivateCustomers_tblPostNo_postNo
foreign Key (postNo) references tblPostNo (ID);

Alter Table tblCompanyCustomers Add Constraint FK_tblCompanyCustomers_tblPostNo_postNo
foreign Key (postNo) references tblPostNo (ID);

Alter Table tblCompanyOrders Add Constraint FK_tblCompanyOrders_tblWorkers_createBy
foreign Key (createBy) references tblWorkers (workNo);

Alter Table tblCompanyOrders Add Constraint FK_tblCompanyOrders_tblBankAccounts_paidToACC
foreign Key (paidToACC) references tblBankAccounts (Id);

Alter Table tblCompanyOrders Add Constraint FK_tblCompanyOrders_tblCompanyCustomers_customer
foreign Key (customer) references tblCompanyCustomers (companyCustomersNo);

Alter Table tblPrivetOrders Add Constraint FK_tblPrivetOrders_tblWorkers_createBy
foreign Key (createBy) references tblWorkers (workNo);

Alter Table tblPrivetOrders Add Constraint FK_tblPrivetOrders_tblBankAccounts_paidToACC
foreign Key (paidToACC) references tblBankAccounts (Id);

Alter Table tblPrivetOrders Add Constraint FK_tblOrivetOrders_tblPrivateCustomers_customers
foreign Key (customers) references tblPrivateCustomers (privateCustomersNo);

go
-- Default Constraints OK
Alter table tblDepartment Add constraint def_tblDepartment_active
default 1 for active;

Alter table tblWorkers Add constraint def_tblWorkers_active
default 1 for active;

Alter table tblPrivateCustomers Add constraint def_tblPrivateCustomers_active
default 1 for active;

Alter table tblCompanyCustomers Add constraint def_tblCompanyCustomers_companyContactPerson
default 'null' for companyContactPerson;

Alter table tblCompanyCustomers Add constraint def_tblCompanyCustomers_active
default 1 for active;

Alter table tblCompanyOrders Add constraint def_tblCompanyOrders_createdDate
default GETDATE() for createdDate;

Alter table tblCompanyOrders Add constraint def_tblCompanyOrders_daysToPaid
default 0 for daysToPaid;

Alter table tblCompanyOrders Add constraint def_tblCompanyOrders_paid
default 0 for paid;

Alter table tblPrivetOrders Add Constraint def_tblPrivetOrders_createDate
default GETDATE() for createdDate;

Alter table tblPrivetOrders Add constraint def_tblPrivetOrders_daysToPaid
default 0 for daysToPaid;

Alter table tblPrivetOrders Add constraint def_tblPrivetOrders_paid
default 0 for paid;

ALter table tblBankAccounts Add constraint def_tblBankAccounts_balance
default 0 for balance;

go

-- Check Constraints OK
Alter table tblDepartment Add constraint CH_tblDeparment_email
Check (email like '_%@_%._%');

Alter table tblWorkers Add constraint CH_tblWorkers_email
Check (email like '_%@_%._%');

Alter table tblPrivateCustomers Add constraint CH_tblPrivateCustomers_email
Check (email like '_%@_%._%;');

Alter table tblCompanyCustomers Add constraint CH_tblCompanyCustomers_email
Check (email like '_%@_%._%');

go

-- TODO stored procedure need testing
--tblDepartment 
create procedure [tblDepartment.ADD]
	@companyName nvarchar(100),
	@cvrNo int,
	@phoneNo nvarchar(25),
	@altPhoneNo nvarchar(25),
	@address nvarchar(100),
	@postNo int,
	@email nvarchar(50),
	@active bit,
	@departmentHeadNo int
as
	INSERT INTO [dbo].[tblDepartment](
		companyName,
		cvrNo,
		phoneNo,
		altPhoneNo,
		[_address],
		postNo,
		email,
		active,
		departmentHeadNo
		) VALUES (
		@companyName,
		@cvrNo,
		@phoneNo,
		@altPhoneNo,
		@address,
		@postNo,
		@email,
		@active,
		@departmentHeadNo
		)
go

create procedure [tblDepartment.FIND]
	@department int
as
	select 
	department,	
	companyName,
	cvrNo,
	phoneNo,
	altPhoneNo,
	postNo,
	_address,
	postNo,
	email,
	active,
	departmentHeadNo
	from [dbo].[tblDepartment] where department = @department
go

create procedure [tblDepartment.GET]
	@active bit
as
	SELECT 
	department,
	companyName,
	cvrNo,
	phoneNo,
	altPhoneNo,
	_address,
	postNo,
	email,
	active,
	departmentHeadNo
	FROM [dbo].[tblDepartment] where active = @active
go

create procedure [tblDepartment.UPDATE]
	@department int,
	@companyName nvarchar(100),
	@cvrNo int,
	@phoneNo nvarchar(25),
	@altPhoneNo nvarchar(25),
	@address nvarchar(100),
	@postNo int,
	@email nvarchar(50),
	@active bit,
	@departmentHeadNo int
as
	UPDATE [dbo].[tblDepartment]
	   SET 
			companyName = @companyName,
			cvrNo = @cvrNo,
			phoneNo = @phoneNo,
			altPhoneNo = @altPhoneNo,
			_address = @address,
			postNo = @postNo,
			email = @email,
			active = @active,
			departmentHeadNo = @departmentHeadNo 
	 WHERE department = @department
go

create procedure [tblDepartment.DELETE]
	@Department int
as
	declare @result nvarchar(MAX)

	select @result = coalesce('[tblDepartment] { department = ' + CAST(department as nvarchar(10)) + ', cvrNo = ' + CAST(cvrNo as nvarchar(10)) +
	', phoneNo = ' + phoneNo + ', altPhoneNo = ' + altPhoneNo + ', _address = ' + _address + ', postNo = ' + CAST(postNo as nvarchar(10)) + 
	', email = ' + email + ', active = ' + CAST(active as nvarchar(1)) + ', departmentHeadNo = ' + CAST(departmentHeadNo as nvarchar(10)) + ' }' ,'')
	from [dbo].[tblDepartment] where  department = @Department

	insert into [dbo].[tblDeleteItems](itemInfo, deleteDate, restored) values (@result, GETDATE(), 0)
	delete from [dbo].[tblDepartment] where department = @Department
go

--tblBankAccounts
create procedure [tblBankAccounts.ADD]
	@bank nvarchar(100),
	@accountName nvarchar(50),
	@regNo int,
	@accountNo int,
	@balance decimal(8,4)
as
	Insert into [dbo].[tblBankAccounts]
	(
		bank,
		accountName,
		regNo,
		accountNo,
		balance 
	) Values(
		@bank,
		@accountName,
		@regNo,
		@accountNo,
		@balance
	)
go

create procedure [tblBankAccounts.FIND]
	@id int
as
	Select
	Id,
	bank,
	accountName,
	regNo,
	AccountNo,
	balance
	From [dbo].[tblBankAccounts] where Id = @id
go

create procedure [tblBankAccounts.GET]
	@Id int
as
	Select 
	id,
	bank,
	accountName,
	regNo,
	accountNo
	From [dbo].[tblBankAccounts] where id = @Id
go

create procedure [tblBankAccounts.DELETE]
	@ID int
as
	declare @result nvarchar(MAX)

	select @result = coalesce('[tblBankAccounts] { indexId = ' + CAST(Id as nvarchar(10)) + ', bank = ' + bank + ', accountName = ' +
	accountName + ', regNo = ' + CAST(regNo as nvarchar(10)) + ', accountNo = ' + CAST(accountNo as nvarchar(10)) +  
	', balance = ' + CAST(balance as nvarchar(11)) + ' }','')
	from [dbo].[tblBankAccounts] where Id = @ID

	insert into [dbo].[tblDeleteItems](itemInfo, deleteDate, restored) values (@result, GETDATE(), 0)
	delete from [dbo].[tblBankAccounts] where Id = @ID
go

create procedure [tblBankAccounts.UPDATE]
	@indexId int,
	@bank nvarchar(100),
	@accountName nvarchar(50),
	@regNo int,
	@accountNo int,
	@balance decimal(8,4)
as
	UPDATE [dbo].[tblBankAccounts]
	SET 
		bank = @bank,
		accountName = @accountName,
		regNo = @regNo,
		accountNo = @accountNo,
		balance = @balance
	WHERE Id = @indexId
go


--tblCompanyCustomers
create procedure [tblCompanyCustomers.ADD]
	@companyName nvarchar(100),
	@companyContactPerson nvarchar(100),
	@cvrNo int,
	@phoneNo nvarchar(25),
	@altPhoneNo nvarchar(25),
	@_address nvarchar(100),
	@postNo int,
	@email nvarchar(50),
	@active bit
as
	Insert into [dbo].[tblCompanyCustomers](
	companyName,
	companyContactPerson,
	cvrNo,
	phoneNo,
	altPhoneNo,
	_address,
	postNo,
	email,
	active
	) Values (
	@companyName,
	@companyContactPerson,
	@cvrNo,
	@phoneNo,
	@altPhoneNo,
	@_address,
	@postNo,
	@email,
	@active
	)
go

create procedure [tblCompanyCustomers.FIND]
	@companyCustomersNo int
as
	Select
	companyCustomersNo,
	companyName,
	companyContactPerson,
	cvrNo,
	phoneNo,
	altPhoneNo,
	_address,
	postNo,
	email,
	active
	From [dbo].[tblCompanyCustomers] where companyCustomersNo = @companyCustomersNo
go

create procedure [tblCompanyCustomers.GET]
	@companyCustomersNo int
as
	Select
	companyCustomersNo,
	companyName,
	companyContactPerson,
	cvrNo,
	phoneNo,
	altPhoneNo,
	_address,
	postNo,
	email,
	active
	From [dbo].[tblCompanyCustomers] where companyCustomersNo = @companyCustomersNo
go

create procedure [tblCompanyCustomers.DELETE]
	@companyCustomersNo int
as
	declare @result nvarchar(max)

	select @result = coalesce('[tblCompanyCustomers] { companyCustomersNo = ' + CAST(companyCustomersNo as nvarchar(10)) + ', CompanyName = ' +
	companyName + ', companyContactPerson = ' + companyContactPerson + ', cvrNo = ' + CAST(cvrNo as nvarchar(10)) + ', phoneNo = ' + phoneNo +
	', altPhoneNo = ' + altPhoneNo + ', _address = ' + _address + ', postNo = ' + CAST(postNo as nvarchar(10)) + ', email = ' + email + ', active = ' +
	CAST(active as nvarchar(1)) + ' }','')
	from [dbo].[tblCompanyCustomers] where companyCustomersNo = @companyCustomersNo

	insert into [dbo].[tblDeleteItems](itemInfo, deleteDate, restored) values (@result, GETDATE(), 0)
	delete from [dbo].[tblCompanyCustomers] where companyCustomersNo = @companyCustomersNo
go

create procedure [tblCompanyCustomers.UPDATE]
	@companyCustomersNo int,
	@companyName nvarchar(100),
	@companyContactPerson nvarchar(100),
	@cvrNo int,
	@phoneNo nvarchar(25),
	@altPhoneNo nvarchar(25),
	@_address nvarchar(100),
	@postNo int,
	@email nvarchar(50),
	@active bit
as
	Update [dbo].[tblCompanyCustomers] Set
	companyName = @companyName,
	companyContactPerson = @companyContactPerson,
	cvrNo = @cvrNo,
	phoneNo = @phoneNo,
	altPhoneNo = @altPhoneNo,
	_address = @_address,
	postNo = @postNo,
	email = @email,
	active = @active
	where companyCustomersNo = @companyCustomersNo
go


--tblCompanyOders
create procedure [tblCompanyOrders.ADD]
	@createdDate smalldatetime,
    @taskDate smalldatetime,
    @descriptionTask nvarchar(200),
    @dateSendBill smalldatetime,
    @daysToPaid int,
    @hoursUse decimal(3,1),
    @paidHour decimal(3,1),
    @createBy int,
    @paidToACC int,
    @customer int,
    @paid bit
as
	Insert into [dbo].[tblCompanyOrders](
		createdDate,
        taskDate,
        descriptionTask,
        dateSendBill,
        daysToPaid,
        hoursUse,
        paidHour,
        createBy,
        paidToACC,
        customer,
        paid
		) values (
		@createdDate,
		@taskDate,
		@descriptionTask,
		@dateSendBill,
		@daysToPaid,
		@hoursUse,
		@paid,
		@createBy,
		@paidToACC,
		@customer,
		@paid
		)
go

create procedure [tblCompanyOrders.FIND]
	@invoiceNo int
as
	select
	invoiceNo,
    createdDate,
    taskDate,
    descriptionTask,
    dateSendBill,
    daysToPaid,
    hoursUse,
    paidHour,
    createBy,
    paidToACC,
    customer,
    paid
  FROM [dbo].[tblCompanyOrders] where invoiceNo = @invoiceNo
go

create procedure [tblCompanyOrders.GET]
	@invoiceNo int
as
	select
	invoiceNo,
    createdDate,
    taskDate,
    descriptionTask,
    dateSendBill,
    daysToPaid,
    hoursUse,
    paidHour,
    createBy,
    paidToACC,
    customer,
    paid
  FROM [dbo].[tblCompanyOrders] where invoiceNo = @invoiceNo
go

create procedure [tblCompanyOrders.DELETE]
	@invoiceNo int
as
	declare @result nvarchar(MAX)

	select @result = coalesce('[tblCompanyOrders] { invoiceNo = ' + CAST(invoiceNo as nvarchar(10)) + ', createdDate = ' + CAST(createdDate as nvarchar(20)) + 
	', taskDate = ' + CAST(taskDate as nvarchar(20)) + ', descriptionTask = ' + descriptionTask + ', dateSendBill = ' + CAST(dateSendBill as nvarchar(20)) + 
	', daysToPaid = ' + CAST(daysToPaid as nvarchar(10)) + ', hoursUse = ' + CAST(hoursUse as nvarchar(3)) + ', paidHour =  ' + CAST(paidHour as nvarchar(3)) +
	', createBy = ' + CAST(createBy as nvarchar(10)) + ', paidToACC = ' + CAST(paidToACC as nvarchar(10)) + ', customer = ' + CAST(customer as nvarchar(10)) + 
	', paid = ' + CAST(paid as nvarchar(1)),'')
	from [dbo].[tblCompanyOrders] where invoiceNo = @invoiceNo

	insert into [dbo].[tblDeleteItems](itemInfo, deleteDate, restored) values (@result, GETDATE(), 0)
	delete from [dbo].[tblCompanyOrders] where invoiceNo = @invoiceNo
go

create procedure [tblCompanyOrders.UPDATE]
	@invoiceNo int,
	@createdDate smalldatetime,
    @taskDate smalldatetime,
    @descriptionTask nvarchar(200),
    @dateSendBill smalldatetime,
    @daysToPaid int,
    @hoursUse decimal(3,1),
    @paidHour decimal(3,1),
    @createBy int,
    @paidToACC int,
    @customer int,
    @paid bit
as
	Update [dbo].[tblCompanyOrders] set
	createdDate = @createdDate,
	taskDate = @taskDate,
	descriptionTask = @descriptionTask,
	dateSendBill = @dateSendBill,
	daysToPaid = @daysToPaid,
	hoursUse = @hoursUse,
	createBy = @createBy,
	paidToACC = @paidToACC,
	customer = @customer,
	paid = @paid
	where invoiceNo = @invoiceNo
go

--tblPostNo
create procedure [tblPostNo.ADD]
	@postNo int,
	@city nvarchar(100)
as
	Insert into [dbo].[tblPostNo](
	postNo,
	city
	) values (
	@postNo,
	@city
	)
go

create procedure [tblPostNo.FIND]
	@ID int
as
	Select 
	ID,
	postNo,
	city
	from [dbo].[tblPostNo] where ID = @ID
go

create procedure [tblPostNo.GET]
	@ID int
as
	Select 
	ID,
	postNo,
	city
	from [dbo].[tblPostNo] where ID = @ID
go

create procedure [tblPostNo.DELETE]
	@ID int
as
	declare @result nvarchar(MAX)

	select @result = coalesce('[tblPostNo] { ID = ' + CAST(ID as nvarchar(10)) + ', postNo = ' + CAST(postNo as nvarchar(10)) +
	 ', city = ' + city + ' }' ,'') 
	from [dbo].[tblPostNo] where ID = @ID

	insert into [dbo].[tblDeleteItems](itemInfo, deleteDate, restored) values (@result, GETDATE(), 0)
	delete from [dbo].[tblPostNo] where ID = @ID 
go

create procedure [tblPostNo.UPDATE]
	@ID int,
	@postNo int,
	@city nvarchar(100)
as
	Update [dbo].[tblPostNo] Set
	postNo = @postNo,
	city = @city
	where ID = @ID
go


--privateCustomersNo
create procedure [tblPrivateCustomers.ADD]
	@name nvarchar(50),
	@surname nvarchar(50),
	@phoneNo nvarchar(25),
	@altPoneNo nvarchar(25),
	@homeAddress nvarchar(100),
	@postNo int,
	@email nvarchar(50),
	@active bit
as
	Insert into [dbo].[tblPrivateCustomers](
	name,
	surname,
	phoneNo,
	altPhoneNo,
	homeAddress,
	postNo,
	email,
	active
	) values (
	@name,
	@surname,
	@phoneNo,
	@altPoneNo,
	@homeAddress,
	@postNo,
	@email,
	@active
	)
go

create procedure [tblPrivateCustomers.FIND]
	@privateCustomersNo int
as
	select 
	privateCustomersNo,
	name,
	surname,
	phoneNo,
	altPhoneNo,
	homeAddress,
	postNo,
	email,
	active
	from [dbo].[tblPrivateCustomers] where privateCustomersNo = @privateCustomersNo
go

create procedure [tblPrivateCustomers.GET]
	@privateCustomersNo int
as
	select 
	privateCustomersNo,
	name,
	surname,
	phoneNo,
	altPhoneNo,
	homeAddress,
	postNo,
	email,
	active
	from [dbo].[tblPrivateCustomers] where privateCustomersNo = @privateCustomersNo
go

create procedure [tblPrivateCustomers.DELETE]
	@privetCustomersNo int
as
	declare @result nvarchar(MAX)

	select @result = coalesce('[tblPrivateCustomers] { privateCustomersNo = ' + CAST(privateCustomersNo as nvarchar(10)) + ', name = ' + name +
	', surname = ' + surname + ', phoneNo = ' + phoneNo + ', alePhoneNo = ' + altPhoneNo + ', homeAddress = ' + homeAddress + 
	', postNo = ' + CAST(postNo as nvarchar(10)) + ', email = ' + email + ', active = ' + CAST(active as nvarchar(1)),'')
	from [dbo].[tblPrivateCustomers] where privateCustomersNo = @privetCustomersNo

	insert into [dbo].[tblDeleteItems] (itemInfo, deleteDate, restored) values (@result, GETDATE(), 0)
	delete from [dbo].[tblPrivateCustomers] where privateCustomersNo = @privetCustomersNo
go

create procedure [tblPrivateCustomers.UPDATE]
	@privateCustomersNo int,
	@name nvarchar(50),
	@surname nvarchar(50),
	@phoneNo nvarchar(25),
	@altPoneNo nvarchar(25),
	@homeAddress nvarchar(100),
	@postNo int,
	@email nvarchar(50),
	@active bit
as
	Update [dbo].[tblPrivateCustomers] Set
	name = @name,
	surname = @surname,
	phoneNo = @phoneNo,
	altPhoneNo = @altPoneNo,
	homeAddress = @homeAddress,
	postNo = @postNo,
	email = @email,
	active = @active
	where privateCustomersNo = @privateCustomersNo
go


--tblPrivetOrders
create procedure [tblPrivetOrders.ADD]
    @createdDate smalldatetime,
    @taskDate smalldatetime,
    @descriptionTask nvarchar(200),
    @dateSendBill smalldatetime,
    @daysToPaid int,
    @hoursUse decimal(3,1),
    @paidHour decimal(3,1),
    @createBy int,
    @paidToACC int,
    @customers int,
    @paid bit
as
	Insert into [dbo].[tblPrivetOrders](
	createdDate,
	taskDate,
	descriptionTask,
	dateSendBill,
	daysToPaid,
	hoursUse,
	paidHour,
	createBy,
	paidToACC,
	customers,
	paid
	) values (
	@createdDate,
	@taskDate,
	@descriptionTask,
	@dateSendBill,
	@daysToPaid,
	@hoursUse,
	@paidHour,
	@createBy,
	@paidToACC,
	@customers,
	@paid
	)
go

create procedure [tblPrivetOrders.FIND]
	@invoiceNo int
as
	Select
	invoiceNo,
	createdDate,
	taskDate,
	descriptionTask,
	dateSendBill,
	daysToPaid,
	hoursUse,
	paidHour,
	createBy,
	paidToACC,
	customers
	from [dbo].[tblPrivetOrders] where invoiceNo = @invoiceNo
go

create procedure [tblPrivetOrders.GET]
as
	Select 
	invoiceNo,
	taskDate,
	descriptionTask,
	dateSendBill,
	daysToPaid,
	hoursUse,
	paidHour,
	createBy,
	paidToACC,
	customers,
	paid
	from [dbo].[tblPrivetOrders]
go

create procedure [tblPrivetOrders.DELETE] 
	@invoiceNo int
as
	declare @result nvarchar(MAX)

	select @result = coalesce('[tblPrivetOrders] { invoiceNo = ' + CAST(invoiceNo as nvarchar(10)) + ', taskDate = ' + CAST(taskDate as nvarchar(20)) + 
	', descriptionTask = ' + descriptionTask + ', daysToPaid = ' + CAST(daysToPaid as nvarchar(10)) + ', hoursUse = ' + CAST(hoursUse as nvarchar(3)) + 
	', paidHour = ' + CAST(paidHour as nvarchar(3)) + ', createBy = ' + CAST(createBy as nvarchar(10)) + ', paidToACC = ' + CAST(paidToACC as nvarchar(10)) + 
	', customers = ' + CAST(customers as nvarchar(10)) + ', paid = ' + CAST(paid as nvarchar(1)) + ' }','')
	from [dbo].[tblPrivetOrders] where invoiceNo = @invoiceNo

	insert into [dbo].[tblDeleteItems](itemInfo, deleteDate, restored) values (@result, GETDATE(), 0)
	delete from [dbo].[tblPrivetOrders] where invoiceNo = @invoiceNo
go

create procedure [tblPrivetOrders.UPDATE]
	@invoiceNo int,
    @createdDate smalldatetime,
    @taskDate smalldatetime,
    @descriptionTask nvarchar(200),
    @dateSendBill smalldatetime,
    @daysToPaid int,
    @hoursUse decimal(3,1),
    @paidHour decimal(3,1),
    @createBy int,
    @paidToACC int,
    @customers int,
    @paid bit
as
	Update [dbo].[tblPrivetOrders] Set
	createdDate = @createdDate,
	taskDate = @taskDate,
	descriptionTask = @descriptionTask,
	dateSendBill = @dateSendBill,
	daysToPaid = @daysToPaid,
	hoursUse =  @hoursUse,
	paidHour = @paidHour,
	createBy = @createBy,
	paidToACC = @paidToACC,
	customers = @customers,
	paid = @paid
	where invoiceNo = @invoiceNo
go


--tblWorkers
create procedure [tblWorkers.GET]
	@workNo int
as
	Select
	workNo,
	name,
	surname,
	workerStatus,
	phoneNo,
	altPhoneNo,
	homeAddress,
	postNo,
	email,
	active
	from [dbo].[tblWorkers] where workNo = @workNo
go


create procedure [tblWorkers.ADD]
    @name nvarchar(50),
    @surname nvarchar(50),
    @workerStatus int,
    @phoneNo nvarchar(25),
    @altPhoneNo nvarchar(25),
    @homeAddress nvarchar(100),
    @postNo int,
    @email nvarchar(50),
    @active bit
as
	Insert into [dbo].[tblWorkers](
	name,
	surname,
	workerStatus,
	phoneNo,
	altPhoneNo,
	homeAddress,
	postNo,
	email,
	active
	) values (
	@name,
	@surname,
	@workerStatus,
	@postNo,
	@altPhoneNo,
	@homeAddress,
	@postNo,
	@email,
	@active
	)
go

create procedure [tblWorkers.FIND]
	@workNo int
as
	Select
	workNo,
	name,
	surname,
	workerStatus,
	phoneNo,
	altPhoneNo,
	homeAddress,
	postNo,
	email,
	active
	from [dbo].[tblWorkers] where workNo = @workNo
	
go

create procedure [tblWorkers.DELETE]
	@workNo int
as
	declare @result nvarchar(Max)

	select @result = coalesce('[tblWorkers] { workNo = ' + workNo + ', name = ' + name + ', surname = ' + surname + 
		', workerStatus = ' + CAST(workerStatus as nvarchar(10)) + ', phoneNo = ' + phoneNo + ' , altPhoneNo = ' + altPhoneNo + 
		', homeAddress = ' + homeAddress + ', postNo = ' + CAST(postNo as nvarchar(10)) + ', email = ' + email + 
		', active = ' + CAST(active as nvarchar(1)),'')
	from [dbo].[tblWorkers] where workNo = @workNo
	
	insert into [dbo].[tblDeleteItems](itemInfo, deleteDate, restored) values (@result, GETDATE(), 0)
	delete from [dbo].tblWorkers where workNo = @workNo 
go

create procedure [tblWorkers.UPDATE]
	@workNo int,
	@name nvarchar(50),
    @surname nvarchar(50),
    @workerStatus int,
    @phoneNo nvarchar(25),
    @altPhoneNo nvarchar(25),
    @homeAddress nvarchar(100),
    @postNo int,
    @email nvarchar(50),
    @active bit
as
	Update [dbo].[tblWorkers] Set
	name = @name,
	surname = @surname,
	workerStatus = @workerStatus,
	phoneNo = @phoneNo,
	altPhoneNo = @altPhoneNo,
	homeAddress = @homeAddress,
	postNo = @postNo,
	email = @email,
	active = @active
	where workNo = @workNo
go


--tblWorkerStatus
create procedure [tblWorkerStatus.ADD]
	@status nvarchar(30)
as
	Insert into [dbo].[tblWorkerStatus](
	status
	) values (
	@status
	)
go

create procedure [tblWorkerStatus.FIND]
	@statusNo int
as
	Select
	statusNo,
	status
	from [dbo].[tblWorkerStatus] where statusNo = @statusNo
go



create procedure [tblWorkerStatus.DELETE]
	@statusNo int
as
	declare @results varchar(Max)

	select @results = coalesce('[tblWorkerStatus] { statusNo = ' + CAST(statusNo as nvarchar(10)) + ', status = ' + status + ' }', '')
	from tblWorkerStatus
	where statusNo = @statusNo

	insert into tblDeleteItems(itemInfo, deleteDate, restored) values (@results, GetDate(), 0)

	delete from tblWorkerStatus where statusNo = @statusNo
go

create procedure [tblWorkerStatus.UPDATE]
	@statusNo int,
	@status nvarchar(30)
as
	Update [dbo].[tblWorkerStatus] Set
	status = @status
	where statusNo = @statusNo
go
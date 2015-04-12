
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/10/2015 08:47:18
-- Generated from EDMX file: C:\Users\diosc_000\Source\Repos\Pointersecurity\SecurityMonitor\Doormandondemand\doormanondemand.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Pointerdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ActiveManager_To_Manager]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActiveManager] DROP CONSTRAINT [FK_ActiveManager_To_Manager];
GO
IF OBJECT_ID(N'[dbo].[FK_ActiveManager_ToBuilding]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActiveManager] DROP CONSTRAINT [FK_ActiveManager_ToBuilding];
GO
IF OBJECT_ID(N'[dbo].[FK_Apartment_Buildings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apartment] DROP CONSTRAINT [FK_Apartment_Buildings];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Buildings_Clients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Buildings] DROP CONSTRAINT [FK_Buildings_Clients];
GO
IF OBJECT_ID(N'[dbo].[FK_Buildings_Manager]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Buildings] DROP CONSTRAINT [FK_Buildings_Manager];
GO
IF OBJECT_ID(N'[dbo].[FK_Buildings_PendingModules]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PendingModules] DROP CONSTRAINT [FK_Buildings_PendingModules];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingUSer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BuildingUser] DROP CONSTRAINT [FK_BuildingUSer];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingUserMapping_AspNetUsers1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BuildingUserMapping] DROP CONSTRAINT [FK_BuildingUserMapping_AspNetUsers1];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingUserMapping_Buildings1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BuildingUserMapping] DROP CONSTRAINT [FK_BuildingUserMapping_Buildings1];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_Manager_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Manager] DROP CONSTRAINT [FK_Manager_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Manager_Clients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Manager] DROP CONSTRAINT [FK_Manager_Clients];
GO
IF OBJECT_ID(N'[dbo].[FK_ManagerBuilding_Buildings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ManagerBuilding] DROP CONSTRAINT [FK_ManagerBuilding_Buildings];
GO
IF OBJECT_ID(N'[dbo].[FK_ManagerBuilding_Manager]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ManagerBuilding] DROP CONSTRAINT [FK_ManagerBuilding_Manager];
GO
IF OBJECT_ID(N'[dbo].[FK_Module_Buildings1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Module] DROP CONSTRAINT [FK_Module_Buildings1];
GO
IF OBJECT_ID(N'[dbo].[FK_Module_Listofmodule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PendingModules] DROP CONSTRAINT [FK_Module_Listofmodule];
GO
IF OBJECT_ID(N'[dbo].[FK_Package_PackageDeliveryStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Package] DROP CONSTRAINT [FK_Package_PackageDeliveryStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_Package_PackageType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Package] DROP CONSTRAINT [FK_Package_PackageType];
GO
IF OBJECT_ID(N'[dbo].[FK_Package_Shipment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Package] DROP CONSTRAINT [FK_Package_Shipment];
GO
IF OBJECT_ID(N'[dbo].[FK_Package_ShippingCarrier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Package] DROP CONSTRAINT [FK_Package_ShippingCarrier];
GO
IF OBJECT_ID(N'[dbo].[FK_Package_ShippingService]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Package] DROP CONSTRAINT [FK_Package_ShippingService];
GO
IF OBJECT_ID(N'[dbo].[FK_PendingModuleRequests_Buildings1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Module] DROP CONSTRAINT [FK_PendingModuleRequests_Buildings1];
GO
IF OBJECT_ID(N'[dbo].[FK_PendingModuleRequests1_Buildings1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Module] DROP CONSTRAINT [FK_PendingModuleRequests1_Buildings1];
GO
IF OBJECT_ID(N'[dbo].[FK_PermissionMapRole_ASPNETRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermissionMapRole] DROP CONSTRAINT [FK_PermissionMapRole_ASPNETRole];
GO
IF OBJECT_ID(N'[dbo].[FK_PermissionMapRole_ASPNETUSERS]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermissionMapRole] DROP CONSTRAINT [FK_PermissionMapRole_ASPNETUSERS];
GO
IF OBJECT_ID(N'[dbo].[FK_Request_Tenant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requests] DROP CONSTRAINT [FK_Request_Tenant];
GO
IF OBJECT_ID(N'[dbo].[FK_Role_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Role] DROP CONSTRAINT [FK_Role_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_Role_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Role] DROP CONSTRAINT [FK_Role_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Shipment_Apartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Shipment] DROP CONSTRAINT [FK_Shipment_Apartment];
GO
IF OBJECT_ID(N'[dbo].[FK_Shipment_AspNETUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Shipment] DROP CONSTRAINT [FK_Shipment_AspNETUser];
GO
IF OBJECT_ID(N'[dbo].[FK_Shipment_Buildings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Shipment] DROP CONSTRAINT [FK_Shipment_Buildings];
GO
IF OBJECT_ID(N'[dbo].[FK_Tenant_Apartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tenant] DROP CONSTRAINT [FK_Tenant_Apartment];
GO
IF OBJECT_ID(N'[dbo].[FK_Tenant_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tenant] DROP CONSTRAINT [FK_Tenant_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_UserActivityLog_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserActivityLog] DROP CONSTRAINT [FK_UserActivityLog_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_UserActivityLog_Buildings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserActivityLog] DROP CONSTRAINT [FK_UserActivityLog_Buildings];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ActiveManager]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActiveManager];
GO
IF OBJECT_ID(N'[dbo].[Apartment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apartment];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Buildings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Buildings];
GO
IF OBJECT_ID(N'[dbo].[BuildingUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BuildingUser];
GO
IF OBJECT_ID(N'[dbo].[BuildingUserMapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BuildingUserMapping];
GO
IF OBJECT_ID(N'[dbo].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[GanttLinkId]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GanttLinkId];
GO
IF OBJECT_ID(N'[dbo].[GanttTask]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GanttTask];
GO
IF OBJECT_ID(N'[dbo].[ListOfModule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ListOfModule];
GO
IF OBJECT_ID(N'[dbo].[Manager]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Manager];
GO
IF OBJECT_ID(N'[dbo].[ManagerBuilding]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ManagerBuilding];
GO
IF OBJECT_ID(N'[dbo].[MasterProfileFields]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MasterProfileFields];
GO
IF OBJECT_ID(N'[dbo].[Module]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Module];
GO
IF OBJECT_ID(N'[dbo].[Package]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Package];
GO
IF OBJECT_ID(N'[dbo].[PackageDeliveryStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PackageDeliveryStatus];
GO
IF OBJECT_ID(N'[dbo].[PackageType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PackageType];
GO
IF OBJECT_ID(N'[dbo].[PendingModules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PendingModules];
GO
IF OBJECT_ID(N'[dbo].[PermissionMapRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PermissionMapRole];
GO
IF OBJECT_ID(N'[dbo].[ReqType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReqType];
GO
IF OBJECT_ID(N'[dbo].[Requests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Requests];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[Shipment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Shipment];
GO
IF OBJECT_ID(N'[dbo].[ShippingCarrier]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShippingCarrier];
GO
IF OBJECT_ID(N'[dbo].[ShippingService]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShippingService];
GO
IF OBJECT_ID(N'[dbo].[SignalRMessageTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SignalRMessageTable];
GO
IF OBJECT_ID(N'[dbo].[States]', 'U') IS NOT NULL
    DROP TABLE [dbo].[States];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Tenant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tenant];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserActivityLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserActivityLog];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ActiveManager'
CREATE TABLE [dbo].[ActiveManager] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ManagerID] nvarchar(128)  NOT NULL,
    [BuildingID] int  NOT NULL
);
GO

-- Creating table 'Apartment'
CREATE TABLE [dbo].[Apartment] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ApartmentNumber] nvarchar(max)  NOT NULL,
    [FloorNumber] nvarchar(50)  NOT NULL,
    [BuildingID] int  NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Buildings'
CREATE TABLE [dbo].[Buildings] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BuildingName] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [State] nvarchar(2)  NOT NULL,
    [Zipcode] nvarchar(5)  NOT NULL,
    [NumberOfApartment] int  NULL,
    [ClientID] int  NULL,
    [BuildingPhone] nvarchar(10)  NULL,
    [Manager] nvarchar(max)  NULL,
    [ManagersID] nvarchar(128)  NULL
);
GO

-- Creating table 'BuildingUser'
CREATE TABLE [dbo].[BuildingUser] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BuildingID] int  NOT NULL,
    [FirstName] nvarchar(225)  NOT NULL,
    [LastName] nvarchar(225)  NOT NULL,
    [Phone] nvarchar(10)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [UserID] nvarchar(128)  NULL
);
GO

-- Creating table 'BuildingUserMapping'
CREATE TABLE [dbo].[BuildingUserMapping] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] nvarchar(128)  NOT NULL,
    [BuildingID] int  NOT NULL
);
GO

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ClientName] nvarchar(max)  NOT NULL,
    [BuildingCount] int  NULL,
    [Address] nvarchar(max)  NULL,
    [Phone] nvarchar(10)  NULL,
    [Fax] nvarchar(10)  NULL,
    [Email] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [State] nvarchar(2)  NULL,
    [ZipCode] nvarchar(5)  NULL
);
GO

-- Creating table 'GanttLinkId'
CREATE TABLE [dbo].[GanttLinkId] (
    [GantLinkID] int IDENTITY(1,1) NOT NULL,
    [Type] varchar(1)  NOT NULL,
    [SourceTaskId] int  NOT NULL,
    [TargerTaskId] int  NOT NULL
);
GO

-- Creating table 'GanttTask'
CREATE TABLE [dbo].[GanttTask] (
    [GantTaskID] int IDENTITY(1,1) NOT NULL,
    [Text] varchar(255)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [Duration] int  NOT NULL,
    [Progress] decimal(18,0)  NOT NULL,
    [SortOrder] int  NOT NULL,
    [Type] varchar(max)  NOT NULL,
    [ParentID] int  NULL
);
GO

-- Creating table 'ListOfModule'
CREATE TABLE [dbo].[ListOfModule] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ModuleName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Manager'
CREATE TABLE [dbo].[Manager] (
    [ID] nvarchar(128)  NOT NULL,
    [FirstName] nvarchar(100)  NOT NULL,
    [LastName] nvarchar(100)  NOT NULL,
    [Phone] nvarchar(10)  NOT NULL,
    [ClientID] int  NULL
);
GO

-- Creating table 'ManagerBuilding'
CREATE TABLE [dbo].[ManagerBuilding] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BuildingID] int  NOT NULL,
    [UserID] nvarchar(128)  NOT NULL,
    [ManagerID] nvarchar(128)  NULL
);
GO

-- Creating table 'MasterProfileFields'
CREATE TABLE [dbo].[MasterProfileFields] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Label] nvarchar(max)  NOT NULL,
    [Controller] nvarchar(2)  NOT NULL
);
GO

-- Creating table 'Module'
CREATE TABLE [dbo].[Module] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BuildingID] int  NOT NULL,
    [ServiceName] nvarchar(max)  NOT NULL,
    [ListOfModuleID] int  NOT NULL
);
GO

-- Creating table 'Package'
CREATE TABLE [dbo].[Package] (
    [TrackingNumber] nvarchar(225)  NOT NULL,
    [ShippingServiceID] int  NOT NULL,
    [ShippingCarrierID] int  NOT NULL,
    [PakageTypeID] int  NOT NULL,
    [ArrivalTime] datetime  NOT NULL,
    [DeliverTime] datetime  NULL,
    [ShipmentID] nvarchar(128)  NOT NULL,
    [PackageDeliveryStatusID] int  NOT NULL,
    [Note] nvarchar(max)  NULL
);
GO

-- Creating table 'PackageDeliveryStatus'
CREATE TABLE [dbo].[PackageDeliveryStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'PackageType'
CREATE TABLE [dbo].[PackageType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PackageType1] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'PendingModules'
CREATE TABLE [dbo].[PendingModules] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BuildingID] int  NOT NULL,
    [ServiceName] nvarchar(max)  NOT NULL,
    [ListOfModuleID] int  NOT NULL
);
GO

-- Creating table 'PermissionMapRole'
CREATE TABLE [dbo].[PermissionMapRole] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserID] nvarchar(128)  NOT NULL,
    [RoleID] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'ReqType'
CREATE TABLE [dbo].[ReqType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ReqType1] varchar(max)  NOT NULL
);
GO

-- Creating table 'Requests'
CREATE TABLE [dbo].[Requests] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RequestType] nvarchar(150)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL,
    [PIN] nvarchar(4)  NULL,
    [TenantID] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Role'
CREATE TABLE [dbo].[Role] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] nvarchar(128)  NOT NULL,
    [RoleID] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Shipment'
CREATE TABLE [dbo].[Shipment] (
    [ID] nvarchar(128)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(10)  NOT NULL,
    [isNewUser] bit  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [State] nvarchar(2)  NOT NULL,
    [Zipcode] nvarchar(5)  NOT NULL,
    [ApartmentNumber] nvarchar(50)  NOT NULL,
    [Created] datetime  NOT NULL,
    [BuildingID] int  NOT NULL,
    [aptID] int  NOT NULL,
    [BuildingUserID] nvarchar(128)  NOT NULL,
    [TenantID] nvarchar(128)  NOT NULL,
    [Notified] bit  NOT NULL
);
GO

-- Creating table 'ShippingCarrier'
CREATE TABLE [dbo].[ShippingCarrier] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Services] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ShippingService'
CREATE TABLE [dbo].[ShippingService] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Service] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'SignalRMessageTable'
CREATE TABLE [dbo].[SignalRMessageTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [message] nvarchar(max)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [State] nvarchar(2)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Tenant'
CREATE TABLE [dbo].[Tenant] (
    [ID] nvarchar(128)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(10)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Created] datetime  NOT NULL,
    [isTemPWord] nvarchar(max)  NULL,
    [aptID] int  NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] varchar(max)  NOT NULL,
    [Created] datetime  NOT NULL,
    [lastActivity] datetime  NOT NULL,
    [isTempPword] nvarchar(5)  NULL
);
GO

-- Creating table 'UserActivityLog'
CREATE TABLE [dbo].[UserActivityLog] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] nvarchar(128)  NULL,
    [BuildingID] int  NULL,
    [FunctionPerformed] nvarchar(max)  NOT NULL,
    [DateOfEvent] datetime  NOT NULL,
    [Message] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ActiveManager'
ALTER TABLE [dbo].[ActiveManager]
ADD CONSTRAINT [PK_ActiveManager]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'Apartment'
ALTER TABLE [dbo].[Apartment]
ADD CONSTRAINT [PK_Apartment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'Buildings'
ALTER TABLE [dbo].[Buildings]
ADD CONSTRAINT [PK_Buildings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'BuildingUser'
ALTER TABLE [dbo].[BuildingUser]
ADD CONSTRAINT [PK_BuildingUser]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'BuildingUserMapping'
ALTER TABLE [dbo].[BuildingUserMapping]
ADD CONSTRAINT [PK_BuildingUserMapping]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [ID] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [GantLinkID] in table 'GanttLinkId'
ALTER TABLE [dbo].[GanttLinkId]
ADD CONSTRAINT [PK_GanttLinkId]
    PRIMARY KEY CLUSTERED ([GantLinkID] ASC);
GO

-- Creating primary key on [GantTaskID] in table 'GanttTask'
ALTER TABLE [dbo].[GanttTask]
ADD CONSTRAINT [PK_GanttTask]
    PRIMARY KEY CLUSTERED ([GantTaskID] ASC);
GO

-- Creating primary key on [ID] in table 'ListOfModule'
ALTER TABLE [dbo].[ListOfModule]
ADD CONSTRAINT [PK_ListOfModule]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Manager'
ALTER TABLE [dbo].[Manager]
ADD CONSTRAINT [PK_Manager]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ManagerBuilding'
ALTER TABLE [dbo].[ManagerBuilding]
ADD CONSTRAINT [PK_ManagerBuilding]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MasterProfileFields'
ALTER TABLE [dbo].[MasterProfileFields]
ADD CONSTRAINT [PK_MasterProfileFields]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Module'
ALTER TABLE [dbo].[Module]
ADD CONSTRAINT [PK_Module]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [TrackingNumber] in table 'Package'
ALTER TABLE [dbo].[Package]
ADD CONSTRAINT [PK_Package]
    PRIMARY KEY CLUSTERED ([TrackingNumber] ASC);
GO

-- Creating primary key on [ID] in table 'PackageDeliveryStatus'
ALTER TABLE [dbo].[PackageDeliveryStatus]
ADD CONSTRAINT [PK_PackageDeliveryStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PackageType'
ALTER TABLE [dbo].[PackageType]
ADD CONSTRAINT [PK_PackageType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PendingModules'
ALTER TABLE [dbo].[PendingModules]
ADD CONSTRAINT [PK_PendingModules]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'PermissionMapRole'
ALTER TABLE [dbo].[PermissionMapRole]
ADD CONSTRAINT [PK_PermissionMapRole]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'ReqType'
ALTER TABLE [dbo].[ReqType]
ADD CONSTRAINT [PK_ReqType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [PK_Requests]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Role'
ALTER TABLE [dbo].[Role]
ADD CONSTRAINT [PK_Role]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Shipment'
ALTER TABLE [dbo].[Shipment]
ADD CONSTRAINT [PK_Shipment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ShippingCarrier'
ALTER TABLE [dbo].[ShippingCarrier]
ADD CONSTRAINT [PK_ShippingCarrier]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ShippingService'
ALTER TABLE [dbo].[ShippingService]
ADD CONSTRAINT [PK_ShippingService]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SignalRMessageTable'
ALTER TABLE [dbo].[SignalRMessageTable]
ADD CONSTRAINT [PK_SignalRMessageTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID] in table 'Tenant'
ALTER TABLE [dbo].[Tenant]
ADD CONSTRAINT [PK_Tenant]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserID] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [ID] in table 'UserActivityLog'
ALTER TABLE [dbo].[UserActivityLog]
ADD CONSTRAINT [PK_UserActivityLog]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ManagerID] in table 'ActiveManager'
ALTER TABLE [dbo].[ActiveManager]
ADD CONSTRAINT [FK_ActiveManager_To_Manager]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[Manager]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActiveManager_To_Manager'
CREATE INDEX [IX_FK_ActiveManager_To_Manager]
ON [dbo].[ActiveManager]
    ([ManagerID]);
GO

-- Creating foreign key on [BuildingID] in table 'ActiveManager'
ALTER TABLE [dbo].[ActiveManager]
ADD CONSTRAINT [FK_ActiveManager_ToBuilding]
    FOREIGN KEY ([BuildingID])
    REFERENCES [dbo].[Buildings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActiveManager_ToBuilding'
CREATE INDEX [IX_FK_ActiveManager_ToBuilding]
ON [dbo].[ActiveManager]
    ([BuildingID]);
GO

-- Creating foreign key on [BuildingID] in table 'Apartment'
ALTER TABLE [dbo].[Apartment]
ADD CONSTRAINT [FK_Apartment_Buildings]
    FOREIGN KEY ([BuildingID])
    REFERENCES [dbo].[Buildings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Apartment_Buildings'
CREATE INDEX [IX_FK_Apartment_Buildings]
ON [dbo].[Apartment]
    ([BuildingID]);
GO

-- Creating foreign key on [aptID] in table 'Shipment'
ALTER TABLE [dbo].[Shipment]
ADD CONSTRAINT [FK_Shipment_Apartment]
    FOREIGN KEY ([aptID])
    REFERENCES [dbo].[Apartment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Shipment_Apartment'
CREATE INDEX [IX_FK_Shipment_Apartment]
ON [dbo].[Shipment]
    ([aptID]);
GO

-- Creating foreign key on [aptID] in table 'Tenant'
ALTER TABLE [dbo].[Tenant]
ADD CONSTRAINT [FK_Tenant_Apartment]
    FOREIGN KEY ([aptID])
    REFERENCES [dbo].[Apartment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Tenant_Apartment'
CREATE INDEX [IX_FK_Tenant_Apartment]
ON [dbo].[Tenant]
    ([aptID]);
GO

-- Creating foreign key on [RoleID] in table 'PermissionMapRole'
ALTER TABLE [dbo].[PermissionMapRole]
ADD CONSTRAINT [FK_PermissionMapRole_ASPNETRole]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermissionMapRole_ASPNETRole'
CREATE INDEX [IX_FK_PermissionMapRole_ASPNETRole]
ON [dbo].[PermissionMapRole]
    ([RoleID]);
GO

-- Creating foreign key on [RoleID] in table 'Role'
ALTER TABLE [dbo].[Role]
ADD CONSTRAINT [FK_Role_AspNetRoles]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Role_AspNetRoles'
CREATE INDEX [IX_FK_Role_AspNetRoles]
ON [dbo].[Role]
    ([RoleID]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [UserID] in table 'BuildingUserMapping'
ALTER TABLE [dbo].[BuildingUserMapping]
ADD CONSTRAINT [FK_BuildingUserMapping_AspNetUsers1]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingUserMapping_AspNetUsers1'
CREATE INDEX [IX_FK_BuildingUserMapping_AspNetUsers1]
ON [dbo].[BuildingUserMapping]
    ([UserID]);
GO

-- Creating foreign key on [ID] in table 'Manager'
ALTER TABLE [dbo].[Manager]
ADD CONSTRAINT [FK_Manager_AspNetUsers]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserID] in table 'PermissionMapRole'
ALTER TABLE [dbo].[PermissionMapRole]
ADD CONSTRAINT [FK_PermissionMapRole_ASPNETUSERS]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermissionMapRole_ASPNETUSERS'
CREATE INDEX [IX_FK_PermissionMapRole_ASPNETUSERS]
ON [dbo].[PermissionMapRole]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'Role'
ALTER TABLE [dbo].[Role]
ADD CONSTRAINT [FK_Role_AspNetUsers]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Role_AspNetUsers'
CREATE INDEX [IX_FK_Role_AspNetUsers]
ON [dbo].[Role]
    ([UserID]);
GO

-- Creating foreign key on [BuildingUserID] in table 'Shipment'
ALTER TABLE [dbo].[Shipment]
ADD CONSTRAINT [FK_Shipment_AspNETUser]
    FOREIGN KEY ([BuildingUserID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Shipment_AspNETUser'
CREATE INDEX [IX_FK_Shipment_AspNETUser]
ON [dbo].[Shipment]
    ([BuildingUserID]);
GO

-- Creating foreign key on [ID] in table 'Tenant'
ALTER TABLE [dbo].[Tenant]
ADD CONSTRAINT [FK_Tenant_AspNetUsers]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserID] in table 'UserActivityLog'
ALTER TABLE [dbo].[UserActivityLog]
ADD CONSTRAINT [FK_UserActivityLog_AspNetUsers]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserActivityLog_AspNetUsers'
CREATE INDEX [IX_FK_UserActivityLog_AspNetUsers]
ON [dbo].[UserActivityLog]
    ([UserID]);
GO

-- Creating foreign key on [ClientID] in table 'Buildings'
ALTER TABLE [dbo].[Buildings]
ADD CONSTRAINT [FK_Buildings_Clients]
    FOREIGN KEY ([ClientID])
    REFERENCES [dbo].[Clients]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Buildings_Clients'
CREATE INDEX [IX_FK_Buildings_Clients]
ON [dbo].[Buildings]
    ([ClientID]);
GO

-- Creating foreign key on [ManagersID] in table 'Buildings'
ALTER TABLE [dbo].[Buildings]
ADD CONSTRAINT [FK_Buildings_Manager]
    FOREIGN KEY ([ManagersID])
    REFERENCES [dbo].[Manager]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Buildings_Manager'
CREATE INDEX [IX_FK_Buildings_Manager]
ON [dbo].[Buildings]
    ([ManagersID]);
GO

-- Creating foreign key on [BuildingID] in table 'PendingModules'
ALTER TABLE [dbo].[PendingModules]
ADD CONSTRAINT [FK_Buildings_PendingModules]
    FOREIGN KEY ([BuildingID])
    REFERENCES [dbo].[Buildings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Buildings_PendingModules'
CREATE INDEX [IX_FK_Buildings_PendingModules]
ON [dbo].[PendingModules]
    ([BuildingID]);
GO

-- Creating foreign key on [BuildingID] in table 'BuildingUser'
ALTER TABLE [dbo].[BuildingUser]
ADD CONSTRAINT [FK_BuildingUSer]
    FOREIGN KEY ([BuildingID])
    REFERENCES [dbo].[Buildings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingUSer'
CREATE INDEX [IX_FK_BuildingUSer]
ON [dbo].[BuildingUser]
    ([BuildingID]);
GO

-- Creating foreign key on [BuildingID] in table 'BuildingUserMapping'
ALTER TABLE [dbo].[BuildingUserMapping]
ADD CONSTRAINT [FK_BuildingUserMapping_Buildings1]
    FOREIGN KEY ([BuildingID])
    REFERENCES [dbo].[Buildings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingUserMapping_Buildings1'
CREATE INDEX [IX_FK_BuildingUserMapping_Buildings1]
ON [dbo].[BuildingUserMapping]
    ([BuildingID]);
GO

-- Creating foreign key on [BuildingID] in table 'ManagerBuilding'
ALTER TABLE [dbo].[ManagerBuilding]
ADD CONSTRAINT [FK_ManagerBuilding_Buildings]
    FOREIGN KEY ([BuildingID])
    REFERENCES [dbo].[Buildings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerBuilding_Buildings'
CREATE INDEX [IX_FK_ManagerBuilding_Buildings]
ON [dbo].[ManagerBuilding]
    ([BuildingID]);
GO

-- Creating foreign key on [BuildingID] in table 'Shipment'
ALTER TABLE [dbo].[Shipment]
ADD CONSTRAINT [FK_Shipment_Buildings]
    FOREIGN KEY ([BuildingID])
    REFERENCES [dbo].[Buildings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Shipment_Buildings'
CREATE INDEX [IX_FK_Shipment_Buildings]
ON [dbo].[Shipment]
    ([BuildingID]);
GO

-- Creating foreign key on [BuildingID] in table 'UserActivityLog'
ALTER TABLE [dbo].[UserActivityLog]
ADD CONSTRAINT [FK_UserActivityLog_Buildings]
    FOREIGN KEY ([BuildingID])
    REFERENCES [dbo].[Buildings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserActivityLog_Buildings'
CREATE INDEX [IX_FK_UserActivityLog_Buildings]
ON [dbo].[UserActivityLog]
    ([BuildingID]);
GO

-- Creating foreign key on [ClientID] in table 'Manager'
ALTER TABLE [dbo].[Manager]
ADD CONSTRAINT [FK_Manager_Clients]
    FOREIGN KEY ([ClientID])
    REFERENCES [dbo].[Clients]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Manager_Clients'
CREATE INDEX [IX_FK_Manager_Clients]
ON [dbo].[Manager]
    ([ClientID]);
GO

-- Creating foreign key on [ListOfModuleID] in table 'Module'
ALTER TABLE [dbo].[Module]
ADD CONSTRAINT [FK_Module_Buildings1]
    FOREIGN KEY ([ListOfModuleID])
    REFERENCES [dbo].[ListOfModule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Module_Buildings1'
CREATE INDEX [IX_FK_Module_Buildings1]
ON [dbo].[Module]
    ([ListOfModuleID]);
GO

-- Creating foreign key on [ListOfModuleID] in table 'PendingModules'
ALTER TABLE [dbo].[PendingModules]
ADD CONSTRAINT [FK_Module_Listofmodule]
    FOREIGN KEY ([ListOfModuleID])
    REFERENCES [dbo].[ListOfModule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Module_Listofmodule'
CREATE INDEX [IX_FK_Module_Listofmodule]
ON [dbo].[PendingModules]
    ([ListOfModuleID]);
GO

-- Creating foreign key on [ListOfModuleID] in table 'Module'
ALTER TABLE [dbo].[Module]
ADD CONSTRAINT [FK_PendingModuleRequests_Buildings1]
    FOREIGN KEY ([ListOfModuleID])
    REFERENCES [dbo].[ListOfModule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PendingModuleRequests_Buildings1'
CREATE INDEX [IX_FK_PendingModuleRequests_Buildings1]
ON [dbo].[Module]
    ([ListOfModuleID]);
GO

-- Creating foreign key on [ListOfModuleID] in table 'Module'
ALTER TABLE [dbo].[Module]
ADD CONSTRAINT [FK_PendingModuleRequests1_Buildings1]
    FOREIGN KEY ([ListOfModuleID])
    REFERENCES [dbo].[ListOfModule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PendingModuleRequests1_Buildings1'
CREATE INDEX [IX_FK_PendingModuleRequests1_Buildings1]
ON [dbo].[Module]
    ([ListOfModuleID]);
GO

-- Creating foreign key on [ManagerID] in table 'ManagerBuilding'
ALTER TABLE [dbo].[ManagerBuilding]
ADD CONSTRAINT [FK_ManagerBuilding_Manager]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[Manager]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerBuilding_Manager'
CREATE INDEX [IX_FK_ManagerBuilding_Manager]
ON [dbo].[ManagerBuilding]
    ([ManagerID]);
GO

-- Creating foreign key on [PackageDeliveryStatusID] in table 'Package'
ALTER TABLE [dbo].[Package]
ADD CONSTRAINT [FK_Package_PackageDeliveryStatus]
    FOREIGN KEY ([PackageDeliveryStatusID])
    REFERENCES [dbo].[PackageDeliveryStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Package_PackageDeliveryStatus'
CREATE INDEX [IX_FK_Package_PackageDeliveryStatus]
ON [dbo].[Package]
    ([PackageDeliveryStatusID]);
GO

-- Creating foreign key on [PakageTypeID] in table 'Package'
ALTER TABLE [dbo].[Package]
ADD CONSTRAINT [FK_Package_PackageType]
    FOREIGN KEY ([PakageTypeID])
    REFERENCES [dbo].[PackageType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Package_PackageType'
CREATE INDEX [IX_FK_Package_PackageType]
ON [dbo].[Package]
    ([PakageTypeID]);
GO

-- Creating foreign key on [ShipmentID] in table 'Package'
ALTER TABLE [dbo].[Package]
ADD CONSTRAINT [FK_Package_Shipment]
    FOREIGN KEY ([ShipmentID])
    REFERENCES [dbo].[Shipment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Package_Shipment'
CREATE INDEX [IX_FK_Package_Shipment]
ON [dbo].[Package]
    ([ShipmentID]);
GO

-- Creating foreign key on [ShippingCarrierID] in table 'Package'
ALTER TABLE [dbo].[Package]
ADD CONSTRAINT [FK_Package_ShippingCarrier]
    FOREIGN KEY ([ShippingCarrierID])
    REFERENCES [dbo].[ShippingCarrier]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Package_ShippingCarrier'
CREATE INDEX [IX_FK_Package_ShippingCarrier]
ON [dbo].[Package]
    ([ShippingCarrierID]);
GO

-- Creating foreign key on [ShippingServiceID] in table 'Package'
ALTER TABLE [dbo].[Package]
ADD CONSTRAINT [FK_Package_ShippingService]
    FOREIGN KEY ([ShippingServiceID])
    REFERENCES [dbo].[ShippingService]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Package_ShippingService'
CREATE INDEX [IX_FK_Package_ShippingService]
ON [dbo].[Package]
    ([ShippingServiceID]);
GO

-- Creating foreign key on [TenantID] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK_Request_Tenant]
    FOREIGN KEY ([TenantID])
    REFERENCES [dbo].[Tenant]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Request_Tenant'
CREATE INDEX [IX_FK_Request_Tenant]
ON [dbo].[Requests]
    ([TenantID]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
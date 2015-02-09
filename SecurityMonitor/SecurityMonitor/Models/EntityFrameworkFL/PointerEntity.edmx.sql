
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/09/2015 14:06:12
-- Generated from EDMX file: C:\Users\dioscar\Source\Repos\Pointersecurity\SecurityMonitor\SecurityMonitor\Models\EntityFrameworkFL\PointerEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Pointersecurity];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__2D27B809]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Membership] DROP CONSTRAINT [FK__aspnet_Me__Appli__2D27B809];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Me__UserI__2E1BDC42]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Membership] DROP CONSTRAINT [FK__aspnet_Me__UserI__2E1BDC42];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pa__Appli__66603565]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Paths] DROP CONSTRAINT [FK__aspnet_Pa__Appli__66603565];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__6E01572D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers] DROP CONSTRAINT [FK__aspnet_Pe__PathI__6E01572D];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__73BA3083]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] DROP CONSTRAINT [FK__aspnet_Pe__PathI__73BA3083];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pe__UserI__74AE54BC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] DROP CONSTRAINT [FK__aspnet_Pe__UserI__74AE54BC];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Pr__UserI__440B1D61]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Profile] DROP CONSTRAINT [FK__aspnet_Pr__UserI__440B1D61];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__4F7CD00D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Roles] DROP CONSTRAINT [FK__aspnet_Ro__Appli__4F7CD00D];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Us__Appli__1920BF5C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_Users] DROP CONSTRAINT [FK__aspnet_Us__Appli__1920BF5C];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__5629CD9C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_UsersInRoles] DROP CONSTRAINT [FK__aspnet_Us__RoleI__5629CD9C];
GO
IF OBJECT_ID(N'[dbo].[FK__aspnet_Us__UserI__5535A963]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[aspnet_UsersInRoles] DROP CONSTRAINT [FK__aspnet_Us__UserI__5535A963];
GO
IF OBJECT_ID(N'[dbo].[FK_Apartment_Buildings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apartment] DROP CONSTRAINT [FK_Apartment_Buildings];
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
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
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
IF OBJECT_ID(N'[dbo].[FK_PendingModuleRequests_Buildings1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Module] DROP CONSTRAINT [FK_PendingModuleRequests_Buildings1];
GO
IF OBJECT_ID(N'[dbo].[FK_PendingModuleRequests1_Buildings1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Module] DROP CONSTRAINT [FK_PendingModuleRequests1_Buildings1];
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
IF OBJECT_ID(N'[dbo].[FK_Tenant_Apartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tenant] DROP CONSTRAINT [FK_Tenant_Apartment];
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

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[Apartment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apartment];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Applications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Applications];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Membership]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Membership];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Paths]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Paths];
GO
IF OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_PersonalizationAllUsers];
GO
IF OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_PersonalizationPerUser];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Profile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Profile];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Roles];
GO
IF OBJECT_ID(N'[dbo].[aspnet_SchemaVersions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_SchemaVersions];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Users];
GO
IF OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_UsersInRoles];
GO
IF OBJECT_ID(N'[dbo].[aspnet_WebEvent_Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_WebEvent_Events];
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
IF OBJECT_ID(N'[dbo].[PendingModules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PendingModules];
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

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
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

-- Creating table 'aspnet_Applications'
CREATE TABLE [dbo].[aspnet_Applications] (
    [ApplicationName] nvarchar(256)  NOT NULL,
    [LoweredApplicationName] nvarchar(256)  NOT NULL,
    [ApplicationId] uniqueidentifier  NOT NULL,
    [Description] nvarchar(256)  NULL
);
GO

-- Creating table 'aspnet_Membership'
CREATE TABLE [dbo].[aspnet_Membership] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [Password] nvarchar(128)  NOT NULL,
    [PasswordFormat] int  NOT NULL,
    [PasswordSalt] nvarchar(128)  NOT NULL,
    [MobilePIN] nvarchar(16)  NULL,
    [Email] nvarchar(256)  NULL,
    [LoweredEmail] nvarchar(256)  NULL,
    [PasswordQuestion] nvarchar(256)  NULL,
    [PasswordAnswer] nvarchar(128)  NULL,
    [IsApproved] bit  NOT NULL,
    [IsLockedOut] bit  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [LastLoginDate] datetime  NOT NULL,
    [LastPasswordChangedDate] datetime  NOT NULL,
    [LastLockoutDate] datetime  NOT NULL,
    [FailedPasswordAttemptCount] int  NOT NULL,
    [FailedPasswordAttemptWindowStart] datetime  NOT NULL,
    [FailedPasswordAnswerAttemptCount] int  NOT NULL,
    [FailedPasswordAnswerAttemptWindowStart] datetime  NOT NULL,
    [Comment] nvarchar(max)  NULL
);
GO

-- Creating table 'aspnet_Paths'
CREATE TABLE [dbo].[aspnet_Paths] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [PathId] uniqueidentifier  NOT NULL,
    [Path] nvarchar(256)  NOT NULL,
    [LoweredPath] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'aspnet_PersonalizationAllUsers'
CREATE TABLE [dbo].[aspnet_PersonalizationAllUsers] (
    [PathId] uniqueidentifier  NOT NULL,
    [PageSettings] varbinary(max)  NOT NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_PersonalizationPerUser'
CREATE TABLE [dbo].[aspnet_PersonalizationPerUser] (
    [Id] uniqueidentifier  NOT NULL,
    [PathId] uniqueidentifier  NULL,
    [UserId] uniqueidentifier  NULL,
    [PageSettings] varbinary(max)  NOT NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_Profile'
CREATE TABLE [dbo].[aspnet_Profile] (
    [UserId] uniqueidentifier  NOT NULL,
    [PropertyNames] nvarchar(max)  NOT NULL,
    [PropertyValuesString] nvarchar(max)  NOT NULL,
    [PropertyValuesBinary] varbinary(max)  NOT NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_Roles'
CREATE TABLE [dbo].[aspnet_Roles] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [RoleId] uniqueidentifier  NOT NULL,
    [RoleName] nvarchar(256)  NOT NULL,
    [LoweredRoleName] nvarchar(256)  NOT NULL,
    [Description] nvarchar(256)  NULL
);
GO

-- Creating table 'aspnet_SchemaVersions'
CREATE TABLE [dbo].[aspnet_SchemaVersions] (
    [Feature] nvarchar(128)  NOT NULL,
    [CompatibleSchemaVersion] nvarchar(128)  NOT NULL,
    [IsCurrentVersion] bit  NOT NULL
);
GO

-- Creating table 'aspnet_Users'
CREATE TABLE [dbo].[aspnet_Users] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [LoweredUserName] nvarchar(256)  NOT NULL,
    [MobileAlias] nvarchar(16)  NULL,
    [IsAnonymous] bit  NOT NULL,
    [LastActivityDate] datetime  NOT NULL
);
GO

-- Creating table 'aspnet_WebEvent_Events'
CREATE TABLE [dbo].[aspnet_WebEvent_Events] (
    [EventId] char(32)  NOT NULL,
    [EventTimeUtc] datetime  NOT NULL,
    [EventTime] datetime  NOT NULL,
    [EventType] nvarchar(256)  NOT NULL,
    [EventSequence] decimal(19,0)  NOT NULL,
    [EventOccurrence] decimal(19,0)  NOT NULL,
    [EventCode] int  NOT NULL,
    [EventDetailCode] int  NOT NULL,
    [Message] nvarchar(1024)  NULL,
    [ApplicationPath] nvarchar(256)  NULL,
    [ApplicationVirtualPath] nvarchar(256)  NULL,
    [MachineName] nvarchar(256)  NOT NULL,
    [RequestUrl] nvarchar(1024)  NULL,
    [ExceptionType] nvarchar(256)  NULL,
    [Details] nvarchar(max)  NULL
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

-- Creating table 'PendingModules'
CREATE TABLE [dbo].[PendingModules] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BuildingID] int  NOT NULL,
    [ServiceName] nvarchar(max)  NOT NULL,
    [ListOfModuleID] int  NOT NULL
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
    [TenantID] int  NULL
);
GO

-- Creating table 'Role'
CREATE TABLE [dbo].[Role] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] nvarchar(128)  NOT NULL,
    [RoleID] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [State] varchar(2)  NOT NULL
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
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(10)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NULL,
    [Created] datetime  NOT NULL,
    [isTemPWord] nvarchar(max)  NULL,
    [aptID] int  NULL,
    [LogintableID] nvarchar(128)  NULL
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

-- Creating table 'aspnet_UsersInRoles'
CREATE TABLE [dbo].[aspnet_UsersInRoles] (
    [aspnet_Roles_RoleId] uniqueidentifier  NOT NULL,
    [aspnet_Users_UserId] uniqueidentifier  NOT NULL
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

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [ID] in table 'Apartment'
ALTER TABLE [dbo].[Apartment]
ADD CONSTRAINT [PK_Apartment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ApplicationId] in table 'aspnet_Applications'
ALTER TABLE [dbo].[aspnet_Applications]
ADD CONSTRAINT [PK_aspnet_Applications]
    PRIMARY KEY CLUSTERED ([ApplicationId] ASC);
GO

-- Creating primary key on [UserId] in table 'aspnet_Membership'
ALTER TABLE [dbo].[aspnet_Membership]
ADD CONSTRAINT [PK_aspnet_Membership]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [PathId] in table 'aspnet_Paths'
ALTER TABLE [dbo].[aspnet_Paths]
ADD CONSTRAINT [PK_aspnet_Paths]
    PRIMARY KEY CLUSTERED ([PathId] ASC);
GO

-- Creating primary key on [PathId] in table 'aspnet_PersonalizationAllUsers'
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]
ADD CONSTRAINT [PK_aspnet_PersonalizationAllUsers]
    PRIMARY KEY CLUSTERED ([PathId] ASC);
GO

-- Creating primary key on [Id] in table 'aspnet_PersonalizationPerUser'
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]
ADD CONSTRAINT [PK_aspnet_PersonalizationPerUser]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'aspnet_Profile'
ALTER TABLE [dbo].[aspnet_Profile]
ADD CONSTRAINT [PK_aspnet_Profile]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [RoleId] in table 'aspnet_Roles'
ALTER TABLE [dbo].[aspnet_Roles]
ADD CONSTRAINT [PK_aspnet_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [Feature], [CompatibleSchemaVersion] in table 'aspnet_SchemaVersions'
ALTER TABLE [dbo].[aspnet_SchemaVersions]
ADD CONSTRAINT [PK_aspnet_SchemaVersions]
    PRIMARY KEY CLUSTERED ([Feature], [CompatibleSchemaVersion] ASC);
GO

-- Creating primary key on [UserId] in table 'aspnet_Users'
ALTER TABLE [dbo].[aspnet_Users]
ADD CONSTRAINT [PK_aspnet_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [EventId] in table 'aspnet_WebEvent_Events'
ALTER TABLE [dbo].[aspnet_WebEvent_Events]
ADD CONSTRAINT [PK_aspnet_WebEvent_Events]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
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

-- Creating primary key on [ID] in table 'PendingModules'
ALTER TABLE [dbo].[PendingModules]
ADD CONSTRAINT [PK_PendingModules]
    PRIMARY KEY CLUSTERED ([ID] ASC);
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

-- Creating primary key on [aspnet_Roles_RoleId], [aspnet_Users_UserId] in table 'aspnet_UsersInRoles'
ALTER TABLE [dbo].[aspnet_UsersInRoles]
ADD CONSTRAINT [PK_aspnet_UsersInRoles]
    PRIMARY KEY CLUSTERED ([aspnet_Roles_RoleId], [aspnet_Users_UserId] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on [ApplicationId] in table 'aspnet_Membership'
ALTER TABLE [dbo].[aspnet_Membership]
ADD CONSTRAINT [FK__aspnet_Me__Appli__2D27B809]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Me__Appli__2D27B809'
CREATE INDEX [IX_FK__aspnet_Me__Appli__2D27B809]
ON [dbo].[aspnet_Membership]
    ([ApplicationId]);
GO

-- Creating foreign key on [ApplicationId] in table 'aspnet_Paths'
ALTER TABLE [dbo].[aspnet_Paths]
ADD CONSTRAINT [FK__aspnet_Pa__Appli__66603565]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Pa__Appli__66603565'
CREATE INDEX [IX_FK__aspnet_Pa__Appli__66603565]
ON [dbo].[aspnet_Paths]
    ([ApplicationId]);
GO

-- Creating foreign key on [ApplicationId] in table 'aspnet_Roles'
ALTER TABLE [dbo].[aspnet_Roles]
ADD CONSTRAINT [FK__aspnet_Ro__Appli__4F7CD00D]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Ro__Appli__4F7CD00D'
CREATE INDEX [IX_FK__aspnet_Ro__Appli__4F7CD00D]
ON [dbo].[aspnet_Roles]
    ([ApplicationId]);
GO

-- Creating foreign key on [ApplicationId] in table 'aspnet_Users'
ALTER TABLE [dbo].[aspnet_Users]
ADD CONSTRAINT [FK__aspnet_Us__Appli__1920BF5C]
    FOREIGN KEY ([ApplicationId])
    REFERENCES [dbo].[aspnet_Applications]
        ([ApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Us__Appli__1920BF5C'
CREATE INDEX [IX_FK__aspnet_Us__Appli__1920BF5C]
ON [dbo].[aspnet_Users]
    ([ApplicationId]);
GO

-- Creating foreign key on [UserId] in table 'aspnet_Membership'
ALTER TABLE [dbo].[aspnet_Membership]
ADD CONSTRAINT [FK__aspnet_Me__UserI__2E1BDC42]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PathId] in table 'aspnet_PersonalizationAllUsers'
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]
ADD CONSTRAINT [FK__aspnet_Pe__PathI__6E01572D]
    FOREIGN KEY ([PathId])
    REFERENCES [dbo].[aspnet_Paths]
        ([PathId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PathId] in table 'aspnet_PersonalizationPerUser'
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]
ADD CONSTRAINT [FK__aspnet_Pe__PathI__73BA3083]
    FOREIGN KEY ([PathId])
    REFERENCES [dbo].[aspnet_Paths]
        ([PathId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Pe__PathI__73BA3083'
CREATE INDEX [IX_FK__aspnet_Pe__PathI__73BA3083]
ON [dbo].[aspnet_PersonalizationPerUser]
    ([PathId]);
GO

-- Creating foreign key on [UserId] in table 'aspnet_PersonalizationPerUser'
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]
ADD CONSTRAINT [FK__aspnet_Pe__UserI__74AE54BC]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__aspnet_Pe__UserI__74AE54BC'
CREATE INDEX [IX_FK__aspnet_Pe__UserI__74AE54BC]
ON [dbo].[aspnet_PersonalizationPerUser]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'aspnet_Profile'
ALTER TABLE [dbo].[aspnet_Profile]
ADD CONSTRAINT [FK__aspnet_Pr__UserI__440B1D61]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
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

-- Creating foreign key on [ID] in table 'Manager'
ALTER TABLE [dbo].[Manager]
ADD CONSTRAINT [FK_Manager_AspNetUsers]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
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

-- Creating foreign key on [aspnet_Roles_RoleId] in table 'aspnet_UsersInRoles'
ALTER TABLE [dbo].[aspnet_UsersInRoles]
ADD CONSTRAINT [FK_aspnet_UsersInRoles_aspnet_Roles]
    FOREIGN KEY ([aspnet_Roles_RoleId])
    REFERENCES [dbo].[aspnet_Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [aspnet_Users_UserId] in table 'aspnet_UsersInRoles'
ALTER TABLE [dbo].[aspnet_UsersInRoles]
ADD CONSTRAINT [FK_aspnet_UsersInRoles_aspnet_Users]
    FOREIGN KEY ([aspnet_Users_UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_aspnet_UsersInRoles_aspnet_Users'
CREATE INDEX [IX_FK_aspnet_UsersInRoles_aspnet_Users]
ON [dbo].[aspnet_UsersInRoles]
    ([aspnet_Users_UserId]);
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/06/2020 17:40:01
-- Generated from EDMX file: C:\Users\Marti\source\repos\AuctionInterface_Buyers\AuctionInterface_Sellers\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Auction_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AccountBid]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bids] DROP CONSTRAINT [FK_AccountBid];
GO
IF OBJECT_ID(N'[dbo].[FK_AuctionAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Auctions] DROP CONSTRAINT [FK_AuctionAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_AuctionAccount1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Auctions] DROP CONSTRAINT [FK_AuctionAccount1];
GO
IF OBJECT_ID(N'[dbo].[FK_BidAuction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bids] DROP CONSTRAINT [FK_BidAuction];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[Auctions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Auctions];
GO
IF OBJECT_ID(N'[dbo].[Bids]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bids];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Auctions'
CREATE TABLE [dbo].[Auctions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Amount] float  NOT NULL,
    [Metal] int  NOT NULL,
    [StartingPrice] float  NOT NULL,
    [MinimunBidIncrease] float  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [ClosingTime] datetime  NOT NULL,
    [State] int  NOT NULL,
    [OwnerAccountId] int  NOT NULL,
    [WinnerAccountId] int  NULL
);
GO

-- Creating table 'Bids'
CREATE TABLE [dbo].[Bids] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [State] int  NOT NULL,
    [OwnerAccountId] int  NOT NULL,
    [AuctionId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Auctions'
ALTER TABLE [dbo].[Auctions]
ADD CONSTRAINT [PK_Auctions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Bids'
ALTER TABLE [dbo].[Bids]
ADD CONSTRAINT [PK_Bids]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [OwnerAccountId] in table 'Bids'
ALTER TABLE [dbo].[Bids]
ADD CONSTRAINT [FK_AccountBid]
    FOREIGN KEY ([OwnerAccountId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountBid'
CREATE INDEX [IX_FK_AccountBid]
ON [dbo].[Bids]
    ([OwnerAccountId]);
GO

-- Creating foreign key on [OwnerAccountId] in table 'Auctions'
ALTER TABLE [dbo].[Auctions]
ADD CONSTRAINT [FK_AuctionAccount]
    FOREIGN KEY ([OwnerAccountId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuctionAccount'
CREATE INDEX [IX_FK_AuctionAccount]
ON [dbo].[Auctions]
    ([OwnerAccountId]);
GO

-- Creating foreign key on [WinnerAccountId] in table 'Auctions'
ALTER TABLE [dbo].[Auctions]
ADD CONSTRAINT [FK_AuctionAccount1]
    FOREIGN KEY ([WinnerAccountId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuctionAccount1'
CREATE INDEX [IX_FK_AuctionAccount1]
ON [dbo].[Auctions]
    ([WinnerAccountId]);
GO

-- Creating foreign key on [AuctionId] in table 'Bids'
ALTER TABLE [dbo].[Bids]
ADD CONSTRAINT [FK_BidAuction]
    FOREIGN KEY ([AuctionId])
    REFERENCES [dbo].[Auctions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BidAuction'
CREATE INDEX [IX_FK_BidAuction]
ON [dbo].[Bids]
    ([AuctionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
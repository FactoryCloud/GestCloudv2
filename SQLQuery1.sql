ALTER TABLE [dbo].[Movements]
    ADD CONSTRAINT [FK_Movements_ConditionID_Conditions] FOREIGN KEY ([ConditionID]) REFERENCES [dbo].[Conditions] ([ConditionID]);
ALTER TABLE [dbo].[Movements]
    ADD CONSTRAINT [FK_Movements_DocumentTypeID_DocumentTypes] FOREIGN KEY ([DocumentTypeID]) REFERENCES [dbo].[DocumentTypes] ([DocumentTypeID]);

GO
ALTER TABLE [dbo].[Movements]
    ADD CONSTRAINT [FK_Movements_ProductID_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID]);

GO
ALTER TABLE [dbo].[MTGCards]
    ADD CONSTRAINT [FK_MTGCards_ExpansionID_Expansions] FOREIGN KEY ([ExpansionID]) REFERENCES [dbo].[Expansions] ([Id]);

GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [FK_Products_ProductTypeID_ProductTypes] FOREIGN KEY ([ProductTypeID]) REFERENCES [dbo].[ProductTypes] ([ProductTypeId]);

GO
ALTER TABLE [dbo].[UsersAccessControl]
    ADD CONSTRAINT [FK_UserID_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]);

GO
ALTER TABLE [dbo].[UserPermissions]
    ADD CONSTRAINT [FK_UserPermissions_PermissionTypeID_PermissionTypes] FOREIGN KEY ([PermissionTypeID]) REFERENCES [dbo].[PermissionTypes] ([PermissionTypeId]);

GO
ALTER TABLE [dbo].[UserPermissions]
    ADD CONSTRAINT [FK_UserPermissions_UserID_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]);

GO
ALTER TABLE [dbo].[UserPermissions]
    ADD CONSTRAINT [FK_UserPermissions_UserTypeID_UserTypes] FOREIGN KEY ([UserTypeID]) REFERENCES [dbo].[UserTypes] ([UserTypeId]);

GO
ALTER TABLE [dbo].[AccessTypes]
    ADD CONSTRAINT [PK_AccessTypes] PRIMARY KEY CLUSTERED ([AccessTypeID] ASC);

GO
ALTER TABLE [dbo].[Conditions]
    ADD CONSTRAINT [PK_Conditions] PRIMARY KEY CLUSTERED ([ConditionID] ASC);

GO
ALTER TABLE [dbo].[DeliveryPurchases]
    ADD CONSTRAINT [PK_DeliveryPurchases] PRIMARY KEY CLUSTERED ([DeliveryPurchaseID] ASC);

GO
ALTER TABLE [dbo].[DeliverySale]
    ADD CONSTRAINT [PK_DeliverySales] PRIMARY KEY CLUSTERED ([DeliverySaleID] ASC);

GO
ALTER TABLE [dbo].[DocumentTypes]
    ADD CONSTRAINT [PK_DocumentTypes] PRIMARY KEY CLUSTERED ([DocumentTypeID] ASC);

GO
ALTER TABLE [dbo].[Expansions]
    ADD CONSTRAINT [PK_Expansions] PRIMARY KEY CLUSTERED ([Id] ASC);

GO
ALTER TABLE [dbo].[InvoicePurchases]
    ADD CONSTRAINT [PK_InvoicePurchases] PRIMARY KEY CLUSTERED ([InvoicePurchaseID] ASC);

GO
ALTER TABLE [dbo].[InvoiceSale]
    ADD CONSTRAINT [PK_InvoiceSales] PRIMARY KEY CLUSTERED ([InvoiceSaleID] ASC);

GO
ALTER TABLE [dbo].[Movements]
    ADD CONSTRAINT [PK_Movements] PRIMARY KEY CLUSTERED ([MovementID] ASC);

GO
ALTER TABLE [dbo].[MTGCards]
    ADD CONSTRAINT [PK_MTGCards] PRIMARY KEY CLUSTERED ([Id] ASC);

GO
ALTER TABLE [dbo].[PermissionTypes]
    ADD CONSTRAINT [PK_PermissionTypes] PRIMARY KEY CLUSTERED ([PermissionTypeId] ASC);

GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductID] ASC);

GO
ALTER TABLE [dbo].[ProductTypes]
    ADD CONSTRAINT [PK_ProductTypes] PRIMARY KEY CLUSTERED ([ProductTypeId] ASC);

GO
ALTER TABLE [dbo].[StockAdjusts]
    ADD CONSTRAINT [PK_StockAdjusts] PRIMARY KEY CLUSTERED ([StockAdjustID] ASC);

GO
ALTER TABLE [dbo].[UserPermissions]
    ADD CONSTRAINT [PK_UserPermissions] PRIMARY KEY CLUSTERED ([UserPermissionID] ASC);

GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC);

GO
ALTER TABLE [dbo].[UsersAccessControl]
    ADD CONSTRAINT [PK_UsersAccessControl] PRIMARY KEY CLUSTERED ([UserAccessControlID] ASC);

GO
ALTER TABLE [dbo].[UserTypes]
    ADD CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED ([UserTypeId] ASC);

GO
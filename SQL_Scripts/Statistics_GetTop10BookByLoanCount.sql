USE GeorgiaTechLibrary_BA_Project_DB;

SELECT TOP 10 COUNT([BookCopy]) AS [BorrowQuantity]
, MAX([LibraryItems].[LibraryItem].[Title]) AS [Title]
FROM [LibraryItems].[Borrow]
INNER JOIN [LibraryItems].[BookCopy]
ON [LibraryItems].[BookCopy].[BookCopyID] = [LibraryItems].[Borrow].[BookCopy]
INNER JOIN [LibraryItems].[LibraryItem]
ON [LibraryItems].[LibraryItem].[LibraryItemID] = [LibraryItems].[BookCopy].[BookID]
WHERE [LibraryItems].[BookCopy].[BookStatus] = 'On Loan'
OR 
[LibraryItems].[BookCopy].[BookStatus] = 'Overdue'
GROUP BY [BookID] ORDER BY COUNT([BookID]) DESC

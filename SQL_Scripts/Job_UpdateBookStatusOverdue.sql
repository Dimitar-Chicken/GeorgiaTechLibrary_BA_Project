UPDATE [LibraryItems].[BookCopy]
SET [BookStatus] = 'Overdue'
FROM [LibraryItems].[BookCopy]
INNER JOIN [LibraryItems].[Borrow] ON [LibraryItems].[Borrow].[BookCopy] = [LibraryItems].[BookCopy].[BookCopyID]
WHERE [ReturnDate] < GETDATE()
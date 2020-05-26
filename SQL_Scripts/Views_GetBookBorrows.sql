SELECT [LibraryItems].[BookCopy].[ISBN]
, [LibraryItems].[LibraryItem].[Title]
, CONCAT([People].[Person].[FirstName], ' ', [People].[Person].[LastName]) AS [PersonName]
, [LibraryItems].[BookCopy].[BookStatus]
, [LibraryItems].[Borrow].[BorrowDate]
, [LibraryItems].[Borrow].[ReturnDate]
FROM [LibraryItems].[Borrow]
INNER JOIN [People].[Person] 
ON [People].[Person].[SSN] = [LibraryItems].[Borrow].[BorrowerSSN]
INNER JOIN [LibraryItems].[BookCopy] 
ON [LibraryItems].[BookCopy].[BookCopyID] = [LibraryItems].[Borrow].[BookCopy]
INNER JOIN [LibraryItems].[LibraryItem]
ON [LibraryItems].[LibraryItem].[LibraryItemID] = [LibraryItems].[BookCopy].[BookID]

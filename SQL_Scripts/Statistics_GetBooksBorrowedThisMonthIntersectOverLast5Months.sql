USE GeorgiaTechLibrary_BA_Project_DB;

SELECT [LibraryItems].[LibraryItem].[Title]
FROM [LibraryItems].[Borrow]
INNER JOIN [LibraryItems].[BookCopy]
ON [LibraryItems].[BookCopy].[BookCopyID] = [LibraryItems].[Borrow].[BookCopy]
INNER JOIN [LibraryItems].[LibraryItem]
ON [LibraryItems].[LibraryItem].[LibraryItemID] = [LibraryItems].[BookCopy].[BookID]
WHERE MONTH([BorrowDate]) = MONTH(GETDATE())

INTERSECT

SELECT [LibraryItems].[LibraryItem].[Title]
FROM [LibraryItems].[Borrow]
INNER JOIN [LibraryItems].[BookCopy]
ON [LibraryItems].[BookCopy].[BookCopyID] = [LibraryItems].[Borrow].[BookCopy]
INNER JOIN [LibraryItems].[LibraryItem]
ON [LibraryItems].[LibraryItem].[LibraryItemID] = [LibraryItems].[BookCopy].[BookID]
WHERE MONTH([BorrowDate]) >= MONTH(GETDATE()) - 6
AND
MONTH([BorrowDate]) <= MONTH(GETDATE()) - 1

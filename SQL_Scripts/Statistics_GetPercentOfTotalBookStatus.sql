USE GeorgiaTechLibrary_BA_Project_DB;

SELECT [BookStatus] AS [Status]
, COUNT(*) AS [CountOfTotal]
, COUNT([BookStatus]) * 100 / (SELECT(COUNT(*)) FROM [LibraryItems].[BookCopy]) AS [PercentOfTotal]
FROM [LibraryItems].[BookCopy]
GROUP BY [BookStatus]

using GTL_Application.Interfaces;
using GTL_Application.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace GTL_Application.Services
{
    public class DataAccess : IDataAccess
    {
        private string connectionString;

        public DataAccess()
        {
            connectionString = @"Server=ANOOBIS-DESKTOP\SQL2019;" +
                                "Database=GeorgiaTechLibrary_BA_Project_DB;" +
                                "User Id=GTLClient;" +
                                "Password=1234;";
        }

        public ObservableCollection<ILibraryItem> GetLibraryItemList()
        {
            ObservableCollection<ILibraryItem> libraryItems = new ObservableCollection<ILibraryItem>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT * FROM [Views].[GetLibraryItems]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    LibraryItem libraryItem = new LibraryItem
                    {
                        Title = dataReader["Title"].ToString(),
                        Authors = dataReader["AuthorsNames"].ToString(),
                        SubjectArea = dataReader["SubjectArea"].ToString(),
                        ItemDescription = dataReader["ItemDescription"].ToString(),
                        TypeName = dataReader["LibraryItemType"].ToString()
                    };

                    libraryItems.Add(libraryItem);
                }
            }

            return libraryItems;
        }

        public ObservableCollection<ILibraryItemBorrow> GetLibraryItemBorrowsList()
        {
            ObservableCollection<ILibraryItemBorrow> libraryItemBorrows = new ObservableCollection<ILibraryItemBorrow>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT * FROM [Views].[GetBookBorrows]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    LibraryItemBorrow libraryItemBorrow = new LibraryItemBorrow
                    {
                        PersonName = dataReader["PersonName"].ToString(),
                        Title = dataReader["Title"].ToString(),
                        ISBN = dataReader["ISBN"].ToString(),
                        Status = dataReader["BookStatus"].ToString(),
                        //TODO: Add check for Parsing success.
                        BorrowDate = Convert.ToDateTime(dataReader["BorrowDate"]),
                        ReturnDate = Convert.ToDateTime(dataReader["ReturnDate"])
                    };

                    libraryItemBorrows.Add(libraryItemBorrow);
                }
            }

            return libraryItemBorrows;
        }

        public ObservableCollection<IBorrowableBookCopy> GetBorrowableBookCopiesList()
        {
            ObservableCollection<IBorrowableBookCopy> borrowableBookCopies = new ObservableCollection<IBorrowableBookCopy>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT * FROM [Views].[GetBorrowableBookCopies]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    BorrowableBookCopy borrowableBookCopy = new BorrowableBookCopy
                    {
                        ID = ConvertToSecureString(dataReader["BookCopyID"].ToString()),
                        Title = dataReader["Title"].ToString(),
                        Authors = dataReader["AuthorsNames"].ToString()
                    };

                    borrowableBookCopies.Add(borrowableBookCopy);
                }
            }

            return borrowableBookCopies;
        }

        public ObservableCollection<IPerson> GetPeople()
        {
            ObservableCollection<IPerson> people = new ObservableCollection<IPerson>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT * FROM [Views].[GetMembers]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Person person = new Person
                    {
                        SSN = ConvertToSecureString(dataReader["SSN"].ToString()),
                        PersonName = dataReader["PersonName"].ToString(),
                        Type = dataReader["Type"].ToString(),
                        Address = dataReader["Address"].ToString(),
                        Phone = dataReader["Phone"].ToString(),
                        Email = dataReader["Email"].ToString()
                    };

                    if (person.Type == "Member")
                    {
                        person.MembershipStartDate = Convert.ToDateTime(dataReader["MembershipStartDate"]);
                        person.MembershipEndDate = Convert.ToDateTime(dataReader["MembershipEndDate"]);
                    }

                    people.Add(person);
                }
            }

            return people;
        }

        public bool CreateNewBookBorrow(SecureString SSN, IBorrowableBookCopy borrowableBookCopy)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Declaring the SQL command.
                SqlCommand command = new SqlCommand(null, connection);
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("CreateNewBookBorrowTransaction");

                command.Transaction = transaction;

                try
                {
                    //Creating the parameters.
                    SqlParameter borrowerSSN = new SqlParameter("@borrowerSSN", SqlDbType.Char, 13);
                    SqlParameter bookCopy = new SqlParameter("@bookCopy", SqlDbType.Int);
                    SqlParameter borrowDate = new SqlParameter("@borrowDate", SqlDbType.Date);
                    SqlParameter returnDate = new SqlParameter("@returnDate", SqlDbType.Date);

                    //Setting the parameter values.
                    DateTime currentDate = DateTime.Today;

                    borrowerSSN.Value = ConvertSecureStringToString(SSN);
                    bookCopy.Value = Int32.Parse(ConvertSecureStringToString(borrowableBookCopy.ID));
                    borrowDate.Value = currentDate;
                    returnDate.Value = currentDate.AddDays(21);

                    //Adding the parameters to the SQL command.
                    command.Parameters.Add(borrowerSSN);
                    command.Parameters.Add(bookCopy);
                    command.Parameters.Add(borrowDate);
                    command.Parameters.Add(returnDate);

                    //Preparing the SQL command and executing it.
                    command.CommandText = "INSERT INTO [LibraryItems].[Borrow]([BorrowerSSN],[BookCopy],[BorrowDate],[ReturnDate]) " +
                                  "VALUES(@borrowerSSN,@bookCopy,@borrowDate,@returnDate)";
                    command.Prepare();
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE [LibraryItems].[BookCopy] " +
                                          "SET[BookStatus] = 'On Loan' " +
                                          "WHERE [BookCopyID] = @bookCopy";
                    command.Prepare();
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {

                    }
                }
            }

            return result >= 0;
        }

        public static String ConvertSecureStringToString(SecureString input)
        {
            IntPtr bstr = Marshal.SecureStringToBSTR(input);

            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }

        /// <summary>
        /// Method used to convert string from query result to a SecureString type.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>SecureString result</returns>
        public SecureString ConvertToSecureString(string input)
        {
            if (input == null)
                return null;

            SecureString result = new SecureString();
            foreach (char c in input)
            {
                result.AppendChar(c);
            }

            return result;
        }
    }
}

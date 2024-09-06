using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsStore_DataAccessLayer
{
    public class clsWordsDataAccess
    {

        public static bool GetWordInfoByID(int WordID, ref string Word, ref string Definition, ref string Example,
                                           ref string Synonym)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Words WHERE WordID = @WordID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@WordID", WordID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    IsFound = true;

                    Word = (string)reader["Word"];
                    Definition = (string)reader["Definition"];
                    Example = (string)reader["Example"];
                    Synonym = (string)reader["Synonym"];

                }
                else
                {
                    // The record was not found
                    IsFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool GetWordInfoByName(ref int WordID, string Word, ref string Definition, ref string Example,
                                          ref string Synonym)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Words WHERE Word = @Word";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Word", Word);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    IsFound = true;
                    WordID = (int)reader["WordID"];
                    Definition = (string)reader["Definition"];
                    Example = (string)reader["Example"];
                    Synonym = (string)reader["Synonym"];

                }
                else
                {
                    // The record was not found
                    IsFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static int AddNewWord(string Word, string Definition, string Example,
                                     string Synonym)
        {
            //this function will return the new contact id if succeeded and -1 if not.
            int WordID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Words (Word, Definition, Example, Synonym)
                             VALUES (@Word, @Definition, @Example, @Synonym);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Word", Word);

            command.Parameters.AddWithValue("@Definition", Definition);

            if (Example != "")
                command.Parameters.AddWithValue("@Example", Example);
            else
                command.Parameters.AddWithValue("@Example", DBNull.Value);

            if (Synonym != "")
                command.Parameters.AddWithValue("@Synonym", Synonym);
            else
                command.Parameters.AddWithValue("@Synonym", DBNull.Value);



            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    WordID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return WordID;
        }

        public static bool UpdateWord(int WordID, string Word, string Definition, string Example,
                                     string Synonym)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Words  
                            SET Word = @Word, 
                                Definition = @Definition, 
                                Example = @Example, 
                                Synonym = @Synonym                          
                                WHERE WordID = @WordID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@WordID", WordID);

            command.Parameters.AddWithValue("@Word", Word);

            command.Parameters.AddWithValue("@Definition", Definition);

            if (Example != "")
                command.Parameters.AddWithValue("@Example", Example);
            else
                command.Parameters.AddWithValue("@Example", DBNull.Value);

            if (Synonym != "")
                command.Parameters.AddWithValue("@Synonym", Synonym);
            else
                command.Parameters.AddWithValue("@Synonym", DBNull.Value);

                   

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllWords()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Words";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static int GetNumberOfWords()
        {

            int NumberOfWords = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT COUNT(Word) From Words WHERE Word is not null";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int AcualNumOfWords))
                {
                    NumberOfWords = AcualNumOfWords;
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return NumberOfWords;

        }

        public static DataTable GetAllWordsContain(string Statment)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $"SELECT * FROM Words where Word LIKE '{Statment}%' ";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }


        public static bool DeleteWord(int WordID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Words 
                                where WordID = @WordID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@WordID", WordID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }

        public static bool DoesWordExist(int WordID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Words WHERE WordID = @WordID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@WordID", WordID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }



    }
}

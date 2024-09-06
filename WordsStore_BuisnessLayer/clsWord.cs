using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsStore_DataAccessLayer;

namespace WordsStore_BuisnessLayer
{
    public class clsWord
    {
        enum enMode { eAddMew = 0, eUpdate = 1 }

        enMode Mode = enMode.eAddMew;


        public int WordID { get; set; }

        public string Word { get; set; }

        public string Definition { get; set; }

        public string Example { get; set; }

        public string Synonym { get; set; }


        public clsWord()
        {
            WordID = -1;
            Word = "";
            Definition = "";
            Example = "";
            Synonym = "";
            Mode = enMode.eAddMew;
        }

        private clsWord(int WordID, string Word, string Definition, string Example, string Synonym)
        {
            this.WordID = WordID;
            this.Word = Word;
            this.Definition = Definition;
            this.Example = Example;
            this.Synonym = Synonym;
            Mode = enMode.eUpdate;

        }

        private bool _AddNewWord()
        {
            this.WordID = clsWordsDataAccess.AddNewWord(this.Word, this.Definition, this.Example, this.Synonym);

            return (this.WordID != -1);                
        }

        private bool _UpdateWord()
        {
            return (clsWordsDataAccess.UpdateWord(this.WordID, this.Word, this.Definition, this.Example, this.Synonym));
        }

        public static clsWord Find(int wordID)
        {         
            string Word = "", Definition = "", Example = "", Synonym = "";

            if (clsWordsDataAccess.GetWordInfoByID(wordID, ref Word, ref Definition, ref Example, ref Synonym))
            {
                return new clsWord(wordID, Word, Definition, Example, Synonym);
            }
            else
            {
                return null;
            }
        }

        public static clsWord Find(string Word)
        {
            int wordID = -1;
            string Definition = "", Example = "", Synonym = "";

            if (clsWordsDataAccess.GetWordInfoByName(ref wordID, Word, ref Definition, ref Example, ref Synonym))
            {
                return new clsWord(wordID, Word, Definition, Example, Synonym);
            }
            else
            {
                return null;
            }
        }


        public static DataTable GetAllWords()
        {
            return (clsWordsDataAccess.GetAllWords());
        }

        public static DataTable GetAllWordsContain(string Statment)
        {
            return clsWordsDataAccess.GetAllWordsContain(Statment);
        }

        public static int GetNumberOfWords()
        {
            return clsWordsDataAccess.GetNumberOfWords();
        }

        public static bool DeleteWord(int wordID)
        {
            return clsWordsDataAccess.DeleteWord(wordID);
        }

        public static bool DoesWordExist(int wordID)
        {
            return clsWordsDataAccess.DoesWordExist(wordID);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.eAddMew:
                    {
                        if (_AddNewWord())
                        {
                            Mode = enMode.eUpdate;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.eUpdate:
                    return (_UpdateWord());

            }

            return false;
        }



    }
}

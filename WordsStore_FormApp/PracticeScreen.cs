using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordsStore_BuisnessLayer;

namespace WordsStore_FormApp
{
    public partial class PracticeScreen : Form
    {
        public PracticeScreen()
        {
            InitializeComponent();
        }


        DataTable _AllWordsList = new DataTable();
        
        short _QuestionsCounter = 0;

        short[] _QuesWordsIndexsInList = new short[clsWord.GetNumberOfWords()];

        clsStates _States = new clsStates(0, 0);

        class clsStates
        {
            public short RightAnswers { get; set; }
            public short WrongAnswers { get; set; }

            public clsStates(short RightAnswers, short WrongAnswers)
            {
                this.RightAnswers = RightAnswers;
                this.WrongAnswers = WrongAnswers;
            }

        }

        public static void ShuffleArray<T>(T[] array)
        {
            Random rnd = new Random();
            int[] order = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                order[i] = rnd.Next();
            }
            Array.Sort(order, array);
        }
          
        static void GetRndWordsIndexesForQuestions(ref short[] WordsIndexes)
        {
            for (short i = 0; i < clsWord.GetNumberOfWords(); i++)
            {
                WordsIndexes[i] = i;
            }

            ShuffleArray(WordsIndexes);

        }

        void GenerateQuestionHeader(short CurrentQuesRowIndex)
        {
            lblQuestion.Text = "What Is The Definition Of " +
                _AllWordsList.Rows[CurrentQuesRowIndex][1].ToString();
        }

        void DisplayAnswerOnButton(Button B, string Answer, char Tag = 'n')
        {
            B.Text = Answer.Trim();
            B.Tag = Tag;
        }

        void ShuffleAndDisplayAnswers()
        {
            Button[] btnAnswers = new Button[4] { btnAnswerA, btnAnswerB, btnAnswerC, btnAnswerD };

            ShuffleArray(btnAnswers);

            SwapButtons(btnAnswers[0], btnAnswerA);
            SwapButtons(btnAnswers[1], btnAnswerB);
            SwapButtons(btnAnswers[2], btnAnswerC);
            SwapButtons(btnAnswers[3], btnAnswerD);

            //Double shuffling cuz iam a nerd :)
            ShuffleArray(btnAnswers);

            SwapButtons(btnAnswers[0], btnAnswerA);
            SwapButtons(btnAnswers[1], btnAnswerB);
            SwapButtons(btnAnswers[2], btnAnswerC);
            SwapButtons(btnAnswers[3], btnAnswerD);
        }

        void GenerateAnswers()
        {
            Button[] btnAnswers = new Button[4] { btnAnswerA, btnAnswerB, btnAnswerC, btnAnswerD };

            //putting the right answer first.
            DisplayAnswerOnButton(btnAnswers[0],
                       _AllWordsList.Rows[_QuesWordsIndexsInList[_QuestionsCounter]][2].ToString(), 'y');


            byte j = 0;
            for (byte i = 1; i < 4; i++)
            {
                //to avoid repeating an answer.
                if (j == _QuestionsCounter)
                {
                    j++;
                    DisplayAnswerOnButton(btnAnswers[i],
                   _AllWordsList.Rows[_QuesWordsIndexsInList[j]][2].ToString());
                }
                else
                    DisplayAnswerOnButton(btnAnswers[i],
                   _AllWordsList.Rows[_QuesWordsIndexsInList[j]][2].ToString());

                j++;
            }


            ShuffleAndDisplayAnswers();
        }

        void GenerateQuestion()
        {
            ChangeQuestionCounter();
            GenerateQuestionHeader(_QuesWordsIndexsInList[_QuestionsCounter]);
            GenerateAnswers();
        }

        bool IsAnswerRight(Button B)
        {
            if (B.Tag.Equals('y'))
            {
                return true;
            }
            return false;
        }

        void SwapButtons(Button B1, Button B2)
        {
            Button btnTemp = new Button();

            btnTemp.Text = B1.Text;
            btnTemp.Tag = B1.Tag;


            B1.Text = B2.Text;
            B1.Tag = B2.Tag;

            B2.Text = btnTemp.Text;
            B2.Tag = btnTemp.Tag;

        }

        void ResetButtonsColors()
        {
            Button[] btnAnswers = new Button[4] { btnAnswerA, btnAnswerB, btnAnswerC, btnAnswerD };

            for(byte i = 0; i < 4; i++)
            {
                btnAnswers[i].BackColor = Color.Gainsboro;
            }
        }

        void EnableOrDisableButton(Button B, bool Enable)
        {
            B.Enabled = Enable;
        }

        void EnableOrDisableAnswersButtons(bool Enable)
        {
            EnableOrDisableButton(btnAnswerA, Enable);
            EnableOrDisableButton(btnAnswerB, Enable);
            EnableOrDisableButton(btnAnswerC, Enable);
            EnableOrDisableButton(btnAnswerD, Enable);
        }

        bool IsGameOver()
        {
            return (_QuestionsCounter == PracticeSettingsScreen.PracticeSettings.NumberOfQuestions - 1);
        }

        void ChangeQuestionCounter()
        {
            short QuestionsCounter = _QuestionsCounter;
            QuestionsCounter++;
            lblQuestionsCounter.Text = QuestionsCounter.ToString() + "/" +
                PracticeSettingsScreen.PracticeSettings.NumberOfQuestions;
        }

        void EnableOrDisableAllButtons(bool Enable)
        {
            EnableOrDisableAnswersButtons(Enable);
            EnableOrDisableButton(btnNextQuestion, Enable);
            EnableOrDisableButton(btnStart, Enable);
        }

        void ResetButtonsInfo()
        {
            Button[] btnAnswers = new Button[4] { btnAnswerA, btnAnswerB, btnAnswerC, btnAnswerD };

            for (byte i = 0; i < 4; i++)
            {
                btnAnswers[i].Text = "";
                btnAnswers[i].Tag = null;
            }
        }

        void ClearScreen()
        {
            ResetButtonsInfo();
            ResetButtonsColors();
            lblQuestionsCounter.Text = "0/0";
            lblQuestion.Text = "What Is The Definition Of ";

        }

        void ResetGame()
        {
            ClearScreen();
            EnableOrDisableAnswersButtons(false);
            EnableOrDisableButton(btnStart, true);
            EnableOrDisableButton(btnNextQuestion, false);
            GetRndWordsIndexesForQuestions(ref _QuesWordsIndexsInList);
            PracticeSettingsScreen.ResetPracticeSettings();
            _QuestionsCounter = 0;


        }

        void GreenRightAnswer()
        {
            Button[] btnAnswers = new Button[4] { btnAnswerA, btnAnswerB, btnAnswerC, btnAnswerD };

            for (byte i = 0; i < 4; i++)
            {
                if (btnAnswers[i].Tag.Equals('y'))
                    btnAnswers[i].BackColor = Color.Green;

            }
        }

        void RefreshStates()
        {
            lblRightAnswers.Text = _States.RightAnswers.ToString();
            lblWrongAnswers.Text = _States.WrongAnswers.ToString();
        }

        void ResetStates()
        {
            _States = new clsStates(0, 0);
        }

        bool IsPassed()
        {
            if (_States.RightAnswers >= _States.WrongAnswers)
                return true;

            return false;

        }

        string GetLastMessage()
        {
            if (IsPassed())
                return "You Passed";

            return "You Failed";
        }

        bool IsGameValid()
        {
            return _AllWordsList.Rows.Count > 3;
        }

        private void PracticeScreen_Load(object sender, EventArgs e)
        {

            _AllWordsList = clsWord.GetAllWords();
            GetRndWordsIndexesForQuestions(ref _QuesWordsIndexsInList);
            EnableOrDisableButton(btnNextQuestion, false);
            EnableOrDisableAnswersButtons(false);
            ResetStates();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!IsGameValid())
            {
                MessageBox.Show("You Cant Start This Activity Unless You Have Atleast 4 Words In Your List");
                return;
            }    

            EnableOrDisableAllButtons(true);
            GenerateQuestion();
            EnableOrDisableButton(btnNextQuestion, true);
            EnableOrDisableButton(btnStart, false);
            ResetStates();
            RefreshStates();
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {     
            if (IsAnswerRight((Button)sender))
            {
                ((Button)sender).BackColor = Color.Green;
                _States.RightAnswers++;
            }
            else
            {
                ((Button)sender).BackColor = Color.Red;
                GreenRightAnswer();
                _States.WrongAnswers++;

            }

            RefreshStates();
            EnableOrDisableAnswersButtons(false);
        }

        private void btnNextQuestion_Click(object sender, EventArgs e)
        {
            ResetButtonsColors();
            EnableOrDisableAnswersButtons(true);

            if (IsGameOver())
            {
                EnableOrDisableAllButtons(false);
                MessageBox.Show(GetLastMessage(), "Game Over");
                return;
            }

            _QuestionsCounter++;          
            GenerateQuestion();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ResetGame();
            ResetStates();
            RefreshStates();
        }

        private void btnBackToMainScreen_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainScreen frm = new MainScreen();
            frm.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            PracticeSettingsScreen frm = new PracticeSettingsScreen();
            frm.ShowDialog();
        }


    }
}

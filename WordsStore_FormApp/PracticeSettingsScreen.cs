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
    public partial class PracticeSettingsScreen : Form
    {
        public PracticeSettingsScreen()
        {
            InitializeComponent();
        }


        public static class PracticeSettings
        {
            public static int NumberOfQuestions = clsWord.GetNumberOfWords();
        }

        static public void ResetPracticeSettings()
        {
            PracticeSettings.NumberOfQuestions = clsWord.GetNumberOfWords();
        }

        public void ResetNumOfQuestionBar()
        {
            tbNumOfQuestions.Value = 0;
            tbNumOfQuestions.Minimum = 1;
            tbNumOfQuestions.Maximum = clsWord.GetNumberOfWords();
        }


        private void tbNumOfQuestions_Scroll(object sender, EventArgs e)
        {
            PracticeSettings.NumberOfQuestions = tbNumOfQuestions.Value;
            lblNumberOfQuestions.Text = tbNumOfQuestions.Value.ToString();
        }

        private void PracticeSettingsScreen_Load(object sender, EventArgs e)
        {
            ResetNumOfQuestionBar();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ResetPracticeSettings();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
using static System.Net.Mime.MediaTypeNames;

namespace WordsStore_FormApp
{
    public partial class WordDetailsScreen : Form
    {
        private string _ItemText;

        private bool _CanUpdate = false;

        public static clsWord Word;

        public WordDetailsScreen(string ItemText)
        {
            this._ItemText = ItemText;
            InitializeComponent();
        }

        public void EnableOrDisableTexts(bool Enable)
        {
            if (Enable)
            {
                txtWord.Enabled = true;
                txtDefinition.Enabled = true;
                txtExample.Enabled = true;
                txtSynonym.Enabled = true;
            }
            else
            {
                txtWord.Enabled = false;
                txtDefinition.Enabled = false;
                txtExample.Enabled = false;
                txtSynonym.Enabled = false;
            }
        }

        public void SetWordDetails()
        {
            if (Word != null)
            {
                txtWord.Text = Word.Word;
                txtDefinition.Text = Word.Definition;
                txtExample.Text = Word.Example;
                txtSynonym.Text = Word.Synonym;


                txtDefinition.Text = txtDefinition.Text.Trim();
                txtWord.Text = txtWord.Text.Trim();
                txtExample.Text = txtExample.Text.Trim();
                txtSynonym.Text = txtSynonym.Text.Trim();             
            }
            else
            {
                txtWord.Text = "";
                txtDefinition.Text = "";
                txtExample.Text = "";
                txtSynonym.Text = "";
            }

        }

        public bool SaveWordChanges()
        {
            
            Word.Word = txtWord.Text.Trim();
            Word.Definition = txtDefinition.Text.Trim();
            Word.Example = txtExample.Text.Trim();
            Word.Synonym = txtSynonym.Text.Trim();

            return Word.Save();
        }

        public void AlterSaveButton(bool Enable, bool Visible)
        {
            if (Enable)
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }

            if (Visible)
            {
                btnSave.Visible = true;
            }
            else
            {
                btnSave.Visible = false;
            }


        }

        private void WordDetails_Load(object sender, EventArgs e)
        {
            Word = clsWord.Find(_ItemText.Trim());
            EnableOrDisableTexts(false);
            SetWordDetails();
            AlterSaveButton(false, false);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Delete This Word ?", "" ,
                 MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                clsWord.DeleteWord(Word.WordID);

                if (MessageBox.Show("Word Deleted Successfully.", "",
                     MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {

                    this.Close();

                }
            }

            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!_CanUpdate)
            {
                _CanUpdate = true;
                if (MessageBox.Show("You Can Update Now.") == DialogResult.OK)
                {
                    EnableOrDisableTexts(true);
                }

                return;
            }


            MessageBox.Show("Please Continue Updating Or Click Save.");
            
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            AlterSaveButton(true, true);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveWordChanges()) 
            {
                AlterSaveButton(false, false);
            }
            else
            {
                MessageBox.Show("Something Wrong Happend!", "Error");
            }

        }
    }
}

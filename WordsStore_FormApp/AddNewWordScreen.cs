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
    public partial class AddNewWordScreen : Form
    {
        public AddNewWordScreen()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            clsWord NewWord = new clsWord();

            NewWord.Word = txtWord.Text.ToString().Trim().ToLower();
            NewWord.Definition = txtDefinition.Text.ToString().Trim().ToLower();
            NewWord.Example = txtExample.Text.ToString().Trim().ToLower();
            NewWord.Synonym = txtSynonym.Text.ToString().Trim().ToLower();

            if (MessageBox.Show("Are You Sure You Want To Add This Word ?",
                "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (NewWord.Save())
                {
                    if (MessageBox.Show("Word has been added successfully.",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        this.Close();
                    }

                }
                else
                {
                    if (MessageBox.Show("Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                         == DialogResult.OK)
                    {
                        this.Close();
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtWord_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWord.Text))
            {
                e.Cancel = true;
                txtWord.Focus();
                errorProvValidateAddWord.SetError(txtWord, "This Field Can't Be Empty");
            }
            else
            {
                e.Cancel = false;
                errorProvValidateAddWord.SetError(txtWord, "");

            }
        }

        private void txtDefinition_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDefinition.Text))
            {
                e.Cancel = true;
                txtDefinition.Focus();
                errorProvValidateAddWord.SetError(txtDefinition, "This Field Can't Be Empty");
            }
            else
            {
                e.Cancel = false;
                errorProvValidateAddWord.SetError(txtDefinition, "");

            }
        }

        


        //Check if you want to change the nullble value of the definition.....
    }
}

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
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddNewWordScreen frm = new AddNewWordScreen();
            frm.ShowDialog();


        }

        public void DisplayAllWords()
        {
            DataTable DT = clsWord.GetAllWords();

            int counter = 1;

            foreach (DataRow Row in DT.Rows)
            {
                ListViewItem item = new ListViewItem(counter.ToString());

                item.Text = Row["Word"].ToString();             
                listView1.Items.Add(item);
                counter++;

            }
        }

        public void DisplayWordsContain(string Statment)
        {
            DataTable DT = clsWord.GetAllWordsContain(Statment);

            int counter = 1;

            foreach (DataRow Row in DT.Rows)
            {
                ListViewItem item = new ListViewItem(counter.ToString());

                item.Text = Row["Word"].ToString();
                listView1.Items.Add(item);
                counter++;

            }
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
           DisplayAllWords();
        }
      
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            WordDetailsScreen frm = new WordDetailsScreen(listView1.SelectedItems[0].Text);
            frm.ShowDialog();          
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            DisplayAllWords();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (lblSearch.Text != "")
                lblSearch.Text = "";

            listView1.Items.Clear();
            DisplayWordsContain(txtSearch.Text.Trim());

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            lblSearch.Text = "Search...";
        }

        private void btnPractice_Click(object sender, EventArgs e)
        {
            this.Hide();
            PracticeScreen frm = new PracticeScreen();
            frm.ShowDialog();
        }
    }
}

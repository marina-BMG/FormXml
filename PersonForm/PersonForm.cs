using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonForm
{
    public partial class PersonForm : Form
    {
        List<Person> persons;
        PersonUIService personUISvc;
        bool isModifyMode;
        Person selectedPerson;

        public PersonForm()
        {
            InitializeComponent();

            persons = new List<Person>();
            personUISvc = new PersonUIService();
        }

        private void mainTabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainTabCtrl.SelectedIndex == 0)
            {
                MyRefresh();
            }

        }

        private void MyRefresh()
        {
            persons = personUISvc.ListAll();
            personGrid.DataSource = persons;
        }

        private void PersonForm_Load(object sender, EventArgs e)
        {
            MyRefresh();
        }

        private void personGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            selectedPerson = personGrid.SelectedRows[0].DataBoundItem as Person;

            IdTxtBox.Text = selectedPerson.ID.ToString();
            IdTxtBox.Enabled = false;
            NameTxtBox.Text = selectedPerson.FirstName;
            mainTabCtrl.SelectedIndex = 1;
            isModifyMode = true;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            mainTabCtrl.SelectedIndex = 1;
            IdTxtBox.Enabled = true;
            IdTxtBox.Text = string.Empty;
            NameTxtBox.Text = string.Empty;

            isModifyMode = false;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            IdTxtBox.Text = string.Empty;
            NameTxtBox.Text = string.Empty;

            mainTabCtrl.SelectedIndex = 0;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (isModifyMode)
            {
                selectedPerson.FirstName = NameTxtBox.Text;
                personUISvc.Modify(selectedPerson);
                isModifyMode = false;
            }
            else
            {
                var person = new Person();

                person.ID = int.Parse(IdTxtBox.Text);
                person.FirstName = NameTxtBox.Text;

                personUISvc.Add(person);
            }

            mainTabCtrl.SelectedIndex = 0;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            selectedPerson = personGrid.SelectedRows[0].DataBoundItem as Person;

            personUISvc.Delete(selectedPerson.ID);
            MyRefresh();
        }
    }
}

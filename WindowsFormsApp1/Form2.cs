using System;
using System.Windows.Forms;

namespace Авторизация
{
    public partial class Sign : Form
    {
        string login { get; set; }
        public Sign(string login = "")
        {
            InitializeComponent();
            if (login != "")
                this.login = login;
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

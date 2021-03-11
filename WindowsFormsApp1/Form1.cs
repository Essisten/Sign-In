using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Авторизация
{
    public partial class Login : Form
    {
        bool registration = false;
        public List<User> Users = new List<User>();
        public int time = 900;
        public Login()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (registration == true)
            {
                if (LoginTextBox.Text != "" && PasswordTextBox.Text != "" && LastNameTextBox.Text != "" && EmailTextBox.Text != "" && RobotCheckBox.Checked == true && GenderComboBox.SelectedItem != null && BirthDateTimePicker.Value != null)
                {
                    Users.Add(new User(LoginTextBox.Text, LastNameTextBox.Text, PasswordTextBox.Text, EmailTextBox.Text, GenderComboBox.SelectedItem, Convert.ToString(BirthDateTimePicker.Value)));
                    folderBrowserDialog1.ShowDialog();
                    File.AppendAllText(folderBrowserDialog1.SelectedPath + "\\Data.txt", $"Имя: {LoginTextBox.Text}, Фамилия: {LastNameTextBox.Text}, Электронная почта: {PasswordTextBox.Text}, Пароль: {EmailTextBox.Text}, Пол: {GenderComboBox.SelectedItem}, Дата рождения: {BirthDateTimePicker.Value}.\n");
                    LoginTextBox.Clear();
                    PasswordTextBox.Clear();
                    LastNameTextBox.Clear();
                    EmailTextBox.Clear();
                    RobotCheckBox.Checked = false;
                    GenderComboBox.SelectedItem = "";
                    BirthDateTimePicker.Value = DateTime.Now;
                }

            }
            else
            {
                for (int i = 0; i < Users.Count; i++)
                {
                    if (LoginTextBox.Text == User.GetInfo(Users[i], 0) && PasswordTextBox.Text == User.GetInfo(Users[i], 3))
                    {
                        Sign sign = new Sign(User.GetInfo(Users[i], 0));
                        sign.TextLabel.Text = "Авторизация прошла успешно!";
                        sign.Show();
                        return;
                    }
                }
                Sign form2 = new Sign();
                form2.TextLabel.Text = "Неправильный логин или пароль";
                form2.Show();
            }
        }
        public void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            if (time == 0)
            {
                Close();
                timer1.Stop();
            }
            int min = time / 60;
            TimerLabel.Text = Convert.ToString(min) + ":" + Convert.ToString(time - min * 60);
        }
        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            if (registration)
            {
                LastNameTextBox.Hide();
                EmailTextBox.Hide();
                label1.Text = "Вход";
                LoginLabel.Text = "Логин";
                LastNameLabel.Hide();
                EmailLabel.Hide();
                GenderLabel.Hide();
                GenderComboBox.Hide();
                BirthDateLabel.Hide();
                BirthDateTimePicker.Hide();
                LoginButton.Text = "Войти";
                RegistrationButton.Text = "Регистрация";
                RobotCheckBox.Text = "Я не робот";
                registration = false;
            }
            else
            {
                LastNameTextBox.Show();
                EmailTextBox.Show();
                label1.Text = "Регистрация";
                LoginLabel.Text = "Имя";
                LastNameLabel.Show();
                EmailLabel.Show();
                GenderLabel.Show();
                GenderComboBox.Show();
                BirthDateLabel.Show();
                BirthDateTimePicker.Show();
                LoginButton.Text = "Создать";
                RegistrationButton.Text = "Авторизация";
                RobotCheckBox.Text = "Принимаю условия\nсоглашения и т.д.";
                registration = true;
            }

        }

        private void RobotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RobotCheckBox.Checked && LoginTextBox.Text != null && ((PasswordTextBox.Text != null && LastNameTextBox.Text != null && EmailTextBox.Text != null && GenderComboBox.SelectedItem != null && BirthDateTimePicker.Value != null) || (registration != true)))
                LoginButton.Enabled = true;
            else
                LoginButton.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] users = File.ReadAllLines(OpenFileDialog.FileName);
                List<string> UsersList = new List<string>();
                foreach (string line in users)
                {
                    string l = line.Replace("Имя: ", "");
                    l = l.Replace(" Фамилия: ", "");
                    l = l.Replace(" Электронная почта: ", "");
                    l = l.Replace(" Пароль: ", "");
                    l = l.Replace(" Пол: ", "");
                    l = l.Replace(" Дата рождения: ", "");
                    UsersList.Add(l);
                }
                for (int i = 0; i < UsersList.Count; i++)
                {
                    string[] tmp = UsersList[i].Split(',');
                    Users.Add(new User(tmp[0], tmp[1], tmp[2], tmp[3], tmp[4], tmp[5]));
                }
            }
        }
    }
    public class User
    {
        string name { get; set; }
        string name2 { get; set; }
        string email { get; set; }
        string password { get; set; }
        string gender { get; set; }
        string date { get; set; }
        public User(string name, string name2, string email, string password, object gender, string date)
        {
            this.name = name;
            this.name2 = name2;
            this.email = email;
            this.password = password;
            this.gender = Convert.ToString(gender);
            this.date = date;
        }
        public static string GetInfo(User user, byte i)
        {
            switch (i)
            {
                case 0:
                    return user.name;
                case 1:
                    return user.name2;
                case 2:
                    return user.email;
                case 3:
                    return user.password;
                case 4:
                    return user.gender;
                case 5:
                    return user.date;
                default:
                    return null;
            }
        }
    }
}

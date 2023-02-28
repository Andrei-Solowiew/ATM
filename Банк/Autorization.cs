using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Банк
{
    public partial class Autorization : Form
    {
        public static string connectsString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=БД.mdb";
        private OleDbConnection myConnection;
        public Autorization()
        {
            InitializeComponent();
            main = new Main();
            myConnection = new OleDbConnection(connectsString);
            myConnection.Open();
        }
        Main main;

        private void Autorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            string map = textBox1.Text;
            string pin_user = textBox2.Text;
            //string queryString = "SELECT [Телефон] FROM [Карты] WHERE [Номер карты] = '123'";
            string queryStringCount = "SELECT COUNT(*) FROM Карты WHERE [Номер карты] = '" + map + "'";
            string queryString = "SELECT [Пин-код] FROM [Карты] WHERE [Номер карты] = '" + map + "'";
            string queryPhone = "SELECT Телефон FROM [Карты] WHERE [Номер карты] = '" + map + "'";



        
            

            OleDbCommand command_count = new OleDbCommand(queryStringCount, myConnection);
            int count = (int)command_count.ExecuteScalar();




            if (count > 0) 
             {
                OleDbCommand command = new OleDbCommand(queryString, myConnection);
                OleDbDataReader reader = command.ExecuteReader();
                OleDbCommand comand_phone = new OleDbCommand(queryPhone, myConnection);
                OleDbDataReader phone = comand_phone.ExecuteReader();

                string pin = "";
                string phone_number = "";

                if (reader.Read() && phone.Read())
                {
                    pin = reader.GetInt32(0).ToString();
                    phone_number = phone.GetString(0);
                }

                if (pin == pin_user)
                {
                    main.Show();
                    
                    string save = "INSERT INTO [Сохранения] ([Телефон]) VALUES('" + phone_number + "')";
                    OleDbCommand command_write = new OleDbCommand(save, myConnection);
                    command_write.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Неверный логин и пароль");
                }
            }
            else
            {
                MessageBox.Show("Карты с данным номером не существует");

            }

        }
    }
}

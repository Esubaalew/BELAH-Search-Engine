using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Media_Player
{
    public partial class RegisterScreen : Form
    {
        public RegisterScreen()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void RegisterScreen_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(
                "Do you want leave registration?",
                "Terminate Registration",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2,
                MessageBoxOptions.ServiceNotification

               );
            if (result == DialogResult.Yes)
            {
                this.Hide();

            }
            else if (result == DialogResult.No)
            {
                this.Activate();


            }
        }
        public bool IsUserRegisterd(string userName)
        {
            string connectionString = "Data Source=ESUBIE;Initial Catalog=SearchEngine;Integrated Security=True";
            string query = "SELECT * FROM EngineUser WHERE UserName=@UserName";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand search= new SqlCommand(query, connection)) {
                    search.Parameters.AddWithValue("@UserName", userName);

                    using(SqlDataAdapter adapter= new SqlDataAdapter(search))
                    {
                        using (DataTable table = new DataTable())
                        {
                            adapter.Fill(table);
                            
                            if (table.Rows.Count==1)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        
                     

                    }
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                  $"Sure on Registering?",
                  "RegConfirm",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question,
                  MessageBoxDefaultButton.Button1,
                  MessageBoxOptions.RtlReading);
            if (result == DialogResult.Yes) { 
                if (IsUserRegisterd(txtUserName.Text.Trim())==false){


                    string connectionString = "Data Source=ESUBIE;Initial Catalog=SearchEngine;Integrated Security=True";
                    string query = "INSERT INTO EngineUser (UserFirstName, UserName, UserPass, DateJoined, IsLogged)";
                    query += "VALUES(@UserFirstName, @UserName, @UserPass, @DateJoined, @IsLogged)";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add(@"UserFirstName", SqlDbType.NVarChar, 30).Value = txtName.Text;
                            command.Parameters.Add(@"UserName", SqlDbType.NVarChar, 20).Value = txtUserName.Text;
                            command.Parameters.Add(@"UserPass", SqlDbType.NVarChar, 30).Value = txtPassword.Text;
                            command.Parameters.Add(@"DateJoined", SqlDbType.Date).Value = DateTime.Now;
                            command.Parameters.Add(@"IsLogged", SqlDbType.Int).Value = 0;
                            command.ExecuteNonQuery();


                            DialogResult Dresult = MessageBox.Show(
                          $"{txtName.Text}, Your Registration has succeed!",
                          "Registration Success",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information,
                          MessageBoxDefaultButton.Button1,
                          MessageBoxOptions.RtlReading
                         );
                            if (Dresult == DialogResult.OK)
                            {
                                this.Hide();
                            }

                        }
                    }
                }
                else
                {
                    DialogResult resultE = MessageBox.Show(
                $"You are already registered",
                "Registerd user",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading);
                    if (resultE == DialogResult.OK)
                    {
                        this.Hide();
                    }
                }

            }
        }
    }
}

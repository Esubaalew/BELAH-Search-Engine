using System.Data.SqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Media_Player
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();

        }

        private void OnExit(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Do you want leave Login?",
                "Terminate Login",
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

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            //txtName.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
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
            
            if (IsUserRegisterd(txtUserName.Text.Trim()))
            {
                string connectionString = "Data Source=ESUBIE;Initial Catalog=SearchEngine;Integrated Security=True";
                string query = "SELECT * FROM EngineUser WHERE UserName=@UserName";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand search = new SqlCommand(query, connection))
                    {
                        search.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                        using (SqlDataAdapter adapter = new SqlDataAdapter(search))
                        {
                            using (DataTable table = new DataTable())
                            {
                                adapter.Fill(table);
                                DataRow targetTuple = table.Rows[0];
                                string ValidUserPass = $"{targetTuple["UserPass"]}";
                                string CurrentUserPass=txtPassword.Text.Trim();
                                


                                if (ValidUserPass == CurrentUserPass)
                                {
                                    UpdateTable(txtUserName.Text.Trim());
                                   
                                    (this.Owner as MainForm).iconButton7.Visible=false;
                                    (this.Owner as MainForm).Text = $"Engine as {targetTuple["UserName"]}";





                                 DialogResult resultE = MessageBox.Show(
                                $"Ahh Login Succeed",
                                "Login Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.RtlReading);

                                    if (resultE == DialogResult.OK)
                                    {
                                        this.Hide();
                                    }
                                }
                                else
                                {
                                DialogResult resultE = MessageBox.Show(
                                $"Ahh Your Password is Wrong",
                                "Login Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.RtlReading);
                                    if (resultE == DialogResult.OK)
                                    {
                                        this.Activate();
                                    }

                                }

                            }
                        }
                    }
                    
                    
                }

            }
            else
            {
             DialogResult result = MessageBox.Show(
                               $"Opps this UserName Not registerd",
                               "Login Success",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1,
                               MessageBoxOptions.RtlReading);
                if (result == DialogResult.OK)
                {
                    this.Activate();
                    txtUserName.Text = "";
                    txtPassword.Text = "";             
                        
                }

            }
        }

        public void UpdateTable(string UserName)
        {
            string query = "UPDATE EngineUser SET IsLogged=@IsLogged WHERE UserName=@UserName";
            string connectionString = "Data Source=ESUBIE;Initial Catalog=SearchEngine;Integrated Security=True";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                sqlConnection.Open();
                using (SqlCommand update = new SqlCommand(query, sqlConnection))
                {
                    update.Parameters.Add("@IsLogged", SqlDbType.Int).Value = 1;
                    update.Parameters.AddWithValue("@UserName", SqlDbType.VarChar).Value = UserName;
                    update.ExecuteNonQuery();
                }
            }
        }
    }
}
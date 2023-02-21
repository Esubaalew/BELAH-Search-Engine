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
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }

        private void panelResult_Paint(object sender, PaintEventArgs e)
        {

        }
        public bool HistoryIsAvailable()
        {
            string conStr = "Data Source=ESUBIE;Initial Catalog=SearchEngine;Integrated Security=True";
            string query = "SELECT * FROM Search";
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                {
                    using (DataTable table = new DataTable())
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
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


        private void History_Load(object sender, EventArgs e)
        {
            string conStr = "Data Source=ESUBIE;Initial Catalog=SearchEngine;Integrated Security=True";
            string query = "SELECT SearchType, SearchWord, SearchDate FROM  Search";
            if (HistoryIsAvailable())
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        using (DataTable table = new DataTable())
                        {
                            adapter.Fill(table);
                            dataGridView1.DataSource = table;
                        }
                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("There is no History", "Empty History", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (result == DialogResult.OK)
                {
                    this.Hide();
                }
            }
        }
    }
}
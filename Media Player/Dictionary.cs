using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Windows.Input;
using System.Data.SqlClient;

namespace Media_Player
{
    public partial class Dictionary : Form
    {
        public Dictionary()
        {
            InitializeComponent();
            panelResult.Visible = false;
            panelBarolder.Anchor = AnchorStyles.None;
            panelFooter.Visible=false;
           
        }
        public void SaveAtions()
        {
            string conStr = "Data Source=ESUBIE;Initial Catalog=SearchEngine;Integrated Security=True";
            string query = "INSERT INTO Search (SearchType,SearchWord,SearchDate)";
            query += "VALUES(@SearchType,@SearchWord,@SearchDate)";
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@SearchType", SqlDbType.VarChar).Value = "Dictionary";
                    command.Parameters.Add("@SearchWord", SqlDbType.VarChar).Value = textBox1.Text.Trim();
                    command.Parameters.Add("@SearchDate", SqlDbType.DateTime).Value = DateTime.Now;

                    command.ExecuteNonQuery();
                }

            }
        }
        public void RegisterActivity()
        {
            //Check if traverse the database and find ine whise  attrr is =1
            string conStr = "Data Source=ESUBIE;Initial Catalog=SearchEngine;Integrated Security=True";
            string query = "SELECT * FROM EngineUser";
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        using (DataTable table = new DataTable())
                        {
                            adapter.Fill(table);
                            if (table.Rows.Count != 0)
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    if (Convert.ToInt32(row["IsLogged"]) == 1)
                                    {
                                        SaveAtions();
                                        break;

                                    }
                                }
                            }

                        }
                    }
                }
            }
        }


        private void iconButton1_Click(object sender, EventArgs e)
        {
            RegisterActivity();
            panelResult.Visible = true;
            panelResult.Dock = DockStyle.Top;
            panelBarolder.Visible = false;
            // panelBarolder.Visible = false;
            textBox2.Text = textBox1.Text;
            richTextBox1.Visible = true;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.BorderStyle = BorderStyle.None;
            ResultPanel.BackColor = Color.FromArgb(48, 49, 52);
            panelResult.BackColor = Color.FromArgb(48, 49, 52);
            iconButton5.BackColor = Color.FromArgb(48, 49, 52);
            textBox2.BackColor = Color.FromArgb(48, 49, 52);
            iconButton4.BackColor = Color.FromArgb(48, 49, 52);
            iconButton6.BackColor = Color.FromArgb(48, 49, 52);
           
            panelFooter.Visible = true;
            panelFooter.BackColor=Color.FromArgb(48, 49, 52);
            panelFooter.BorderStyle= BorderStyle.None;









            string? key = textBox1.Text;
            string? result = $"";
            string url = $"https://www.macmillandictionary.com/dictionary/british/{key}";
            var html = new HtmlWeb();
            var htmldoc = html.Load(url);
            var body = htmldoc.DocumentNode.SelectSingleNode("//body");
            if (htmldoc.Text.Contains("Sorry, no search result for"))
            {
                
              
               // Console.WriteLine($"Sorry, no results for \"{key}\" in the English Dictionary.");
                richTextBox1.Text = $"Sorry, no results for \"{key}\" in the English Dictionary.";
               
            }
            else
            {
       
                Console.WriteLine($"Search Results for \"{key}\": ");
                Console.WriteLine();
                var defnitions = body.SelectNodes("//span[@class='DEFINITION']");
                int count = 1;
                string? unwanted = "&nbsp";
                
                foreach (var defnition in defnitions)
                {
                    if ((defnition.InnerText).Contains(unwanted))
                    {
                        string @new = (defnition.InnerText).Replace(unwanted, "");
                        Console.WriteLine($"{count}: {@new}");
                    }
                    else
                    {
                        Console.WriteLine($"{count}: {defnition.InnerText}");
                        result += $"{count}: {defnition.InnerText}\n";
                    }
                    count++;
                }
                richTextBox1.Text = $"Showing Results For {key}:\n\n" + result;
            }
         

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            panelResult.Visible = true;
            panelResult.Dock = DockStyle.Top;
            panelBarolder.Visible = false;
            // textBox2.Text = textBox1.Text;
            richTextBox1.Visible = true;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.BorderStyle = BorderStyle.None;
            ResultPanel.BackColor = Color.FromArgb(48, 49, 52);
            iconButton5.BackColor = Color.FromArgb(48, 49, 52);
            textBox2.BackColor = Color.FromArgb(48, 49, 52);
            iconButton4.BackColor = Color.FromArgb(48, 49, 52);
            panelFooter.BackColor = Color.FromArgb(48, 49, 52);
            panelFooter.Visible = true;
            iconButton6.BackColor = Color.FromArgb(48, 49, 52);


            try
            {

                string? key = textBox2.Text;
                string? result = "";
                string url = $"https://www.macmillandictionary.com/dictionary/british/{key}";
                var html = new HtmlWeb();
                var htmldoc = html.Load(url);
                var body = htmldoc.DocumentNode.SelectSingleNode("//body");

                if (htmldoc.Text.Contains("Sorry, no search result for"))
                {

                    Console.WriteLine($"Sorry, no results for \"{key}\" in the English Dictionary.");
                    richTextBox1.Text = $"Sorry, no results for \"{key}\" in the English Dictionary.";



                }
                else
                {

                    Console.WriteLine($"Search Results for \"{key}\": ");
                    Console.WriteLine();
                    var defnitions = body.SelectNodes("//span[@class='DEFINITION']");
                    int count = 1;
                    string? unwanted = "&nbsp";

                    foreach (var defnition in defnitions)
                    {
                        if ((defnition.InnerText).Contains(unwanted))
                        {
                            string @new = (defnition.InnerText).Replace(unwanted, "");
                            Console.WriteLine($"{count}: {@new}");
                        }
                        else
                        {
                            Console.WriteLine($"{count}: {defnition.InnerText}");
                            result += $"{count}: {defnition.InnerText}\n";
                        }
                        count++;
                    }
                    richTextBox1.Text = $"Showing Results For {key}:\n\n" + result;
                }
            }
            catch (System.Net.WebException ex)
            {
                richTextBox1.Text = ("It seems your device is not connected to a functioning network\nPlease Connect to The Internet");
                


            }

        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
            {
                textBox2.Text = null;
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                textBox1.Text = null;
            }
        }
    }
}

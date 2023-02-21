using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace Media_Player
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            panelResult.Visible = false;
            panelBarolder.Anchor = AnchorStyles.None;


        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    command.Parameters.Add("@SearchType", SqlDbType.VarChar).Value = "AAU";
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
            richTextBox1.Visible = true;
            panelResult.BackColor = Color.FromArgb(48, 49, 52);
            richTextBox1.Dock = DockStyle.Fill;
            textBox2.BackColor = Color.FromArgb(48, 49, 52);
            iconButton5.BackColor = Color.FromArgb(48, 49, 52);
            iconButton4.BackColor = Color.FromArgb(48, 49, 52);
            iconButton6.BackColor = Color.FromArgb(48, 49, 52);
            textBox2.Text = textBox1.Text;


            




            string? search_key = textBox1.Text;
            string url = $"http://www.aau.edu.et/?s={search_key}";
            
            var html = new HtmlWeb();

            var htmldoc = html.Load(url);
            var result_page = htmldoc.DocumentNode.SelectSingleNode("html");
            string search_failed_response = "Sorry, but nothing matched your search criteria";

            if ((result_page.InnerText).Contains(search_failed_response))
            {
                Console.WriteLine($"Sorry, but nothing matched your search criteria: {search_key}. Please try again with some different keywords.");
                richTextBox1.Text = $"Sorry, but nothing matched your search criteria: {search_key}. Please try again with some different keywords.";
            }

            else
            {

                var resultName = htmldoc.DocumentNode.SelectSingleNode("//h2[@class='page-title']");
                Console.WriteLine(resultName.InnerText);
                var titles = htmldoc.DocumentNode.SelectNodes("//h2[@class='title']");

                var links = new ArrayList();
                for (int i = 0; i < titles.Count; i++)
                {
                    var anchor = titles[i].FirstChild;

                    var link = anchor.Attributes["href"].Value;
                    links.Add(link);

                }

                string? result = "";

                for (int i = 0; i < links.Count; i++)
                {
                    Console.WriteLine($"Resut {i + 1}: {titles[i].InnerText}");
                    Console.WriteLine($"SeeMore: {links[i]}");

                    result += $"Resut {i + 1}: {titles[i].InnerText}" + "\n" + $"SeeMore: {links[i]}\n";
                }
                richTextBox1.Text = "\n\n\n\n"+result;
               

                //Check if there is more than one page for search result
                bool anyOther = (result_page.InnerText).Contains(" Older posts");
                if (anyOther)
                {
                    var MainDiv = htmldoc.DocumentNode.SelectSingleNode("//div[@class='navigation clearfix']");
                    var subDiv = MainDiv.SelectSingleNode("//div[@class='alignleft']");
                    var seeOlderPostsAnchor = (subDiv.FirstChild);
                    var seeMoreLink = seeOlderPostsAnchor.Attributes["href"].Value; //see olderposts
                    Console.WriteLine(seeMoreLink);
                }






                //Upcoming feature:
                //as long as there is another search result load the next page of the
                //website Example the 2nd page of the "good" key is http://www.aau.edu.et/page/2/?s=good
            }



















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

        private void iconButton6_Click(object sender, EventArgs e)
        {
            panelResult.Visible = true;
            richTextBox1.Dock = DockStyle.Fill;
          //panelResult.Dock = DockStyle.Top;
            panelBarolder.Visible = false;
       
          
            panelResult.BackColor = Color.FromArgb(48, 49, 52);
         
         // titles.Dock= DockStyle.Top;
          //richTextBox1.Dock = DockStyle.Fill;





            string? search_key = textBox2.Text;
            string url = $"http://www.aau.edu.et/?s={search_key}";
            Console.WriteLine($"Finding '{search_key}' on AAU Website... ");
            var html = new HtmlWeb();

            var htmldoc = html.Load(url);
            var result_page = htmldoc.DocumentNode.SelectSingleNode("html");
            string search_failed_response = "Sorry, but nothing matched your search criteria";

            if ((result_page.InnerText).Contains(search_failed_response))
            {
                Console.WriteLine($"Sorry, but nothing matched your search criteria: {search_key}. Please try again with some different keywords.");
              richTextBox1.Text = $"Sorry, but nothing matched your search criteria: {search_key}. Please try again with some different keywords.";
            }

            else
            {

                var resultName = htmldoc.DocumentNode.SelectSingleNode("//h2[@class='page-title']");
                Console.WriteLine(resultName.InnerText);
                var titles = htmldoc.DocumentNode.SelectNodes("//h2[@class='title']");

                var links = new ArrayList();
                for (int i = 0; i < titles.Count; i++)
                {
                    var anchor = titles[i].FirstChild;

                    var link = anchor.Attributes["href"].Value;
                    links.Add(link);

                }

                string? result = "";

                for (int i = 0; i < links.Count; i++)
                {
                    Console.WriteLine($"Resut {i + 1}: {titles[i].InnerText}");
                    Console.WriteLine($"SeeMore: {links[i]}");

                    result += $"Resut {i + 1}: {titles[i].InnerText}" + "\n" + $"SeeMore: {links[i]}" + "\n\n";
                }

                richTextBox1.Text = "\n\n\n\n" + result;

                //Check if there is more than one page for search result
                bool anyOther = (result_page.InnerText).Contains(" Older posts");
                if (anyOther)
                {
                    var MainDiv = htmldoc.DocumentNode.SelectSingleNode("//div[@class='navigation clearfix']");
                    var subDiv = MainDiv.SelectSingleNode("//div[@class='alignleft']");
                    var seeOlderPostsAnchor = (subDiv.FirstChild);
                    var seeMoreLink = seeOlderPostsAnchor.Attributes["href"].Value; //see olderposts
                    Console.WriteLine(seeMoreLink);
                }






                //Upcoming feature:
                //as long as there is another search result load the next page of the
                //website Example the 2nd page of the "good" key is http://www.aau.edu.et/page/2/?s=good













            }
        }
    }
}

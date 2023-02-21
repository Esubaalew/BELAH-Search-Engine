using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Runtime.CompilerServices;
using FontAwesome.Sharp;
using HtmlAgilityPack;

namespace Media_Player
{
   
    public partial class MainForm : Form
    {
        string currentUser = "";
        public string MenuBtn = "";
        public MainForm() 
        {
            InitializeComponent();
            intialize();
            HideMenu();
           




        }
        public bool LoginShow
        {
            get { return iconButton7.Visible; }
            set { iconButton7.Visible = value; }
        }


        private Form? activeForm = null;

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
                activeForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle= FormBorderStyle.None;
                childForm.Dock= DockStyle.Fill;
                panelContainer.Controls.Add(childForm);
                panelContainer.Tag= childForm;
                childForm.BringToFront();
                childForm.Show();
            
        }
        private void intialize()
        {
            ResutShow.Visible = false;
            panelContainer.Dock=DockStyle.Fill;
            panelBarolder.Anchor = AnchorStyles.None;
            iconButton1.Enabled = false;
            iconButton6.Enabled = false;
            richTextBox1.Visible=false;
            richTextBox1.ReadOnly=true;
            richTextBox1.Text = null;
            listBox1.Visible = false;
            label1.Visible = false;
            listBox1.Items.Clear();
            panelFooter.Visible = false;
            iconButton7.Text = "Login";
            iconButton8.Text = "Register";

        }
      

        private void btnAAU_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form2());
            panelFooter.Visible = false;
            ResutShow.Visible = false;
            label1.Visible=false;
        }

        private void btnEncy_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form3());
            ResutShow.Visible = false;
            panelFooter.Visible = false;
            label1.Visible = false;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            HideMenu();
            
        }

        private void HideMenu()
        {
            if (this.panelSideMenu.Width > 200)
            {
                panelSideMenu.Width = 100;
                pictureBox2.Visible = false;
                btnMenu.Dock=DockStyle.Top;
                foreach(Button menuBtn in panelSideMenu.Controls.OfType<Button>())
                {
                    menuBtn.Text = "";
                    menuBtn.ImageAlign = ContentAlignment.MiddleCenter;
                    menuBtn.Padding = new Padding(0);
                }
            }
            else
            {
                panelSideMenu.Width = 230;
                pictureBox2.Visible = true;
                btnMenu.Dock = DockStyle.None;

                foreach (Button menuBtn in panelSideMenu.Controls.OfType<Button>())
                {
                    menuBtn.Text = "   "+menuBtn.Tag.ToString();
                    menuBtn.ImageAlign = ContentAlignment.MiddleLeft;
                    menuBtn.Padding = new Padding(10,0,0,0);
                }

            }
        }
       

   
        private void AboutClicked(object sender, EventArgs e)
        {
            // OpenChildForm(new About());
            Form about = new About();
            label1.Visible = false;
            about.ShowDialog();
            btnAbout.BackColor = Color.FromArgb(117, 122, 180);


            btnAbout.BackColor = Color.FromArgb(98, 105, 150);
            // this.FormBorderStyle = FormBorderStyle.None;
            ResutShow.Visible = false;
            listBox1.Visible = false;
            label1.Visible = false;
            panelFooter.Visible = false;

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form3());
            ResutShow.Visible = false;
            panelFooter.Visible = false;
            label1.Visible = false;





        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                textBox1.Text = null;
            }
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
                    command.Parameters.Add("@SearchType", SqlDbType.VarChar).Value = "ENCYCLOPEDIA";
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



           

            if (textBox1.Text != null)
            {
                iconButton1.Enabled = true;
                panelBarolder.Visible = false;
                ResutShow.Visible = true;
                ResutShow.Dock = DockStyle.Top;
                panelContainer.Dock = DockStyle.Fill;
                textBox2.Text = textBox1.Text;
                richTextBox1.Visible = true;
                richTextBox1.Dock = DockStyle.Fill;
                richTextBox1.BorderStyle = BorderStyle.None;
                ResutShow.BackColor = Color.FromArgb(48, 49, 52);
                iconButton5.BackColor = Color.FromArgb(48, 49, 52);
                textBox2.BackColor = Color.FromArgb(48, 49, 52);
                iconButton4.BackColor = Color.FromArgb(48, 49, 52);
                iconButton6.BackColor = Color.FromArgb(48, 49, 52);
                panelFooter.Visible = true;
                panelFooter.BorderStyle = BorderStyle.None;
                listBox1.Visible = false;
                label1.Visible = false;










                try
                {


                    string? search_key;
                    search_key = textBox1.Text;
                    string url = $"https://en.m.wikipedia.org/wiki/{search_key}";
                    //Console.WriteLine($"Finding {search_key} on Wikipedia... ");

                    var html = new HtmlWeb();
                    var htmldoc = html.Load(url);
                    string search_result = "";
                    var page = htmldoc.DocumentNode.Element("html");
                    string search_failed_response = "We  do not have an article with this exact name";
                    if ((page.InnerHtml).Contains(search_failed_response))
                    {
                        Console.WriteLine(search_failed_response);
                        Console.WriteLine("You can search the word in :");
                        label1.Text = search_failed_response + "\n You can search the word in :\n";
                        label1.Visible = true;
                        label1.Dock = DockStyle.Top;
                        var nav = page.SelectSingleNode("//nav");
                        nav.Remove();
                        var ul_first = page.SelectSingleNode("//ul[@id='page-actions']");
                        ul_first.Remove();
                        var bad_div = page.SelectSingleNode("//div[@id='noarticletext_technical']");
                        bad_div.Remove();
                        var options = page.SelectNodes("//li");
                        var wiki_sisters = new List<string>();
                        foreach (var option in options)
                        {
                            wiki_sisters.Add(option.InnerText);
                            if ((option.InnerText).Contains("Wikispecies"))
                            {
                                break;
                            }

                        }
                        foreach (var sister in wiki_sisters)
                        {
                            Console.WriteLine(sister);
                            listBox1.Visible = true;
                            listBox1.Items.Add("\t\t\t\t" + sister);
                            listBox1.Dock = DockStyle.Fill;
                            listBox1.BackColor = Color.FromArgb(32, 33, 36);
                            listBox1.ForeColor = Color.Blue;
                            // listBox1.Location = new System.Drawing.Point(52, 113);
                            listBox1.Anchor = AnchorStyles.None;
                            //listBox1.Size = new Size(603, 194);
                            richTextBox1.Visible = false;
                            listBox1.BorderStyle = BorderStyle.None;
                            listBox1.Dock = DockStyle.Fill;

                        }
                        /*
                        var sis = new List<Row> { };
                        foreach(var sister in wiki_sisters)
                        {
                            sis.Add(new Row { sister = sister });
                        }
                        using (var writter = new StreamWriter("D:\\C#ProjectFile\\new.csv"))
                        using (var csv = new CsvWriter(writter, CultureInfo.InvariantCulture))
                        {
                            csv.WriteRecords(sis);
                        }
                       */
                    }
                    //string? bad = ;
                    else if (htmldoc.Text.Contains($"Disambiguation pages"))
                    {
                        richTextBox1.Text = "Ambigious";
                    }
                    else
                    {
                        var body = htmldoc.DocumentNode.SelectSingleNode("/html/body");
                        //var references = body.SelectNodes("//sup");


                        //foreach (var reference in references)
                        //{
                        //    reference.Remove();
                        //}




                        var paragrahs = body.SelectNodes("//p");

                        foreach (var paragraph in paragrahs)
                        {
                            Console.WriteLine(paragraph.InnerText);
                            search_result += paragraph.InnerText;


                            //richTextBox1.BackColor = Color.FromArgb(255, 255, 255);
                            //richTextBox1.ForeColor = Color.Black;



                        }
                        richTextBox1.Text = search_result;
                    }

                }
                catch (System.NullReferenceException ex)
                {
                    richTextBox1.Text = $"Sorry, it seems the word you enetered is ambigious.\n Please not that words or phrases with more than one meaning should be clear from your query\n. searching 'who' instead of WHO is ambigious for  example.";
                }
                catch (System.Net.WebException ex)
                {
                  richTextBox1.Text=("It seems your device is not connected to a functioning network\nPlease Connect to The Internet");
                     
                   



                }

            }
                
        }

        private void btnImages_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Images());
            ResutShow.Visible = false;
            listBox1.Visible = false;
            label1.Visible = false;
            panelFooter.Visible = false;

        }

        private void BtnDiction_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Dictionary());
            ResutShow.Visible = false;
            label1.Visible = false;
            listBox1.Visible = false;
            panelFooter.Visible = false;
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {

            OpenChildForm(new History());
            ResutShow.Visible = false;
            label1.Visible = false;
            listBox1.Visible = false;
            panelFooter.Visible = false;

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Settings());
            ResutShow.Visible = false;
            label1.Visible = false;
            listBox1.Visible = false;
            panelFooter.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            MessageBoxButtons boxButtons = MessageBoxButtons.YesNo;
            MessageBoxIcon boxIcon = MessageBoxIcon.Exclamation;
            MessageBoxDefaultButton messageBoxDefaultButton = MessageBoxDefaultButton.Button1;
            MessageBoxOptions messageBoxOptions = MessageBoxOptions.ServiceNotification;
            DialogResult result;
            result = MessageBox.Show("Do you want to exit?", "Exiting Engine", boxButtons, boxIcon, messageBoxDefaultButton, messageBoxOptions);
            if (result == DialogResult.Yes)
            {
                Application.Exit();



            }
            else
            {
                this.Activate();

            }
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            textBox2 = null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

                iconButton1.Enabled = true;

           



        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            RegisterActivity();
            try
            {
                string? search_key;
                search_key = textBox2.Text;
                string url = $"https://en.m.wikipedia.org/wiki/{search_key}";
                //Console.WriteLine($"Finding {search_key} on Wikipedia... ");

                var html = new HtmlWeb();
                var htmldoc = html.Load(url);

                var page = htmldoc.DocumentNode.Element("html");
                string search_failed_response = "Wikipedia does not have an article with this exact name";

                if ((page.InnerHtml).Contains(search_failed_response))
                {
                    Console.WriteLine(search_failed_response);
                    Console.WriteLine("You can search the word in :");
                    label1.Text = search_failed_response + "\n You can search the word in :\n";
                    label1.Visible = true;
                    label1.Dock = DockStyle.Top;
                    var nav = page.SelectSingleNode("//nav");
                    nav.Remove();
                    var ul_first = page.SelectSingleNode("//ul[@id='page-actions']");
                    ul_first.Remove();
                    var bad_div = page.SelectSingleNode("//div[@id='noarticletext_technical']");
                    bad_div.Remove();
                    var options = page.SelectNodes("//li");
                    var wiki_sisters = new List<string>();
                    foreach (var option in options)
                    {
                        wiki_sisters.Add(option.InnerText);
                        if ((option.InnerText).Contains("Wikispecies"))
                        {
                            break;
                        }

                    }

                    foreach (var sister in wiki_sisters)
                    {
                        Console.WriteLine(sister);

                        listBox1.Visible = true;
                        listBox1.Items.Add("\t\t\t\t" + sister);
                        listBox1.Dock = DockStyle.Fill;
                        listBox1.BackColor = Color.FromArgb(32, 33, 36);
                        listBox1.ForeColor = Color.Blue;

                        listBox1.Anchor = AnchorStyles.None;

                        richTextBox1.Visible = false;
                        listBox1.BorderStyle = BorderStyle.None;
                        listBox1.Dock = DockStyle.Fill;



                    }

                }
                else if (htmldoc.Text.Contains($"Disambiguation pages"))
                {
                    richTextBox1.Text = "Ambigious";
                }
                else
                {
                    var body = htmldoc.DocumentNode.SelectSingleNode("/html/body");
                    var references = body.SelectNodes("//sup");

                    //foreach (var reference in references)
                    //{
                    //    reference.Remove();
                    //}

                    var paragrahs = body.SelectNodes("//p");
                    string search_result = "";
                    foreach (var paragraph in paragrahs)
                    {
                        Console.WriteLine(paragraph.InnerText);
                        search_result += paragraph.InnerText;
                        richTextBox1.Text = search_result;
                        listBox1.Visible = false;
                        label1.Visible = false;

                        richTextBox1.BorderStyle = BorderStyle.None;
                        ResutShow.BackColor = Color.FromArgb(48, 49, 52);
                        iconButton5.BackColor = Color.FromArgb(48, 49, 52);
                        textBox2.BackColor = Color.FromArgb(48, 49, 52);
                        iconButton4.BackColor = Color.FromArgb(48, 49, 52);
                        iconButton6.BackColor = Color.FromArgb(48, 49, 52);
                        panelFooter.Visible = true;
                        panelFooter.BorderStyle = BorderStyle.None;
                    }

                }
            }
            catch (System.Net.WebException ex)
            {
                richTextBox1.Text = ("It seems your device is not connected to a functioning network\nPlease Connect to The Internet");
                // richTextBox1.ForeColor= Color.Red;


            }



        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
                iconButton6.Enabled = true;
            
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {

        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            Form login = new LoginScreen();
           // this.IsMdiContainer = true;
           // login.MdiParent = this;
            login.Owner = this;
            login.ShowDialog();
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            Form register = new RegisterScreen();
            register.ShowDialog();
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Check if traverse the database and find ine whise  attrr is =1
            string conStr = "Data Source=ESUBIE;Initial Catalog=SearchEngine;Integrated Security=True";
            string query = "SELECT * FROM EngineUser";
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                using (SqlCommand command= new SqlCommand(query, connection))
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
                                        this.Text = $"Engine as {row["UserName"]}";
                                        iconButton7.Visible = false;
                                        currentUser = $"{row["Username"]}";


                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void UpdateTable()
        {
            string query = $"UPDATE EngineUser SET IsLogged=@IsLogged WHERE IsLogged={1}";
            string connectionString = "Data Source=ESUBIE;Initial Catalog=SearchEngine;Integrated Security=True";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                sqlConnection.Open();
                using (SqlCommand update = new SqlCommand(query, sqlConnection))
                {
                    update.Parameters.Add("@IsLogged", SqlDbType.Int).Value = 0;
                    
                    update.ExecuteNonQuery();
                }
            }
        }
        public void ClearHistory()
        {
            string conStr = "Data Source=ESUBIE;Initial Catalog=SearchEngine;Integrated Security=True";
            string query = "DELETE  FROM Search";
            using (SqlConnection connection= new SqlConnection(conStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearHistory();
          //  UpdateTable();

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
                                        this.Text = $"Engine";
                                        
                                        iconButton7.Visible = true;
                                        UpdateTable();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }


        }

        private void ResutShow_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
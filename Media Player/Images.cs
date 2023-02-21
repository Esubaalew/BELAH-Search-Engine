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
using System.Net;
using System.Windows.Documents;
using System.Collections.Immutable;
using System.Net.Http.Headers;
using Microsoft.Win32;
using System.Diagnostics.Metrics;
using System.Drawing.Imaging;
using System.Data.SqlClient;

namespace Media_Player
{

    public partial class Images : Form
    {
        private const int V = 50;

        public Images()
        {
            InitializeComponent();
            panelResult.Visible = false;
            panelBarolder.Anchor = AnchorStyles.None;

        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            this.Close();


        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            RegisterActivity();
            if (listView1.Items.Count != 0)
            {
                listView1.Items.Clear();
            }
            panelResult.Visible = true;
            panelResult.Dock = DockStyle.Top;
            panelBarolder.Visible = false;
            listView1.Dock = DockStyle.Fill;
            listView1.Visible = true;
            listView1.ForeColor = Color.White;
            // panelOptions.Visible = true;
            panelOptions.Dock = DockStyle.Bottom;

           
            string? key = textBox1.Text;
            textBox2.Text = textBox1.Text;
            List<string> links = new List<string>();
            string? url = $"https://en.m.wikipedia.org/wiki/{key}";

            var html = new HtmlWeb();
            var htmldoc = html.Load(url);

            var page = htmldoc.DocumentNode.Element("html");
            string search_failed_response = "Wikipedia does not have an article with this exact name";
            if ((page.InnerHtml).Contains(search_failed_response))
            {
                //Console.WriteLine($"Ehh..there is no image related to {key}! Edit {key} AGAIN!");

            }
            else if (page.InnerHtml.Contains("This disambiguation page lists articles associated with the title")){
                MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                MessageBox.Show("Your query is a bit ambigios be more specific", "Word Ambigious", messageBoxButtons, icon);
            }
            else
            {
                //New!
                //ImageContainerParent=htmldoc.DocumentNode.SelectNodes("//a[@class='image']");
                //ImageConatainer=ImageContainerParent
                var imgs = htmldoc.DocumentNode.SelectNodes("//img[@class='thumbimage']");
                
                foreach (var img in imgs)
                {
                    var src = img.Attributes["src"].Value;
                    //Console.WriteLine(src);
                    links.Add($"https:{src.ToString()}");

                    //Console.WriteLine("-----------------");
                }
            }
            ImageList images = new ImageList();
            images.ImageSize = new Size(200, 200);
            images.ColorDepth = ColorDepth.Depth32Bit;


            for (int i = 0; i < links.Count; i++)
            {
                
                var clinet2 = new HttpClient();
                clinet2.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");
                

           

                Image im = Image.FromStream(await clinet2.GetStreamAsync(links[i]));
                images.Images.Add(im);
                Uri uri = new Uri(links[i]);
                listView1.Items.Add(uri.Segments.Last(), i);
                // listView1.Items.Add("", i);
            }
            panelOptions.Visible = true;
            listView1.SmallImageList = images;
            listView1.LargeImageList = images;


        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
            {
                textBox2.Text = null;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                textBox1.Text = null;
            }
        }

        private async void iconButton6_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0)
            {
                listView1.Items.Clear();
            }
            panelResult.Visible = true;
            panelResult.Dock = DockStyle.Top;
            panelBarolder.Visible = false;
            listView1.Dock = DockStyle.Fill;
            listView1.Visible = true;
            listView1.ForeColor = Color.White;

            panelOptions.Dock = DockStyle.Bottom;




            string? key = textBox2.Text;
            List<string> links = new List<string>();
            string? url = $"https://en.m.wikipedia.org/wiki/{key}";

            try
            {
                var html = new HtmlWeb();
                var htmldoc = html.Load(url);

                var page = htmldoc.DocumentNode.Element("html");
                string search_failed_response = "WE does not have an article with this exact name";
                if ((page.InnerHtml).Contains(search_failed_response))
                {


                }
                else if (page.InnerHtml.Contains("This disambiguation page lists articles associated with the title"))
                {
                    MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Error;
                    MessageBox.Show("Your query is a bit ambigios be more specific", "Word Ambigious", messageBoxButtons, icon);
                }
                else
                {
                    var imgs = htmldoc.DocumentNode.SelectNodes("//img[@class='thumbimage']");
                    foreach (var img in imgs)
                    {
                        var src = img.Attributes["src"].Value;
                        //Console.WriteLine(src);
                        links.Add($"https:{src.ToString()}");

                        //Console.WriteLine("-----------------");
                    }
                }
                ImageList images = new ImageList();
                images.ImageSize = new Size(200, 200);
                images.ColorDepth = ColorDepth.Depth32Bit;


                for (int i = 0; i < links.Count; i++)
                {

                    var client2 = new HttpClient();
                    client2.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");


                    Image im = Image.FromStream(await client2.GetStreamAsync(links[i]));
                    images.Images.Add(im);
                    Uri uri = new Uri(links[i]);
                    listView1.Items.Add(uri.Segments.Last(), i);

                }
                panelOptions.Visible = true;
                listView1.SmallImageList = images;
                listView1.LargeImageList = images;
            }
             
            catch (System.NullReferenceException){
                MessageBox.Show("No Images");
            }

            catch (System.Net.Http.HttpRequestException ex)
            {
                MessageBox.Show("No Images");
            }
            catch (Exception)
            {
                MessageBox.Show("No Images");
            }

        }

        private void OnModeChange(object sender, EventArgs e)
        {
            if ((string)comboBox1.SelectedItem == "Tile")
            {
                listView1.View = View.Tile;
            }

            else if ((string)comboBox1.SelectedItem == "Small Icon")
            {
                listView1.View = View.SmallIcon;
            }
            else if ((string)comboBox1.SelectedItem == "List")
            {
                listView1.View = View.List;
            }
            else if ((string)comboBox1.SelectedItem == "Large Icon")
            {
                listView1.View = View.LargeIcon;
            }

        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.InitialDirectory = "C:\\Documents";
            saveFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                
                int counter = 1;
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    var image = item.ImageList.Images[item.ImageIndex]; //or imageIndex
                    using (MemoryStream memory = new MemoryStream())
                    {
                        using (BinaryWriter fs = new BinaryWriter(System.IO.File.Create(path)))
                        {
                            image?.Save(memory, ImageFormat.Jpeg);
                            byte[] bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                        }
                    }
                    counter++;
                }

                MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                
                MessageBox.Show($"Image was successfully Saved to {path}!", "Save Success", messageBoxButtons, icon);
            }
        }

        private void OnImageSelected(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.InitialDirectory = "C:\\Documents";
            saveFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                int counter = 1;
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    var image = item.ImageList.Images[item.ImageIndex]; //or imageIndex
                    using (MemoryStream memory = new MemoryStream())
                    {
                        using (BinaryWriter fs = new BinaryWriter(System.IO.File.Create(path)))
                        {
                            image?.Save(memory, ImageFormat.Jpeg);
                            byte[] bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                        }
                    }
                    counter++;
                }

                MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                MessageBox.Show($"Image was successfully Saved to {path}", "Save Success", messageBoxButtons, icon);


            }
        }

        private void listView1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = listView1.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    listView1.ContextMenuStrip = contextMenuStrip1;
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
        
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                var image = item.ImageList.Images[item.ImageIndex];
               // item.ImageList.ColorDepth = ColorDepth.Depth4Bit;
                item.ImageList.ImageSize = new Size(50, 50);
               

            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    var image = item.ImageList.Images[item.ImageIndex];
                    // item.ImageList.ColorDepth = ColorDepth.Depth4Bit;
                    item.ImageList.ImageSize = new Size(100, 100);


                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                MessageBox.Show("Illegal attempt", "Wrong Attempt", messageBoxButtons, icon);
            }
        }

        private void Images_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                var image = item.ImageList.Images[item.ImageIndex];
                // item.ImageList.ColorDepth = ColorDepth.Depth4Bit;
                item.ImageList.ImageSize = new Size(200, 200);


            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            if (font.ShowDialog()== DialogResult.OK)
            {
                listView1.ForeColor = font.Color;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)comboBox1.SelectedItem == "Small")
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    var image = item.ImageList.Images[item.ImageIndex];
                    // item.ImageList.ColorDepth = ColorDepth.Depth4Bit;
                    item.ImageList.ImageSize = new Size(50, 50);


                }
            }

            else if ((string)comboBox1.SelectedItem == "Medium")
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    var image = item.ImageList.Images[item.ImageIndex];
                    // item.ImageList.ColorDepth = ColorDepth.Depth4Bit;
                    item.ImageList.ImageSize = new Size(100, 100);


                }
            }
            else if ((string)comboBox1.SelectedItem == "Large")
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    var image = item.ImageList.Images[item.ImageIndex];
                    // item.ImageList.ColorDepth = ColorDepth.Depth4Bit;
                    item.ImageList.ImageSize = new Size(200, 200);


                }
            }
           


        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
               listView1.BackColor = color.Color;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog()== DialogResult.OK)
            {
                listView1.ForeColor=dialog.Color;
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
                    command.Parameters.Add("@SearchType", SqlDbType.VarChar).Value = "IMAGES";
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

    }
}


//Registration Case 

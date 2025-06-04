using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace final_weatherly
{
    public partial class Form1 : Form
    {

        // database info
        string _server = "127.0.0.1";
        int _port = 3306;
        string _database = "csc340";
        string _id = "root";
        string _pw = "";
        string _connectionAddress = "";
        BindingSource dbgridBindingSource = new BindingSource();


        public Form1()
        {

            this.Hide();
            Login ss = new Login();
            ss.Show();

            // getts to the data a

            InitializeComponent();
            _connectionAddress =
            string.Format("Server={0};Port={1};DataBase={2};Username={3};Password={4}",
            _server, _port, _database, _id, _pw);
            selectTable();

        }
       
        

        public void changeHeader()
        {
            dataGridView1.Columns[0].HeaderText = "VIN";
            dataGridView1.Columns[1].HeaderText = "LICENSE PLATE";
            dataGridView1.Columns[2].HeaderText = "MAKER";
            dataGridView1.Columns[3].HeaderText = "MODEL";
            dataGridView1.Columns[4].HeaderText = "YEAR";
        }


        // displays the grid
        private void selectTable()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionAddress))
                {
                    conn.Open();
                    // getts the whole database
                    string selectQuery = string.Format("SELECT * FROM events");
                    MySqlCommand command = new MySqlCommand(selectQuery, conn);
                    MySqlDataReader table = command.ExecuteReader();
                    dbgridBindingSource.DataSource = table;

                    dataGridView1.DataSource = dbgridBindingSource;

                    // hides the rest of the tables
                    this.dataGridView1.Columns["location"].Visible = false;
                    this.dataGridView1.Columns["startTime"].Visible = false;
                    this.dataGridView1.Columns["endTime"].Visible = false;
                    this.dataGridView1.Columns["description"].Visible = false;
                    this.dataGridView1.Columns["month"].Visible = false;

                    changeHeader();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error!!:" + exc.Message);
            }
        }
        
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // sets the databases to the data grid
            int sel_idx = e.RowIndex;
            this.EventTitletextBox.Text = dataGridView1.Rows[sel_idx].Cells["title"].Value.ToString();
            this.LocationTextBox.Text =
             dataGridView1.Rows[sel_idx].Cells["location"].Value.ToString();
            this.StartTimeTextBox.Text = dataGridView1.Rows[sel_idx].Cells["startTime"].Value.ToString();
            this.EndTimeTextBox.Text = dataGridView1.Rows[sel_idx].Cells["endTime"].Value.ToString();
            this.descriptionText.Text = dataGridView1.Rows[sel_idx].Cells["description"].Value.ToString(); dbgridBindingSource.ResetBindings(false); // refresh table
        }



        private void Addbutton_Click(object sender, EventArgs e) // add event button
        {
            var month = monthCalendar1.SelectionStart.Month.ToString();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionAddress))
                {
                    conn.Open();
                    // SQL code
                    string insertQuery = string.Format("INSERT INTO events (title, location, startTime, endTime, description, month) " + "VALUES ('{0}', '{1}', '{2}','{3}','{4}','{5}');",
                    EventTitletextBox.Text, LocationTextBox.Text, StartTimeTextBox.Text, EndTimeTextBox.Text, descriptionText.Text, month);
                    MySqlCommand command = new MySqlCommand(insertQuery, conn); if (command.ExecuteNonQuery() != 1)
                        MessageBox.Show("Failed to insert data.");
                    // Empties textbox
                    EventTitletextBox.Text = "";
                    LocationTextBox.Text = "";
                    StartTimeTextBox.Text = "";
                    EndTimeTextBox.Text = "";
                    descriptionText.Text = "";
                    selectTable();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonUpdate_Click_1(object sender, EventArgs e) // update button
        {
            try
            {
                using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
                {
                    mysql.Open();
                    // collects the data from the text boxs
                    var title = EventTitletextBox.Text;
                    var location = LocationTextBox.Text;
                    var startTime = StartTimeTextBox.Text;
                    var endTime = EndTimeTextBox.Text;
                    var description = descriptionText.Text;
                    var month = monthCalendar1.SelectionStart.Month.ToString();
                    // sql code
                    string updateQuery = "UPDATE events SET title=@title, location=@location, startTime= @startTime, endTime = @endTime, description = @description, month = @month WHERE title = @title";
                    using (MySqlCommand command = new MySqlCommand(updateQuery, mysql))
                    {
                        // putts all the new data into the databases
                        command.Parameters.AddWithValue("title", title);
                        command.Parameters.AddWithValue("location", location);
                        command.Parameters.AddWithValue("startTime", startTime);
                        command.Parameters.AddWithValue("endTime", endTime);
                        command.Parameters.AddWithValue("description", description);
                        command.Parameters.AddWithValue("month", month);
                        if (command.ExecuteNonQuery() != 1)
                            MessageBox.Show("Failed to delete data.");
                    }
                    // resets the text box
                    EventTitletextBox.Text = "";
                    LocationTextBox.Text = "";
                    StartTimeTextBox.Text = "";
                    EndTimeTextBox.Text = "";
                    descriptionText.Text = "";

                    selectTable();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonDelete_Click_1(object sender, EventArgs e) //delete button
        {
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(_connectionAddress))
                    {
                        conn.Open();
                        string title = EventTitletextBox.Text;
                        // SQL code to delte the data from the database
                        string deleteQuery = string.Format("DELETE FROM events WHERE title='{0}';");
                        MySqlCommand command = new MySqlCommand(deleteQuery, conn);
                        if (command.ExecuteNonQuery() != 1)
                            MessageBox.Show("Failed to delete data.");
                        // resets the textboxs
                        EventTitletextBox.Text = "";
                        LocationTextBox.Text = "";
                        StartTimeTextBox.Text = "";
                        EndTimeTextBox.Text = "";
                        descriptionText.Text = "";
                        selectTable();
                        conn.Close();
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
        


        private void LocationTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void StartTimeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Logoutbutton_Click(object sender, EventArgs e)
        {

            // moves the window to the login page
            this.Hide();
            Login ss = new Login();
            ss.Show();
        }

        private void Clearbutton_Click(object sender, EventArgs e)
        {
            // sets every text box to blank
            EventTitletextBox.Text = "";
            LocationTextBox.Text = "";
            StartTimeTextBox.Text = "";
            EndTimeTextBox.Text = "";
            descriptionText.Text = "";

            selectTable();
        }

        private void Monthlybutton_Click(object sender, EventArgs e)
        {
            // getts the month
            var monh = monthCalendar1.SelectionStart.Month.ToString();

            // setts the buttons and data on a roation
            dataGridView1.Visible = false;
            listBox1.Visible = true;
            AllEventsButton.Visible = true;
            Monthlybutton.Visible = false;
            
            // Getts the SQL code from the database to the list box
            MySqlConnection conn = new MySqlConnection(_connectionAddress);
            MySqlCommand cmd = new MySqlCommand("SELECT title FROM events WHERE month = '" + monh + "'");
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            listBox1.DataSource = dt;
            listBox1.DisplayMember = "title";
            
        }

        private void AllEventsButton_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            listBox1.Visible = false;
            button2.Visible = false;
            Monthlybutton.Visible = true;
        }
    }
}
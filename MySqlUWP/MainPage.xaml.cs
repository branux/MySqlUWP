using MySql.Data.MySqlClient;
using System;
using Windows.UI.Xaml;

namespace MySqlUWP
{
    public sealed partial class MainPage
    {
        private MySqlConnection _connection;

        public MainPage()
        {
            InitializeComponent();
        }

        private void ButtonCreate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (_connection = new MySqlConnection(
                    "Database=test;Data Source=localhost;User Id=root;Password=teste;SslMode=None;"))
                {
                    _connection.Open();
                    var createCommand = new MySqlCommand("CREATE TABLE user(id int,name varchar(255))", _connection);
                    createCommand.ExecuteNonQuery();


                    var addCommand = new MySqlCommand("INSERT INTO user (id,name) VALUES (1,'João')", _connection);
                    addCommand.ExecuteNonQuery();

                    TextBoxErro.Text = "Table and data created successfully.";
                }
            }
            catch (Exception er)
            {
                TextBoxErro.Text = $"ERROR: {er.Message}";
            }
        }

        private void ButtonFind_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (_connection = new MySqlConnection(
                "Database=test;Data Source=localhost;User Id=root;Password=teste!@#;SslMode=None;"))
                {
                    _connection.Open();
                    var cmd = new MySqlCommand("select name from user where id=1", _connection);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TextBlockName.Text = (reader.GetString("name"));
                        }
                    }
                }
            }
            catch (Exception er)
            {
                TextBlockName.Text = $"ERROR: {er.Message}";
            }
        }
    }
}
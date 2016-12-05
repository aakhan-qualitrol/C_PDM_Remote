using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace C_PDM_Remote
{
    public partial class Main_Form : Form
    {

        public string DB_Connection_String = @"data source=" + Directory.GetCurrentDirectory() + "\\C_PDM_Remote_DB.db";
        public Main_Form()
        {
            InitializeComponent();
        }

        private void DateTime_timer_Tick(object sender, EventArgs e)
        {
            Main_Form_Statusbar_DateTime.Text = DateTime.Now.ToString();
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'c_PDM_Remote_dataset.Remote_Client_Communication_List' table. You can move, or remove it, as needed.
            this.remote_Client_Communication_ListTableAdapter.Fill(this.c_PDM_Remote_dataset.Remote_Client_Communication_List);
            // TODO: This line of code loads data into the 'c_PDM_Remote_dataset.RAU_Communication_List' table. You can move, or remove it, as needed.
            this.rAU_Communication_ListTableAdapter.Fill(this.c_PDM_Remote_dataset.RAU_Communication_List);
            // TODO: This line of code loads data into the 'c_PDM_Remote_dataset.Alarm_Warning_Ack_Log' table. You can move, or remove it, as needed.
            this.alarm_Warning_Ack_LogTableAdapter.Fill(this.c_PDM_Remote_dataset.Alarm_Warning_Ack_Log);
            // TODO: This line of code loads data into the 'c_PDM_Remote_dataset1.Alarm_Warning_UnAck_Log' table. You can move, or remove it, as needed.
            this.alarm_Warning_UnAck_LogTableAdapter.Fill(this.c_PDM_Remote_dataset.Alarm_Warning_UnAck_Log);
            // TODO: This line of code loads data into the 'c_PDM_Remote_dataset.RAU_List' table. You can move, or remove it, as needed.
            this.rAU_ListTableAdapter.Fill(this.c_PDM_Remote_dataset.RAU_List);
            // TODO: This line of code loads data into the 'c_PDM_Remote_dataset.System_Log' table. You can move, or remove it, as needed.
            this.system_LogTableAdapter.Fill(this.c_PDM_Remote_dataset.System_Log);
            // TODO: This line of code loads data into the 'c_PDM_Remote_dataset.Company_Information' table. You can move, or remove it, as needed.
            this.company_InformationTableAdapter.Fill(this.c_PDM_Remote_dataset.Company_Information);
        }

        private void Main_Form_Settings_System_Information_Change_Logo_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog _OpenFileDialog = new OpenFileDialog();

            _OpenFileDialog.InitialDirectory = @"C:\";
            _OpenFileDialog.Title = "Please Select Image File";
            _OpenFileDialog.CheckFileExists = true;
            _OpenFileDialog.CheckPathExists = true;
            _OpenFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)| *.bmp; *.jpg; *.jpeg; *.png";
            _OpenFileDialog.RestoreDirectory = true;

            if (_OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image _Logo = Image.FromFile(_OpenFileDialog.FileName);
                Main_Form_Settings_System_Information_Company_Logo_image.Image = _Logo;

                SQLiteConnection Connection = new SQLiteConnection(DB_Connection_String);
                if (Connection.State != System.Data.ConnectionState.Open)
                {
                    Connection.Open();
                }

                //ImageConverter _Image_Converter = new ImageConverter();
                //byte[] _Image_Bytes = (byte[])_Image_Converter.ConvertTo(_Logo, typeof(byte[]));
                //SQLiteCommand Command = new SQLiteCommand("Update Company_Information SET Substation_Image = @Substation_Image WHERE Customer_ID = @Customer_ID", Connection);
                //Command.Parameters.AddWithValue("Substation_Image", _Image_Bytes);
                //Command.Parameters.AddWithValue("Customer_ID", Main_Form_Settings_System_Information_Customer_ID_numericbox.Value);
                //Command.ExecuteNonQuery();

                ImageConverter _Image_Converter = new ImageConverter();
                byte[] _Image_Bytes = (byte[])_Image_Converter.ConvertTo(_Logo, typeof(byte[]));
                SQLiteCommand Command = new SQLiteCommand("Update Company_Information SET Company_Logo = @Company_Logo WHERE Customer_ID = @Customer_ID", Connection);
                Command.Parameters.AddWithValue("Company_Logo", _Image_Bytes);
                Command.Parameters.AddWithValue("Customer_ID", Main_Form_Settings_System_Information_Customer_ID_numericbox.Value);
                Command.ExecuteNonQuery();
            }
        }

        private void Main_Form_Statusbar_About_button_Click(object sender, EventArgs e)
        {
            AboutBox _About_Box = new AboutBox();
            _About_Box.ShowDialog();
        }
    }
}

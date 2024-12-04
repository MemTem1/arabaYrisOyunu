using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIZLI_AMA_SAKİN
{
    public partial class Form3 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DBGame.accdb");
        public Form3()
        {
            InitializeComponent();
        }
        
        DataSet ds;       
        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("Select * From oyuncular  ORDER BY score DESC", baglanti);
                ds = new DataSet();
                baglanti.Open();
                da.Fill(ds, "oyuncular");
                dataGridView1.DataSource = ds.Tables["oyuncular"];
                baglanti.Close();

            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
               
            }
            finally
            {
                baglanti.Close();
            }
        }
    }
}

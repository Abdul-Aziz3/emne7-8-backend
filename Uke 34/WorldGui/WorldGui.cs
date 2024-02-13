using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using WorldModelLibrary.models;
using WorldViewLibrary.Services;
using WorldViewLibrary.Services.Interfaces;

namespace WorldGui
{
    public partial class WorldGui : Form
    {
        private IWorldDataService _worldDataService;
        private static string s_projectDir =
                 Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));

        //private string _cityFile = 
        //private static string s_basePath = @$"{s_projectDir}\Files\";
        //private static string s_cityFile = $"{s_basePath}City.csv";
        //private static string s_countryFile = $"{s_basePath}Country.csv";
        //private static string s_countryLanguageFile = $"{s_basePath}CountryLanguage.csv";

        private readonly static string s_cityFile = $"Files\\City.csv";
        private readonly static string s_countryFile = $"Files\\Country.csv";
        private readonly static string s_countryLanguageFile = $"Files\\CountryLanguage.csv";

        private string _filter = string.Empty;
        public WorldGui()
        {
            InitializeComponent();

            _worldDataService = new WorldDataService(LogError);
            _worldDataService.LoadData(s_countryFile, s_cityFile, s_countryLanguageFile);
            rdbtnCountry.Checked = true;
        }
                
 
        private void LogError(string message)
        {
            listErrors.Items.Add(message);
        }

        private void RdbtnCountry_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnCountry.Checked)
            {
                txtFilter.Text = _filter;
                UpdateGrid();
            }
                
        }

        private void RdbtnCity_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }
        private void RdbtnLanguage_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {            
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            if (rdbtnLanguage.Checked)
            {
                dataGridView.DataSource = _worldDataService.GetCountryLanguageByCountryCode(txtFilter.Text).ToList();
                return;
            }

            if (rdbtnCity.Checked)
            {
                dataGridView.DataSource = _worldDataService.GetCitiesByCountryCode(txtFilter.Text).ToList();
                return;
            }

            if (rdbtnCountry.Checked)
            {
                dataGridView.DataSource = _worldDataService.GetCountriesByName(txtFilter.Text).ToList();
                return;
            }
        }
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (rdbtnCountry.Checked && e.ColumnIndex == 0 && e.RowIndex > -1)
            {               
                _filter = txtFilter.Text;
                var cntrCode = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                txtFilter.Text = cntrCode.ToString();
                rdbtnCity.Checked = true;             
            }
        }
    }
}
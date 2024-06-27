using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace APIApteka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            LoadMethods();
        }

        private void LoadMethods()
        {
            // Предположим, что у нас есть два метода для демонстрации
            MethodComboBox.Items.Add("method1");
            MethodComboBox.Items.Add("method2");
            MethodComboBox.SelectedIndex = 0;
        }

        private async void GetResponseButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedMethod = MethodComboBox.SelectedItem.ToString();
            string response = await GetApiResponse(selectedMethod);
            ShowResponseDialog(response);
        }

        private async Task<string> GetApiResponse(string method)
        {
            string apiUrl = $"https://champapi1.nntc.nnov.ru/{method}";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseBody);
                return jsonResponse.ToString();
            }
            catch (Exception ex)
            {
                return "Стандартный ответ: ошибка при получении данных";
            }
        }

        private void ShowResponseDialog(string response)
        {
            MessageBox.Show(response, "API Response", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

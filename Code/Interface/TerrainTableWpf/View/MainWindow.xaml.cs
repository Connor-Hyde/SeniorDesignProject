using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TerrainTableWpf.Extensions;
using TerrainTableWpf.Model;
using TerrainTableWpf.ViewModel;

namespace TerrainTableWpf.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var square = MainViewModel.TerrainGrid[i, j];
                    var rect = new Rectangle
                    {
                        Fill = Brushes.Green,//square.Fill,
                        Stroke = Brushes.Red,
                        StrokeThickness = 1
                    };
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    TerrainGrid.Children.Add(rect);
                }
            }

            IntToColor(0);
        }
        public void ButtonClick_Import(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = "c:\\";
            dialog.Filter = "txt files (*.txt)|*.txt";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                var fileStream = dialog.OpenFile();
                var fileContent = ReadFileContent(fileStream);
                DeserializeAndSetTerrainGrid(fileContent);
            }
        }

        public string ReadFileContent(Stream fileStream)
        {
            using (StreamReader reader = new StreamReader(fileStream))
            {
                return reader.ReadToEnd();
            }
        }

        public void DeserializeAndSetTerrainGrid(string fileContent)
        {
            var grid = JsonConvert.DeserializeObject<int[][]>(fileContent);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    MainViewModel.TerrainGrid[i, j].Height = grid[i][j];
                    IntToColor(grid[i][j]);
                    var cell = (Rectangle)TerrainGrid.Children
                        .Cast<UIElement>()
                        .First(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j);
                    cell.Fill = SetColor;
                }
            }
        }

        public void ButtonClick_Export(object sender, RoutedEventArgs e)
        {
            var serializedGrid = SerializeGrid();
            var saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = "c:\\";
            saveDialog.Filter = "txt files (*.txt)|*.txt";
            bool? result = saveDialog.ShowDialog();
            if (result == true && saveDialog.FileName != "")
            {
                File.WriteAllText(saveDialog.FileName, serializedGrid);
            }
        }

        public void ButtonClick_Run(object sender, RoutedEventArgs e)
        {
            string serializedGrid = SerializeGrid();
            SerialPort port = new SerialPort("COM4", 9600);
            ISerialPortWrapper portWrapper = new SerialPortWrapper(port);
            SendSerializedGridToSerialPort(serializedGrid, portWrapper);
        }
        public void SendSerializedGridToSerialPort(string serializedGrid, ISerialPortWrapper port)
        {
            if (port.IsOpen)
                port.Close();
            port.Open();
            port.Write(serializedGrid);
            port.Close();
        }

        private void TerrainSquare_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var cell = (Rectangle)e.Source;
            GetCellInformation(e);
            cell.Fill = SetColor;
        }

        public string SerializeGrid()
        {
            decimal[,] grid = new decimal[10, 10];
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    //grid[row, col] = (row + 1) * (col + 1);
                    grid[row, col] =
                        ((decimal)MainViewModel.TerrainGrid[row, col].Height / 100) * 3;
                }
            }
            return JsonConvert.SerializeObject(grid);
        }
        private void GetCellInformation(MouseButtonEventArgs e)
        {
            // Get the position of the mouse click relative to the Grid
            Point position = e.GetPosition(TerrainGrid);

            // Get the size of each cell in the Grid
            double cellWidth = TerrainGrid.ActualWidth / TerrainGrid.ColumnDefinitions.Count;
            double cellHeight = TerrainGrid.ActualHeight / TerrainGrid.RowDefinitions.Count;

            // Calculate the row and column index of the selected cell
            int rowIndex = (int)(position.Y / cellHeight);
            int columnIndex = (int)(position.X / cellWidth);
            MainViewModel.TerrainGrid[rowIndex, columnIndex].Height = SliderValue;
        }
        public void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            decimal inches = ((decimal)val / 100) * 3;
            IntToColor(val);
            string msg = String.Format("Current value: {0} ({1} inches)", val, inches);
            SliderValue = val;
            this.textBlock1.Text = msg;
        }
        public void IntToColor(int val)
        {
            double dVal = val / 100.0; // normalize value between 0 and 1

            byte r, g, b;


            if (dVal < 0.5) // green to yellow gradient
            {
                r = (byte)((dVal * 2) * 255);
                g = (byte)(255);
                b = 0;
            }
            else // yellow to red gradient
            {
                r = (byte)(255);
                g = (byte)((1 - (dVal - 0.5) * 2) * 255);
                b = 0;
            }


            Color color = Color.FromRgb(r, g, b);
            SetColor = new SolidColorBrush(color);
        }

        public SolidColorBrush SetColor { get; set; }
        public int SliderValue { get; set; }
        public MainViewModel MainViewModel { get; set; }
    }
}

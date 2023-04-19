using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TerrainTableWpf.Extensions;
using TerrainTableWpf.Model;
using TerrainTableWpf.View;

namespace TerrainTableWpf.Tests.View
{
    public class MainWindowTests
    {
        [Test]
        public void MainWindowConstructor_CreatesExpectedTerrainGrid()
        {
            //Arrange
            const int GridSize = 10;
            var showMonitor = new ManualResetEventSlim(false);
            var closeMonitor = new ManualResetEventSlim(false);

            //Act & Assert
            Thread th = new Thread(new ThreadStart(delegate
            {
                var mw = new MainWindow();
                showMonitor.Set();
                for (int i = 0; i < GridSize; i++)
                {
                    for (int j = 0; j < GridSize; j++)
                    {
                        var child = mw.TerrainGrid.Children[(i * GridSize) + j] as Rectangle;
                        Assert.NotNull(child, $"Expected a Rectangle at position ({i}, {j})");

                        Assert.AreEqual(i, Grid.GetRow(child), $"Expected row to be {i}");
                        Assert.AreEqual(j, Grid.GetColumn(child), $"Expected column to be {j}");

                        Assert.AreEqual(Brushes.Green, child.Fill, $"Expected Fill to be Green at position ({i}, {j})");
                        Assert.AreEqual(Brushes.Red, child.Stroke, $"Expected Stroke to be Red at position ({i}, {j})");
                        Assert.AreEqual(1, child.StrokeThickness, $"Expected StrokeThickness to be 1 at position ({i}, {j})");
                    }
                }
                closeMonitor.Wait();
            }));
            th.ApartmentState = ApartmentState.STA;
            th.Start();
            showMonitor.Wait();
            Task.Delay(1000).Wait();
        }

        [Test]
        public void ReadFileContent_ReturnsCorrectFileContent()
        {
            //Arrange
            var showMonitor = new ManualResetEventSlim(false);
            var closeMonitor = new ManualResetEventSlim(false);
            var result = "";
            var fileContent = "Sample content";

            //Act
            Thread th = new Thread(new ThreadStart(delegate
            {
                var mw = new MainWindow();
                using var fileStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
                result = mw.ReadFileContent(fileStream);
                showMonitor.Set();
                closeMonitor.Wait();
            }));
            th.ApartmentState = ApartmentState.STA;
            th.Start();
            showMonitor.Wait();
            Task.Delay(1000).Wait();
            closeMonitor.Set();

            //Assert
            Assert.AreEqual(fileContent, result);
        }

        [Test]
        public void DeserializeAndSetTerrainGrid_SetsTerrainGridHeightsCorrectly()
        {
            //Arrange
            var showMonitor = new ManualResetEventSlim(false);
            var closeMonitor = new ManualResetEventSlim(false);
            var fileContent = @"
    [
        [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
        [10, 11, 12, 13, 14, 15, 16, 17, 18, 19],
        [20, 21, 22, 23, 24, 25, 26, 27, 28, 29],
        [30, 31, 32, 33, 34, 35, 36, 37, 38, 39],
        [40, 41, 42, 43, 44, 45, 46, 47, 48, 49],
        [50, 51, 52, 53, 54, 55, 56, 57, 58, 59],
        [60, 61, 62, 63, 64, 65, 66, 67, 68, 69],
        [70, 71, 72, 73, 74, 75, 76, 77, 78, 79],
        [80, 81, 82, 83, 84, 85, 86, 87, 88, 89],
        [90, 91, 92, 93, 94, 95, 96, 97, 98, 99]
    ]";

            //Act and Assert
            Thread th = new Thread(new ThreadStart(delegate
            {
                var mw = new MainWindow();
                mw.DeserializeAndSetTerrainGrid(fileContent);
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Assert.AreEqual(i * 10 + j, mw.MainViewModel.TerrainGrid[i, j].Height);
                    }
                }
                showMonitor.Set();
                closeMonitor.Wait();
            }));
            th.ApartmentState = ApartmentState.STA;
            th.Start();
            showMonitor.Wait();
            Task.Delay(1000).Wait();
            closeMonitor.Set();
        }

        [Test]
        public void SendSerializedGridToSerialPort_WritesSerializedGridToSerialPort()
        {
            //Arrange
            var showMonitor = new ManualResetEventSlim(false);
            var closeMonitor = new ManualResetEventSlim(false);
            var serializedGrid = "serializedGridContent";
            var mockSerialPortWrapper = new Mock<ISerialPortWrapper>();
            mockSerialPortWrapper.Setup(x => x.Write(serializedGrid)).Verifiable();


            //Act
            Thread th = new Thread(new ThreadStart(delegate
            {
                var mw = new MainWindow();
                mw.SendSerializedGridToSerialPort(serializedGrid, mockSerialPortWrapper.Object);

                showMonitor.Set();
                closeMonitor.Wait();
            }));
            th.ApartmentState = ApartmentState.STA;
            th.Start();
            showMonitor.Wait();
            Task.Delay(1000).Wait();
            closeMonitor.Set();

            //Assert
            mockSerialPortWrapper.Verify();
        }

        [Test]
        public void SerializeGrid_SameHeightValues_ReturnsSerializedGrid()
        {
            //Arrange
            var showMonitor = new ManualResetEventSlim(false);
            var closeMonitor = new ManualResetEventSlim(false);
            var serializedGrid = "";
            decimal[,] expectedGrid = new decimal[10, 10];
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    expectedGrid[row, col] = 1.5m;
                }
            }

            //Act
            Thread th = new Thread(new ThreadStart(delegate
            {
                var mw = new MainWindow();
                for (int row = 0; row < 10; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        mw.MainViewModel.TerrainGrid[row, col].Height = 50;
                    }
                }
                serializedGrid = mw.SerializeGrid();
                showMonitor.Set();
                closeMonitor.Wait();
            }));
            th.ApartmentState = ApartmentState.STA;
            th.Start();
            showMonitor.Wait();
            Task.Delay(1000).Wait();
            closeMonitor.Set();

            //Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedGrid), serializedGrid);
        }

        [Test]
        public void SerializeGrid_DifferentHeightValues_ReturnsSerializedGrid()
        {
            //Arrange
            var showMonitor = new ManualResetEventSlim(false);
            var closeMonitor = new ManualResetEventSlim(false);
            var serializedGrid = "";
            decimal[,] expectedGrid = new decimal[10, 10];
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    expectedGrid[row, col] = ((decimal)(row * 10 + col) / 100) * 3;
                }
            }

            //Act
            Thread th = new Thread(new ThreadStart(delegate
            {
                var mw = new MainWindow();
                for (int row = 0; row < 10; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        mw.MainViewModel.TerrainGrid[row, col].Height = row * 10 + col;
                    }
                }
                serializedGrid = mw.SerializeGrid();
                showMonitor.Set();
                closeMonitor.Wait();
            }));
            th.ApartmentState = ApartmentState.STA;
            th.Start();
            showMonitor.Wait();
            Task.Delay(1000).Wait();
            closeMonitor.Set();

            //Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedGrid), serializedGrid);
        }

        [Test]
        public void IntToColor_ValueZero_SetsGreenColor()
        {
            //Arrange
            var showMonitor = new ManualResetEventSlim(false);
            var closeMonitor = new ManualResetEventSlim(false);

            //Act and Assert
            Thread th = new Thread(new ThreadStart(delegate
            {
                var mw = new MainWindow();
                mw.IntToColor(0);
                SolidColorBrush expectedBrush = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                Assert.AreEqual(expectedBrush.Color, mw.SetColor.Color);
                showMonitor.Set();
                closeMonitor.Wait();
            }));
            th.ApartmentState = ApartmentState.STA;
            th.Start();
            showMonitor.Wait();
            Task.Delay(1000).Wait();
            closeMonitor.Set();
        }

        [Test]
        public void IntToColor_ValueOneHundred_SetsRedColor()
        {
            //Arrange
            var showMonitor = new ManualResetEventSlim(false);
            var closeMonitor = new ManualResetEventSlim(false);

            //Act and Assert
            Thread th = new Thread(new ThreadStart(delegate
            {
                var mw = new MainWindow();
                mw.IntToColor(100);
                SolidColorBrush expectedBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                Assert.AreEqual(expectedBrush.Color, mw.SetColor.Color);
                showMonitor.Set();
                closeMonitor.Wait();
            }));
            th.ApartmentState = ApartmentState.STA;
            th.Start();
            showMonitor.Wait();
            Task.Delay(1000).Wait();
            closeMonitor.Set();
        }

        [Test]
        public void IntToColor_ValueFifty_SetsYellowColor()
        {
            //Arrange
            var showMonitor = new ManualResetEventSlim(false);
            var closeMonitor = new ManualResetEventSlim(false);

            //Act and Assert
            Thread th = new Thread(new ThreadStart(delegate
            {
                var mw = new MainWindow();
                mw.IntToColor(50);
                SolidColorBrush expectedBrush = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                Assert.AreEqual(expectedBrush.Color, mw.SetColor.Color);
                showMonitor.Set();
                closeMonitor.Wait();
            }));
            th.ApartmentState = ApartmentState.STA;
            th.Start();
            showMonitor.Wait();
            Task.Delay(1000).Wait();
            closeMonitor.Set();
        }

        [Test]
        public void Slider2_ValueChanged_ValueChanged_SetsSliderValueAndTextBlock1Text()
        {
            //Arrange
            int newValue = 25;
            var eventArgs = new RoutedPropertyChangedEventArgs<double>(0, newValue);
            var showMonitor = new ManualResetEventSlim(false);
            var closeMonitor = new ManualResetEventSlim(false);

            //Act and Assert
            Thread th = new Thread(new ThreadStart(delegate
            {
                var mw = new MainWindow();
                mw.slider2_ValueChanged(null, eventArgs);
                Assert.AreEqual(newValue, mw.SliderValue);
                decimal inches = ((decimal)newValue / 100) * 3;
                string expectedText = $"Current value: {newValue} ({inches} inches)";
                Assert.AreEqual(expectedText, mw.textBlock1.Text);
                showMonitor.Set();
                closeMonitor.Wait();
            }));
            th.ApartmentState = ApartmentState.STA;
            th.Start();
            showMonitor.Wait();
            Task.Delay(1000).Wait();
            closeMonitor.Set();
        }
    }
}

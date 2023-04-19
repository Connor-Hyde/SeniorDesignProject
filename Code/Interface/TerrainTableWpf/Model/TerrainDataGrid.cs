using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TerrainTableWpf.Model
{
    public class TerrainDataGrid : DataGrid
    {
        public TerrainDataGrid()
        {
            // Create the grid
            TerrainGrid = new TerrainSquare[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    TerrainGrid[i, j] = new TerrainSquare();
                }
            }

            // Set the item source of the grid
            ItemsSource = TerrainGrid;
        }

        private TerrainSquare[,] TerrainGrid;

        private void TerrainGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Get the DataGridCell that was clicked
            DataGridCell cell = (DataGridCell) e.Source;

            // Get the corresponding TerrainSquare object
            TerrainSquare square = (TerrainSquare) cell.DataContext;

            // Set the color of the square to the selected color
            square.Fill = new SolidColorBrush(Colors.CadetBlue);
        }
    }

//{
    //    public TerrainDataGrid()
    //    {
    //        // Create a 2D array to represent the grid
    //        var grid = new object[10, 10];
    //
    //        // Initialize each square in the grid with a SolidColorBrush to represent the background color
    //        for (int row = 0; row < 10; row++)
    //        {
    //            for (int column = 0; column < 10; column++)
    //            {
    //                grid[row, column] = new SolidColorBrush(Colors.Black);
    //            }
    //        }
    //
    //        // Create an ItemsControl to display each row of the grid
    //        var rowItemsControl = new ItemsControl()
    //        {
    //            ItemsSource = Enumerable.Range(0, 10)
    //        };
    //
    //        // Define a DataTemplate to display each row of the grid
    //        var rowTemplate = new DataTemplate(typeof(object[]));
    //        var columnItemsControlFactory = new FrameworkElementFactory(typeof(ItemsControl));
    //        columnItemsControlFactory.SetBinding(ItemsControl.ItemsSourceProperty, new Binding());
    //        var columnTemplate = new DataTemplate(typeof(SolidColorBrush));
    //        var rectangleFactory = new FrameworkElementFactory(typeof(Rectangle));
    //        rectangleFactory.SetValue(Rectangle.FillProperty, new Binding());
    //        columnTemplate.VisualTree = rectangleFactory;
    //        columnItemsControlFactory.SetValue(ItemsControl.ItemTemplateProperty, columnTemplate);
    //        rowTemplate.VisualTree = columnItemsControlFactory;
    //        rowItemsControl.ItemTemplate = rowTemplate;
    //
    //        // Set the ItemsSource property of the TerrainDataGrid to the row ItemsControl
    //        ItemsSource = new[] { rowItemsControl };
    //
    //        // Bind the row ItemsControl to the grid array
    //        //rowItemsControl.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = grid });
    //    }
    //}
}

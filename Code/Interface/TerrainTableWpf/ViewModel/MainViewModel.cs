using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TerrainTableWpf.Model;
using TerrainTableWpf.View;

namespace TerrainTableWpf.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            ColorSlider = new ColorSlider();
            TerrainGrid = new TerrainSquare[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    TerrainGrid[i, j] = new TerrainSquare { Fill = Brushes.Black };
                }
            }
        }
        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    OnPropertyChanged(nameof(SelectedColor));
                }
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TerrainSquare[,] TerrainGrid { get; set; }

        public int Value;
        public ColorSlider ColorSlider;

        public event PropertyChangedEventHandler? PropertyChanged;
    }

}
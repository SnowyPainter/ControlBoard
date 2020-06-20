using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Controls_Board
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            currPalette = Palette1;
            var border = VisualTreeHelper.GetParent(currPalette) as Border;
            if (border != null)
                setBorderSelect(border);

        }

        private Border currSelected;
        private Grid currPalette;

        private void OpenColorPicker_Clicked(object sender, RoutedEventArgs e)
        {
            ColorPickerWindow colorPicker = new ColorPickerWindow();
            colorPicker.ShowDialog();

            if (colorPicker.Rgb != null)
            {
                currPalette.Background = new SolidColorBrush(colorPicker.Rgb.Color());
            }
        }
        private void setBorderSelect(Border b)
        {
            if (currSelected != null)
            {
                currSelected.BorderThickness = new Thickness(1);
                currSelected.BorderBrush = Brushes.Black;
            }
            currSelected = b;
            b.BorderThickness = new Thickness(3f);
            b.BorderBrush = Brushes.BurlyWood;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.D1)
            {
                var border = VisualTreeHelper.GetParent(Palette1) as Border;
                currPalette = Palette1;
                if(border != null)
                    setBorderSelect(border);
            }
            else if (e.Key == Key.D2)
            {
                var border = VisualTreeHelper.GetParent(Palette2) as Border;
                currPalette = Palette2;
                if (border != null)
                    setBorderSelect(border);

            }
            else if (e.Key == Key.D3)
            {
                var border = VisualTreeHelper.GetParent(Palette3) as Border;
                currPalette = Palette3;
                if (border != null)
                    setBorderSelect(border);
            }
        }

        // ******************
        // Window interaction
        // ******************

        private WindowState currentState
        {
            get
            {
                return WindowState;
            }
        }

        private double currWidth, currHeight;

        private void OpenPropertyButton_Click(object sender, RoutedEventArgs e)
        {
            //Open Property Window
        }

        private void SetWindowState(WindowState state)
        {
            if(state == WindowState.Maximized && currentState == WindowState.Maximized)
            {
                this.Width = currWidth;
                this.Height = currHeight;
                WindowState = WindowState.Normal;
                return;
            }
            else if(state == WindowState.Maximized && currentState != WindowState.Maximized)
            {
                currWidth = Width;
                currHeight = Height;
            }
            WindowState = state;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            SetWindowState(WindowState.Minimized);
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            SetWindowState(WindowState.Maximized);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Controls_Board
{
    /// <summary>
    /// ColorPickerWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ColorPickerWindow : Window
    {
        public RGB Rgb = null;
        public ColorPickerWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            Rgb = ColorPicker.Selected;
            this.Close();
        }
    }
}

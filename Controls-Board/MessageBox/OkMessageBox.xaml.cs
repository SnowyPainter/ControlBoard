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

namespace Controls_Board.MessageBox
{
    /// <summary>
    /// OkMessageBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OkMessageBox : Window
    {
        bool ok = false;
        public OkMessageBox(string description)
        {
            InitializeComponent();
            Descript.Text = description;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ok = false;
            this.Close();
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ok = true;
            this.Close();
        }

        public bool Ok()
        {
            return ok;
        }
        
    }
}

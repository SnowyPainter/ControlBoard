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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls_Board
{
    /// <summary>
    /// ControlItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ControlItem : UserControl
    {
        public new Brush Background {
            get { 
                return Grid.Background; 
            }
            set {
                Grid.Background = value; 
            }
        }
        public string ControlName
        {
            get { return NameTitle.Text; }
            set { NameTitle.Text = value; }
        }

        public ImageSource ControlIcon
        {
            get { return IconImage.Source; }
            set { IconImage.Source = value; }
        }

        public string Description { get { return Grid.ToolTip.ToString(); } set { Grid.ToolTip = value; } }

        public ControlItem()
        {
            InitializeComponent();
        }
    }
}

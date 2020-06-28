using Controls_Board.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private void InitPalettes()
        {
            //Color init
            Palette1.Background = Brushes.Black;
            selectedPalette = Palette1;
            PenColor.Background = selectedPalette.Background;
            setBorderSelect(VisualTreeHelper.GetParent(selectedPalette) as Border);
        }
        private void InitTools()
        {
            DrawTools.Children.OfType<RadioButton>().ToArray()[0].IsChecked = true;
            selectedTool = Drawing.DrawTool.Pen;

            capturer = new Drawing.Capturer();
            pen = new Drawing.Pen(DrawCanvas);
        }
        public MainWindow()
        {
            InitializeComponent();

            InitPalettes();
            InitTools();
        }
        //*******************
        //Canvas
        //*******************

        Drawing.DrawTool selectedTool;
        private Border selectedPaletteBorder { get; set; }
        private Brush currPenColor
        {
            get { return selectedPalette.Background; }
        }
        private Grid selectedPalette { get; set; } //Auto set every key down occuring
        
        private Drawing.Capturer capturer { get; set; }
        private Drawing.Pen pen { get; set; }

        private void DrawTools_Selected(object sender, RoutedEventArgs e)
        {
            var radioBtn = (RadioButton)sender;
            if (radioBtn == null) {
                selectedTool = Drawing.DrawTool.None;
                return;
            }
            
            switch(radioBtn.Tag)
            {
                case "Pen":
                    selectedTool = Drawing.DrawTool.Pen;
                    break;
                case "Eraser":
                    selectedTool = Drawing.DrawTool.Eraser;
                    break;
                case "Fill":
                    selectedTool = Drawing.DrawTool.Fill;
                    break;
                default:
                    selectedTool = Drawing.DrawTool.None; //Something Bug
                    MessageBox.Show("Something must not be happen happened");
                    break;
            }
        }

        private void DrawCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!pen.IsUp)
                return;

            pen.Stroke = selectedTool == Drawing.DrawTool.Eraser ? Brushes.White : currPenColor;
            pen.StrokeThickness = selectedTool == Drawing.DrawTool.Eraser ? 5f : 3f;
            pen.Down();
        }
        private void DrawCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Add Command
            if (pen.IsUp)
                return;

            capturer.Add(pen.Up());
        }
        private void DrawCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (pen.IsUp && e.LeftButton == MouseButtonState.Pressed)
                pen.Down();
        }
        private void DrawCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!pen.IsUp && e.LeftButton == MouseButtonState.Pressed)
                capturer.Add(pen.Up());
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                pen.Move(e);
            }
        }
        private void OpenColorPicker_Clicked(object sender, RoutedEventArgs e)
        {
            ColorPickerWindow colorPicker = new ColorPickerWindow();
            colorPicker.ShowDialog();

            if (colorPicker.Rgb != null)
            {
                PenColor.Background = selectedPalette.Background = new SolidColorBrush(colorPicker.Rgb.Color());
            }
        }
        private void setBorderSelect(Border b)
        {
            if (selectedPaletteBorder != null)
            {
                selectedPaletteBorder.BorderThickness = new Thickness(1);
                selectedPaletteBorder.BorderBrush = Brushes.Black;
            }
            selectedPaletteBorder = b;
            b.BorderThickness = new Thickness(3f);
            b.BorderBrush = Brushes.BurlyWood;
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
        //ShortCut Keys binding -> proccess
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.R) //Debuging ------- Revers all IDrawables
            {
                DrawCanvas.Children.Clear(); //Temp clearing

                foreach (var d in capturer.Draws)
                {
                    d.Draw();
                }
            }
            if (e.Key == Key.D1)
            {
                var border = VisualTreeHelper.GetParent(Palette1) as Border;
                selectedPalette = Palette1;
                if (border != null)
                    setBorderSelect(border);
            }
            else if (e.Key == Key.D2)
            {
                var border = VisualTreeHelper.GetParent(Palette2) as Border;
                selectedPalette = Palette2;
                if (border != null)
                    setBorderSelect(border);

            }
            else if (e.Key == Key.D3)
            {
                var border = VisualTreeHelper.GetParent(Palette3) as Border;
                selectedPalette = Palette3;
                if (border != null)
                    setBorderSelect(border);
            }
            if ((int)e.Key >= (int)Key.D1 && (int)e.Key <= (int)Key.D3)
            {
                PenColor.Background = selectedPalette.Background;
            }
        }

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
        private void SaveCanvasBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var png = DrawCanvas.ToBitmap().ToPng();

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
            saveFileDialog.Filter = "Png File (*.png) | *.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream fileStream = File.Create(saveFileDialog.FileName))
                {
                    png.Save(fileStream);
                }
            }
        }

        private void OpenCanvasBtn_Clicked(object sender, RoutedEventArgs e)
        {
            //Open jpg(picture file) or own extended file( ex) .cb)
        }

        private void RecordCanvasBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if(currentState != WindowState.Maximized)
            {
                var result = MessageBox.Show("The result of recording will be low quality. Is it ok?","Wrong",MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                    return;
            }


        }

        private void CaptureCanvasBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var png = DrawCanvas.ToBitmap().ToPng();

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
            saveFileDialog.Filter = "Png File (*.png) | *.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream fileStream = File.Create(saveFileDialog.FileName))
                {
                    png.Save(fileStream);
                }
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

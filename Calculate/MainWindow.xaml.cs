using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using CalculateLib.Operands;

namespace CalculateWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Bouton opération

        private void Button_Click_Addition(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '+';
        }

        private void Button_Click_Substract(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '-';
        }

        private void Button_Click_Multiply(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '*';
        }

        private void Button_Click_Divide(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '/';
        }

        private void Button_Click_Equal(object sender, RoutedEventArgs e)
        {
            OperandBase operand = OperandFactory.Create(textInput.Text);
            textInput.Text = operand.Calculate().ToString();
        }

        // Bouton action
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text.Remove(textInput.Text.Length - 1);
        }

        private void Button_Click_DeleteAll(object sender, RoutedEventArgs e)
        {
            textInput.Text = "";
        }

        // Bouton numérique
        private void Button_Click_Point(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + ',';
        }

        private void Button_Click_N_0(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '0';
        }

        private void Button_Click_N_1(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '1';
        }

        private void Button_Click_N_2(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '2';
        }

        private void Button_Click_N_3(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '3';
        }

        private void Button_Click_N_4(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '4';
        }

        private void Button_Click_N_5(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '5';
        }

        private void Button_Click_N_6(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '6';
        }

        private void Button_Click_N_7(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '7';
        }

        private void Button_Click_N_8(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '8';
        }

        private void Button_Click_N_9(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '9';
        }
        private void Button_Click_LeftParenthesis(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + '(';
        }
        private void Button_Click_RightParenthesis(object sender, RoutedEventArgs e)
        {
            textInput.Text = textInput.Text + ')';
        }
    }
}
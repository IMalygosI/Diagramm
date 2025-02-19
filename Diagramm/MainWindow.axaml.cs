using Avalonia.Controls;
using System;

namespace Diagramm
{
    public partial class MainWindow : Window
    {
        private static Diagramma _diagram = new Diagramma(0.95, 0.25, 0.015, 0.001, 70, 50, 70, 45, 70, 40);

        public MainWindow()
        {
            InitializeComponent();
            DiagrammName.DataContext = _diagram;
        }

        // Обработчик события для TextBox
        private void TextBox_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            try
            {
                Diagramma diagram = new Diagramma(
                    Convert.ToDouble(eEdInGod.Text), // ε
                    Convert.ToDouble(vEdGod.Text), // β
                    Convert.ToDouble(aEdGod.Text), // α
                    Convert.ToDouble(bEdGod.Text), // δ
                    Convert.ToDouble(CountShert1.Text), // Начальное количество жертв (цикл 1)
                    Convert.ToDouble(CountHishn1.Text), // Начальное количество хищников (цикл 1)
                    Convert.ToDouble(CountShert2.Text), // Начальное количество жертв (цикл 2)
                    Convert.ToDouble(CountHishn2.Text), // Начальное количество хищников (цикл 2)
                    Convert.ToDouble(CountShert3.Text), // Начальное количество жертв (цикл 3)
                    Convert.ToDouble(CountHishn3.Text) // Начальное количество хищников (цикл 3)
                );
                DiagrammName.DataContext = diagram;
            }
            catch {  }
        }
    }
}
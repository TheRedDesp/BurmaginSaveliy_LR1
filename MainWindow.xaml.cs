using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CalculatorWPF
{
    public partial class MainWindow : Window
    {
        private string currentInput = "0";
        private string previousInput = "";
        private string currentOperation = "";
        private bool isNewInput = true;
        private double memoryValue = 0;
        private string currentTheme = "Light"; 

        public MainWindow()
        {
            InitializeComponent();
            ApplyTheme("Light"); 
        }

        private void ApplyTheme(string theme)
        {
            var resources = Application.Current.Resources;
            currentTheme = theme;

            switch (theme)
            {
                case "Light":
                    this.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));

                    DisplayBorder.Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
                    DisplayBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(224, 224, 224));

                    ExpressionTextBox.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));
                    ResultTextBox.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

                    ApplyButtonStyles("Light");
                    break;

                case "Dark":
                    this.Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));

                    DisplayBorder.Background = new SolidColorBrush(Color.FromRgb(45, 45, 45));
                    DisplayBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(85, 85, 85));

                    ExpressionTextBox.Foreground = new SolidColorBrush(Color.FromRgb(170, 170, 170));
                    ResultTextBox.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

                    ApplyButtonStyles("Dark");
                    break;

                case "Blue":
                    this.Background = new SolidColorBrush(Color.FromRgb(26, 35, 126));

                    DisplayBorder.Background = new SolidColorBrush(Color.FromRgb(18, 24, 88));
                    DisplayBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(57, 73, 171));

                    ExpressionTextBox.Foreground = new SolidColorBrush(Color.FromRgb(200, 200, 255));
                    ResultTextBox.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

                    ApplyButtonStyles("Blue");
                    break;
            }
        }

        private void ApplyButtonStyles(string theme)
        {
            var buttons = FindVisualChildren<Button>(this);

            foreach (var button in buttons)
            {
                if (button.Name == "LightThemeBtn" || button.Name == "DarkThemeBtn" || button.Name == "BlueThemeBtn")
                    continue;

                string content = button.Content?.ToString() ?? "";

                if (IsOperator(content))
                {
                    ApplyOperatorButtonStyle(button, theme);
                }
                else if (IsFunction(content))
                {
                    ApplyFunctionButtonStyle(button, theme);
                }
                else if (content == "=")
                {
                    ApplyEqualsButtonStyle(button, theme);
                }
                else
                {
                    ApplyNumberButtonStyle(button, theme);
                }
            }
        }

        private bool IsOperator(string content)
        {
            return content == "+" || content == "-" || content == "*" || content == "/" ||
                   content == "x^y" || content == "mod" || content == "^";
        }

        private bool IsFunction(string content)
        {
            return content == "C" || content == "CE" || content == "⌫" || content == "%" ||
                   content == "sin" || content == "cos" || content == "tan" ||
                   content == "asin" || content == "acos" || content == "atan" ||
                   content == "log" || content == "ln" || content == "√" ||
                   content == "10^x" || content == "e^x" || content == "x^2" ||
                   content == "|x|" || content == "x!" || content == "1/x";
        }

        private void ApplyNumberButtonStyle(Button button, string theme)
        {
            switch (theme)
            {
                case "Light":
                    button.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                    button.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    break;
                case "Dark":
                    button.Background = new SolidColorBrush(Color.FromRgb(62, 62, 62));
                    button.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    break;
                case "Blue":
                    button.Background = new SolidColorBrush(Color.FromRgb(40, 53, 147));
                    button.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    break;
            }
            button.FontSize = 18;
            button.FontWeight = FontWeights.Bold;
            button.Margin = new Thickness(5);
        }

        private void ApplyOperatorButtonStyle(Button button, string theme)
        {
            switch (theme)
            {
                case "Light":
                    button.Background = new SolidColorBrush(Color.FromRgb(255, 152, 0));
                    break;
                case "Dark":
                    button.Background = new SolidColorBrush(Color.FromRgb(245, 124, 0));
                    break;
                case "Blue":
                    button.Background = new SolidColorBrush(Color.FromRgb(255, 111, 0));
                    break;
            }
            button.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            button.FontSize = 18;
            button.FontWeight = FontWeights.Bold;
            button.Margin = new Thickness(5);
        }

        private void ApplyFunctionButtonStyle(Button button, string theme)
        {
            switch (theme)
            {
                case "Light":
                    button.Background = new SolidColorBrush(Color.FromRgb(96, 125, 139));
                    break;
                case "Dark":
                    button.Background = new SolidColorBrush(Color.FromRgb(69, 90, 100));
                    break;
                case "Blue":
                    button.Background = new SolidColorBrush(Color.FromRgb(0, 105, 92));
                    break;
            }
            button.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            button.FontSize = 14;
            button.FontWeight = FontWeights.Bold;
            button.Margin = new Thickness(5);
        }

        private void ApplyEqualsButtonStyle(Button button, string theme)
        {
            switch (theme)
            {
                case "Light":
                    button.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                    break;
                case "Dark":
                    button.Background = new SolidColorBrush(Color.FromRgb(56, 142, 60));
                    break;
                case "Blue":
                    button.Background = new SolidColorBrush(Color.FromRgb(46, 125, 50));
                    break;
            }
            button.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            button.FontSize = 18;
            button.FontWeight = FontWeights.Bold;
            button.Margin = new Thickness(5);
        }

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string theme = button.Tag.ToString();
            ApplyTheme(theme);
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string number = button.Content.ToString();

            if (isNewInput)
            {
                currentInput = number;
                isNewInput = false;
            }
            else
            {
                if (currentInput == "0" && number != ".")
                    currentInput = number;
                else
                    currentInput += number;
            }

            UpdateDisplay();
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!currentInput.Contains("."))
            {
                if (isNewInput)
                {
                    currentInput = "0.";
                    isNewInput = false;
                }
                else
                {
                    currentInput += ".";
                }
                UpdateDisplay();
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string operation = button.Content.ToString();

            if (!string.IsNullOrEmpty(currentOperation) && !isNewInput)
            {
                Calculate();
            }

            previousInput = currentInput;
            currentOperation = operation;
            isNewInput = true;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentOperation))
            {
                Calculate();
                currentOperation = "";
                isNewInput = true;
            }
        }

        private void Calculate()
        {
            try
            {
                double prev = Convert.ToDouble(previousInput);
                double current = Convert.ToDouble(currentInput);
                double result = 0;

                switch (currentOperation)
                {
                    case "+":
                        result = prev + current;
                        break;
                    case "-":
                        result = prev - current;
                        break;
                    case "*":
                        result = prev * current;
                        break;
                    case "/":
                        if (current != 0)
                            result = prev / current;
                        else
                            throw new DivideByZeroException();
                        break;
                    case "^":
                    case "x^y":
                        result = Math.Pow(prev, current);
                        break;
                    case "mod":
                        result = prev % current;
                        break;
                }

                currentInput = result.ToString();
                UpdateDisplay();
                previousInput = "";
            }
            catch (DivideByZeroException)
            {
                currentInput = "Ошибка: деление на 0";
                UpdateDisplay();
            }
            catch (Exception)
            {
                currentInput = "Ошибка";
                UpdateDisplay();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "0";
            previousInput = "";
            currentOperation = "";
            isNewInput = true;
            UpdateDisplay();
        }

        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "0";
            isNewInput = true;
            UpdateDisplay();
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (currentInput.Length > 1 && currentInput != "0")
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                if (currentInput == "-")
                    currentInput = "0";
            }
            else
            {
                currentInput = "0";
                isNewInput = true;
            }
            UpdateDisplay();
        }

        private void Function_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string function = button.Content.ToString();

            try
            {
                double value = Convert.ToDouble(currentInput);
                double result = 0;

                switch (function)
                {
                    case "sin":
                        result = Math.Sin(value * Math.PI / 180);
                        break;
                    case "cos":
                        result = Math.Cos(value * Math.PI / 180);
                        break;
                    case "tan":
                        result = Math.Tan(value * Math.PI / 180);
                        break;
                    case "asin":
                        result = Math.Asin(value) * 180 / Math.PI;
                        break;
                    case "acos":
                        result = Math.Acos(value) * 180 / Math.PI;
                        break;
                    case "atan":
                        result = Math.Atan(value) * 180 / Math.PI;
                        break;
                    case "log":
                        result = Math.Log10(value);
                        break;
                    case "ln":
                        result = Math.Log(value);
                        break;
                    case "√":
                        result = Math.Sqrt(value);
                        break;
                    case "10^x":
                        result = Math.Pow(10, value);
                        break;
                    case "e^x":
                        result = Math.Exp(value);
                        break;
                    case "x^2":
                        result = Math.Pow(value, 2);
                        break;
                    case "|x|":
                        result = Math.Abs(value);
                        break;
                    case "x!":
                        result = Factorial((int)value);
                        break;
                    case "1/x":
                        result = 1 / value;
                        break;
                }

                currentInput = result.ToString();
                isNewInput = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Ошибка";
                UpdateDisplay();
            }
        }

        private double Factorial(int n)
        {
            if (n < 0) throw new ArgumentException();
            if (n == 0 || n == 1) return 1;
            double result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;
            return result;
        }

        private void Constant_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string constant = button.Content.ToString();

            if (constant == "π")
                currentInput = Math.PI.ToString();
            else if (constant == "e")
                currentInput = Math.E.ToString();

            isNewInput = true;
            UpdateDisplay();
        }

        private void Parenthesis_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            currentInput += button.Content.ToString();
            UpdateDisplay();
        }

        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = Convert.ToDouble(currentInput);
                currentInput = (value / 100).ToString();
                isNewInput = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Ошибка";
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            ResultTextBox.Text = currentInput;

            if (!string.IsNullOrEmpty(previousInput) && !string.IsNullOrEmpty(currentOperation))
                ExpressionTextBox.Text = $"{previousInput} {currentOperation}";
            else
                ExpressionTextBox.Text = "";
        }

        private void ModeToggle_Checked(object sender, RoutedEventArgs e)
        {
            StandardModeGrid.Visibility = Visibility.Collapsed;
            EngineeringModeGrid.Visibility = Visibility.Visible; 
            this.Height = 700;
            this.Width = 700;

            ApplyTheme(currentTheme);
        }

        private void ModeToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            StandardModeGrid.Visibility = Visibility.Visible;
            EngineeringModeGrid.Visibility = Visibility.Collapsed;
            this.Height = 550;
            this.Width = 450;

            ApplyTheme(currentTheme);
        }
    }
}
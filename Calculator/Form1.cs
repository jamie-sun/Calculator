using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        String operation = ""; //current state of math operation pressed
        Double value = 0; //stores values with decimal places
        Boolean resetdisplay = false;
        Boolean operationPressed = false;

        public Form1()
        {
            InitializeComponent();
        }

        //adding of button presses onto main screen
        private void Button_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            switch (b.Name)
            {
                case "buttonClearEntry":
                    textMain.Text = "0";
                    break;
                case "buttonClear":
                    textMain.Text = "0";
                    textEquation.Text = "";
                    operation = "";
                    break;
                case "buttonDelete":
                    if (textMain.Text.Length != 1)
                    {
                        textMain.Text = textMain.Text.Substring(0, textMain.Text.Length - 1);
                    }
                    else
                    {
                        textMain.Text = "0";
                    }
                    break;
                case "buttonPosNeg":
                    if (textMain.Text != "0")
                    {
                        if (textMain.Text.Substring(0, 1) == "-")
                        {
                            textMain.Text = textMain.Text.Substring(1, textMain.Text.Length - 1);
                        }
                        else
                        {
                            textMain.Text = "-" + textMain.Text;
                        }
                    }
                    break;
                case "buttonDot":
                    if (!operationPressed)
                    {
                        if (!textMain.Text.Contains("."))
                        {
                            textMain.Text += ".";
                        }
                    }
                    else
                    {
                        textMain.Text = "0.";
                        resetdisplay = false;
                    }
                    break;
                default:
                    //normal number button is entered into screen on default
                    if ((textMain.Text == "0") || (resetdisplay)) 
                    {
                        textMain.Text = "";
                        resetdisplay = false;
                    }
                    operationPressed = false;
                    textMain.Text += b.Text;
                    break;
            }

        }

        //main screen values and operations pressed 
        //are placed onto secondary screen
        private void Button_OperatorClick(object sender, EventArgs e)
        {
            Button b = sender as Button;
            resetdisplay = true;

            if (!operationPressed)
            {
                if (operation == "") //first part of equation before operations pressed
                {
                    operation = b.Text;
                    value = Convert.ToDouble(textMain.Text);
                    textEquation.Text += textMain.Text + operation;
                }

                else //second part of equation after operations pressed
                {
                    textEquation.Text += textMain.Text + b.Text;
                    solve();
                    operation = b.Text;
                }
                operationPressed = true;
            }
        }

        //function to solve math equations
        private void solve()
        {
            switch (operation)
            {
                case "÷":
                    value = value / Convert.ToDouble(textMain.Text);
                    textMain.Text = value.ToString();
                    break;
                case "×":
                    value = value * Convert.ToDouble(textMain.Text);
                    textMain.Text = value.ToString();
                    break;
                case "-":
                    value = value - Convert.ToDouble(textMain.Text);
                    textMain.Text = value.ToString();
                    break;
                case "+":
                    value = value + Convert.ToDouble(textMain.Text);
                    textMain.Text = value.ToString();
                    break;
                default:
                    break;
            }
        }

        private void Button_EqualsClick(object sender, EventArgs e)
        {
            solve();
            resetdisplay = true;
            operationPressed = false;
            value = Convert.ToDouble(textMain.Text);
            value = 0;
            operation = "";
            textEquation.Text = "";
        }
    }
}
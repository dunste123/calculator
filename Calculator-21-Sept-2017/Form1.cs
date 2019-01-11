using System;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Calculator_21_Sept_2017 {
    public partial class frmMainDSte : Form {

        //Fun ideas
        //Random input
        //btns 0-9 DONE ✅
        //timer for stepper DONE ✅
        //save log file(textbox or text file)

        //Create a variable that holds the last textbox
        TextBox lastActiveInput = null;
        //Create a variable
        int stepperStepDSte = 0;

        public frmMainDSte() {
            InitializeComponent();
            lblMsgDSte.Text = "";
            this.Text = "Calculator";
            menuItemUseFullToolsDSte.Text = "Other Tools";
            mstripGenericDSte.RenderMode = ToolStripRenderMode.System;
            lastActiveInput = txbInput1DSte;
        }

        private void frmMainDSte_Load(object sender, EventArgs e) {
            //This will loop 10 times
            for (int i = 0; i < 10; i++) {
                //Create a new button
                Button button = new Button();
                //Set the top so that the buttons won't be stacked
                button.Top = button.Top + (i * button.Size.Height + 20);
                //Little bit on the left so that the buttons won't be pushed to the side
                button.Left = 5;
                //Show the number in the button
                button.Text = i + "";
                //Add an event to the button
                button.Click += NumBtnClickedDSte;
                //Set the color of the button
                button.UseVisualStyleBackColor = true;
                //Add the button to the numbers box
                gbNumDSte.Controls.Add(button);
            }
        }

        private void btnClearInputs_Click(object sender, EventArgs e) {
            txbInput1DSte.Text = "";
            txbInput2DSte.Text = "";
            txbOutputDSte.Text = "";
            progbarStepDSte.Value = 0;
            stepperStepDSte = 0;
        }

        private void NumBtnClickedDSte(object sender, EventArgs e) {
            //Get the clicked number
            Button clickedButton = (Button)sender;
            //Show the text in the message (for now)
            RenderMessageDSte("Number " + clickedButton.Text);
            //Add the number to the input
            lastActiveInput.Text += clickedButton.Text;
        }

        private void btnPlusDSte_Click(object sender, EventArgs e) {
            CalculateItDSte(txbInput1DSte.Text, "+", txbInput2DSte.Text);
        }

        private void btnMinusDSte_Click(object sender, EventArgs e) {
            CalculateItDSte(txbInput1DSte.Text, "-", txbInput2DSte.Text);
        }

        private void btnDevideDSte_Click(object sender, EventArgs e) {
            CalculateItDSte(txbInput1DSte.Text, "/", txbInput2DSte.Text);
        }

        private void btnMultiplyDSte_Click(object sender, EventArgs e) {
            CalculateItDSte(txbInput1DSte.Text, "*", txbInput2DSte.Text);
        }

        private void btnSqrtDste_Click(object sender, EventArgs e) {
            CalculateItDSte(txbInput1DSte.Text, "sqrt", txbInput2DSte.Text);
        }

        private void btnPowDSte_Click(object sender, EventArgs e) {
            CalculateItDSte(txbInput1DSte.Text, "pow", txbInput2DSte.Text);
        }

        private void btnStepDSte_Click(object sender, EventArgs e) {
            stepperStepDSte++;
            //progbarStepDSte.Value = ( step > progbarStepDSte.Maximum ? step = 1 : step );
            switch(stepperStepDSte) {
                case 1:
                    CalculateItDSte(txbInput1DSte.Text, "+", txbInput2DSte.Text);
                    break;
                case 2:
                    CalculateItDSte(txbInput1DSte.Text, "-", txbInput2DSte.Text);
                    break;
                case 3:
                    CalculateItDSte(txbInput1DSte.Text, "/", txbInput2DSte.Text);
                    break;
                case 4:
                    CalculateItDSte(txbInput1DSte.Text, "*", txbInput2DSte.Text);
                    break;
                case 5:
                    CalculateItDSte(txbInput1DSte.Text, "sqrt", txbInput2DSte.Text);
                    break;
                case 6:
                    CalculateItDSte(txbInput1DSte.Text, "pow", txbInput2DSte.Text);
                    break;
                default:
                    stepperStepDSte = 0;
                    btnStepDSte_Click(sender, e);
                    break;
            }
        }

        private void txbInput2DSte_Enter(object sender, EventArgs e) {
            lastActiveInput = txbInput2DSte;
        }

        private void txbInput1DSte_Enter(object sender, EventArgs e) {
            lastActiveInput = txbInput1DSte;
        }

        //Show author information
        private void menuItemAuthorDSte_Click(object sender, EventArgs e) {
            //Show a popup with author info
           MessageBox.Show("Created by: Duncan Sterken (https://duncte123.ml/)", "Author info");
            //Make it visit my webiste
            Process.Start("explorer.exe", @"https://duncte123.ml/");
        }

        //Open explorer window where program is located
        private void menuItemLocateDSte_Click(object sender, EventArgs e) {
            //open exlorer with the foler of the application
            Process.Start("explorer.exe", Application.StartupPath);
        }
        
        //Open dialog with different color options to set the backgroud to
        private void menuItemColorDSte_Click(object sender, EventArgs e) {
            //Create color pick dialog
            ColorDialog color = new ColorDialog();
            //open the dialog
            color.ShowDialog();
            //set the color to what the user picked
            this.BackColor = color.Color;
        }

        private void btnStartTimerDSte_Click(object sender, EventArgs e) {
            SetStepDSte(0);
            //tmrStepperDSte.Enabled = true;
            tmrStepperDSte.Interval = 1000;
            tmrStepperDSte.Start();
        }

        private void btnStoptTimerDSte_Click(object sender, EventArgs e) {
            tmrStepperDSte.Stop();
        }

        private void tmrStepperDSte_Tick(object sender, EventArgs e) {
            Console.WriteLine("Stepping");
            if (stepperStepDSte == progbarStepDSte.Maximum) {
                tmrStepperDSte.Stop();
                return;
            }
            this.btnStepDSte_Click(sender, e);
        }

        //Take in a object and convert it to a double
        private double ToDoubleDSte(String toConvert) {

            try {
                Regex r = new Regex("[^0-9]");
                return Convert.ToDouble(r.Replace(toConvert, ""));
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
                return 0D;
            }

        }

        private void CalculateItDSte(String num1, String opp, String num2) {

            if (num1 == "" || num2 == "") {
                if (tmrStepperDSte.Enabled) {
                    tmrStepperDSte.Stop();
                }
                RenderMessageDSte("Please enter a number in both fields");
                SetStepDSte(0);
                return;
            }

            double number1 = ToDoubleDSte(num1);
            double number2 = ToDoubleDSte(num2);

            switch(opp) {

                case "+":
                    txbOutputDSte.Text = "" + (number1 + number2);
                    SetStepDSte(1);
                    RenderMessageDSte("Plus");
                    return;
                case "-":
                    txbOutputDSte.Text = "" + (number1 - number2);
                    SetStepDSte(2);
                    RenderMessageDSte("Minus");
                    return;
                case "*":
                    txbOutputDSte.Text = "" + (number1 * number2);
                    SetStepDSte(4);
                    RenderMessageDSte("Times");
                    return;
                case "/":
                    if (number1 <= 0 || number2 <= 0) {
                        RenderMessageDSte("Can't devide by 0 or less");
                        progbarStepDSte.Value = 0;
                        return;
                    }
                    txbOutputDSte.Text = "" + (number1 / number2);
                    SetStepDSte(3);
                    RenderMessageDSte("Divide");
                    return;
                case "sqrt":
                    txbOutputDSte.Text = "" + Math.Sqrt(number1);
                    SetStepDSte(5);
                    RenderMessageDSte("Square Root (only first number used)");
                    return;
                case "pow":
                    txbOutputDSte.Text = "" + Math.Pow(number1, number1);
                    SetStepDSte(6);
                    RenderMessageDSte("Power");
                    return;

                default:
                    RenderMessageDSte("This opperator could not be found, please contact the developer");
                    throw new InvalidOperationException("You noob.");
                    //break;
            }
        }

        private void RenderMessageDSte(String msg) {
            lblMsgDSte.Text = msg;
        }
        
        private void SetStepDSte(int step) {
            progbarStepDSte.Value = step;
            stepperStepDSte = step;
        }
    }
}

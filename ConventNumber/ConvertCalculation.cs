using System;
using System.Drawing;
using System.Windows.Forms;

namespace ConventNumber
{
    public partial class ConvertCalculation : Form
    {
        public string value1, value2;
        public ConvertCalculation()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.calculator;
            InputLabelTextBar.Text = FromComboBox.Text + " to " + ToComboBox.Text;
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static string ZeroAdd(int o)
        {
            string result = string.Empty;
            for(int i = 0; i < o; i++)
            {
                result += "0";
            }
            return result;
        }
        public string NumToBinary(string c, int option)
        {
            int a = Convert.ToInt32(c);
            string result = string.Empty;
            int dev = 1;
            if (option == 0) dev = 2;
            if (a < 0)
            {
                a = -a;

            }
            else if (option == 1) dev = 16;
            do
            {
                int Index = a % dev;
                string b = string.Empty;
                if (Index >= 10)
                {
                    switch (Index)
                    {
                        case 10: b = "A"; break;
                        case 11: b = "B"; break;
                        case 12: b = "C"; break;
                        case 13: b = "D"; break;
                        case 14: b = "E"; break;
                        case 15: b = "F"; break;
                    }
                    result += b;
                }
                else
                {
                    result += Index.ToString();
                }
                a = (a - a % dev) / dev;
            } while (a != 0);
            return Reverse(result);
        }
        public string BinaryToNum(string a)
        {
            string b = Reverse(a);
            double value = 0;
            for(int i = 0; i < a.Length ; i++)
            {
                if (b[i] == '1')
                {
                    value += Math.Pow(2, i);
                }
            }
            return value.ToString();
        }
        public string BinaryAndHex(string a, int option)
        {
            string result = string.Empty;
            string[] hexB = { "1010", "1011", "1100", "1101", "1110", "1111" };
            if (option == 1)
            {
                result = NumToBinary(BinaryToNum(a), 1);
            }
            if(option == 2)
            {
                for(int i = 0; i < a.Length; i++)
                {
                    if (a[i] == 'A') result += hexB[0];
                    else if (a[i] == 'B') result += hexB[1];
                    else if (a[i] == 'C') result += hexB[2];
                    else if (a[i] == 'D') result += hexB[3];
                    else if (a[i] == 'E') result += hexB[4];
                    else if (a[i] == 'F') result += hexB[5];
                    else result += ZeroAdd(4 - NumToBinary(a[i].ToString(), 0).Length) + NumToBinary(a[i].ToString(), 0);
                }
            }
            return result;
        }
        public bool CheckInput(int option, string input)
        {
            if(option == 1)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if ((int)input[i] < 48 || (int)input[i] > 49)
                    {
                        return false;
                    }
                }
            }
            if(option == 0)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if ((int)input[i] < 48 || (int)input[i] > 57)
                    {
                        return false;
                    }
                }
            }
            if(option == 2)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if ((int)input[i] < 48)
                    {
                        return false;
                    }
                    if((int)input[i] > 57 && (int)input[i] < 65)
                    {
                        return false;
                    }
                    if((int)input[i] > 70 && (int)input[i] < 97)
                    {
                        return false;
                    }
                    if((int)input[i] > 102)
                    {
                        return false;
                    }
                }
                return true;
            }
            return true;
        }
        private void Apply_Click(object sender, EventArgs e)
        {
            string checkText = InputValue.Text.Trim();

            if (InputValue.Text.Length== 0)
            {
                InputValue.BackColor = Color.Red;
                MessageBox.Show("Input value is not available!");
            }
            else
            {
                if(CheckInput(FromComboBox.SelectedIndex, checkText) == false)
                {
                    MessageBox.Show("Wrong input format!");
                    ResetValue();
                }
                else
                {
                    if (FromComboBox.Text == "Decimal" && ToComboBox.Text == "Binary")
                    {
                        string result1 = NumToBinary(InputValue.Text, 0);
                        Binary1s.Text = result1;
                        value1 = result1;
                        if (Binary1s.Text.Length < 8)
                        {
                            Binary2s.Text = ZeroAdd(8 - Binary1s.Text.Length) + Binary1s.Text;
                        }
                        else if (Binary1s.Text.Length > 8 && Binary1s.Text.Length < 16)
                        {
                            Binary2s.Text = ZeroAdd(16 - Binary1s.Text.Length) + Binary1s.Text;
                        }
                        else if (result1.Length > 16 && result1.Length < 32)
                        {
                            Binary2s.Text = ZeroAdd(32 - result1.Length) + result1;
                        }
                        else if (result1.Length > 32 && result1.Length < 64)
                        {
                            Binary2s.Text = ZeroAdd(64 - result1.Length) + result1;
                        }
                        else if (result1.Length > 64 && result1.Length < 128)
                        {
                            Binary2s.Text = ZeroAdd(128 - result1.Length) + result1;
                        }
                        value2 = Binary2s.Text;
                        Hex.Text = NumToBinary(InputValue.Text, 1);
                    }else if (FromComboBox.Text == "Binary" && ToComboBox.Text == "Decimal")
                    {
                        ValueTrans1.Text = ToComboBox.Text.ToUpper();
                        ValueTrans2.Visible = false;
                        Binary2s.Visible = false;
                        ValueTrans3.Visible = false;
                        Hex.Visible = false;
                        label6.Visible = false;
                        label7.Visible = false;
                        label8.Text = "10";
                        Binary1s.Text = BinaryToNum(InputValue.Text);               
                    }
                    else if (FromComboBox.Text == "Binary" && ToComboBox.Text == "Hex")
                    {
                        ValueTrans1.Text = ToComboBox.Text.ToUpper();
                        ValueTrans2.Visible = false;
                        Binary2s.Visible = false;
                        ValueTrans3.Visible = false;
                        Hex.Visible = false;
                        label6.Visible = false;
                        label7.Visible = false;
                        label8.Text = "16";
                        Binary1s.Text = BinaryAndHex(InputValue.Text, 1);
                    }
                    else if (FromComboBox.Text == "Hex" && ToComboBox.Text == "Binary")
                    {
                        ValueTrans1.Text = ToComboBox.Text.ToUpper();
                        ValueTrans3.Visible = false;
                        Hex.Visible = false;
                        label6.Visible = false;
                        label7.Text = "2";
                        label8.Text = "2";
                        Binary1s.Text = BinaryAndHex(InputValue.Text, 2);
                        if(Binary1s.Text.Length < 8)
                        {
                            Binary2s.Text = ZeroAdd(8 - Binary1s.Text.Length) + Binary1s.Text;
                        }
                        else if (Binary1s.Text.Length > 8 && Binary1s.Text.Length < 16)
                        {
                            Binary2s.Text = ZeroAdd(16 - Binary1s.Text.Length) + Binary1s.Text;
                        }
                        else if (Binary1s.Text.Length > 16 && Binary1s.Text.Length < 32)
                        {
                            Binary2s.Text = ZeroAdd(32 - Binary1s.Text.Length) + Binary1s.Text;
                        }
                        else if (Binary1s.Text.Length > 32 && Binary1s.Text.Length < 64)
                        {
                            Binary2s.Text = ZeroAdd(64 - Binary1s.Text.Length) + Binary1s.Text;
                        }
                        else if (Binary1s.Text.Length > 64 && Binary1s.Text.Length < 128)
                        {
                            Binary2s.Text = ZeroAdd(128 - Binary1s.Text.Length) + Binary1s.Text;
                        }
                        else
                        {
                            InputValue.BackColor= Color.Red;
                            MessageBox.Show("Error value!");
                        }
                    }
                }
            }

        }
        public string AndCal(string a, string b)
        {
            string result = string.Empty;
            string a1 = NumToBinary(a,0);
            string b1 = NumToBinary(b,0);
            int max = 0;
            if(a1.Length > b1.Length)
            {
                max = a1.Length;
                b1 = ZeroAdd(a1.Length - b1.Length) + b1;
            }
            else
            {
                max = b1.Length;
                a1 = ZeroAdd(b1.Length - a1.Length) + a1;
            }
            for(int i = 0; i < max;i++)
            {
                if (a1[i] == '1' && b1[i] == '1') result += '1';
                else result += '0';
            }
            result = BinaryToNum(result);
            return result;
        }
        public string OrCal(string a, string b)
        {
            string result = string.Empty;
            string a1 = NumToBinary(a, 0);
            string b1 = NumToBinary(b, 0);
            int max = 0;
            if (a1.Length > b1.Length)
            {
                max = a1.Length;
                b1 = ZeroAdd(a1.Length - b1.Length) + b1;
            }
            else
            {
                max = b1.Length;
                a1 = ZeroAdd(b1.Length - a1.Length) + a1;
            }
            for (int i = 0; i < max; i++)
            {
                if (a1[i] == '0' && b1[i] == '0') result += '0';
                else result += '1';
            }
            result = BinaryToNum(result);
            return result;
        }
        public string NotCal(string a)
        {
            string c = NumToBinary(a, 0);
            string result = string.Empty;
            for(int i = 0; i < c.Length;i++)
            {
                if (c[i] == '1') result += '0';
                if (c[i] == '0') result += '1';
            }
            result = BinaryToNum(result);
            return result;
        }
        public string XorCal(string a, string b)
        {
            string result = string.Empty;
            string a1 = NumToBinary(a, 0);
            string b1 = NumToBinary(b, 0);
            int max = 0;
            if (a1.Length > b1.Length)
            {
                max = a1.Length;
                b1 = ZeroAdd(a1.Length - b1.Length) + b1;
            }
            else
            {
                max = b1.Length;
                a1 = ZeroAdd(b1.Length - a1.Length) + a1;
            }
            for (int i = 0; i < max; i++)
            {
                if (a1[i] != b1[i]) result += '1';
                else result += '0';
            }
            result = BinaryToNum(result);
            return result;
        }
        public bool CheckInsertData()
        {
            if (FirstValue.Text.Length == 0 || SecondValue.Text.Length == 0 || FirstValue.Text.Length == 0 && SecondValue.Text.Length == 0)
            {
                if(FirstValue.Text.Length == 0) { FirstValue.BackColor = Color.Red;}
                if (FirstValue.Text.Length == 0) { SecondValue.BackColor = Color.Red; }
                if (FirstValue.Text.Length == 0 && SecondValue.Text.Length == 0) { FirstValue.BackColor = Color.Red; SecondValue.BackColor = Color.Red; }
            }
            return true;
        }
        private void AND_Click(object sender, EventArgs e)
        {
            if(CheckInsertData())
            {
                ResultValue.Text = AndCal(FirstValue.Text.Trim(), SecondValue.Text.Trim());
            }
            else
            {
                MessageBox.Show("Insert value!");
            }
        }

        private void OR_Click(object sender, EventArgs e)
        {
            if (CheckInsertData())
            {
                ResultValue.Text = OrCal(FirstValue.Text.Trim(), SecondValue.Text.Trim());
            }
            else
            {
                MessageBox.Show("Insert value!");
            }
        }

        private void XOR_Click(object sender, EventArgs e)
        {
            if (CheckInsertData())
            {
                 ResultValue.Text = XorCal(FirstValue.Text.Trim(), SecondValue.Text.Trim());               
            }
            else
            {
                MessageBox.Show("Insert value!");
            }
        }
        public void ResetValue()
        {
            FromComboBox.SelectedIndex = 0;
            ToComboBox.SelectedIndex = 1;
            Binary1s.Visible= true;
            Binary2s.Visible= true;
            Hex.Visible= true;
            InputValue.Text = null;
            Binary1s.Text = null;
            Binary2s.Text = null;
            Hex.Text = null;
            FirstValue.Text = null;
            SecondValue.Text = null;
            ResultValue.Text = null;
            DigitalGrouping.CheckState = CheckState.Unchecked;
        }
        private void Reset_Click(object sender, EventArgs e)
        {
            ResetValue();
        }

        private void Swap_Click(object sender, EventArgs e)
        {
            ResetValue();
            int index = FromComboBox.SelectedIndex;
            FromComboBox.SelectedIndex = ToComboBox.SelectedIndex;
            ToComboBox.SelectedIndex = index;
            InputLabelTextBar.Text = FromComboBox.Text + " to " + ToComboBox.Text;
            if (FromComboBox.Text.Trim().Equals("Decimal") && ToComboBox.Text.Trim().Equals("Binary"))
            {
                ValueTrans1.Text = ToComboBox.Text.ToUpper();
                ValueTrans2.Visible = true;
                ValueTrans3.Visible = true;
                Binary1s.Visible = true;
                Binary2s.Visible = true;
                Hex.Visible = true;
                label8.Visible= true;
                label7.Visible= true;
                label6.Visible= true;
                label6.Text = "16";
                label7.Text = "2";
                label8.Text = "2";
            }
            if (FromComboBox.Text.Trim().Equals("Binary") && ToComboBox.Text.Trim().Equals("Decimal"))
            {
                ValueTrans1.Text = ToComboBox.Text.ToUpper();
                ValueTrans2.Visible = false;
                ValueTrans3.Visible = false;
                Binary1s.Visible = true;
                Binary2s.Visible = false;
                Hex.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = true;
                label8.Text = "10";
            }
            if (FromComboBox.Text.Trim().Equals("Binary") && ToComboBox.Text.Trim().Equals("Hex"))
            {
                ValueTrans1.Text = ToComboBox.Text.ToUpper();
                ValueTrans2.Visible = false;
                ValueTrans3.Visible = false;
                Binary1s.Visible = true;
                Binary2s.Visible= false;
                Hex.Visible= false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = true;
                label8.Text = "16";
            }
            if (FromComboBox.Text.Trim().Equals("Hex") && ToComboBox.Text.Trim().Equals("Binary"))
            {
                ValueTrans1.Text = ToComboBox.Text.ToUpper();
                ValueTrans2.Visible = true;
                ValueTrans3.Visible = false;
                Binary1s.Visible = true;
                Binary2s.Visible= true;
                Hex.Visible= false;
                label6.Visible = false;
                label7.Visible = true;
                label8.Visible = true;
                label7.Text = "2";
                label8.Text = "2";
            }
            if (FromComboBox.Text.Trim().Equals("Decimal") && ToComboBox.Text.Trim().Equals("Hex"))
            {
                ValueTrans1.Text = ToComboBox.Text.ToUpper();
                ValueTrans2.Visible = false;
                ValueTrans3.Visible = false;
                Binary1s.Visible = true;
                Binary2s.Visible= false;
                Hex.Visible= false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = true;
                label8.Text = "16";
            }
            if (FromComboBox.Text.Trim().Equals("Hex") && ToComboBox.Text.Trim().Equals("Decimal"))
            {
                ValueTrans1.Text = ToComboBox.Text.ToUpper();
                ValueTrans2.Visible = false;
                ValueTrans3.Visible = false;
                Binary1s.Visible = true;
                Binary2s.Visible= false;
                Hex.Visible= false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = true;
                label8.Text = "10";
            }
        }
        public static string SpaceAdd(string input)
        {
            string index = Reverse(input);
            string result = string.Empty;
            for (int i = 0; i <index.Length; i++)
            {                  
                if((i+1)%4 == 0)
                {
                    result += index[i];
                    result += " ";
                }
                else
                {
                    result += index[i];
                }
            }
            return Reverse(result);
        }

        private void InputValue_TextChanged(object sender, EventArgs e)
        {
            InputValue.BackColor= Color.White;
            DigitalGrouping.CheckState = CheckState.Unchecked;
        }

        private void FirstValue_TextChanged(object sender, EventArgs e)
        {
            FirstValue.BackColor= Color.White;
        }

        private void SecondValue_TextChanged(object sender, EventArgs e)
        {
            SecondValue.BackColor= Color.White;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To convert numbers from decimal to binary, the given decimal number is divided repeatedly by 2 and the remainders are noted down till we get 0 as the final quotient. The following steps is considered as the decimal to binary formula that shows the procedure of conversion.\r\n\r\n" +
                "Step 1: Divide the given decimal number by 2 and note down the remainder.\r\n" +
                "Step 2: Now, divide the obtained quotient by 2, and note the remainder again.\r\n" +
                "Step 3: Repeat the above steps until you get 0 as the quotient.\r\n" +
                "Step 4: Now, write the remainders in such a way that the last remainder is written first, followed by the rest in the reverse order.\r\n" +
                "Step 5: This can also be understood in another way which states that the Least Significant Bit (LSB) of the binary number is at the top and the Most Significant Bit (MSB) is at the bottom. This number is the binary value of the given decimal number.\r\n\r\n" +
                "Converting decimal to hexadecimal\r\n\r\n" +
                "Step 1: Divide the decimal number by 16.   Treat the division as an integer division.  \r\n" +
                "Step 2: Write down the remainder (in hexadecimal).\r\n" +
                "Step 3: Divide the result again by 16.  Treat the division as an integer division.  \r\n" +
                "Step 4: Repeat step 2 and 3 until result is 0.\r\n" +
                "Step 5: The hex value is the digit sequence of the remainders from the last to first.\r\n");
        }

        private void NightMode_Click(object sender, EventArgs e)
        {
            if(this.BackgroundImage == null)
            {
                if(this.BackColor == Color.WhiteSmoke)
                {
                    NightMode.Text = "Light mode";
                    NightMode.Image = Properties.Resources.moon_stars_light;
                    this.BackColor = Color.Black;
                    this.ForeColor= Color.White;
                }
                else
                {
                    NightMode.Text = "Dark mode";
                    NightMode.Image = Properties.Resources.moon_stars_dark;
                    this.BackColor = Color.WhiteSmoke;
                    this.ForeColor = Color.Black;
                }
            }
        }

        private void otherPlanetjpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Other_Planet;
        }

        private void greenChipCurcuitjpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Green_Chip_Curcuit;
        }

        private void greenCurcuitjpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Green_Curcuit;
        }

        private void lightPhoneCurcuitjpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Light_Phone_Curcuit;
        }

        private void electricCurcuitLightjpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Electric_Curcuit_Light;
        }

        private void diagramEarthjpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Diagram_Earth;
        }

        private void hexagonjpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Hexagon;
        }

        private void darkBlueCurcuitjpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Dark_Blue_Curcuit;
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = null;
        }

        private void InputTailValue_TextChanged(object sender, EventArgs e)
        {
            InputValue.BackColor = Color.White;
            DigitalGrouping.CheckState = CheckState.Unchecked;
        }

        private void DigitalGrouping_CheckedChanged(object sender, EventArgs e)
        {
            string orig1 = value1;
            string orig2 = value2;
            if (DigitalGrouping.CheckState == CheckState.Checked)
            {
                Binary1s.Text = SpaceAdd(Binary1s.Text);
                Binary2s.Text = SpaceAdd(Binary2s.Text);
            }
            else
            {
                Binary1s.Text = orig1;
                Binary2s.Text = orig2;
            }
        }
    }
}
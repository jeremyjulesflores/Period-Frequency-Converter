namespace Period_Frequency_Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            List<string> fItems = new List<string>
            {
                "Hz",
                "KHz",
                "MHz",
                "GHz",
                "THz"
            };
            List<string> tItems = new List<string>
            {
                "s",
                "ms",
                "µs",
                "ns",
                "ps"
            };

            comboBox1.Items.AddRange(fItems.ToArray());
            comboBox2.Items.AddRange(tItems.ToArray());
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            label3.Text = "";
            label4.Text = "";
            textBox1.KeyPress += TextBox_Checking;
            textBox2.KeyPress += TextBox_Checking;

            textBox1.TextChanged += TextBox_CheckIfEmpty;
            textBox2.TextChanged += TextBox_CheckIfEmpty;


            button1.Enabled = false;
            button2.Enabled = false;

        }
        private void TextBox_CheckIfEmpty(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Enable or disable button1 based on textBox1's content
            button1.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text);

            // Enable or disable button2 based on textBox2's content
            button2.Enabled = !string.IsNullOrWhiteSpace(textBox2.Text);
        }
        private void TextBox_Checking(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Label warningLabel = null;

            // Determine the associated warning label
            if (textBox == textBox1)
            {
                warningLabel = label3;
            }
            else if (textBox == textBox2)
            {
                warningLabel = label4;
            }

            // Check if the entered character is a number or a valid input
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the character if it's not a number
                warningLabel.Text = "Invalid input. Please enter a number.";
            }
            else
            {
                warningLabel.Text = ""; // Clear the warning label if input is valid
            }



            if (textBox == textBox1)
            {
                // Enable or disable button1 based on textBox1's content
                button1.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text);
            }
            else if (textBox == textBox2)
            {
                // Enable or disable button2 based on textBox2's content
                button2.Enabled = !string.IsNullOrWhiteSpace(textBox2.Text);
            }
        }

        private double GetPeriodInSeconds()
        {
            double periods = double.Parse(textBox2.Text);
            string selectedUnit = comboBox2.SelectedItem.ToString();

            switch (selectedUnit)
            {
                case "s":
                    return periods;
                case "ms":
                    return periods * 1e-3;
                case "µs":
                    return periods * 1e-6;
                case "ns":
                    return periods * 1e-9;
                case "ps":
                    return periods * 1e-12;
                default:
                    return periods;
            }
        }

        private double GetFrequencyInHertz()
        {
            double frequency = double.Parse(textBox1.Text);
            string selectedUnit = comboBox1.SelectedItem.ToString();

            switch (selectedUnit)
            {
                case "Hz":
                    return frequency;
                case "KHz":
                    return frequency * 1e3;
                case "MHz":
                    return frequency * 1e6;
                case "GHz":
                    return frequency * 1e9;
                case "THz":
                    return frequency * 1e12;
                default:
                    return frequency;
            }
        }

        // T -> F
        private void button2_Click(object sender, EventArgs e)
        {
            double periodsInSeconds = GetPeriodInSeconds();
            double frequency = 1.0 / periodsInSeconds;

            double answer = ConvertToOutputUnit(frequency, "Hz", comboBox1.SelectedItem.ToString());
            textBox1.Text = answer.ToString();
        }

        // f -> T
        private void button1_Click(object sender, EventArgs e)
        {
            double frequencyInHertz = GetFrequencyInHertz();
            double periods = 1.0 / frequencyInHertz;
            double answer = ConvertToOutputUnit(periods, "s", comboBox2.SelectedItem.ToString());
            textBox2.Text = answer.ToString();
        }

        private double ConvertToOutputUnit(double value, string inputUnit, string outputUnit)
        {
            switch (inputUnit)
            {
                case "s":
                    switch (outputUnit)
                    {
                        case "s":
                            return value;
                        case "ms":
                            return value * 1e3;
                        case "µs":
                            return value * 1e6;
                        case "ns":
                            return value * 1e9;
                        case "ps":
                            return value * 1e12;
                    }
                    break;
                case "Hz":
                    switch (outputUnit)
                    {
                        case "Hz":
                            return value;
                        case "KHz":
                            return value * 1e-3;
                        case "MHz":
                            return value * 1e-6;
                        case "GHz":
                            return value * 1e-9;
                        case "THz":
                            return value * 1e-12;
                    }
                    break;
                    // Handle other input units and their conversions to output units here
            }

            // Default: return the input value if units do not match
            return value;
        }
    }
}
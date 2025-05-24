using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPILE_RPROJECT
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
       


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1: Get the input code from the TextBox and trim any extra spaces
                string inputCode = txtCodeInput.Text.Trim(); // Trim leading/trailing whitespaces

                // Step 2: Check if the input code is empty
                if (string.IsNullOrEmpty(inputCode))
                {
                    MessageBox.Show("Please enter some code to compile.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit early if code is empty
                }

                // Step 3: Tokenize the input using the Lexer
                Lexer lexer = new Lexer(inputCode);
                List<Token> tokens = lexer.Tokenize();

                // Step 4: Display tokens in the DataGridView
                if (dgvTokens.Columns.Count == 0)
                {
                    dgvTokens.Columns.Add("TokenType", "نوع التوكن"); // Arabic column header
                    dgvTokens.Columns.Add("TokenValue", "قيمة التوكن");
                    dgvTokens.Columns.Add("Line", "رقم السطر");
                    dgvTokens.Columns.Add("Column", "رقم العمود");
                }

                dgvTokens.Rows.Clear(); // Clear any existing rows
                foreach (var token in tokens)
                {
                    dgvTokens.Rows.Add(token.Type.ToString(), token.Value, token.Line, token.Column);
                }

                // Step 5: Parse the tokens using the Parser
                Parser parser = new Parser(tokens);
                parser.Parse(); // Will throw an exception if parsing fails

                // Step 6: Display parser result
                textBox1.Text = "Code is syntactically valid.";
				textBox1.ForeColor = Color.Green; // Change label color to green for valid syntax
            }
            catch (Exception ex)
            {
				// Display syntax, lexer errors or parsing errors
				textBox1.Text = $"Error: {ex.Message}";
				textBox1.ForeColor = Color.Red; // Change label color to red for errors
            }
        }

        private void lblParserOutput_Click(object sender, EventArgs e)
        {

        }

        private void dgvTokens_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtCodeInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

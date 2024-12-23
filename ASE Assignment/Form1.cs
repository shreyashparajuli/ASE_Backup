using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BOOSE;

namespace ASE_Assignment
{
    /// <summary>
    /// The main form of the application that provides an interface to write BOOSE scripts,
    /// run them, clear the canvas, and save the canvas image.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Interface to the canvas object used for drawing.
        /// </summary>
        private ICanvas myCanvas;

        /// <summary>
        /// A factory to create command objects based on parsed instructions.
        /// </summary>
        private CommandFactory factory;

        /// <summary>
        /// A container to store and run the parsed program commands.
        /// </summary>
        private StoredProgram myProgram;

        /// <summary>
        /// Parses BOOSE script input into executable commands.
        /// </summary>
        private Parser parser;

        /// <summary>
        /// Initializes the form, sets up the canvas and parser, 
        /// and attaches necessary event handlers.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Debug.WriteLine(AboutBOOSE.about()); // Logs BOOSE version info

            // Initialize the canvas with a reference to the PictureBox (OutputWindow).
            myCanvas = new CustomCanvas(OutputWindow);
            factory = new CustomCommandFactory();
            myProgram = new StoredProgram(myCanvas);
            parser = new Parser(factory, myProgram);

            // Attach the TextChanged event for real-time syntax checking.
            ProgramWindow.TextChanged += ProgramWindow_TextChanged;

            // Attach event handlers for clear and save buttons if they are present.
            btnClearCanvas.Click += btnClearCanvas_Click;
            btnSaveCanvas.Click += btnSaveCanvas_Click;
        }

        /// <summary>
        /// Event handler for the Run button click. 
        /// Attempts to parse and execute the user-entered BOOSE script.
        /// Displays errors if the parsing or execution fails.
        /// </summary>
        /// <param name="sender">The source of the Run button click.</param>
        /// <param name="e">Additional event data.</param>
        private void Run_Click(object sender, EventArgs e)
        {
            try
            {
                string programText = ProgramWindow.Text;
                parser.ParseProgram(programText);
                myProgram.Run();
                Refresh();
                Debug.WriteLine("Program executed successfully!");
            }
            catch (ParserException ex)
            {
                Debug.WriteLine($"Parser Error: {ex.Message}");
                MessageBox.Show($"Syntax Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FactoryException fex)
            {
                // Re-throws factory-specific exceptions.
                MessageBox.Show(fex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected Error: {ex.Message}");
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Paint event for the output window. 
        /// Draws the current bitmap from the canvas onto the PictureBox.
        /// </summary>
        /// <param name="sender">The source of the Paint event.</param>
        /// <param name="e">Paint event data containing the Graphics object.</param>
        private void OutputWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap bitmap = (Bitmap)myCanvas.getBitmap();
            g.DrawImageUnscaled(bitmap, 0, 0);
        }

        /// <summary>
        /// Real-time syntax validation as the user types. 
        /// Avoids parsing if the input is too short to form a valid command.
        /// </summary>
        /// <param name="sender">The source of the TextChanged event.</param>
        /// <param name="e">Additional event data.</param>
        private void ProgramWindow_TextChanged(object sender, EventArgs e)
        {
            string text = ProgramWindow.Text;

            // If input is too short, do not attempt parsing.
            if (text.Trim().Length < 2)
            {
                // Too short to form a command. Don't parse yet.
                ProgramWindow.BackColor = Color.White;
                return;
            }

            // Uncomment to enable real-time parsing again, if desired:
            // TryParseProgram(text);
        }

        /// <summary>
        /// Attempts to parse the given program text and update the background 
        /// color based on success or failure.
        /// </summary>
        /// <param name="programText">The BOOSE script to parse.</param>
        private void TryParseProgram(string programText)
        {
            try
            {
                parser.ParseProgram(programText);
                ProgramWindow.BackColor = Color.White;
            }
            catch (ParserException)
            {
                ProgramWindow.BackColor = Color.LightPink;
            }
        }

        /// <summary>
        /// Clears the canvas when the Clear button is clicked.
        /// </summary>
        /// <param name="sender">The source of the Click event.</param>
        /// <param name="e">Additional event data.</param>
        private void btnClearCanvas_Click(object sender, EventArgs e)
        {
            // Clear the canvas
            myCanvas.Clear();
            Refresh();
        }

        /// <summary>
        /// Saves the current canvas image as a file when the Save button is clicked.
        /// Opens a SaveFileDialog to specify the file location and format.
        /// </summary>
        /// <param name="sender">The source of the Click event.</param>
        /// <param name="e">Additional event data.</param>
        private void btnSaveCanvas_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                Bitmap bitmap = (Bitmap)myCanvas.getBitmap();
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                sfd.Title = "Save Canvas Image";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bitmap.Save(sfd.FileName);
                }
            }
        }
    }
}

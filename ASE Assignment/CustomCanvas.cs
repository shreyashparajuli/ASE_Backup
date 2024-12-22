using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BOOSE;

namespace ASE_Assignment
{
    /// <summary>
    /// CustomCanvas provides a drawing surface using a PictureBox for output.
    /// It implements the <see cref="ICanvas"/> interface and supports drawing lines, shapes,
    /// and text, as well as changing the pen color and clearing the canvas.
    /// </summary>
    public class CustomCanvas : ICanvas
    {
        /// <summary>
        /// The PictureBox used to display the canvas output.
        /// </summary>
        private PictureBox _outputWindow;

        /// <summary>
        /// The Graphics object associated with the canvas bitmap.
        /// </summary>
        private Graphics _graphics;

        /// <summary>
        /// The current Pen used for drawing.
        /// </summary>
        private Pen _pen;

        /// <summary>
        /// The Bitmap representing the canvas drawing surface.
        /// </summary>
        private Bitmap _bitmap;

        /// <summary>
        /// The current pen color.
        /// </summary>
        private Color _penColour;

        /// <summary>
        /// Default pen width.
        /// </summary>
        private float _penWidth = 5.0f; // Adjust this value as needed

        /// <summary>
        /// The default width of the canvas.
        /// </summary>
        private const int xsize = 640;

        /// <summary>
        /// The default height of the canvas.
        /// </summary>
        private const int ysize = 480;

        /// <summary>
        /// The current X position used for drawing references.
        /// </summary>
        public int Xpos { get; set; }

        /// <summary>
        /// The current Y position used for drawing references.
        /// </summary>
        public int Ypos { get; set; }

        /// <summary>
        /// The current pen color used for drawing.
        /// </summary>
        public Color PenColour
        {
            get => _penColour;
            set
            {
                _penColour = value;
                _pen = new Pen(_penColour, _penWidth);
            }
        }

        /// <summary>
        /// Implements the ICanvas interface property for the pen color
        /// as an <see cref="object"/>. Internally gets or sets <see cref="PenColour"/>.
        /// </summary>
        object ICanvas.PenColour
        {
            get => PenColour;
            set => PenColour = (Color)value;
        }

        /// <summary>
        /// Parameterless constructor that initializes the canvas with a default size 
        /// (<see cref="xsize"/> by <see cref="ysize"/>) and a white background.
        /// </summary>
        public CustomCanvas()
        {
            _bitmap = new Bitmap(xsize, ysize);
            _graphics = Graphics.FromImage(_bitmap);
            Xpos = 0;
            Ypos = 0;
            PenColour = Color.Black; // sets default pen color and pen width
        }

        /// <summary>
        /// Constructor that initializes the canvas to match a given PictureBox's size,
        /// then sets the default pen color and position.
        /// </summary>
        /// <param name="outputWindow">The PictureBox used for displaying the canvas.</param>
        public CustomCanvas(PictureBox outputWindow) : this()
        {
            _outputWindow = outputWindow;
            _bitmap = new Bitmap(_outputWindow.Width, _outputWindow.Height);
            _graphics = Graphics.FromImage(_bitmap);
            PenColour = Color.Black;
            Xpos = 0;
            Ypos = 0;
            UpdateImage();
        }

        /// <summary>
        /// Sets the current drawing position without drawing a line.
        /// </summary>
        /// <param name="x">The X coordinate to move to.</param>
        /// <param name="y">The Y coordinate to move to.</param>
        public void Set(int x, int y)
        {
            Xpos = x;
            Ypos = y;
            // No drawing done here, so no UpdateImage needed.
        }

        /// <summary>
        /// Draws a line from the current position to the specified (x, y) coordinates,
        /// then updates the current position to that point.
        /// </summary>
        /// <param name="x">The X coordinate to draw to.</param>
        /// <param name="y">The Y coordinate to draw to.</param>
        /// <exception cref="CanvasException">Thrown if the coordinates are out of valid range.</exception>
        public void DrawTo(int x, int y)
        {
            if (x < 0 || x > xsize || y < 0 || y > ysize)
                throw new CanvasException("Invalid screen position DrawTo " + x + ", " + y);

            _graphics.DrawLine(_pen, Xpos, Ypos, x, y);
            Xpos = x;
            Ypos = y;
            UpdateImage();
        }

        /// <summary>
        /// Sets the pen color using RGB values.
        /// </summary>
        /// <param name="r">Red component (0-255).</param>
        /// <param name="g">Green component (0-255).</param>
        /// <param name="b">Blue component (0-255).</param>
        public void SetColour(int r, int g, int b)
        {
            PenColour = Color.FromArgb(r, g, b);
            // UpdateImage not strictly necessary here unless desired.
        }

        /// <summary>
        /// Moves the current position to the specified coordinates without drawing.
        /// Logs the new position to the Debug console.
        /// </summary>
        /// <param name="x">X coordinate to move to.</param>
        /// <param name="y">Y coordinate to move to.</param>
        public void MoveTo(int x, int y)
        {
            Xpos = x;
            Ypos = y;
            Debug.WriteLine($"MoveTo executed: New Position: ({Xpos}, {Ypos})");
        }

        /// <summary>
        /// Clears the entire canvas by filling it with white.
        /// </summary>
        public void Clear()
        {
            _graphics.Clear(Color.White);
            Xpos = 0;
            Ypos = 0;
            UpdateImage();
        }

        /// <summary>
        /// Resets the canvas state by clearing it and setting the position to (0,0).
        /// </summary>
        public void Reset()
        {
            Xpos = 0;
            Ypos = 0;
            Clear();
        }

        /// <summary>
        /// Draws a circle at the current position with the specified radius.
        /// Optionally fills the circle instead of drawing only an outline.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="fill">Whether to fill the circle (true) or only draw an outline (false).</param>
        public void Circle(int radius, bool fill)
        {
            var rect = new Rectangle(Xpos - radius, Ypos - radius, radius * 2, radius * 2);
            if (!fill)
            {
                _graphics.DrawEllipse(_pen, rect);
            }
            else
            {
                using (var brush = new SolidBrush(PenColour))
                {
                    _graphics.FillEllipse(brush, rect);
                }
            }

            UpdateImage();
        }

        /// <summary>
        /// Draws a rectangle at the current position with the specified width and height.
        /// Optionally fills the rectangle instead of drawing only an outline.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="fill">True to fill the rectangle, false to draw only an outline.</param>
        public void Rect(int width, int height, bool fill)
        {
            var rect = new Rectangle(Xpos, Ypos, width, height);
            if (fill)
            {
                using (var brush = new SolidBrush(PenColour))
                {
                    _graphics.FillRectangle(brush, rect);
                }
            }
            else
            {
                _graphics.DrawRectangle(_pen, rect);
            }

            UpdateImage();
        }

        /// <summary>
        /// Draws a triangle at the current position, given a width (base) and height.
        /// The triangle is drawn as an outline connecting three points and returning to the initial point.
        /// </summary>
        /// <param name="width">The width of the triangle's base.</param>
        /// <param name="height">The height of the triangle.</param>
        public void Tri(int width, int height)
        {
            var points = new Point[]
            {
                new Point(Xpos, Ypos),
                new Point(Xpos + width / 2, Ypos - height),
                new Point(Xpos + width, Ypos),
                new Point(Xpos, Ypos) // Closes the shape
            };
            _graphics.DrawLines(_pen, points);
            UpdateImage();
        }

        /// <summary>
        /// Writes text at the current position on the canvas. 
        /// Automatically strips any surrounding quotation marks from the text.
        /// </summary>
        /// <param name="text">The string of text to be rendered.</param>
        public void WriteText(string text)
        {
            // Trim surrounding quotes from the input text
            string trimmedText = text.Trim('"');

            Debug.WriteLine($"WriteText called with trimmed text: {trimmedText}, Position: ({Xpos}, {Ypos})");

            if (_graphics != null)
            {
                using (Brush brush = new SolidBrush(PenColour))
                {
                    // Use a custom font and size for text rendering
                    Font font = new Font("Verdana", 14);
                    _graphics.DrawString(trimmedText, font, brush, Xpos, Ypos);
                }
                UpdateImage();
            }
            else
            {
                Debug.WriteLine("Graphics object is null. Cannot render text.");
            }
        }

        /// <summary>
        /// Returns the current Bitmap of the canvas drawing surface.
        /// </summary>
        /// <returns>The current <see cref="Bitmap"/> that represents the canvas.</returns>
        public object getBitmap()
        {
            return _bitmap;
        }

        /// <summary>
        /// Updates the PictureBox image with the current bitmap to reflect any recent drawing changes.
        /// </summary>
        private void UpdateImage()
        {
            if (_outputWindow != null)
            {
                _outputWindow.Image = _bitmap;
                _outputWindow.Refresh(); // Force the PictureBox to redraw
                Debug.WriteLine("OutputWindow updated.");
            }
        }
    }
}

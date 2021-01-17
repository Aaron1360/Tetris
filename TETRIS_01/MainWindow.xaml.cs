
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TETRIS_01
{
    
    public partial class MainWindow : Window
    {
        private const int VELOCIDAD = 700;// milisegundos
        DispatcherTimer tiempo;
        Random randFig;
        private int contFil = 0;
        private int contCol = 0;
        private int contLin = 0;
        private int izqPos = 0;
        private int abPos = 0;
        private int anchoPoliminoActual;
        private int altoPoliminoActual;
        private int numFigActual;
        private int numFigSiguiente;
        private int politrisGridColumn;
        private int politrisGridRow;
        private int rotacion = 0;
        private bool juegoActivo = false;
        private bool sigFigDib = false;
        private int[,] poliminoActual = null;
        private bool gira = false;
        private bool colisionFondo = false;
        private bool colisionIzq = false;
        private bool colisionDer = false;
        private bool elJuegoTermino = false;
        private int velocidad;
        private int subeNivel = 60;// cada 60 segundos incrementa el nivel de 1 a 10
        private double contVel = 0;
        private int nivel = 1;
        private int puntuacion = 0;
        private static Color O_PoliminoColor = Colors.Blue;
        private static Color I_PoliminoColor = Colors.DarkRed;
        private static Color T_PoliminoColor = Colors.DarkGreen;
        private static Color S_PoliminoColor = Colors.Lavender;
        private static Color Z_PoliminoColor = Colors.Gold;
        private static Color J_PoliminoColor = Colors.DarkOrange;
        private static Color L_PoliminoColor = Colors.BlueViolet;
        List<int> filaPoliminoActual = null;
        List<int> columnaPoliminoActual = null;

        // color de las figuras
        Color[] colorFig = {  O_PoliminoColor,I_PoliminoColor,
                                T_PoliminoColor,S_PoliminoColor,
                                Z_PoliminoColor,J_PoliminoColor,
                                L_PoliminoColor
                             };
        // ---------
        string[] arregloPoliminos = { "","O_Polimino" , "I_Polimino_0",
                                        "T_Polimino_0","S_Polimino_0",
                                        "Z_Polimino_0","J_Polimino_0",
                                        "L_Polimino_0"
                                   };

        #region Arreglo de Poliminos

        // arreglo de las figuras
        //---- O Polimino------------
        public int[,] O_Polimino = new int[2, 2] { { 1, 1 },  // * *
                                                    { 1, 1 }}; // * *

        //---- I Polimino------------
        public int[,] I_Polimino_0 = new int[2, 4] { { 1, 1, 1, 1 }, { 0, 0, 0, 0 } };// * * * *

        public int[,] I_Polimino_90 = new int[4, 2] {{ 1,0 },   // *  
                                                       { 1,0 },  // *
                                                       { 1,0 },  // *
                                                       { 1,0 }}; // *
        //---- T Polimino------------
        public int[,] T_Polimino_0 = new int[2, 3] {{0,1,0},    //    * 
                                                     {1,1,1}};   //  * * *

        public int[,] T_Polimino_90 = new int[3, 2] {{1,0},     //  * 
                                                      {1,1},     //  * *
                                                      {1,0}};    //  *  

        public int[,] T_Polimino_180 = new int[2, 3] {{1,1,1},  // * * *
                                                       {0,1,0}}; //   * 

        public int[,] T_Polimino_270 = new int[3, 2] {{0,1},    //   * 
                                                       {1,1},    // * *
                                                       {0,1}};   //   *  
        //---- S Polimino------------
        public int[,] S_Polimino_0 = new int[2, 3] {{0,1,1},    //   * *
                                                     {1,1,0}};   // * *

        public int[,] S_Polimino_90 = new int[3, 2] {{1,0},     // *
                                                      {1,1},     // * *
                                                      {0,1}};    //   *
        //---- Z Polimino------------
        public int[,] Z_Polimino_0 = new int[2, 3] {{1,1,0},    // * *
                                                     {0,1,1}};   //   * *

        public int[,] Z_Polimino_90 = new int[3, 2] {{0,1},     //   *
                                                      {1,1},     // * *
                                                      {1,0}};    // *
        //---- J Polimino------------
        public int[,] J_Polimino_0 = new int[2, 3] {{1,0,0},    // * 
                                                     {1,1,1}};   // * * *

        public int[,] J_Polimino_90 = new int[3, 2] {{1,1},     // * * 
                                                      {1,0},     // *
                                                      {1,0}};    // * 

        public int[,] J_Polimino_180 = new int[2, 3] {{1,1,1},  // * * * 
                                                       {0,0,1}}; //     *

        public int[,] J_Polimino_270 = new int[3, 2] {{0,1},    //   * 
                                                       {0,1},    //   *
                                                       {1,1 }};  // * *

        //---- L Polimino------------
        public int[,] L_Polimino_0 = new int[2, 3] {{0,0,1},    //     * 
                                                     {1,1,1}};   // * * *

        public int[,] L_Polimino_90 = new int[3, 2] {{1,0},     // *  
                                                      {1,0},     // *
                                                      {1,1}};    // * *

        public int[,] L_Polimino_180 = new int[2, 3] {{1,1,1},  // * * * 
                                                       {1,0,0}}; // *

        public int[,] L_Polimino_270 = new int[3, 2] {{1,1},    // * * 
                                                       {0,1},    //   *
                                                       {0,1 }};  //   *

        public object Task { get; private set; }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            velocidad = VELOCIDAD;
            //created event for key press
            KeyDown += flechas;
            // init timer
            tiempo = new DispatcherTimer();
            tiempo.Interval = new TimeSpan(0, 0, 0, 0, velocidad); // 700 millisecond
            tiempo.Tick += tiempo_Tic;
            politrisGridColumn = politrisGrid.ColumnDefinitions.Count;
            politrisGridRow = politrisGrid.RowDefinitions.Count;
            randFig = new Random();
            numFigActual = randFig.Next(1, 8);
            numFigSiguiente = randFig.Next(1, 8);
            nextTxt.Visibility = nivelTxt.Visibility = GameOverTxt.Visibility = Lineas.Visibility =  Visibility.Collapsed;
         }

        //Metodo para mover las figuras
        private void flechas(object sender, KeyEventArgs e)
        {

            if (!tiempo.IsEnabled) { return; }
            switch (e.Key.ToString())
            {
                case "Up":
                    rotacion += 90;
                    if (rotacion > 270) { rotacion = 0; }
                    rotarFig(rotacion);
                    break;
                case "Down":
                    abPos++;
                    break;
                case "Right":
                    // Check if collided
                    poliColision();
                    if (!colisionDer) { izqPos++; }
                    colisionDer = false;
                    break;
                case "Left":
                    // Check if collided
                    poliColision();
                    if (!colisionIzq) { izqPos--; }
                    colisionIzq = false;
                    break;
            }
            moverFig();
        }

        // rotacion de poliminos
        private void rotarFig(int _rotation)
        {
            // Check if collided
            if (rotacionChoca(rotacion))
            {
                rotacion -= 90;
                return;
            }

            if (arregloPoliminos[numFigActual].IndexOf("I_") == 0)
            {
                if (_rotation > 90) { _rotation = rotacion = 0; }
                poliminoActual = tomarVariablePorCadena("I_Polimino_" + _rotation);
            }
            else if (arregloPoliminos[numFigActual].IndexOf("T_") == 0)
            {
                poliminoActual = tomarVariablePorCadena("T_Polimino_" + _rotation);
            }
            else if (arregloPoliminos[numFigActual].IndexOf("S_") == 0)
            {
                if (_rotation > 90) { _rotation = rotacion = 0; }
                poliminoActual = tomarVariablePorCadena("S_Polimino_" + _rotation);
            }
            else if (arregloPoliminos[numFigActual].IndexOf("Z_") == 0)
            {
                if (_rotation > 90) { _rotation = rotacion = 0; }
                poliminoActual = tomarVariablePorCadena("Z_Polimino_" + _rotation);
            }
            else if (arregloPoliminos[numFigActual].IndexOf("J_") == 0)
            {
                poliminoActual = tomarVariablePorCadena("J_Polimino_" + _rotation);
            }
            else if (arregloPoliminos[numFigActual].IndexOf("L_") == 0)
            {
                poliminoActual = tomarVariablePorCadena("L_Polimino_" + _rotation);
            }
            else if (arregloPoliminos[numFigActual].IndexOf("O_") == 0) // El cuadrado no gira
            {
                return;
            }

            gira = true;
            sumFig(numFigActual, izqPos, abPos);
        }


        // metodo de temporizador para mover la figura abajo
        private void tiempo_Tic(object sender, EventArgs e)
        {
            abPos++;
            moverFig();
            if (contVel >= subeNivel)
            {
                if (velocidad >= 50)
                {
                    velocidad -= 50;
                    nivel++;
                    nivelTxt.Text = "Nivel: " + nivel.ToString();
                }
                else { velocidad = 50; }
                tiempo.Stop();
                tiempo.Interval = new TimeSpan(0, 0, 0, 0, velocidad);
                tiempo.Start();
                contVel = 0;
            }
            contVel += (velocidad / 1000f);

        }


        // Metodo para el boton de inicio
        private void Boton_inicio(object sender, RoutedEventArgs e)
        {

            if (elJuegoTermino)
            {
                politrisGrid.Children.Clear();
                figSigCanvas.Children.Clear();
                GameOverTxt.Visibility = Visibility.Collapsed;
                elJuegoTermino = false;
            }
            if (!tiempo.IsEnabled)
            {
                if (!juegoActivo) { scoreTxt.Text = "0"; izqPos = 3; sumFig(numFigActual, izqPos); }
                nextTxt.Visibility = nivelTxt.Visibility = Lineas.Visibility= Visibility.Visible;
                nivelTxt.Text = "nivel: " + nivel.ToString();
                Lineas.Text = "Lineas: " + contLin.ToString();
                tiempo.Start();
                boton_i_o.Content = "Stop Game";
                juegoActivo = true;
            }
            else
            {
                tiempo.Stop();
                boton_i_o.Content = "Start Game";
            }
        }

        // aparece nueva figura
        private void sumFig(int shapeNumber, int _left = 0, int _down = 0)
        {
            // quita la posicion anterior del polimino
            quitarFig();
            filaPoliminoActual = new List<int>();
            columnaPoliminoActual = new List<int>();
            Rectangle square = null;
            if (!gira)
            {
                poliminoActual = null;
                poliminoActual = tomarVariablePorCadena(arregloPoliminos[shapeNumber].ToString());
            }
            int firstDim = poliminoActual.GetLength(0);
            int secondDim = poliminoActual.GetLength(1);
            anchoPoliminoActual = secondDim;
            altoPoliminoActual = firstDim;
            // solo para el polimino I
            if (poliminoActual == I_Polimino_90)
            {
                anchoPoliminoActual = 1;
            }
            else if (poliminoActual == I_Polimino_0) { altoPoliminoActual = 1; }
            //------------------------------------
            for (int row = 0; row < firstDim; row++)
            {
                for (int column = 0; column < secondDim; column++)
                {
                    int bit = poliminoActual[row, column];
                    if (bit == 1)
                    {
                        square = cuadroBasico(colorFig[shapeNumber - 1]);
                        politrisGrid.Children.Add(square);
                        square.Name = "moving_" + Grid.GetRow(square) + "_" + Grid.GetColumn(square);
                        if (_down >= politrisGrid.RowDefinitions.Count - altoPoliminoActual)
                        {
                            _down = politrisGrid.RowDefinitions.Count - altoPoliminoActual;
                        }
                        Grid.SetRow(square, contFil + _down);
                        Grid.SetColumn(square, contCol + _left);
                        filaPoliminoActual.Add(contFil + _down);
                        columnaPoliminoActual.Add(contCol + _left);

                    }
                    contCol++;
                }
                contCol = 0;
                contFil++;
            }
            contCol = 0;
            contFil = 0;
            if (!sigFigDib)
            {
                dibujarSigFig(numFigSiguiente);
            }
        }

        // metodo para que aparezca una nueva figura en una nueva posicion
        private void moverFig()
        {
            colisionIzq = false;
            colisionDer = false;

            // revisar colision
            poliColision();
            if (izqPos > (politrisGridColumn - anchoPoliminoActual))
            {
                izqPos = (politrisGridColumn - anchoPoliminoActual);
            }
            else if (izqPos < 0) { izqPos = 0; }

            if (colisionFondo)
            {
                detenerFig();
                return;
            }
            sumFig(numFigActual, izqPos, abPos);
        }

        //revisar colision si el polimino gira
        private bool rotacionChoca(int _rotation)
        {
            if (revisarColision(0, anchoPoliminoActual - 1)) { return true; }//colision de fondo 
            else if (revisarColision(0, -(anchoPoliminoActual - 1))) { return true; }// colision arriba
            else if (revisarColision(0, -1)) { return true; }// colision arriba
            else if (revisarColision(-1, anchoPoliminoActual - 1)) { return true; }// colision izquierda
            else if (revisarColision(1, anchoPoliminoActual - 1)) { return true; }// colision derecha
            return false;
        }

        // revisar si choca con el fondo, los lados y otras figuras
        private void poliColision()
        {
            colisionFondo = revisarColision(0, 1);
            colisionIzq = revisarColision(-1, 0);
            colisionDer = revisarColision(1, 0);
        }

        //revisar colision
        private bool revisarColision(int _leftRightOffset, int _bottomOffset)
        {
            Rectangle movingSquare;
            int squareRow = 0;
            int squareColumn = 0;
            for (int i = 0; i <= 3; i++)
            {
                squareRow = filaPoliminoActual[i];
                squareColumn = columnaPoliminoActual[i];
                try
                {
                    movingSquare = (Rectangle)politrisGrid.Children
                    .Cast<UIElement>()
                    .FirstOrDefault(e => Grid.GetRow(e) == squareRow + _bottomOffset && Grid.GetColumn(e) == squareColumn + _leftRightOffset);
                    if (movingSquare != null)
                    {
                        if (movingSquare.Name.IndexOf("arrived") == 0)
                        {
                            return true;
                        }
                    }
                }
                catch { }
            }
            if (abPos > (politrisGridRow - altoPoliminoActual)) { return true; }
            return false;
        }

        // Dibujar la siguiente figura en figSigCanvas 
        private void dibujarSigFig(int shapeNumber)
        {
            figSigCanvas.Children.Clear();
            int[,] nextShapeTetromino = null;
            nextShapeTetromino = tomarVariablePorCadena(arregloPoliminos[shapeNumber]);
            int firstDim = nextShapeTetromino.GetLength(0);
            int secondDim = nextShapeTetromino.GetLength(1);
            int x = 0;
            int y = 0;
            Rectangle square;
            for (int row = 0; row < firstDim; row++)
            {
                for (int column = 0; column < secondDim; column++)
                {
                    int bit = nextShapeTetromino[row, column];
                    if (bit == 1)
                    {
                        square = cuadroBasico(colorFig[shapeNumber - 1]);
                        figSigCanvas.Children.Add(square);
                        Canvas.SetLeft(square, x);
                        Canvas.SetTop(square, y);
                    }
                    x += 25;
                }
                x = 0;
                y += 25;
            }
            sigFigDib = true;
        }


        // este metodo se usa cuando la figura llega al fondo o choca con otra
        private void detenerFig()
        {
            tiempo.Stop();
            
            // condicion del fin del juego
            if (abPos <= 2)
            {
                gameOver();
                contLin = 0;
                return;
            }

            int index = 0;
            while (index < politrisGrid.Children.Count)
            {
                UIElement element = politrisGrid.Children[index];
                if (element is Rectangle)
                {
                    Rectangle square = (Rectangle)element;
                    if (square.Name.IndexOf("moving_") == 0)
                    {
                        // cambia el nombre de las figuras cuando llegan al final
                        string newName = square.Name.Replace("moving_", "arrived_");
                        square.Name = newName;
                    }
                }
                index++;
            }
            //revisa que la linea este completa y manda a la siguiente
            revisarCompleta();
            reset();
            tiempo.Start();

        }
        // metodo para revisar la linea completa
        private void revisarCompleta()
        {
            int gridRow = politrisGrid.RowDefinitions.Count;
            int gridColumn = politrisGrid.ColumnDefinitions.Count;
            int squareCount = 0;
            for (int row = gridRow; row >= 0; row--)
            {
                squareCount = 0;
                for (int column = gridColumn; column >= 0; column--)
                {
                    Rectangle square;
                    square = (Rectangle)politrisGrid.Children
                   .Cast<UIElement>()
                   .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
                    if (square != null)
                    {
                        if (square.Name.IndexOf("arrived") == 0)
                        {
                            squareCount++;
                        }
                    }
                }

                // si squareCount == gridColumn significa que la linea esta completa y debe eliminarse 
                if (squareCount == gridColumn)
                {
                    contLin +=1;
                    Lineas.Text = "Lineas: " + contLin.ToString();
                    borrarLinea(row);
                    scoreTxt.Text = obtenerPuntuacion().ToString();
                    revisarCompleta();
                }
            }
        }

        // borra la linea completada
        private void borrarLinea(int row)
        {
            // Delete complete line
            for (int i = 0; i < politrisGrid.ColumnDefinitions.Count; i++)
            {
                Rectangle square;
                try
                {
                    square = (Rectangle)politrisGrid.Children
                   .Cast<UIElement>()
                   .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == i);
                    politrisGrid.Children.Remove(square);
                }
                catch { }

            }
            // Move down the rest shape
            foreach (UIElement element in politrisGrid.Children)
            {
                Rectangle square = (Rectangle)element;
                if (square.Name.IndexOf("arrived") == 0 && Grid.GetRow(square) <= row)
                {
                    Grid.SetRow(square, Grid.GetRow(square) + 1);
                }
            }
        }
        // Get the score
        private int obtenerPuntuacion()
        {
            puntuacion += 50 * nivel;
            return puntuacion;
        }

        // reinicio
        private void reset()
        {
            abPos = 0;
            izqPos = 3;
            gira = false;
            rotacion = 0;
            numFigActual = numFigSiguiente;
            if (!elJuegoTermino) { sumFig(numFigActual, izqPos); }
            sigFigDib = false;
            randFig = new Random();
            numFigSiguiente = randFig.Next(1, 8);
            colisionFondo = false;
            colisionIzq = false;
            colisionDer = false;
        }
        // reinicio despues de perder
        private void gameOver()
        {
            elJuegoTermino = true;
            reset();
            boton_i_o.Content = "Start Game";
            GameOverTxt.Visibility = Visibility.Visible;
            contFil = 0;
            contCol = 0;
            izqPos = 0;
            contVel = 0;
            velocidad = VELOCIDAD;
            nivel = 1;
            juegoActivo = false;
            puntuacion = 0;
            sigFigDib = false;
            poliminoActual = null;
            numFigActual = randFig.Next(1, 8);
            numFigSiguiente = randFig.Next(1, 8);
            tiempo.Interval = new TimeSpan(0, 0, 0, 0, velocidad);

        }


        // borrar figuras
        private void quitarFig()
        {
            int index = 0;
            while (index < politrisGrid.Children.Count)
            {
                UIElement element = politrisGrid.Children[index];
                if (element is Rectangle)
                {
                    Rectangle square = (Rectangle)element;
                    if (square.Name.IndexOf("moving_") == 0)
                    {
                        politrisGrid.Children.Remove(element);
                        index = -1;
                    }
                }
                index++;
            }

        }

        // crea el cuadrado unitario de las figuras
        private Rectangle cuadroBasico(Color rectColor)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = 25;
            rectangle.Height = 25;
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.White;
            rectangle.Fill = gradColor(rectColor);
            return rectangle;
        }

        // toma el gradiente de color para el cuadro unitario

        private LinearGradientBrush gradColor(Color clr)
        {
            LinearGradientBrush gradientColor = new LinearGradientBrush();
            gradientColor.StartPoint = new Point(0, 0);
            gradientColor.EndPoint = new Point(1, 1.5);
            GradientStop black = new GradientStop();
            black.Color = Colors.Black;
            black.Offset = -1.5;
            gradientColor.GradientStops.Add(black);
            GradientStop other = new GradientStop();
            other.Color = clr;
            other.Offset = 0.70;
            gradientColor.GradientStops.Add(other);
            return gradientColor;
        }

        // acceso a la variable por el nombre
        private int[,] tomarVariablePorCadena(string variable)
        {
            return (int[,])this.GetType().GetField(variable).GetValue(this);
        }
       

    }


}

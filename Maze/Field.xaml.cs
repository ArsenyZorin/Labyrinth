using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using Point = System.Windows.Point;

namespace TestTaskMitsar
{
    /// <summary>
    /// Логика взаимодействия для Field.xaml
    /// </summary>
    public partial class Field : UserControl
    {

        public static int SizeOfField;
        private readonly Labyrinth _form;
        private Point _orig;
        private static int[,] _map;

        public Field(Labyrinth form)
        {
            InitializeComponent();
            _form = form;
            FillingField();   
        }
        
        //Создание словаря, в котором будут храниться клетки квадрата
        static Dictionary<string, Rectangle> partsOffield = new Dictionary<string, Rectangle>();


        //Метод, с помощью которого, будет происходить создание поля
        private void FillingField()
        {
            canvasfield.Children.Clear();

            var widthOfSq = _form.SizeOfSq(out SizeOfField);
            
            for (var i = 0; i < SizeOfField; i++)
            {
                for (var j = 0; j < SizeOfField; j++)
                {
                    var left = 5+j*widthOfSq;
                    var top = 5+i*widthOfSq;
                    var index = i + "0" + j;
                    var rect = new Rectangle
                    {
                        Width = widthOfSq,
                        Height = widthOfSq,
                        Stroke = Brushes.Black,
                        Fill = Brushes.Transparent,
                        Margin = new Thickness(left, top, left+widthOfSq, top + widthOfSq),
                        Name = "Rectangle" + index
                    };

                    canvasfield.Children.Add(rect);
                    
                    //При изменении размера формы, выделенные клетки остаются выделенными
                    Rectangle rectFromDict;

                    partsOffield.TryGetValue(index, out rectFromDict);

                    if (!partsOffield.ContainsKey(index)) partsOffield.Add(index, rect);
                    else
                    {
                        if (rectFromDict != null && rect.Fill.Equals(rectFromDict.Fill))
                        {
                            partsOffield.Remove(index);
                            partsOffield.Add(index, rect);
                        }
                        else
                        {
                            if (rectFromDict != null) rect.Fill = rectFromDict.Fill;
                            partsOffield.Remove(index);
                            partsOffield.Add(index, rect);
                        }
                    }
                }
            }
        }

        //Метод, создающий препятствия и располагающий их на поле случайным образом
        public static void CreateObstacles(int fields, int amount)
        {
            foreach (var elem in partsOffield)
            {
                if (elem.Value.Fill.Equals(Brushes.Black)) elem.Value.Fill = Brushes.Transparent;
            }

            var rand = new Random();

            for (var i = 0; i < amount; i++)
            {
                Rectangle rect;
                string index;
                do
                {
                    var a = rand.Next(0, fields);
                    var b = rand.Next(0, fields);

                    index = a + "0" + b; 

                } while (!(partsOffield.TryGetValue(index, out rect) && rect.Fill.Equals(Brushes.Transparent)));

                rect.Fill = Brushes.Black;
            }
        }

        private void canvasfield_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FillingField();
        }
        
        //Обработка события нажатия на элемент поля
        private void Canvasfield_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            _orig.X = e.GetPosition(canvasfield).X;
            _orig.Y = e.GetPosition(canvasfield).Y;

            var counter = 0;
            

            foreach (var elem in partsOffield)
            {
                if (!(elem.Value.Margin.Left <= _orig.X) || !(_orig.X <= elem.Value.Margin.Right) || !(elem.Value.Margin.Top <= _orig.Y) ||
                    !(_orig.Y <= elem.Value.Margin.Bottom)) continue;

                if (elem.Value.Fill.Equals(Brushes.Transparent))
                {
                    foreach (var shape in canvasfield.Children)
                    {
                        if (!(shape.GetType().Name.Equals("Rectangle"))) continue;
                        var rect = (Rectangle)shape;
                        if (!rect.Name.StartsWith("Rectangle")) continue;
                        if (!rect.Fill.Equals(Brushes.Red)) continue;

                        counter++;

                        if (counter.Equals(2))
                        {
                            MessageBox.Show("Нельзя выбрать больше двух клеток", "Предупреждение", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            return;
                        }
                    }

                    elem.Value.Fill = Brushes.Red;
                }
                else if(elem.Value.Fill.Equals(Brushes.Red))
                {
                    elem.Value.Fill = Brushes.Transparent;
                    foreach (var shape in partsOffield)
                    {
                        if (shape.Value.Fill.Equals(Brushes.Yellow)) shape.Value.Fill = Brushes.Transparent;
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя выбрать данную часть поля. Это либо препятствие, либо часть показанного пути",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                break;
            }
        }

        //Очистка поля
        public void ClearAll()
        {
            partsOffield.Clear();
            canvasfield.Children.Clear();
        }

        //Перевод "лабиринта" в матричный вид
        public static void ReadMap()
        {
            var countofsel = 0;
            var countofobst = 0;

            foreach (var elem in partsOffield)
            {
                if (elem.Value.Fill != null && elem.Value.Fill.Equals(Brushes.Red)) countofsel++;

                if (countofsel == 2) break;
            }

            if (countofsel != 2)
            {
                MessageBox.Show("Невозможно рассчитать и показать кратчайший путь, так как выбрано меньше двух клеток",
                    "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            _map = new int[SizeOfField + 2, SizeOfField + 2];
            int fi = 0, fj = 0, si = 0, sj = 0;

            for (var i = 0; i <= SizeOfField + 1; i++)
            {
                for (var j = 0; j <= SizeOfField + 1; j++)
                {

                    var index = (i - 1) + "0" + (j - 1);

                    if (i == 0 || j == 0 || i == SizeOfField + 1 || j == SizeOfField + 1)
                    {
                        _map[i, j] = -1; //Заполнение крайних элементов массива значениями = -1 (непроходимая часть). Во избежании выхода за поле
                    }

                    foreach (var elem in partsOffield)
                    {
                        if (!elem.Key.Equals(index)) continue;

                        if (elem.Value.Fill.Equals(Brushes.Transparent))
                        {
                            _map[i, j] = 0; // нули соответствуют тем клеткам, по которым можно пройти
                            break;
                        }
                        if (elem.Value.Fill.Equals(Brushes.Black))
                        {
                            _map[i, j] = -1;
                            break;
                        }

                        if (elem.Value.Fill.Equals(Brushes.Red))
                        {
                            if (fi == 0 && fj == 0)
                            {
                                fi = i;
                                fj = j;
                            }
                            else
                            {
                                sj = j;
                                si = i;
                            }

                            break;
                        }
                    }
                }
            }
            FindWave(fi,fj,si,sj);
        }

        //Поиск короткого пути и его восстановление волновым алгоритмом
        public static void FindWave(int startX, int startY, int targetX, int targetY)
        {
            var copyMap = new int[SizeOfField + 2, SizeOfField + 2];
            var step = 0;
            var nextstep = true;

            for (var x = 0; x < SizeOfField + 2; x++)
            {
                for (var y = 0; y < SizeOfField + 2; y++)
                {
                    if (_map[x, y] == -1) copyMap[x, y] = -2; //Индикатор стены в копии карты
                    else copyMap[x, y] = -1; //Индикатор клетки, в которую еще не входили
                }
            }
            copyMap[startX, startY] = 0;

            while (nextstep)
            {
                var stepcells = false; //Проверяем, был ли сделан ход в какую-либо клетку на текущем шаге цикла

                for (var x = 0; x < SizeOfField + 2; x++)
                {
                    for (var y = 0; y < SizeOfField + 2; y++)
                    {
                        Rectangle rect;
                        if (!copyMap[x, y].Equals(step)) continue;

                        if (y - 1 >= 0 && copyMap[x - 1, y] != -2 && copyMap[x - 1, y] == -1)
                        {
                            copyMap[x - 1, y] = step + 1;
                            stepcells = true;
                        }
                        if (x - 1 >= 0 && copyMap[x, y - 1] != -2 && copyMap[x, y - 1] == -1)
                        {
                            copyMap[x, y - 1] = step + 1;
                            stepcells = true;

                        }
                        if (y + 1 < SizeOfField + 2 && copyMap[x + 1, y] != -2 && copyMap[x + 1, y] == -1)
                        {
                            copyMap[x + 1, y] = step + 1;
                            stepcells = true;

                        }
                        if (x + 1 < SizeOfField + 2 && copyMap[x, y + 1] != -2 && copyMap[x, y + 1] == -1)
                        {
                            copyMap[x, y + 1] = step + 1;
                            stepcells = true;
                        }
                    }
                }

                step++;

                if (copyMap[targetX, targetY] != -1)
                {
                    nextstep = false;

                    if (step == 1)
                    {
                        MessageBox.Show("Клетки являются соседними", "Результат", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        return;
                    }
                }
                
                if (stepcells == false)
                {
                    nextstep = false;
                    MessageBox.Show("Решение не найдено", "Результат", MessageBoxButton.OK);
                    return;
                }
            }
            
            //Восстанавливаем самый короткий путь для его отрисовки
            var endOfway = false;

            while (!endOfway)
            {
                Rectangle rect;

                if (targetY - 1 >= 0 && copyMap[targetX - 1, targetY] != -2 && copyMap[targetX, targetY] - copyMap[targetX - 1, targetY] == 1)
                {
                    targetX--;
                    var index = (targetX - 1) + "0" + (targetY - 1);
                    partsOffield.TryGetValue(index, out rect);
                    if (rect != null && rect.Fill.Equals(Brushes.Transparent)) rect.Fill = Brushes.Yellow;
                    goto checknextstep;
                }

                if (targetX - 1 >= 0 && copyMap[targetX, targetY - 1] != -2 && copyMap[targetX, targetY] - copyMap[targetX, targetY - 1] == 1)
                {
                    targetY--;
                    var index = (targetX - 1) + "0" + (targetY - 1);
                    partsOffield.TryGetValue(index, out rect);
                    if (rect != null && rect.Fill.Equals(Brushes.Transparent)) rect.Fill = Brushes.Yellow;
                    goto checknextstep;
                }

                if (targetY + 1 < SizeOfField + 2 && copyMap[targetX + 1, targetY] != -2 && copyMap[targetX, targetY] - copyMap[targetX + 1, targetY] == 1)
                {
                    targetX++;
                    var index = (targetX-1) + "0" + (targetY - 1);
                    partsOffield.TryGetValue(index, out rect);
                    if (rect != null && rect.Fill.Equals(Brushes.Transparent)) rect.Fill = Brushes.Yellow;
                    goto checknextstep;

                }
                
                if (targetX + 1 < SizeOfField + 2 && copyMap[targetX, targetY + 1] != -2 && copyMap[targetX, targetY] - copyMap[targetX, targetY + 1] == 1)
                {
                    targetY++;
                    var index = targetX - 1 + "0" + (targetY-1);
                    partsOffield.TryGetValue(index, out rect);
                    if (rect != null && rect.Fill.Equals(Brushes.Transparent)) rect.Fill = Brushes.Yellow;
                }

                checknextstep:
                
                if (targetX.Equals(startX) && targetY.Equals(startY)) endOfway = true;
            }

            MessageBox.Show("Минимальное количество шагов до конца пути: " + (step-1), "Результат", MessageBoxButton.OK);
        }

        //Метод, проверяющий количество свободных ячеек
        public static int CheckForCells()
        {
            var count = 0;
            foreach (var elem in partsOffield)
            {
                if (elem.Value.Fill.Equals(Brushes.Transparent) || elem.Value.Fill.Equals(Brushes.Black)) count++;
            }
            return count;
        }

    }
}

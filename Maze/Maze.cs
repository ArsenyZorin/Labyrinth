using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTaskMitsar
{
    public partial class Labyrinth : Form
    {

        string sizeofFieldValue = "";
        public Labyrinth()
        {
            InitializeComponent();
            SizeOfField_textBox.Text = "10";
            var fl = new Field(this);
            FieldWPF.Child = fl;
        }
        
        //Метод, рассчитывающий размеры квадрата
        public int SizeOfSq(out int fieldSize)
        {
            var width = FieldWPF.Width - 20;
            var height = FieldWPF.Height - 10;

            var widthOfSq = 0;
            fieldSize = int.Parse(SizeOfField_textBox.Text);

            if (width < height) widthOfSq = width/fieldSize;
            else widthOfSq = height/fieldSize;

            if (widthOfSq < 5) widthOfSq = 5;

            return widthOfSq;
        }

        //Событие, которое отслеживает, что в textBox вписано только целое и положительное число
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox) sender;
            var text = textBox.Text;
            var tempText = "";

            for (var i = 0; i < text.Length; i++)
            {

                var ch = text[i];
                if (ch < 58 && ch > 47)
                {
                    tempText += ch;
                }

            }

            var selStart = textBox.SelectionStart;

            textBox.Text = tempText;

            textBox.Select(selStart > textBox.Text.Length ? textBox.Text.Length : selStart, 0);

        }

        //Событие, определяющее значение TextBox при его активации. Нужно, чтобы проверить изменялось ли значение
        private void SizeOfField_textBox_Enter(object sender, EventArgs e)
        {
            sizeofFieldValue = SizeOfField_textBox.Text;
        }

        private void SizeOfField_textBox_Leave(object sender, EventArgs e)
        {
            if (SizeOfField_textBox.Text == "")
            {
                MessageBox.Show("Введите размер поля для продолжения", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var value = int.Parse(SizeOfField_textBox.Text);

            if (value < 10 || value > 50)
            {
                MessageBox.Show("Минимальный размер поля - 10х10, максимальный размер поля - 50х50", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SizeOfField_textBox.Text = "10";
                return;
            }

            var fl = new Field(this);

            if (!SizeOfField_textBox.Text.Equals(sizeofFieldValue)) fl.ClearAll();                     
            FieldWPF.Child = fl;
        }

        //Расстановка препятствий
        private void CreateObstaclesButton_Click(object sender, EventArgs e)
        {
            var cells = Field.CheckForCells();
            if (AmountOfObstacles.Text == "" || SizeOfField_textBox.Text == "")
            {
                MessageBox.Show("Заполните все ячейки для продолжения", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            var value = int.Parse(SizeOfField_textBox.Text);
            var amount = int.Parse(AmountOfObstacles.Text);

            if (amount > Math.Pow(value, 2.0) || amount > cells)
            {
                MessageBox.Show("Недостаточно клеток для установки такого количества препятствий",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (amount > 2000)
            {
                MessageBox.Show("Количество препятствий должно быть меньше 2 000", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Field.CreateObstacles(value, amount);
        }

        //Нахождение кратчайшего пути
        private void FindPath_Click(object sender, EventArgs e)
        {
            Field.ReadMap();
        }

        //Очистка поля
        private void ClearAll_Click(object sender, EventArgs e)
        {
            var field = new Field(this);
            field.ClearAll();
            FieldWPF.Child = null;
            AmountOfObstacles.Text = "";
            FieldWPF.Child = field;
        }

        
    }
}

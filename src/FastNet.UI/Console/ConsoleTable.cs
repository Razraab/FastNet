using System.Reflection;
using System.Text;

namespace FastNet.UI.Console
{
    internal class ConsoleTable
    {
        private List<List<string?>> table = new List<List<string?>>();
        private int PropertyCount { get; set; }
        public int MaxLength { get; private set; }

        public ConsoleTable(int maxLength)
        {
            MaxLength = maxLength;
        }

        private string FormatString(string str)
        {
            if (str.Length > MaxLength)
            {
                return str.Substring(0, MaxLength - 2) + "..";
            }
            else if (str.Length < MaxLength)
            {
                int n = (MaxLength - str.Length) / 2;
                str = str.PadLeft(str.Length + n).PadRight(str.Length + n * 2);
                if (str.Length < MaxLength) str += ' ';
            }
            return str;
        }

        private void SetRow(int row, string[] data)
        {
            if (table.Count > row)
            {
                foreach (string str in data)
                {
                    table[row].Add(FormatString(str));
                }
                return;
            }
            table.Add(new List<string?>());
            SetRow(row, data);
        }

        public void SetData<T>(IEnumerable<T> data)
        {
            Type type = typeof(T);
            // Get type property names to set the table header
            string[] names = type.GetProperties().Select(p => p.Name).ToArray();
            PropertyCount = type.GetProperties().Length;
            SetRow(0, names);
            int i = 1;
            foreach (T item in data)
            {
                string[] row = new string[PropertyCount];
                int j = 0;
                foreach (string name in names)
                {
                    string? value = type.GetProperty(name)?.GetValue(item)?.ToString();
                    if (value != null) row[j++] = value;
                }
                SetRow(i++, row);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < table.Count; row++)
            {
                for (int col = 0; col < table[0].Count; col++)
                {
                    sb.Append(string.Concat(" | ", table[row][col]));
                }
                sb.Append(" |\n");
                if (row == 0)
                    sb.AppendLine(" ".PadRight(MaxLength * PropertyCount + 4 * PropertyCount - 1, '-'));
            }
            return sb.ToString();
        }
    }
}

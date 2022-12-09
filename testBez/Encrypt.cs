using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class Encode
    {
        /// <summary>
        /// Шифр Цезаря
        /// </summary>
        /// <param name="alf">Алфавит</param>
        /// <param name="str">Строка для работы</param>
        /// <param name="step">Шаг</param>
        /// <returns></returns>
        public static string Cesar(string alf, string str, int step)
        {
            int c;

            string codeSh = "";
            for (int i = 0; i < str.Length; i++)
                for (int j = 0; j < alf.Length; j++)
                    if (alf[j] == str[i])
                    {
                        c = j;
                        c += step;
                        if (c >= alf.Length)
                            c -= alf.Length;
                        codeSh += alf[c].ToString();
                    }
            return codeSh;
        }

        /// <summary>
        /// Шифр Атбаш
        /// </summary>
        /// <param name="alf">Алфавит</param>
        /// <param name="str">Строка для работы</param>
        /// <returns></returns>
        public static string Atbash(string alf, string str)
        {
            string ins = "";
            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < alf.Length; j++)
                {
                    if (str[i] == alf[j])
                    {
                        ins += alf[alf.Length - j - 1];
                    }
                }
            }
            return ins;
        }

        /// <summary>
        /// Зашифровка пропорционального шифра
        /// </summary>
        /// <param name="alf">Алфавит</param>
        /// <param name="str">Строка для шифровки</param>
        /// <returns></returns>
        public static string ProportionEncryption(Dictionary<char, int[]> alf, string str)
        {
            var countWord = new Dictionary<char, int[]>(alf);
            foreach (var word in countWord.Keys)
                countWord[word] = new int[] { 0 };
            string line = "";
            int c = 0;
            for (int i = 0; i < str.Length; i++)
            {
                //var b = str[i];
                foreach (var word in alf.Keys)
                    if (word == str[i])
                    {
                        c = alf[word][countWord[word][0]];

                        countWord[word][0]++;
                        if (countWord[word][0] == alf[word].Length)
                            countWord[word][0] -= alf[word].Length;

                        if (c < 99)
                            line += '0'.ToString() + c.ToString();
                        else
                            line += c.ToString();
                    }
            }
            return line;
        }

        /// <summary>
        /// Расшифровка пропорционального шифра
        /// </summary>
        /// <param name="alf">Алфавит</param>
        /// <param name="str">Строка для расшифровки</param>
        /// <returns></returns>
        public static string ProportionDecryption(Dictionary<char, int[]> alf, string str)
        {
            string code;
            string line = "";
            //int c = 0;
            string codeWordStr;
            for (int i = 0; i < str.Length; i += 3)
            {
                code = str[i].ToString() + str[i + 1].ToString() + str[i + 2].ToString();
                foreach (var word in alf.Keys)
                    foreach (var codeWord in alf[word])
                    {
                        if (codeWord < 99)
                            codeWordStr = '0'.ToString() + codeWord.ToString();
                        else
                            codeWordStr = codeWord.ToString();
                        if (codeWordStr == code)
                        {
                            line += word;
                            break;
                        }
                    }
            }
            return line;

        }

        /// <summary>
        /// Шифр Вернама
        /// </summary>
        /// <param name="alf">Алфавит</param>
        /// <param name="str">Строка для обработки</param>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public static string Vernam(string alf, string str, string key)
        {
            var alfCode = new int[alf.Length];

            for (int i = 0; i < alf.Length; i++)
                alfCode[i] = i;

            var line = "";

            var keyCode = new int[key.Length];
            for (int k = 0; k < key.Length; k++)
                for (int i = 0; i < alf.Length; i++)
                    if (alf[i] == key[k])
                        keyCode[k] = i;

            var lineCode = new int[str.Length];

            for (var i = 0; i < str.Length; i++)
            {
                for (var j = 0; j < alf.Length; j++)
                    if (str[i] == alf[j])
                    {
                        lineCode[i] = j ^ keyCode[i % key.Length];
                        line += alf[lineCode[i]];
                    }
            }

            return line;
        }

        /// <summary>
        /// Зашифровка шифром Вижинера
        /// </summary>
        /// <param name="alf">Алфавит</param>
        /// <param name="str">Строка для зашифровки</param>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public static string ViginereEncryption(string alf, string str, string key)
        {
            var line = "";

            var alfCode = new char[alf.Length, alf.Length];
            for (int i = 0; i < alfCode.GetLength(1); i++)
                for (int j = 0; j < alfCode.GetLength(0); j++)
                    alfCode[i, j] = alf[(i + j) % alf.Length];

            int keyLength = key.Length;
            for (int i = 0; key.Length < str.Length; i++)
                if (i < key.Length)
                    continue;
                else
                    key += key[i % keyLength];

            for (int k = 0; k < str.Length; k++)
                for (int i = 0; i < alf.Length; i++)
                {
                    if (alfCode[i, 0] == key[k])
                        for (int j = 0; j < alf.Length; j++)
                        {
                            if (alfCode[0, j] == str[k])
                            {
                                line += alfCode[i, j].ToString();
                            }
                        }
                }
            return line;
        }

        /// <summary>
        /// Расшифровка шифра Вижинера
        /// </summary>
        /// <param name="alf">Алфавит</param>
        /// <param name="str">Строка для расшифровки</param>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public static string ViginereDecryption(string alf, string str, string key)
        {
            var line = "";

            var alfCode = new char[alf.Length, alf.Length];
            for (int i = 0; i < alfCode.GetLength(1); i++)
                for (int j = 0; j < alfCode.GetLength(0); j++)
                    alfCode[i, j] = alf[(i + j) % alf.Length];

            int keyLength = key.Length;
            for (int i = 0; key.Length < str.Length; i++)
                if (i < key.Length)
                    continue;
                else
                    key += key[i % keyLength];

            for (int k = 0; k < str.Length; k++)
                for (int i = 0; i < alf.Length; i++)
                {
                    if (alfCode[i, 0] == key[k])
                        for (int j = 0; j < alf.Length; j++)
                        {
                            if (alfCode[i, j] == str[k])
                            {
                                line += alfCode[0, j].ToString();
                            }
                        }
                }
            return line;
        }

        /// <summary>
        /// Зашифровка шифром Плейфейера 
        /// </summary>
        /// <param name="alf">Алфавит</param>
        /// <param name="str">Строка для зашифровки</param>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public static string PlayfairEncryption(string alf, string str, string key)
        {
            for(var i = 0; i < key.Length; i++)
                for (var j = i + 1; j < key.Length; j++)
                    if (key[i] == key[j])
                        key = key.Remove(j, 1);
            
            var matrix = Matrix(alf, key);
            var bigramms = Bigramms(str);

            var bigrammCode = new List<string>();

            var corner = new[] { new int[] { 0, 0 }, new int[] { 0, 0 } };

            foreach (var bigram in bigramms)
            {
                for (int i = 0; i < matrix.GetLength(1); i++)
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (matrix[i, j] == bigram[0])
                        {
                            corner[0][0] = i;
                            corner[0][1] = j;
                        }
                        if (matrix[i, j] == bigram[1])
                        {
                            corner[1][0] = i;
                            corner[1][1] = j;
                        }
                    }
                // По столбцам
                if (corner[0][1] == corner[1][1])
                {
                    corner[0][0]++;
                    corner[1][0]++;
                    if (corner[0][0] == matrix.GetLength(0))
                        corner[0][0] = 0;
                    if (corner[1][0] == matrix.GetLength(0))
                        corner[1][0] = 0;
                    bigrammCode.Add(matrix[corner[0][0], corner[0][1]].ToString() +
                                    matrix[corner[1][0], corner[1][1]].ToString());
                }
                else
                // По строкам
                if (corner[0][0] == corner[1][0])
                {
                    corner[0][1]++;
                    corner[1][1]++;
                    if (corner[0][1] == matrix.GetLength(1))
                        corner[0][1] = 0;
                    if (corner[1][1] == matrix.GetLength(1))
                        corner[1][1] = 0;
                    bigrammCode.Add(matrix[corner[0][0], corner[0][1]].ToString() +
                                    matrix[corner[1][0], corner[1][1]].ToString());
                }
                else
                // Прямоугольник
                {
                    /*
                     *      A       *   
                     * 
                     *      *       B
                     */
                    if (corner[0][0] < corner[1][0] && corner[0][1] < corner[1][1])
                    {
                        bigrammCode.Add(matrix[corner[0][0], corner[1][1]].ToString() +
                                        matrix[corner[1][0], corner[0][1]].ToString());
                    }

                    /*
                     *      *       A   
                     * 
                     *      B       *
                     */
                    if (corner[0][0] < corner[1][0] && corner[0][1] > corner[1][1])
                    {
                        bigrammCode.Add(matrix[corner[1][0], corner[0][1]].ToString() +
                                        matrix[corner[0][0], corner[1][1]].ToString());
                    }

                    /*
                     *      B       *   
                     * 
                     *      *       A
                     */
                    if (corner[1][0] < corner[0][0] && corner[1][1] < corner[0][1])
                    {
                        bigrammCode.Add(matrix[corner[0][0], corner[1][1]].ToString() +
                                        matrix[corner[1][0], corner[0][1]].ToString());
                    }

                    /*
                     *      *       B   
                     * 
                     *      A       *
                     */
                    if (corner[1][0] < corner[0][0] && corner[1][1] > corner[0][1])
                    {
                        bigrammCode.Add(matrix[corner[1][0], corner[0][1]].ToString() +
                                        matrix[corner[0][0], corner[1][1]].ToString());
                    }
                }
            }

            var result = new StringBuilder();
            foreach (var word in bigrammCode)
                result.Append(word.ToString());
            var line = result.ToString();
            return line; ;
        }

        /// <summary>
        /// Расшифровка шифра Плейфейера
        /// </summary>
        /// <param name="alf">Алфавит</param>
        /// <param name="str">Строка для расшифровки</param>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public static string PlayfairDecryption(string alf, string str, string key)
        {
            for (var i = 0; i < key.Length; i++)
                for (var j = i + 1; j < key.Length; j++)
                    if (key[i] == key[j])
                        key = key.Remove(j, 1);

            var matrix = Matrix(alf, key);
            var bigramms = Bigramms(str);

            var bigrammCode = new List<string>();

            var corner = new[] { new int[] { 0, 0 }, new int[] { 0, 0 } };

            foreach (var bigram in bigramms)
            {
                for (int i = 0; i < matrix.GetLength(1); i++)
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (matrix[i, j] == bigram[0])
                        {
                            corner[0][0] = i;
                            corner[0][1] = j;
                        }
                        if (matrix[i, j] == bigram[1])
                        {
                            corner[1][0] = i;
                            corner[1][1] = j;
                        }
                    }
                // По столбцам
                if (corner[0][1] == corner[1][1])
                {
                    corner[0][0]--;
                    corner[1][0]--;
                    if (corner[1][0] == -1)
                        corner[1][0] = matrix.GetLength(0) - 1;
                    if (corner[0][0] == -1)
                        corner[0][0] = matrix.GetLength(0) - 1;
                    bigrammCode.Add(matrix[corner[0][0], corner[0][1]].ToString() +
                                    matrix[corner[1][0], corner[1][1]].ToString());
                }
                else
                // По строкам
                if (corner[0][0] == corner[1][0])
                {
                    corner[0][1]--;
                    corner[1][1]--;
                    if (corner[0][1] == -1)
                        corner[0][1] = matrix.GetLength(1) - 1;
                    if (corner[1][1] == -1)
                        corner[1][1] = matrix.GetLength(1) - 1;
                    bigrammCode.Add(matrix[corner[0][0], corner[0][1]].ToString() +
                                    matrix[corner[1][0], corner[1][1]].ToString());
                }
                else
                // Прямоугольник
                {
                    /*
                     *      A       *   
                     * 
                     *      *       B
                     */
                    if (corner[0][0] < corner[1][0] && corner[0][1] < corner[1][1])
                    {
                        bigrammCode.Add(matrix[corner[1][0], corner[0][1]].ToString() +
                                        matrix[corner[0][0], corner[1][1]].ToString());
                    }

                    /*
                     *      *       A   
                     * 
                     *      B       *
                     */
                    if (corner[0][0] < corner[1][0] && corner[0][1] > corner[1][1])
                    {
                        bigrammCode.Add(matrix[corner[0][0], corner[1][1]].ToString() +
                                        matrix[corner[1][0], corner[0][1]].ToString());
                    }

                    /*
                     *      B       *   
                     * 
                     *      *       A
                     */
                    if (corner[1][0] < corner[0][0] && corner[1][1] < corner[0][1])
                    {
                        bigrammCode.Add(matrix[corner[1][0], corner[0][1]].ToString() +
                                        matrix[corner[0][0], corner[1][1]].ToString());
                    }

                    /*
                     *      *       B   
                     * 
                     *      A       *
                     */
                    if (corner[1][0] < corner[0][0] && corner[1][1] > corner[0][1])
                    {
                        bigrammCode.Add(matrix[corner[0][0], corner[1][1]].ToString() +
                                        matrix[corner[1][0], corner[0][1]].ToString());
                    }
                }
            }


            var result = new StringBuilder();
            foreach (var word in bigrammCode)
                result.Append(word.ToString());
            var line = result.ToString();

            for (var i = 1; i < line.Length - 1; i++)
                if (line[i] == 'Ъ')
                    if (line[i - 1] == line[i + 1])
                        line = line.Remove(i, 1);
            if (line[line.Length - 1] == 'Ъ')
                line = line.Remove(line.Length - 1, 1);

            return line;
        }

        /// <summary>
        /// Не лезь, убьёт!
        /// </summary>
        /// <param name="alf"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static char[,] Matrix(string alf, string key)
        {
            var matrix = new char[6, 6];

            for (int j = 0; j < key.Length; j++)
                matrix[j / 6, j % 6] = key[j];

            int k = 0;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        while (k < alf.Length)
                            if (!key.Contains(alf[k]))
                            {
                                matrix[i, j] = alf[k];
                                k++;
                                break;
                            }
                            else
                                k++;
                    }
                }
            return matrix;
        }

        /// <summary>
        /// Не лезь, убьёт!
        /// </summary>
        /// <param name="alf"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string[] Bigramms(string str)
        {
            var bidrammsList = new List<string>();

            int i = 0;
            while (true)
            {
                if (i < str.Length)
                {
                    if (i + 1 == str.Length || str[i] == str[i + 1])
                    {
                        // Ъ в качестве знака дубля
                        bidrammsList.Add(str[i].ToString() + 'Ъ');
                        i++;
                        continue;
                    }
                    else
                    {
                        bidrammsList.Add(str.Substring(i, 2));
                        i += 2;
                        continue;
                    }
                }
                else
                    break;
            }
            return bidrammsList.ToArray();
        }

        /// <summary>
        /// Зашифровка строки гаммированием
        /// </summary>
        /// <param name="alf">Алфавит</param>
        /// <param name="str">Строка для зашифровки</param>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public static string GammaEncryption(string alf, string str, string key)
        {
            var a = "";
            a += alf[alf.Length - 1];
            for (var i = 1; i < alf.Length; i++)
                a += alf[i - 1];
            alf = a;
            
            var alfCode = new int[alf.Length];
            var keyCode = new int[key.Length];
            var line = "";

            for (var i = 0; i < alf.Length; i++)
            {
                alfCode[i] = i;
                for (var j = 0; j < key.Length; j++)
                    if (key[j] == alf[i])
                        keyCode[j] = i;
            }

            var c = 0;
            for (var i = 0; i < str.Length; i++)
                for (var j = 0; j < alf.Length; j++)
                {
                    if (str[i] == alf[j])
                    {
                        c = (j + keyCode[i % key.Length]) % alf.Length;
                        line += alf[c];
                    }
                }

            return line;
        }

        /// <summary>
        /// Расшифровка строки гаммированием
        /// </summary>
        /// <param name="alf">Алфавит</param>
        /// <param name="str">Строка для расшифровки</param>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public static string GammaDecryption(string alf, string str, string key)
        {
            var a = "";
            a += alf[alf.Length - 1];
            for (var i = 1; i < alf.Length; i++)
                a += alf[i - 1];
            alf = a;

            var alfCode = new int[alf.Length];
            var keyCode = new int[key.Length];
            var line = "";

            for (var i = 0; i < alf.Length; i++)
            {
                alfCode[i] = i;
                for (var j = 0; j < key.Length; j++)
                    if (key[j] == alf[i])
                        keyCode[j] = i;
            }

            var c = 0;
            for (var i = 0; i < str.Length; i++)
                for (var j = 0; j < alf.Length; j++)
                    if (str[i] == alf[j])
                    {
                        c = j - keyCode[i % key.Length];
                        if (c < 0)
                            c = alf.Length + c;
                        line += alf[c];
                    }

            return line;
        }

        /// <summary>
        /// Зашифрвка с фиксированным периодом
        /// </summary>
        /// <param name="str">Строка для зашифровки</param>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public static string PermutationEncryption(string str, string key)
        {
            var line = "";

            int k = 0;
            var s = "";

            while (str.Length % 6 != 0)
                str += ' ';

            while (line.Length < str.Length)
            {
                for (var i = 0; i < 6; i++)
                    if (k + i < str.Length)
                        s += str[k + i];
                k += 6;

                for (var i = 0; i < 6; i++)
                    for (var j = 0; j < 6; j++)
                        if (key[i].ToString() == (j + 1).ToString())
                            line += s[j];
                s = "";
            }

            var c = 0;
            while (c < line.Length)
            {
                if (line[c] == ' ')
                    line = line.Remove(c, 1);
                else
                    c++;
            }

            return line;
        }

        /// <summary>
        /// Расшифровка  с фиксированным периодом
        /// </summary>
        /// <param name="str">Строка для зашифровки</param>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        public static string PermutationDecryption(string str, string key)
        {
            var line = "";

            int k = 0;
            var s = "";

            while (str.Length % 6 != 0)
                str += ' ';

            while (line.Length < str.Length)
            {
                for (var i = 0; i < 6; i++)
                    if (k + i < str.Length)
                        s += str[k + i];
                k += 6;

                for (var i = 0; i < 6; i++)
                    for (var j = 0; j < 6; j++)
                        if (key[j].ToString() == (i + 1).ToString())
                            line += s[j];
                s = "";
            }

            var c = 0;
            while (c < line.Length)
            {
                if (line[c] == ' ')
                    line = line.Remove(c, 1);
                else
                    c++;
            }

            return line;
        }

        /// <summary>
        /// Расшифровка перестановкой по таблице
        /// </summary>
        /// <param name="str">Строка для расшифровки</param>
        /// <param name="wight">Ширина таблицы</param>
        /// <param name="height">Высота таблицы</param>
        /// <returns></returns>
        public static string TableDecryption(string str, int wight, int height)
        {
            var line = "";
            var i = 0;
            var j = 0;
            var f = 0;
            var table = new char[height, wight];
            var end = str.Length % wight;

            for (var k = 0; k < str.Length; k++)
            {
                table[i, j] = str[k];
                i++;
                if (i == height)
                {
                    i = 0;
                    j++;
                }
                if (j == end && f == 0)
                {
                    height--;
                    f = 1;
                }
            }

            height++;
            for (var k = 0; k < height; k++)
                for (var l = 0; l < wight; l++)
                    if (table[k, l] != 0)
                        line += table[k, l];

            return line;
        }

        /// <summary>
        /// Зашифровка перестановкой по таблице
        /// </summary>
        /// <param name="str">Строка для расшифровки</param>
        /// <param name="wight">Ширина таблицы</param>
        /// <param name="height">Высота таблицы</param>
        /// <returns></returns>
        public static string TableEncryption(string str, int wight, int height)
        {
            var line = "";
            var i = 0;
            var j = 0;
            var f = 0;
            var k = 0;
            var table = new char[height, wight];

            while (k < str.Length)
            {
                table[i, j] = str[k];
                k++;
                j++;
                if (j == wight)
                {
                    j = 0;
                    i++;
                }
            }

            for (i = 0; i < wight; i++)
                for (j = 0; j < height; j++)
                    if (table[j, i] != 0)
                        line += table[j, i];
            return line;
        }
    }
}

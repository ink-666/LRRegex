using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RegexLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №6: РЕГУЛЯРНЫЕ ВЫРАЖЕНИЯ");
            Console.WriteLine("==========================================\n");

            // ==========================================================
            // ЧАСТЬ 1: ЗАГРУЗКА ТЕКСТА ИЗ ФАЙЛА
            // ==========================================================
            Console.WriteLine("📁 ЧАСТЬ 1: ЗАГРУЗКА ТЕКСТА");
            Console.WriteLine("==========================================\n");

            string filePath = "text.txt";
            
            // Проверяем, существует ли файл
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"❌ Файл {filePath} не найден!");
                Console.WriteLine("Создайте файл text.txt с текстом для анализа.");
                return;
            }

            // Читаем весь текст из файла
            string text = File.ReadAllText(filePath);
            string[] lines = File.ReadAllLines(filePath);
            
            Console.WriteLine($"📄 Загружен текст из файла: {filePath}");
            Console.WriteLine($"📊 Всего строк: {lines.Length}");
            Console.WriteLine($"📊 Всего символов: {text.Length}");
            Console.WriteLine("\n📝 Первые 100 символов текста:");
            Console.WriteLine(text.Substring(0, Math.Min(100, text.Length)) + "...\n");

            // ==========================================================
            // ЧАСТЬ 2: ПОИСК КОЛИЧЕСТВА СЛОВ
            // ==========================================================
            Console.WriteLine("\n🔍 ЧАСТЬ 2: ПОДСЧЁТ КОЛИЧЕСТВА СЛОВ");
            Console.WriteLine("==========================================");

            // Регулярное выражение для поиска слов (последовательности букв)
            string wordPattern = @"\b[а-яА-ЯёЁa-zA-Z]+\b";
            MatchCollection wordMatches = Regex.Matches(text, wordPattern);
            
            Console.WriteLine($"\n📌 Регулярное выражение: {wordPattern}");
            Console.WriteLine($"📊 Всего слов в тексте: {wordMatches.Count}");
            
            // Покажем первые 10 слов
            Console.WriteLine("\n📝 Первые 10 слов:");
            for (int i = 0; i < Math.Min(10, wordMatches.Count); i++)
            {
                Console.WriteLine($"   {i + 1}. {wordMatches[i].Value}");
            }

            // ==========================================================
            // ЧАСТЬ 3: ПОИСК ОПРЕДЕЛЁННЫХ СЛОВОСОЧЕТАНИЙ
            // ==========================================================
            Console.WriteLine("\n\n🔍 ЧАСТЬ 3: ПОИСК СЛОВОСОЧЕТАНИЙ");
            Console.WriteLine("==========================================");

            // Найдём словосочетания, где есть слово "не" с последующим словом
            string phrasePattern = @"\bне\s+[а-яА-ЯёЁ]+\b";
            MatchCollection phraseMatches = Regex.Matches(text, phrasePattern, RegexOptions.IgnoreCase);
            
            Console.WriteLine($"\n📌 Поиск словосочетаний с 'не': {phrasePattern}");
            Console.WriteLine($"📊 Найдено: {phraseMatches.Count}");
            
            foreach (Match match in phraseMatches)
            {
                Console.WriteLine($"   ➡️ {match.Value}");
            }

            // Найдём сочетания "и" между словами
            string andPattern = @"\b[а-яА-ЯёЁ]+\s+и\s+[а-яА-ЯёЁ]+\b";
            MatchCollection andMatches = Regex.Matches(text, andPattern, RegexOptions.IgnoreCase);
            
            Console.WriteLine($"\n📌 Поиск сочетаний 'слово и слово': {andPattern}");
            Console.WriteLine($"📊 Найдено: {andMatches.Count}");
            
            foreach (Match match in andMatches)
            {
                Console.WriteLine($"   ➡️ {match.Value}");
            }

            // ==========================================================
            // ЧАСТЬ 4: ПОИСК ОПРЕДЕЛЁННЫХ СИМВОЛОВ
            // ==========================================================
            Console.WriteLine("\n\n🔍 ЧАСТЬ 4: ПОИСК ОПРЕДЕЛЁННЫХ СИМВОЛОВ");
            Console.WriteLine("==========================================");

            // Подсчитаем количество знаков препинания
            string punctuationPattern = @"[.,;:!?-]";
            MatchCollection punctuationMatches = Regex.Matches(text, punctuationPattern);
            
            Console.WriteLine($"\n📌 Знаки препинания: {punctuationPattern}");
            Console.WriteLine($"📊 Всего знаков препинания: {punctuationMatches.Count}");
            
            // Подсчитаем количество гласных букв
            string vowelsPattern = @"[аеёиоуыэюяАЕЁИОУЫЭЮЯ]";
            MatchCollection vowelsMatches = Regex.Matches(text, vowelsPattern);
            
            Console.WriteLine($"\n📌 Гласные буквы: {vowelsPattern}");
            Console.WriteLine($"📊 Всего гласных: {vowelsMatches.Count}");
            
            // Подсчитаем количество согласных
            string consonantsPattern = @"[бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ]";
            MatchCollection consonantsMatches = Regex.Matches(text, consonantsPattern);
            
            Console.WriteLine($"\n📌 Согласные буквы: {consonantsPattern}");
            Console.WriteLine($"📊 Всего согласных: {consonantsMatches.Count}");

            // ==========================================================
            // ЧАСТЬ 5: СТРОКИ, НАЧИНАЮЩИЕСЯ С ОПРЕДЕЛЁННОГО СЛОВА
            // ==========================================================
            Console.WriteLine("\n\n🔍 ЧАСТЬ 5: СТРОКИ, НАЧИНАЮЩИЕСЯ С 'Мой'");
            Console.WriteLine("==========================================");

            string startPattern = @"^Мой.*";
            Console.WriteLine($"📌 Регулярное выражение: {startPattern}\n");

            int foundStartLines = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], startPattern, RegexOptions.IgnoreCase))
                {
                    Console.WriteLine($"   Строка {i + 1}: {lines[i]}");
                    foundStartLines++;
                }
            }
            
            if (foundStartLines == 0)
            {
                Console.WriteLine("   Строк, начинающихся с 'Мой', не найдено.");
            }

            // ==========================================================
            // ЧАСТЬ 6: СТРОКИ, ОКАНЧИВАЮЩИЕСЯ ОПРЕДЕЛЁННЫМ СИМВОЛОМ
            // ==========================================================
            Console.WriteLine("\n\n🔍 ЧАСТЬ 6: СТРОКИ, ОКАНЧИВАЮЩИЕСЯ НА ';' ИЛИ ','");
            Console.WriteLine("==========================================");

            string endPattern = @"[;,]";
            Console.WriteLine($"📌 Регулярное выражение: {endPattern}$\n");

            int foundEndLines = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i].Trim(), endPattern + "$"))
                {
                    Console.WriteLine($"   Строка {i + 1}: {lines[i]}");
                    foundEndLines++;
                }
            }
            
            if (foundEndLines == 0)
            {
                Console.WriteLine("   Строк, оканчивающихся на ';' или ',', не найдено.");
            }

            // ==========================================================
            // ЧАСТЬ 7: ЗАМЕНА ЧАСТИ ТЕКСТА
            // ==========================================================
            Console.WriteLine("\n\n✏️ ЧАСТЬ 7: ЗАМЕНА ЧАСТИ ТЕКСТА");
            Console.WriteLine("==========================================");

            // Заменим все слова "не" на "НИ"
            string oldPattern = @"\bне\b";
            string replacement = "НИ";
            
            string modifiedText = Regex.Replace(text, oldPattern, replacement, RegexOptions.IgnoreCase);
            
            Console.WriteLine($"📌 Замена: '{oldPattern}' → '{replacement}'\n");
            Console.WriteLine("📝 Текст после замены (первые 200 символов):");
            Console.WriteLine(modifiedText.Substring(0, Math.Min(200, modifiedText.Length)) + "...");

            // ==========================================================
            // ЧАСТЬ 8: ДОПОЛНИТЕЛЬНЫЕ ПРИМЕРЫ
            // ==========================================================
            Console.WriteLine("\n\n✨ ЧАСТЬ 8: ДОПОЛНИТЕЛЬНЫЕ ПРИМЕРЫ");
            Console.WriteLine("==========================================");

            // Поиск слов, начинающихся с большой буквы
            string capitalPattern = @"\b[А-ЯЁ][а-яё]*\b";
            MatchCollection capitalMatches = Regex.Matches(text, capitalPattern);
            
            Console.WriteLine($"\n📌 Слова с большой буквы: {capitalPattern}");
            Console.WriteLine($"📊 Найдено: {capitalMatches.Count}");
            
            // Покажем первые 5
            for (int i = 0; i < Math.Min(5, capitalMatches.Count); i++)
            {
                Console.WriteLine($"   {i + 1}. {capitalMatches[i].Value}");
            }

            // Поиск слов, состоящих ровно из 5 букв
            string fiveLetterPattern = @"\b[а-яА-ЯёЁ]{5}\b";
            MatchCollection fiveLetterMatches = Regex.Matches(text, fiveLetterPattern);
            
            Console.WriteLine($"\n📌 Слова из 5 букв: {fiveLetterPattern}");
            Console.WriteLine($"📊 Найдено: {fiveLetterMatches.Count}");
            
            foreach (Match match in fiveLetterMatches)
            {
                Console.WriteLine($"   ➡️ {match.Value}");
            }

            // ==========================================================
            // ИТОГИ
            // ==========================================================
            Console.WriteLine("\n" + "=".PadRight(60, '='));
            Console.WriteLine("📊 ИТОГОВАЯ СТАТИСТИКА");
            Console.WriteLine("=".PadRight(60, '='));

            Console.WriteLine($"\n📄 Всего строк в тексте: {lines.Length}");
            Console.WriteLine($"📝 Всего символов: {text.Length}");
            Console.WriteLine($"🔤 Всего слов: {wordMatches.Count}");
            Console.WriteLine($"🔣 Знаков препинания: {punctuationMatches.Count}");
            Console.WriteLine($"🅰️ Гласных букв: {vowelsMatches.Count}");
            Console.WriteLine($"🅱️ Согласных букв: {consonantsMatches.Count}");
            
            Console.WriteLine("\n" + "=".PadRight(60, '='));
            Console.WriteLine("✅ ПРОГРАММА ЗАВЕРШЕНА");
            Console.WriteLine("=".PadRight(60, '='));
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Lb5_9
{
    public struct ValueStruct
    {
        public int Xo;
        public int Xn;
        public int XStep;
        public int Yo;
        public int Yn;
        public int YCount;
    }

    public class Calculator
    {
        public List<ValueStruct> Values = new List<ValueStruct>(10);


        public void Calculate()
        {
            if (Values.Count == 0) return;

            int pad = 5;//значение формата(по 5 символов c пробелами на каждое значение)
            StreamWriter wLog = new StreamWriter(Environment.CurrentDirectory + "\\myProgram.log");
            StreamWriter wErr = new StreamWriter(Environment.CurrentDirectory + "\\myErrors.log");
            wLog.WriteLine("Программа: Lb5_9; Вариант 9");
            wLog.WriteLine(DateTime.Now);
            wLog.WriteLine("2^(x/y^(1/2))");

            int numFile = 1;
            foreach (var set in Values)
            {
                using (StreamWriter wData = new StreamWriter(Environment.CurrentDirectory + "\\G" + numFile + ".dat"))
                {
                    wLog.WriteLine("G" + numFile + ".dat");

                    double[] xArr = new double[(set.Xn - set.Xo) / set.XStep + 1];//самый неуверенный расчёт в текущих 2 абзацах
                    xArr[0] = set.Xo;
                    xArr[xArr.Length - 1] = set.Xn;
                    for (int i = 1; i < xArr.Length - 1; i++)
                        xArr[i] = xArr[i - 1] + set.XStep;

                    double[] yArr = new double[set.YCount];
                    yArr[0] = set.Yo;
                    yArr[yArr.Length - 1] = set.Yn;
                    for (int i = 1; i < yArr.Length - 1; i++)
                        yArr[i] = yArr[i - 1] + (set.Yn - set.Yo) / (set.YCount - 2);//расчет шага - (set.Yn - set.Yo)/set.YCount

                    string wr = ("x|y").PadRight(pad); //первая строка с Y значениями | PadRight(pad) - добиваем нулями справа до 5 символов
                    for (int i = 0; i < yArr.Length; i++)
                        wr += "  " + yArr[i].ToString().PadRight(pad);
                    wData.WriteLine(wr);

                    foreach (double X in xArr)
                    {
                        wr = X.ToString().PadRight(pad);
                        foreach (double Y in yArr)
                        {
                            Result res = Calc(X, Y);
                            if (res.ErrMessage != null)//запись ошибок
                            {
                                wData.WriteLine("G"+numFile);
                                wData.WriteLine("2^(x/y^(1/2))");
                                wData.WriteLine("X: " + X + "Y: " + Y);
                                wData.WriteLine(res.ErrMessage + "\n");
                            }
                            wr += "  " +res.Value.PadRight(pad);
                        }
                        wData.WriteLine(wr);
                    }
                }
                numFile++;
            }

            wErr.Close();
            wLog.Close();
        }


        private struct Result
        {
            public string ErrMessage;
            public string Value;
        }
        private Result Calc(double x, double y)//расчёт ф-ии в точке
        {
            Result result = new Result();
            try
            {
                double v = Math.Round( Math.Pow(2, x / Math.Pow(y, 2)), 2);//формула + округление до 2 знаков после запятой
                //if(double.IsInfinity(v))
                //    result.Value = "Nan";
                result.Value = v.ToString();
            }
            catch(Exception ex) 
            {
                result.Value = "NaN";
                result.ErrMessage = ex.Message;
            }
            return result;
        }


        //модуль для чтения
        public void Read(string way)
        {
            if (!File.Exists(way))//файл не существует
                return;

            List<Point> points = new List<Point>(10);//в этот лист будет считан файл
            try
            {
                using (StreamReader sr = new StreamReader(way))
                {
                    double[] yArr;

                    string s = Regex.Replace(sr.ReadLine(), @"\s{2,}", ";", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);//удалим повторяющиеся пробелы
                    if (s.Last() == ';') s = s.Remove(s.Length - 1);//удалим символ разделения в конце строки(если он есть)
                    string[] ss = s.Split(';');
                    yArr = new double[ss.Length];//[0] останется пустым для удобств программиста 
                    for (int i = 1; i < ss.Length; i++)
                        yArr[i] = Convert.ToDouble(ss[i]);

                    while(!sr.EndOfStream)
                    {
                        s = Regex.Replace(sr.ReadLine(), @"\s{2,}", ";", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);//удалим повторяющиеся пробелы
                        if (s.Last() == ';') s = s.Remove(s.Length - 1);
                        ss = s.Split(';');
                        for (int i = 1; i < ss.Length; i++)
                        {
                            Point point = new Point
                            {
                                X = Convert.ToDouble(ss[0]),
                                Y = Convert.ToDouble(yArr[i]),
                                Val = Convert.ToDouble(ss[i]),
                            };
                            points.Add(point);
                        }
                    }
                }
            }
            catch/*(Exception ex)*/ {/*что-то в случае исключения*/ };
        }
        private struct Point
        {
            public double X;
            public double Y;
            public double Val;
        }
    }
}

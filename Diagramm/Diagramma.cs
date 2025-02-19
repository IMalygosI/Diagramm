using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using System;
using System.Collections.Generic;

namespace Diagramm
{
    public class Diagramma
    {
        // Параметры модели "хищник-жертва"
        private double _preyGrowthRate; // ε (биотический потенциал жертв)
        private double _predatorDeathRate; // β (коэффициент гибели хищников)
        private double _predationRate; // α (коэффициент гибели жертв от хищников)
        private double _predatorGrowthRate; // δ (коэффициент роста хищников)

        // Начальные условия для трех циклов
        private double _initialPrey1; // Начальное количество жертв (цикл 1)
        private double _initialPredator1; // Начальное количество хищников (цикл 1)
        private double _initialPrey2; // Начальное количество жертв (цикл 2)
        private double _initialPredator2; // Начальное количество хищников (цикл 2)
        private double _initialPrey3; // Начальное количество жертв (цикл 3)
        private double _initialPredator3; // Начальное количество хищников (цикл 3)

        // Данные для графиков
        private List<ObservablePoint> _phasePoints = new(); // Точки для фазового портрета
        private List<double> _preyPopulation = new(); // Данные для графика жертв
        private List<double> _predatorPopulation = new(); // Данные для графика хищников

        // Свойства для доступа к параметрам модели
        public double PreyGrowthRate => _preyGrowthRate;
        public double PredatorDeathRate => _predatorDeathRate;
        public double PredationRate => _predationRate;
        public double PredatorGrowthRate => _predatorGrowthRate;
        public double InitialPrey1 => _initialPrey1;
        public double InitialPredator1 => _initialPredator1;
        public double InitialPrey2 => _initialPrey2;
        public double InitialPredator2 => _initialPredator2;
        public double InitialPrey3 => _initialPrey3;
        public double InitialPredator3 => _initialPredator3;

        // Конструктор для инициализации модели
        public Diagramma(double preyGrowthRate, double predatorDeathRate, double predationRate, double predatorGrowthRate,
                         double initialPrey1, double initialPredator1,
                         double initialPrey2, double initialPredator2,
                         double initialPrey3, double initialPredator3)
        {
            // Инициализация параметров модели
            _preyGrowthRate = preyGrowthRate;
            _predatorDeathRate = predatorDeathRate;
            _predationRate = predationRate;
            _predatorGrowthRate = predatorGrowthRate;
            _initialPrey1 = initialPrey1;
            _initialPredator1 = initialPredator1;
            _initialPrey2 = initialPrey2;
            _initialPredator2 = initialPredator2;
            _initialPrey3 = initialPrey3;
            _initialPredator3 = initialPredator3;

            // Инициализация фазового портрета
            PhasePortraitSeries = [
                new LineSeries<ObservablePoint>
                {
                    Values = InitializePhasePortrait(),
                    Stroke = new SolidColorPaint(new SKColor(33, 150, 243), 4),
                    Fill = null,
                    GeometrySize = 0
                }
            ];

            // Инициализация графиков популяций
            InitializePopulationGraphs();

            PopulationGraphSeries = [
                new LineSeries<double> { Values = _preyPopulation },
                new LineSeries<double> { Values = _predatorPopulation }
            ];
        }

        // Свойства для привязки данных в XAML
        public ISeries[] PhasePortraitSeries { get; set; } =
            [
                new LineSeries<ObservablePoint>
                {
                }
            ];
        public ISeries[] PopulationGraphSeries { get; set; } =
            [
                new LineSeries<ObservablePoint>
                {
                }
            ];

        // Метод для расчета популяций жертв и хищников
        private void CalculatePopulation(double initialPrey, double initialPredator)
        {
            List<ObservablePoint> generation = new();
            generation.Add(new ObservablePoint()
            {
                X = initialPrey,
                Y = initialPredator
            });

            for (int i = 0; i < 150; i++)
            {
                double nextPrey = Math.Round((double)((_preyGrowthRate - _predationRate * generation[i].Y) * generation[i].X + generation[i].X), 2);
                generation.Add(new ObservablePoint()
                {
                    X = nextPrey,
                    Y = Math.Round((double)((_predatorGrowthRate * nextPrey - _predatorDeathRate) * generation[i].Y + generation[i].Y), 2)
                });
            }
            _phasePoints.AddRange(generation);
        }

        // Метод для инициализации фазового портрета
        private List<ObservablePoint> InitializePhasePortrait()
        {
            _phasePoints.Clear();

            CalculatePopulation(_initialPrey1, _initialPredator1);
            CalculatePopulation(_initialPrey2, _initialPredator2);
            CalculatePopulation(_initialPrey3, _initialPredator3);
            return _phasePoints;
        }

        // Метод для инициализации графиков популяций
        private void InitializePopulationGraphs()
        {
            _predatorPopulation.Clear();
            _preyPopulation.Clear();
            foreach (ObservablePoint point in _phasePoints)
            {
                _preyPopulation.Add((double)point.X);
                _predatorPopulation.Add((double)point.Y);
            }
        }
    }
}
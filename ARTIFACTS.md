ARTIFACTS.md

Артефакты и протоколы проекта «WPF Калькулятор»

Проект: burmagin (WPF Calculator)
Версия документа: 1.0.0
Дата: 17.09.2026
Автор: burmagin


ВВЕДЕНИЕ

Назначение документа

Документ описывает перечень артефактов проекта «WPF Калькулятор», их классификацию по типам и этапам жизненного цикла, а также протоколы взаимодействия между модулями. Документ служит единой точкой правды для команды разработчиков, упрощает онбординг, интеграцию модулей и сопровождение проекта.

Область применения

Документ применяется при командной разработке проекта, интеграции модулей, code review, подготовке релизов и рефакторинге архитектуры.

Термины и определения

Артефакт — любой материальный или информационный результат, создаваемый в ходе разработки (код, конфигурация, документация, сборки).

Протокол — соглашение о правилах и форматах взаимодействия между модулями (интерфейсы, форматы данных, события).


ПЕРЕЧЕНЬ АРТЕФАКТОВ

Проектные артефакты

README.md — документация, проектный, описание проекта, в Git: да, корень.

ARTIFACTS.md — документация, проектный, перечень артефактов и протоколов, в Git: да, корень.

ARCHITECTURE.md — документация, проектный, описание архитектуры, в Git: да, корень.

docs/modules-diagram.drawio — документация, проектный, UML-диаграмма модулей, в Git: да, docs/.

Артефакты исходного кода

App.xaml — исходный, код, точка входа и ресурсы, в Git: да, корень.

App.xaml.cs — исходный, код, логика приложения, в Git: да, корень.

MainWindow.xaml — исходный, код, разметка главного окна, в Git: да, корень.

MainWindow.xaml.cs — исходный, код, логика калькулятора, в Git: да, корень.

AssemblyInfo.cs — исходный, код, метаданные сборки, в Git: да, Properties/.

Resources.Designer.cs — исходный, код, автогенерируемый класс ресурсов, в Git: да, Properties/.

Resources.resx — ресурс, код, ресурсы приложения, в Git: да, Properties/.

Settings.Designer.cs — исходный, код, автогенерируемый класс настроек, в Git: да, Properties/.

Settings.settings — конфигурация, код, настройки приложения, в Git: да, Properties/.

Артефакты конфигурации

burmagin.csproj — конфигурационный, конфигурация, файл проекта C#, в Git: да, корень.

App.config — конфигурационный, конфигурация, конфигурация runtime, в Git: да, корень.

.gitignore — конфигурационный, конфигурация, исключения Git, в Git: да, корень.

.gitattributes — конфигурационный, конфигурация, настройки Git для типов файлов, в Git: да, корень.

Артефакты сборки (НЕ версионируются)

bin/ — производный, сборка, результаты сборки, в Git: нет, корень.

obj/ — производный, сборка, промежуточные файлы компиляции, в Git: нет, корень.

burmagin.exe — производный, сборка, исполняемый файл, в Git: нет, bin/Debug/.

burmagin.pdb — производный, сборка, отладочные символы, в Git: нет, bin/Debug/.

*.baml — производный, сборка, скомпилированный XAML, в Git: нет, obj/Debug/.

*.cache — производный, сборка, кэш сборки, в Git: нет, obj/Debug/.

Тестовые артефакты

Calculator.Tests/ — тестовый, тестовый, проект unit-тестов (планируется), в Git: да, корень.

Calculator.Tests/*.cs — тестовый, тестовый, тестовые классы, в Git: да, Calculator.Tests/.

Вспомогательные артефакты

.github/workflows/build.yml — вспомогательный, CI/CD, автосборка (планируется), в Git: да, .github/.

scripts/build.ps1 — вспомогательный, скрипт, скрипт сборки (планируется), в Git: да, scripts/.

DocumentLayout.json — служебный, VS, настройки окон VS, в Git: нет, корень.

DocumentLayout.backup.json — служебный, VS, бэкап настроек окон VS, в Git: нет, корень.

burmagin.csproj.FileListAbsolute.txt — служебный, VS, список путей сборки, в Git: нет, корень.


ПРОТОКОЛЫ ВЗАИМОДЕЙСТВИЯ МЕЖДУ МОДУЛЯМИ

Протокол ICalculatorService

Участники: View Logic — Calculation Service.

Назначение: предоставление математических операций для моделей представления.

Интерфейс:

double Calculate(double a, double b, string op);
double Evaluate(string function, double value);
double Factorial(int n);

Формат данных: примитивы C# (double, int, string).

События: не публикуются.

Ограничения: при делении на ноль выбрасывается DivideByZeroException; при некорректной функции — ArgumentException; факториал определён только для неотрицательных целых чисел.

Соглашения: метод Calculate не изменяет входные данные; метод Evaluate принимает имя функции в нижнем регистре (sin, cos, log).

Протокол ICalculatorState

Участники: View Logic — State Service.

Назначение: хранение и изменение состояния калькулятора.

Интерфейс:

string CurrentInput { get; set; }
string PreviousInput { get; set; }
string Operation { get; set; }
bool IsNewInput { get; set; }
void Reset();
void ResetEntry();

Формат данных: строки и примитивы C#.

События: не публикуются.

Ограничения: Reset() очищает все поля; ResetEntry() сбрасывает только CurrentInput; изменение свойств не вызывает исключений.

Соглашения: IsNewInput = true означает, что следующий ввод заменит текущее число; Operation хранится в виде символа (+, -, *, /, ^, mod).

Протокол IThemeService

Участники: View Logic — Theme Service — UI.

Назначение: управление цветовыми темами оформления.

Интерфейс:

string CurrentTheme { get; }
void ApplyTheme(string themeName);
event EventHandler<string> ThemeChanged;

Формат данных: строки (Light, Dark, Blue).

События: ThemeChanged — публикуется после успешной смены темы.

Ограничения: неизвестное имя темы игнорируется или вызывает ArgumentException; темы хранятся в ResourceDictionary.

Соглашения: имена тем — PascalCase (Light, Dark, Blue); цветовые ресурсы имеют префикс темы (LightBackground, DarkButtonBg).

Протокол IModeService

Участники: View Logic — Mode Service — UI.

Назначение: переключение стандартного и инженерного режимов калькулятора.

Интерфейс:

bool IsEngineeringMode { get; }
void ToggleMode();
event EventHandler<bool> ModeChanged;

Формат данных: bool.

События: ModeChanged — публикуется при переключении режима.

Ограничения: ToggleMode() инвертирует текущий режим; View подписывается на ModeChanged для изменения видимости сеток.

Соглашения: true — инженерный режим, false — стандартный; размеры окна меняются в View, а не в сервисе.


СОГЛАШЕНИЯ ОБ ИМЕНОВАНИИ И ВЕРСИОНИРОВАНИИ

Именование в коде

Классы — PascalCase (MainWindow, CalculatorService).

Интерфейсы — I + PascalCase (ICalculatorService).

Методы — PascalCase, глагол + существительное (Calculate, ApplyTheme).

Свойства — PascalCase (CurrentInput, IsNewInput).

Приватные поля — _ + camelCase (_currentInput, _themeService).

Локальные переменные — camelCase (result, previousValue).

Константы — UPPER_SNAKE_CASE (MAX_INPUT_LENGTH).

Пространства имён — PascalCase (CalculatorWPF.Services).

Именование файлов

Файлы классов — PascalCase (MainWindow.xaml, CalculatorService.cs).

Файлы конфигурации — lowercase (appsettings.json, .gitignore).

Файлы документации — UPPER_SNAKE_CASE (README.md, ARTIFACTS.md).

Диаграммы — kebab-case (modules-diagram.drawio).

Ветки Git

main — стабильная версия (production).

develop — основная ветка разработки.

feature/название — новая функциональность.

bugfix/название — исправление бага.

hotfix/название — срочное исправление в main.

release/x.y.z — подготовка релиза.

Сообщения коммитов (Conventional Commits)

Формат: тип(область): описание.

feat — новая функциональность.

fix — исправление бага.

docs — изменения в документации.

refactor — рефакторинг без изменения поведения.

test — добавление или изменение тестов.

chore — служебные изменения (сборка, CI).

style — форматирование, отступы.

perf — улучшение производительности.

Примеры:

feat(calc): add factorial function
fix(theme): correct dark theme button color
docs(readme): update installation instructions
refactor(state): extract ICalculatorState interface

Версионирование (SemVer)

Формат: MAJOR.MINOR.PATCH.

MAJOR — несовместимые изменения API.

MINOR — новая функциональность без breaking changes.

PATCH — исправления багов.

Текущая версия проекта: 1.0.0.

Теги Git: v1.0.0, v1.1.0, v2.0.0.

Версия сборки задаётся в AssemblyInfo.cs:

AssemblyVersion("1.0.0.0")
AssemblyFileVersion("1.0.0.0")


ЗАКЛЮЧЕНИЕ

Документ фиксирует полный перечень артефактов проекта «WPF Калькулятор», их классификацию по шести категориям и четыре протокола взаимодействия между модулями: ICalculatorService, ICalculatorState, IThemeService, IModeService.

Выявлены артефакты, не подлежащие версионированию (bin/, obj/, DocumentLayout.json, *.FileListAbsolute.txt), которые должны быть исключены через .gitignore.

Определены соглашения об именовании (PascalCase, camelCase, I-префикс) и правила версионирования (SemVer, Conventional Commits).

Документ будет обновляться при изменении архитектуры проекта и добавлении новых модулей.


ПРИЛОЖЕНИЯ

Диаграмма модулей: docs/modules-diagram.drawio

Описание архитектуры: ARCHITECTURE.md

Основной README: README.md

Последнее обновление: 17.09.2026

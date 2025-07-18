using DataAccess.Repositories;
using Domain;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp;

public partial class MainWindow : Window
{
    private readonly UserRepository _userRepository;

    public List<User> Users { get; set; }

    public MainWindow(UserRepository userRepository)
    {
        ArgumentNullException.ThrowIfNull(userRepository);

        InitializeComponent();

        _userRepository = userRepository;
        Users = _userRepository.GetAll().ToList();

        AddColumns();
        AddUserRows();
    }

    public void AddColumns()
    {
        DateTime initialDate = new DateTime(DateTime.Now.Year + 1, 1, 1);
        DateTime lastDate = new DateTime(DateTime.Now.Year + 1, 12, 31);

        int column = 1;

        for (DateTime date = initialDate;
             date <= lastDate;
             date = date.AddDays(1), column++)
        {
            MainGrid.ColumnDefinitions.Add(
                new ColumnDefinition()
                {
                    Width = GridLength.Auto,
                }
            );

            TextBlock dateTextBlock = new()
            {
                Text = date.ToString("dd"),
                Width = 30
            };
            Grid.SetRow(dateTextBlock, 2);
            Grid.SetColumn(dateTextBlock, column);
            MainGrid.Children.Add(dateTextBlock);
        }

        column = 1;

        for (DateTime date = initialDate; date <= lastDate; date = date.AddMonths(1))
        {
            TextBlock monthTextBlock = new()
            {
                Text = date.ToString("MMMM", CultureInfo.GetCultureInfo("ru")),
                VerticalAlignment = VerticalAlignment.Center,
            };

            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);


            Grid.SetRow(monthTextBlock, 1);
            Grid.SetColumn(monthTextBlock, column);
            Grid.SetColumnSpan(monthTextBlock, daysInMonth);
            MainGrid.Children.Add(monthTextBlock);

            column += daysInMonth;
        }
    }

    /// <summary>
    /// Очищаем старые строки (кроме заголовков)
    /// </summary>
    public void RemoveUserRows()
    {
        for (int i = MainGrid.RowDefinitions.Count - 1; i > 1; i--)
        {
            MainGrid.RowDefinitions.RemoveAt(i);

            // Удаляем элементы в этих строках
            var childrenToRemove = MainGrid.Children
                .Cast<UIElement>()
                .Where(x => Grid.GetRow(x) == i)
                .ToList();

            foreach (var child in childrenToRemove)
            {
                MainGrid.Children.Remove(child);
            }
        }
    }

    /// <summary>
    /// Добавляем новые строки и заполняем их данными
    /// </summary>
    public void AddUserRows()
    {
        for (int i = 0; i < Users.Count; i++)
        {
            User user = Users[i];
            int row = i + 2; // +2 потому что 0, 1 строки - заголовки

            MainGrid.RowDefinitions.Add(
                new RowDefinition()
                {
                    Height = new GridLength(30)
                }
            );

            TextBlock userNameTextBlock = new()
            {
                Text = user.Name,
                Margin = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Center,
            };
            Grid.SetRow(userNameTextBlock, row);
            Grid.SetColumn(userNameTextBlock, 0);
            MainGrid.Children.Add(userNameTextBlock);


        }
    }
}
using Naninovel;
using UnityEngine;

[CommandAlias("CheckScore")]
public class CheckScoreCommand : Command
{
    private ScoreManager scoreManager;

    // Переменная для хранения предыдущего активного принтера
    private string previousPrinterId;

    public CheckScoreCommand()
    {
        scoreManager = Engine.GetService<ScoreManager>();  // Получаем сервис ScoreManager
    }

    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        int currentScore = scoreManager.GetScore();  // Получаем текущие очки

        // Получаем менеджер текстовых принтеров
        var printerManager = Engine.GetService<ITextPrinterManager>();

        // Сохраняем ID текущего активного принтера
        previousPrinterId = printerManager.DefaultPrinterId;

        // Устанавливаем новый активный принтер (например, с ID "bubble")
        printerManager.DefaultPrinterId = "bubble";

        // Создаем LocalizableText с правильным набором LocalizableTextPart
        var text = new LocalizableText(new LocalizableTextPart[]
        {
            LocalizableTextPart.FromPlainText("Current Score: "), // Текст до значения
            LocalizableTextPart.FromPlainText(currentScore.ToString()) // Текст с текущими очками
        });

        // Выводим текст через новый активный принтер
        await printerManager.PrintTextAsync(printerManager.DefaultPrinterId, text);

        // Проверка, если принтер с таким ID не найден
        if (printerManager.DefaultPrinterId == null)
        {
            Debug.LogWarning("Принтер с ID 'bubble' не найден.");
        }

        // Восстановление предыдущего активного принтера
        printerManager.DefaultPrinterId = previousPrinterId;
    }
}

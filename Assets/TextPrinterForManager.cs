using UnityEngine;
using Naninovel;


public class TextPrinterForManager : MonoBehaviour, ITextPrinterActor, IActor
{
    // Переменная для задержки между символами
    public float CharacterDelay = 0.1f;

    // Реализация свойств из ITextPrinterActor
    public string Text { get; set; } // Текст, который будет выводиться
    public string AuthorId { get; set; } // Идентификатор автора
    public string RichTextTags { get; set; } // Разметка для RichText
    public float RevealProgress { get; set; } // Прогресс раскрытия текста (по умолчанию 0 или 1)

    // Переменные для свойств актера
    public string Id { get; set; }
    public string Appearance { get; set; } // Тип изменен на string
    public bool Visible { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }
    public Color TintColor { get; set; }

    // Переопределяем метод для вывода текста с задержкой
    public async UniTask PrintTextAsync(string text, TextPrinterOptions options = null)
    {
        for (int i = 0; i < text.Length; i++)
        {
            // Выводим каждый символ с задержкой
            Engine.GetService<ITextPrinter>().Print(text.Substring(0, i + 1), options);
            await UniTask.Delay(System.TimeSpan.FromSeconds(CharacterDelay));
        }
    }

    // Реализуем метод из ITextPrinterActor (необходим для интерфейса)
    public async UniTask RevealTextAsync(float delay, AsyncToken token = default)
    {
        // Логика для раскрытия текста
        await UniTask.Delay(System.TimeSpan.FromSeconds(delay), cancellationToken: token);
    }

    // Реализация асинхронных методов для IActor
    public async UniTask InitializeAsync()
    {
        // Логика для инициализации
        await UniTask.CompletedTask;
    }

    public async UniTask ChangeAppearanceAsync(string appearance, float duration, EasingType easingType, Transition? transition, AsyncToken token = default)
    {
        // Логика для смены внешности
        Appearance = appearance; // Сохраняем appearance как строку
        await UniTask.CompletedTask;
    }

    public async UniTask ChangeVisibilityAsync(bool visible, float duration, EasingType easingType, AsyncToken token = default)
    {
        // Логика для смены видимости
        Visible = visible;
        await UniTask.CompletedTask;
    }

    public async UniTask ChangePositionAsync(Vector3 position, float duration, EasingType easingType, AsyncToken token = default)
    {
        // Логика для изменения позиции
        Position = position;
        await UniTask.CompletedTask;
    }

    public async UniTask ChangeRotationAsync(Quaternion rotation, float duration, EasingType easingType, AsyncToken token = default)
    {
        // Логика для изменения вращения
        Rotation = rotation;
        await UniTask.CompletedTask;
    }

    public async UniTask ChangeScaleAsync(Vector3 scale, float duration, EasingType easingType, AsyncToken token = default)
    {
        // Логика для изменения масштаба
        Scale = scale;
        await UniTask.CompletedTask;
    }

    public async UniTask ChangeTintColorAsync(Color tintColor, float duration, EasingType easingType, AsyncToken token = default)
    {
        // Логика для изменения цвета
        TintColor = tintColor;
        await UniTask.CompletedTask;
    }

    // Метод теперь возвращает UniTask, как требуется в интерфейсе IActor
    public async UniTask HoldResourcesAsync(string resource, object data)
    {
        // Логика для удержания ресурсов
        await UniTask.CompletedTask;
    }

    // Метод теперь возвращает UniTask, как требуется в интерфейсе IActor
    public async UniTask ReleaseResources(string resource, object data)
    {
        // Логика для освобождения ресурсов
        await UniTask.CompletedTask;
    }
}
